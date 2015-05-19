﻿using System;
using System.Reflection;
using BMonitor.Common.Interfaces;
using log4net;
using Ninject;
using Ninject.Parameters;
using Quartz;
using Quartz.Spi;

namespace BMonitor.Service.Monitor.Quartz
{
    public class NinjectJobFactory : IJobFactory
    {
        private readonly IKernel _kernel;
        private readonly ILog _log;

        public NinjectJobFactory(IKernel kernel)
        {
            _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            _kernel = kernel;
            _log.Debug("Constructing NinjectJobFactory");
        }

        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            _log.Debug("NewJob");
            try
            {
                string monitorTypeString = bundle.JobDetail.JobDataMap.GetString("MonitorType");
                _log.Debug(string.Format("MonitorType {0}", monitorTypeString));

                Type monitorType = Type.GetType(monitorTypeString, throwOnError: true);
                var monitorInstance = Activator.CreateInstance(monitorType);
                PropertyInfo[] properties = monitorType.GetProperties();

                foreach (var property in properties)
                {
                    if (bundle.JobDetail.JobDataMap.ContainsKey(property.Name))
                    {
                        property.SetValue(monitorInstance, bundle.JobDetail.JobDataMap[property.Name], null);
                    }
                }

                QuartzJob wrapperJob = new QuartzJob(monitorInstance as IMonitor);

                _log.Debug("Returning");
                return wrapperJob;
            }
            catch (Exception ex)
            {
                _log.Info(string.Format("Exception raised in JobFactory"), ex);
            }
            return null;
        }

        public void ReturnJob(IJob job)
        {
            _kernel.Release(job);
        }
    }
}