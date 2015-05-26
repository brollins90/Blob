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

        private DeviceInfo _deviceInfo;

        private bool _enableCommandConnection;
        private bool _enablePerformanceMonitoring;
        private bool _enableStatusMonitoring;
        private bool _isRegistered;

        public MonitorManager(IKernel kernel, ILog log)
        {
            // override the callback to validate the server certificate.  This is a hack for early dev ONLY
            System.Net.ServicePointManager.ServerCertificateValidationCallback = ((sender, certificate, chain, sslPolicyErrors) => true);
            _kernel = kernel;
            _log = log;
            _deviceInfo = new DeviceInfo();
        }

        public void Initialize()
        {
            _log.Debug("Initializing MonitorManager");
            BMonitorSection config = ConfigurationManager.GetSection("BMonitor") as BMonitorSection;
            if (config == null)
                throw new ConfigurationErrorsException();

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

                RegisterDevice(_deviceInfo);
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

        public void RegisterDevice(DeviceInfo info)
        {
            if (!info.DeviceId.Equals(default(Guid)))
            {
                _log.Error("Registration was requested even though this device is already registered.");
                return;
                // throw?
            }

            try
            {
                Guid deviceGuid = Guid.NewGuid();
                _log.Info(string.Format("Registering this agent with the BlobService with id:{0}.", deviceGuid));
                RegisterDeviceDto regMessage = new RegisterDeviceDto
                                                 {
                                                     DeviceId = deviceGuid.ToString(),
                                                     DeviceKey1 = "key1",
                                                     DeviceKey1Format = 0,
                                                     DeviceKey2 = "key2",
                                                     DeviceKey2Format = 0,
                                                     DeviceName = "Test 1",
                                                     DeviceType = "WindowsDesktop",
                                                     TimeSent = DateTime.Now
                                                 };
                _log.Debug(string.Format("RegistrationMessage request: {0}", regMessage));

                var u = new Ninject.Parameters.ConstructorArgument("username", info.Username);
                var p = new Ninject.Parameters.ConstructorArgument("password", info.Password);
                DeviceStatusClient statusClient = _kernel.Get<DeviceStatusClient>(u, p);
                statusClient.ClientErrorHandler += HandleException;


                RegisterDeviceResponseDto regInfo = Task.Run(() => statusClient.RegisterDeviceAsync(regMessage)).Result;
                if (regInfo.Succeeded != true)
                {
                    _log.Error("Registration failed");
                }
                _log.Debug(string.Format("RegistrationInformation response: {0}", regInfo));

                Debug.Assert(deviceGuid.Equals(Guid.Parse(regInfo.DeviceId)));
                info.DeviceId = deviceGuid;
                _isRegistered = true;

                // move this to an event...
                // todo:
                Start();

            }
            catch (Exception e)
            {
                _log.Error("Error while registering this agent with the server.", e);
            }
        }


        private void HandleException(Exception ex)
        {
            _log.Error(string.Format("Error from client proxy."), ex);
            ;
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
