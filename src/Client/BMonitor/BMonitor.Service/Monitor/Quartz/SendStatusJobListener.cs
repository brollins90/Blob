using System;
using BMonitor.Common.Models;
using BMonitor.Service.Helpers;
using log4net;
using Quartz;

namespace BMonitor.Service.Monitor.Quartz
{
    public class SendStatusJobListener : IJobListener
    {
        private readonly ILog _log;
        private readonly BMonitorStatusReporter _statusReporter;
        private Guid _deviceId;
        private bool _enableStatus;
        private bool _enablePerf;

        public SendStatusJobListener(BMonitorStatusReporter statusReporter, Guid deviceId, bool enableStatus, bool enablePerf)
        {
            _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            _statusReporter = statusReporter;
            _deviceId = deviceId;
            _enableStatus = enableStatus;
            _enablePerf = enablePerf;
        }

        public void JobExecutionVetoed(IJobExecutionContext context) { }

        public void JobToBeExecuted(IJobExecutionContext context) { }

        public void JobWasExecuted(IJobExecutionContext context, JobExecutionException jobException)
        {
            _log.Debug("JobWasExecuted");
            if (context.Result != null)
            {
                ResultData result = context.Result as ResultData;
                _statusReporter.SendResults(result, _deviceId, _enableStatus, _enablePerf);

            }
        }

        public string Name
        {
            get { return "SendStatusJobListener"; }
        }
    }
}
