using System;
using System.Configuration;
using System.Diagnostics;
using System.Threading.Tasks;
using Blob.Contracts.Models;
using Blob.Proxies;
using BMonitor.Monitors;
using BMonitor.Service.Configuration;
using BMonitor.Service.Connection;
using BMonitor.Service.Helpers;
using BMonitor.Service.Monitor;
using BMonitor.Service.Monitor.Quartz;
using log4net;
using Ninject;

namespace BMonitor.Service
{
    // http://adrianhesketh.com/2015/03/17/wcf-client-proxy-creation-performance-with-ninject/
    public class MonitorManager : IDisposable
    {
        private readonly IKernel _kernel;
        private readonly ILog _log;
        private IMonitorScheduler _jobHandler;
        private IConnectionThread _connectionThread;
        private Registrator _registrator;

        private DeviceInfo _deviceInfo;

        private bool _enableCommandConnection;
        private bool _enablePerformanceMonitoring;
        private bool _enableStatusMonitoring;
        private bool _isRegistered;

        public MonitorManager(IKernel kernel, ILog log, Registrator registrator)
        {
            // override the callback to validate the server certificate.  This is a hack for early dev ONLY
            System.Net.ServicePointManager.ServerCertificateValidationCallback = ((sender, certificate, chain, sslPolicyErrors) => true);
            _kernel = kernel;
            _log = log;
            _registrator = registrator;
            _deviceInfo = new DeviceInfo();
        }

        public void LoadConfig()
        {
            _log.Debug("LoadingConfig"); 
            System.Configuration.Configuration configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            BMonitorSection config = configFile.GetSection("BMonitor") as BMonitorSection;
            if (config == null) { throw new ConfigurationErrorsException(); }
            ConfigurationManager.RefreshSection("BMonitor");

            _deviceInfo.DeviceId = config.Service.DeviceId;
            if (_deviceInfo.DeviceId == Guid.Empty)
            {
                _log.Warn("Failed to load the DeviceId from the config file.  Registration required.");
                _isRegistered = false;
                _deviceInfo.Username = config.Service.Username;
                _deviceInfo.Password = config.Service.Password;
            }
            else
            {
                _log.Info(string.Format("Loaded device id of {0}.", _deviceInfo.DeviceId));
                _isRegistered = true;
                _deviceInfo.Username = _deviceInfo.DeviceId.ToString();
                _deviceInfo.Password = _deviceInfo.DeviceId.ToString();
            }
            _enableCommandConnection = config.Service.EnableCommandConnection;
            _enablePerformanceMonitoring = config.Service.EnablePerformanceMonitoring;
            _enableStatusMonitoring = config.Service.EnableStatusMonitoring;
        }

        public void MonitorTick()
        {
            _log.Debug("Tick");
            
            // check if we are in a good, running state.
            if (!_isRegistered)
            {
                //if things are running, stop them
                if (_jobHandler != null) _jobHandler.Stop();
                if (_connectionThread != null) _connectionThread.Stop();
                if (!_registrator.IsRegistered())
                {
                    var u = new Ninject.Parameters.ConstructorArgument("username", _deviceInfo.Username);
                    var p = new Ninject.Parameters.ConstructorArgument("password", _deviceInfo.Password);
                    Guid devId = _registrator.RegisterDevice(_kernel.Get<DeviceStatusClient>(u, p));
                    _log.Debug(devId);
                    SaveRegistration(new RegisterDeviceResponseDto {DeviceId = devId});

                    Start();
                }
            }
            else
            {
                if (_enableCommandConnection) // && connection is open
                {
                    //commandClient.Ping(_deviceId);
                }

                if (_enableStatusMonitoring || _enablePerformanceMonitoring)
                {
                    _jobHandler.Tick();
                }
            }
        }

        private void SaveRegistration(RegisterDeviceResponseDto response)
        {
            System.Configuration.Configuration configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            BMonitorSection config = configFile.GetSection("BMonitor") as BMonitorSection;
            if (config == null) { throw new ConfigurationErrorsException(); }
            ConfigurationManager.RefreshSection("BMonitor");

            config.Service.DeviceId = response.DeviceId;
            //config.Service.Username = "";
            //config.Service.Password = "";


            configFile.Save(ConfigurationSaveMode.Full, true);

            _isRegistered = true;
            LoadConfig();
            //Start();
        }
        
        private void HandleException(Exception ex)
        {
            _log.Error(string.Format("Error from client proxy."), ex);
        }

        public void Dispose()
        {
            PerfmonCounterManager manager = PerfmonCounterManager.Instance;
            manager.Dispose();
        }

        public void Start()
        {
            if (_isRegistered && _enableCommandConnection)
            {
                _log.Info("Creating command connection.");
                //todo: spin up a new thread
                if (_connectionThread == null)
                {
                    _connectionThread = new ConnectionThread(_kernel, _deviceInfo.DeviceId);
                    _connectionThread.Start();
                }
            }

            if (_isRegistered && _enableStatusMonitoring || _enablePerformanceMonitoring)
            {
                _jobHandler = new QuartzMonitorScheduler(_kernel, new BMonitorStatusHelper(_kernel, _deviceInfo.DeviceId, _enablePerformanceMonitoring, _enableStatusMonitoring));// new BasicJobHandler(_kernel);
                _jobHandler.LoadConfig();
                _jobHandler.Start();
            }
        }
    }
}
