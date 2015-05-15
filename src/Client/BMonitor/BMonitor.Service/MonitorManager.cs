using System;
using System.Configuration;
using System.Diagnostics;
using System.Threading.Tasks;
using Blob.Contracts.Models;
using Blob.Proxies;
using BMonitor.Monitors;
using BMonitor.Service.Configuration;
using BMonitor.Service.Connection;
using BMonitor.Service.Quartz;
using log4net;
using Ninject;

namespace BMonitor.Service
{
    // http://adrianhesketh.com/2015/03/17/wcf-client-proxy-creation-performance-with-ninject/
    public class MonitorManager : IDisposable
    {
        public event EventHandler ConfigChanged; 

        private readonly IKernel _kernel;
        private readonly ILog _log;
        private IJobHandler _jobHandler;
        private ConnectionThread _connectionThread;

        private string Username;
        private string Password;



        private Guid _deviceId;

        private bool _enableCommandConnection;
        private bool _enablePerformanceMonitoring;
        private bool _enableStatusMonitoring;
        private bool _isRegistered;

        public MonitorManager(IKernel kernel)
        {
            // override the callback to validate the server certificate.  This is a hack for early dev ONLY
            System.Net.ServicePointManager.ServerCertificateValidationCallback = ((sender, certificate, chain, sslPolicyErrors) => true);
            _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            _kernel = kernel;
        }

        public void Initialize()
        {
            _log.Debug("Initializing MonitorManager");
            BMonitorConfigSection config = ConfigurationManager.GetSection("BMonitor") as BMonitorConfigSection;
            if (config == null)
                throw new ArgumentNullException("config");

            _deviceId = config.Main.DeviceId;
            if (_deviceId == Guid.Empty)
            {
                _log.Warn("Failed to load the DeviceId from the config file.  Registration required.");
                _isRegistered = false;
                Username = config.Main.Username;
                Password = config.Main.Password;
            }
            else
            {
                _log.Info(string.Format("Loaded device id of {0}.", _deviceId));
                _isRegistered = true;
                Username = _deviceId.ToString();
                Password = _deviceId.ToString();
            }
            _enableCommandConnection = config.Main.EnableCommandConnection;
            _enablePerformanceMonitoring = config.Main.EnablePerformanceMonitoring;
            _enableStatusMonitoring = config.Main.EnableStatusMonitoring;
        }

        void UpdateConfigSetting(string key, string val)
        {
            //var mySection = (BMonitorConfigurationSection)ConfigurationManager.GetSection("BMonitor");
            
            //mySection[key].Value = val;
            //mySection.SectionInformation.ForceSave = true; //
            //Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None); 
            //config.Save(ConfigurationSaveMode.Full);
            ////ConfigurationManager.RefreshSection("appSettings");
        }

        public void MonitorTick()
        {
            _log.Debug("Tick");
            
            // check if we are in a good, running state.
            if (!_isRegistered)
            {
                //if things are running, stop them
                _jobHandler.Stop();
                _connectionThread.Stop();

                RegisterDevice(Username, Password);
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

        public void RegisterDevice(string username, string password)
        {
            if (!_deviceId.Equals(default(Guid)))
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

                var u = new Ninject.Parameters.ConstructorArgument("username", Username);
                var p = new Ninject.Parameters.ConstructorArgument("password", Password);
                DeviceStatusClient statusClient = _kernel.Get<DeviceStatusClient>(u, p);
                statusClient.ClientErrorHandler += HandleException;


                RegisterDeviceResponseDto regInfo = Task.Run(() => statusClient.RegisterDeviceAsync(regMessage)).Result;
                _log.Debug(string.Format("RegistrationInformation response: {0}", regInfo));

                Debug.Assert(deviceGuid.Equals(Guid.Parse(regInfo.DeviceId)));
                _deviceId = deviceGuid;

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
                    _connectionThread = new ConnectionThread(_kernel, _deviceId);
                    _connectionThread.Start();
                }
            }

            if (_isRegistered && _enableStatusMonitoring || _enablePerformanceMonitoring)
            {
                _jobHandler = new BasicJobHandler(_kernel);
                _jobHandler.LoadConfig();
                _jobHandler.Start();
            }
        }
    }
}
