using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using Blob.Contracts.Models;
using Blob.Proxies;
using BMonitor.Common.Interfaces;
using BMonitor.Common.Models;
using BMonitor.Monitors;
using BMonitor.Service.Configuration;
using log4net;
using Ninject;

namespace BMonitor.Service.Quartz
{
    public class BasicJobHandler : IJobHandler
    {
        private readonly IKernel _kernel;
        private readonly ILog _log;
        private readonly ICollection<IMonitor> _monitors;

        private Guid _deviceId;
        private string _monitorPath;

        private bool _enableStatusMonitoring;
        private bool _enablePerformanceMonitoring;
        private bool _started;

        public BasicJobHandler(IKernel kernel)
        {
            _kernel = kernel;
            _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            _monitors = new List<IMonitor>();
        }


        public bool LoadConfig()
        {
            _log.Debug("LoadConfig");
            BMonitorConfigSection config = ConfigurationManager.GetSection("BMonitor") as BMonitorConfigSection;
            if (config == null)
                throw new ArgumentNullException("config");

            _deviceId = config.Main.DeviceId;

            _enablePerformanceMonitoring = config.Main.EnablePerformanceMonitoring;
            _enableStatusMonitoring = config.Main.EnableStatusMonitoring;

            _monitorPath = config.Main.MonitorPath;
            _log.Info(string.Format("Loading monitors from {0}", _monitorPath));
            
            _monitors.Add(new FreeDiskSpace(driveLetter: "C", driveDescription: "OS"));
            //,
            //unitOfMeasure: UoM.Percent,
            //warningLevel: 20,
            //criticalLevel: 10)
            //);
            _log.Info("Loaded FreeDiskSpace monitor");

            //_monitors.Add(new PerfMonMonitor("Memory", "Available Bytes"));
            //_log.Info("Loaded Memory monitor");

            //_monitors.Add(new PerfMonMonitor("Processor", "% Processor Time", "_Total"));
            //_log.Info("Loaded Processor monitor");
            return true;
        }

        public void Tick()
        {
            if (_started)
            {
                foreach (IMonitor monitor in _monitors)
                {
                    _log.Debug(string.Format("Executing {0}", monitor.GetType()));

                    // where am i going to get all the config info?
                    ResultData result = monitor.Execute(true);
                    AddStatusRecordDto statusData = new AddStatusRecordDto()
                                                    {
                                                        AlertLevel = (int) result.AlertLevel,
                                                        CurrentValue = result.Value,
                                                        DeviceId = _deviceId,
                                                        MonitorDescription = result.MonitorDescription,
                                                        MonitorName = result.MonitorName,
                                                        TimeGenerated = result.TimeGenerated,
                                                        TimeSent = DateTime.Now
                                                    };

                    AddPerformanceRecordDto spd = null;
                    if (_enablePerformanceMonitoring && result.Perf.Any())
                    {
                        spd = new AddPerformanceRecordDto
                              {
                                  DeviceId = _deviceId,
                                  MonitorDescription = result.MonitorDescription,
                                  MonitorName = result.MonitorName,
                                  TimeGenerated = result.TimeGenerated,
                                  TimeSent = DateTime.Now
                              };

                        foreach (PerformanceData perf in result.Perf)
                        {
                            spd.Data.Add(new PerformanceRecordValue
                                         {
                                             Critical = perf.Critical,
                                             Label = perf.Label,
                                             Max = perf.Max,
                                             Min = perf.Min,
                                             UnitOfMeasure = perf.UnitOfMeasure,
                                             Value = perf.Value,
                                             Warning = perf.Warning
                                         });
                        }

                        _log.Debug(statusData.CurrentValue + "|" + result.Perf.FirstOrDefault().ToString());
                    }
                    _log.Debug(statusData);

                    _log.Info("Creating new StatusClient");
                    var u = new Ninject.Parameters.ConstructorArgument("username", _deviceId.ToString());
                    var p = new Ninject.Parameters.ConstructorArgument("password", _deviceId.ToString());
                    DeviceStatusClient statusClient = _kernel.Get<DeviceStatusClient>(u, p);
                    statusClient.ClientErrorHandler += HandleException;

                    // send
                    if (_enableStatusMonitoring)
                    {
                        if (_enablePerformanceMonitoring && spd != null)
                        {
                            statusData.PerformanceRecordDto = spd;
                        }
                        _log.Debug("Sending status message.");
                        Task.Run(() => statusClient.AddStatusRecordAsync(statusData));
                    }
                    else
                    {
                        if (_enablePerformanceMonitoring && spd != null)
                        {
                            _log.Debug("Sending performance message.");
                            Task.Run(() => statusClient.AddPerformanceRecordAsync(spd));
                        }
                    }
                }
            }
        }

        private void HandleException(Exception ex)
        {
            _log.Error(string.Format("Error from client proxy."), ex);
            ;
        }


        public void Start()
        {
            _started = true;
        }

        public void Stop()
        {
            _started = false;
        }
    }
}