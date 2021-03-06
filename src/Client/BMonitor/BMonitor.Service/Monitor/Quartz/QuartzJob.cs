﻿using System;
using BMonitor.Common.Interfaces;
using BMonitor.Common.Models;
using log4net;
using Quartz;

namespace BMonitor.Service.Monitor.Quartz
{
    // http://www.codeproject.com/Tips/399786/Quartz-Net-Custom-Base-Job

    //[DisallowConcurrentExecution]
    public class QuartzJob : IJob
    {
        private readonly ILog _log;
        private readonly IMonitor _internalMonitor;

        public QuartzJob(IMonitor monitor)
        {
            _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            _internalMonitor = monitor;
        }
        
        public void Execute(IJobExecutionContext context)
        {
            _log.Debug(string.Format("Execute {0}", _internalMonitor.GetType()));
            try
            {
                ResultData result = InternalExecute(context);
                _log.Debug(string.Format("Results from {0}: {1}", _internalMonitor.GetType().Name, result.Value));

                context.Result = result;

                // if not OK, reschedule todo:


            }
            catch (Exception ex)
            {
                JobExecutionException jex = new JobExecutionException(ex);
                throw jex;
            }
        }

        protected ResultData InternalExecute(IJobExecutionContext context)
        {
            bool collectPerfdata = context.MergedJobDataMap.GetBooleanValue("collectPerfdata");
            return _internalMonitor.Execute(collectPerfdata);
        }
    }
}
