using BMonitor.Common.Models;
using BMonitor.Service.Helpers;
using log4net;
using Quartz;

namespace BMonitor.Service.Monitor.Quartz
{
    public class SendStatusJobListener : IJobListener
    {
        private readonly ILog _log;
        private BMonitorStatusHelper _statusHelper;

        public SendStatusJobListener(BMonitorStatusHelper statusHelper)
        {
            _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            _statusHelper = statusHelper;
        }

        public void JobExecutionVetoed(IJobExecutionContext context) { }

        public void JobToBeExecuted(IJobExecutionContext context) { }

        public void JobWasExecuted(IJobExecutionContext context, JobExecutionException jobException)
        {
            _log.Debug("JobWasExecuted");
            if (context.Result != null)
            {
                ResultData result = context.Result as ResultData;
                _statusHelper.SendResults(result);

            }
        }

        public string Name
        {
            get { return "SendStatusJobListener"; }
        }
    }
}
