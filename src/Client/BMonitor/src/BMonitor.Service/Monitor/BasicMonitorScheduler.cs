using System;
using System.Collections.Generic;
using System.Configuration;
using BMonitor.Common.Interfaces;
using BMonitor.Common.Models;
using BMonitor.Configuration;
using BMonitor.Monitors;
using BMonitor.Service.Helpers;
using log4net;
using Ninject;

namespace BMonitor.Service.Monitor
{
    public class BasicMonitorScheduler : IMonitorScheduler
    {
        private readonly IKernel _kernel;
        private readonly ILog _log;
        private readonly ICollection<IMonitor> _monitors;

        private Guid _deviceId;
        //private string _monitorPath;

        private bool _enableStatusMonitoring;
        private bool _enablePerformanceMonitoring;
        private bool _started;

        private BMonitorStatusReporter _statusHelper;
        public BasicMonitorScheduler(IKernel kernel, BMonitorStatusReporter statusHelper)
        {
            _kernel = kernel;
            _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            _monitors = new List<IMonitor>();
            _statusHelper = statusHelper;
        }


        public bool LoadConfig()
        {
            _log.Debug("LoadConfig");
            BMonitorConfigurationSection config = ConfigurationManager.GetSection("BMonitor") as BMonitorConfigurationSection;
            if (config == null)
                throw new ConfigurationErrorsException();

            _deviceId = config.Service.DeviceId;

            _enablePerformanceMonitoring = config.Service.EnablePerformanceMonitoring;
            _enableStatusMonitoring = config.Service.EnableStatusMonitoring;

            //_monitorPath = config.Service.MonitorPath;
            //_log.Info(string.Format("Loading monitors from {0}", _monitorPath));

            _monitors.Add(new FreeDiskSpace { DriveLetter = "C", DriveDescription = "OS" });
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
                    _statusHelper.SendResults(result, _deviceId,_enableStatusMonitoring, _enablePerformanceMonitoring);
                }
            }
        }


        public void Start()
        {
            _started = true;
        }

        public void Stop()
        {
            _started = false;
        }

        public void RunJob(string jobName)
        {
        }


        public void AddJob(JobSettings settings)
        {
            throw new NotImplementedException();
        }

        public void RemoveJob(string jobName)
        {
            throw new NotImplementedException();
        }
    }
}