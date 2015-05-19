﻿using System;
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

        public QuartzMonitorScheduler(IKernel kernel, BMonitorStatusHelper statusHelper)
        {
            _kernel = kernel;
            _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            _statusHelper = statusHelper;
        }

        public bool LoadConfig()
        {
            _log.Debug("LoadConfig");
            try
            {
                _scheduler = StdSchedulerFactory.GetDefaultScheduler();
                _scheduler.JobFactory = new NinjectJobFactory(_kernel);
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