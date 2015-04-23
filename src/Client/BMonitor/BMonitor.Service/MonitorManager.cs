using Blob.Contracts.Models;
using Blob.Proxies;
using BMonitor.Common.Interfaces;
using BMonitor.Monitors.Default;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Security;
using System.Threading.Tasks;

namespace BMonitor.Service
{
    public class MonitorManager
    {
        private string User_Username = "customerUser1";
        private string User_password = "password";
        private string Username;
        private string Password;


        private readonly ILog _log;
        private readonly ICollection<IMonitor> _monitors;

        private Guid _deviceId;
        private string _monitorPath;

        private bool _enableCommandConnection;
        private bool _enablePerformanceMonitoring;
        private bool _enableStatusMonitoring;
        private bool _isRegistered;

        private CommandClient commandClient;

        public MonitorManager()
        {
            _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            _monitors = new List<IMonitor>();
            Username = User_Username;
            Password = User_password;
        }

        public void Initialize(NameValueCollection config)
        {
            _log.Debug("Initializing MonitorManager");
            if (config == null)
                throw new ArgumentNullException("config");

            if (!Guid.TryParse(GetConfigValue(config["deviceId"], ""), out _deviceId))
            {
                _log.Warn("Failed to load the DeviceId from the config file.  Registration required.");
                _isRegistered = false;
                Username = User_Username;
                Password = User_password;

                //_deviceId = Guid.Parse("1C6F0042-750E-4F5A-B1FA-41DD4CA9368A");
            }
            else
            {
                _isRegistered = true;
                Username = _deviceId.ToString();
                Password = _deviceId.ToString();
            }


            _monitorPath = GetConfigValue(config["monitorPath"], @"/Monitors/");
            _enableCommandConnection = Convert.ToBoolean(GetConfigValue(config["enableCommandConnection"], "false"));
            _enablePerformanceMonitoring = Convert.ToBoolean(GetConfigValue(config["enablePerformanceMonitoring"], "false"));
            _enableStatusMonitoring = Convert.ToBoolean(GetConfigValue(config["enableStatusMonitoring"], "false"));

            if (_enableCommandConnection)
            {
                OpenCommandConnection();
            }

            if (_enableStatusMonitoring || _enablePerformanceMonitoring)
            {
                LoadMonitors();
            }
        }

        public void OpenCommandConnection()
        {
            if (_isRegistered && _enableCommandConnection)
            {
                _log.Info("Creating command connection.");
                //todo: spin up a new thread

                InstanceContext callbackInstance = new InstanceContext(new CommandServiceCallbackHandler());

                commandClient = new CommandClient(callbackInstance, "CommandService");
                commandClient.ClientCredentials.UserName.UserName = Username;
                commandClient.ClientCredentials.UserName.Password = Password;
                commandClient.ClientCredentials.ServiceCertificate.Authentication.CertificateValidationMode = X509CertificateValidationMode.None;

                commandClient.Connect(_deviceId);
            }

        }

        public bool LoadMonitors()
        {
            _log.Info(string.Format("Loading monitors from {0}", _monitorPath));

            if (_deviceId.Equals(default(Guid)))
            {
                _log.Warn("LoadMonitors was requested before registration was completed.");
                return false;
            }

            _monitors.Add(new FreeDiskSpace(
                driveLetter: "C",
                driveDescription: "OS",
                unitOfMeasure: UnitOfMeasure.PERCENT,
                warningLevel: 20,
                criticalLevel: 10));
            _log.Info("Loaded FreeDiskSpace monitor");
            return true;
        }

        public void MonitorTick()
        {
            _log.Debug("Tick");
            
            if (_enableCommandConnection) // && connection is open
            {
                commandClient.Ping(_deviceId);
            }

            foreach (IMonitor monitor in _monitors)
            {
                _log.Debug(string.Format("Executing {0}", monitor.GetType()));
                
                // where am i going to get all the config info?
                MonitorResult result = monitor.Execute(collectPerfData: true);
                StatusData statusData = new StatusData()
                             {
                                 AlertLevel = (int) result.AlertLevel,
                                 CurrentValue = result.Value,
                                 DeviceId = _deviceId,
                                 MonitorDescription = result.MonitorDescription,
                                 MonitorName = result.MonitorName,
                                 TimeGenerated = result.TimeGenerated,
                                 TimeSent = DateTime.Now
                             };
                StatusPerformanceData spd = new StatusPerformanceData
                {
                    DeviceId = _deviceId,
                    MonitorDescription = result.MonitorDescription,
                    MonitorName = result.MonitorName,
                    TimeGenerated = result.TimeGenerated,
                    TimeSent = DateTime.Now
                };

                foreach (MonitorPerf perf in result.Perf)
                {
                    spd.AddPerformanceDataValue(new PerformanceDataValue
                    {
                        Critical = perf.Critical,
                        Label = perf.Label,
                        Max = perf.Max,
                        Min = perf.Min,
                        UnitOfMeasure = perf.UnitOfMeasure.ToString(),
                        Value = perf.Value,
                        Warning = perf.Warning
                    });
                }

                _log.Debug(statusData.CurrentValue + "|" + result.Perf.First().ToString());

                _log.Info("Creating new StatusClient");
                StatusClient statusClient = new StatusClient("StatusService");
                statusClient.ClientCredentials.UserName.UserName = Username;
                statusClient.ClientCredentials.UserName.Password = Password;
                statusClient.ClientCredentials.ServiceCertificate.Authentication.CertificateValidationMode = X509CertificateValidationMode.None;

                _log.Debug("Sending status and then performance message.");
                // send
                Task.Run(() => statusClient.SendStatusToServer(statusData));
                //Task.Run(() => statusClient.SendStatusPerformanceToServer(spd));
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
                RegistrationMessage regMessage = new RegistrationMessage
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

                RegistrationClient registrationClient = new RegistrationClient("RegistrationService");
                registrationClient.ClientCredentials.UserName.UserName = username;
                registrationClient.ClientCredentials.UserName.Password = password;
                registrationClient.ClientCredentials.ServiceCertificate.Authentication.CertificateValidationMode = X509CertificateValidationMode.None;

                RegistrationInformation regInfo = Task.Run(() => registrationClient.Register(regMessage)).Result;
                _log.Debug(string.Format("RegistrationInformation response: {0}", regInfo));

                Debug.Assert(deviceGuid.Equals(Guid.Parse(regInfo.DeviceId)));
                _deviceId = deviceGuid;

                LoadMonitors();
            }
            catch (Exception e)
            {
                _log.Error("Error while registering this agent with the server.", e);
            }
        }

        /// <summary>
        /// Reads a config value from the config file.
        /// </summary>
        /// <param name="configValue">Value name in the config file.</param>
        /// <param name="defaultValue">Default value to return if the specified value does not exist.</param>
        /// <returns>the value of the config element or the default specified if the element is null.</returns>
        private static string GetConfigValue(string configValue, string defaultValue)
        {
            return (string.IsNullOrEmpty(configValue)) ? defaultValue : configValue;
        }
    }
}
