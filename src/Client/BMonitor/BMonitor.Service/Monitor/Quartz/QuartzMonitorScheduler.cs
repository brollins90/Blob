using System;
using System.Configuration;
using System.IO;
using System.Reflection;
using BMonitor.Service.Configuration;
using BMonitor.Service.Helpers;
using log4net;
using Ninject;
using Quartz;
using Quartz.Impl;
using Quartz.Impl.Matchers;

namespace BMonitor.Service.Monitor.Quartz
{
    class QuartzMonitorScheduler : IMonitorScheduler
    {
        private readonly ILog _log;
        private readonly IKernel _kernel;
        private IScheduler _scheduler;
        private BMonitorStatusHelper _statusHelper;

        private Guid _deviceId;
        //private string _monitorPath;
        private bool _enablePerformanceMonitoring;

        public QuartzMonitorScheduler(IKernel kernel, BMonitorStatusHelper statusHelper)
        {
            _kernel = kernel;
            _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            _statusHelper = statusHelper;
        }

        //public void LoadAllModuleDirectoryAssemblies(string monitorPath)
        //{
        //    //string binPath = System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "bin"); // note: don't use CurrentEntryAssembly or anything like that.
        //    //binPath = @"C:\_\src\Blake\Blob\src\Client\BMonitor\BMonitor.Monitors.Custom\bin\Debug\";

        //    foreach (string dll in Directory.GetFiles(monitorPath, "*.dll", SearchOption.AllDirectories))
        //    {
        //        _log.Debug("loading " + dll);
        //        try
        //        {
        //            Assembly loadedAssembly = Assembly.LoadFile(dll);
        //        }
        //        catch (FileLoadException loadEx)
        //        { } // The Assembly has already been loaded.
        //        catch (BadImageFormatException imgEx)
        //        { } // If a BadImageFormatException exception is thrown, the file is not an assembly.

        //    } // foreach dll
        //}

        public bool LoadConfig()
        {
            _log.Debug("LoadConfig");
            BMonitorSection config = ConfigurationManager.GetSection("BMonitor") as BMonitorSection;
            if (config == null)
                throw new ConfigurationErrorsException();

            try
            {
                _deviceId = config.Service.DeviceId;
                _enablePerformanceMonitoring = config.Service.EnablePerformanceMonitoring;

                //_monitorPath = config.Service.MonitorPath;
                //LoadAllModuleDirectoryAssemblies(_monitorPath);

                _scheduler = StdSchedulerFactory.GetDefaultScheduler();
                _scheduler.JobFactory = new NinjectJobFactory(_kernel, _enablePerformanceMonitoring);//, _monitorPath);
                _scheduler.ListenerManager.AddJobListener(new SendStatusJobListener(_statusHelper), GroupMatcher<JobKey>.AnyGroup());

                return true;
            }
            catch (Exception)
            {
                throw;
                //return false;
            }
        }

        public void Start()
        {
            // start the schedule 
            _scheduler.Start();
        }

        public void Stop()
        {
            // shut down the scheduler
            _scheduler.Shutdown(true);
        }

        public void Tick()
        {
        }
    }
}
