using System;
using System.Data.Entity;
using System.Linq;
using Blob.Data;
using Blob.Managers.Notification;
using log4net;
using Quartz;

namespace Behind.Service.Notification
{
    [PersistJobDataAfterExecution]
    [DisallowConcurrentExecution]
    public class BuildNotificationsJob : IJob
    {
        private readonly ILog _log;
        private readonly INotificationManager _notificationManager;

        public BuildNotificationsJob(ILog log, INotificationManager notificationManager)
        {
            _log = log;
            _log.Debug("BuildNotificationsJob constructed");
            _notificationManager = notificationManager;
        }

        public void Execute(IJobExecutionContext context)
        {
            string text;
            try
            {
                text = System.IO.File.ReadAllText(@"C:\_\emailjobtime.txt");
            }
            catch
            {
                text = DateTime.UtcNow.ToString("o");
            }

            DateTime lastDate;
            DateTime.TryParse(text, out lastDate);
            if (lastDate == null) lastDate = new DateTime();

            _log.Debug("last: " + lastDate);

            using (BlobDbContext dbContext = new BlobDbContext())
            {
                //var statusesSinceLast = dbContext.DeviceStatuses.Include("Device")//.Include("Customer").Include("Users")

                var statusesSinceLast = dbContext.DeviceStatuses
                    .Include(x => x.Device.Customer.Users)
                    .Where(x => x.TimeGenerated > lastDate);
                _log.Debug("count: " + statusesSinceLast.Count());

                foreach (var status in statusesSinceLast)
                {
                    string statusMessage = string.Format("{0}", status.CurrentValue);

                    var users = status.Device.Customer.Users;
                    foreach (var user in users)
                    {
                        if (!string.IsNullOrEmpty(user.Email) && user.EmailConfirmed && user.Enabled)
                        {
                            _notificationManager.AddNotificationToBatch(new EmailNotification { Recipient = user.Email, Message = statusMessage });
                        }
                    }

                    _log.Debug(status);
                }
            }

            System.IO.File.WriteAllLines(@"C:\_\emailjobtime.txt", new string[] { DateTime.UtcNow.ToString("o") });
        }
    }
}
