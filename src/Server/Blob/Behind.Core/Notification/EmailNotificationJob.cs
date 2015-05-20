using System;
using System.Data.Entity;
using Common.Logging;
using Quartz;

namespace Behind.Core.Notification
{
    public class EmailNotificationJob : IJob
    {
        private ILog _log;
        private DbContext _dbContext;
        public DateTime LastRunTime { private get; set; }

        public EmailNotificationJob(DbContext context, ILog log)
        {
            _log = log;
            _dbContext = context;
        }

        public void Execute(IJobExecutionContext context)
        {
            JobKey key = context.JobDetail.Key;

            JobDataMap dataMap = context.MergedJobDataMap;

            _log.Debug(string.Format("Instance {0} of EmailNotificationJob. now is {1}, LastRunTime {2}.", key, DateTime.Now, LastRunTime));
        }
    }
}
