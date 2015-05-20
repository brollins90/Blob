using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Mail;
using System.Text;
using Blob.Data;
using Blob.Managers.Notification;
using log4net;
using Quartz;

namespace Behind.Service.Notification
{
    [PersistJobDataAfterExecution]
    [DisallowConcurrentExecution]
    public class EmailNotificationJob : IJob
    {
        private readonly ILog _log;
        private readonly INotificationManager _notificationManager;

        public EmailNotificationJob(ILog log, INotificationManager notificationManager)
        {
            _log = log;
            _log.Debug("EmailNotificationJob constructed");
            _notificationManager = notificationManager;
        }

        public void Execute(IJobExecutionContext context)
        {
            _log.Debug("Execute");
            var notifications = _notificationManager.GetNotificationsToSend();

            _log.Debug("Got " + notifications.Count + " notifications.");

            foreach (var notification in notifications)
            {
                var emailBody = new StringBuilder();
                foreach (var item in notification.Value)
                {
                    emailBody.AppendLine(item.GetMessage());
                }
                _log.Debug("Send");
                new SmtpClient().Send("notifications@blobservice.rritc.com", notification.Key, "Blob notification", emailBody.ToString());
            }
        }
    }
}
