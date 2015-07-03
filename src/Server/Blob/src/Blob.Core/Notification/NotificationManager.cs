namespace Blob.Core.Notification
{
    using System.Collections.Generic;
    using log4net;

    public interface INotificationManager
    {
        void AddNotificationToBatch(INotification notification);
        IDictionary<string, IList<INotification>> GetNotificationsToSend();
    }

    public interface INotification
    {
        string GetRecipient();
        string GetMessage();
    }

    public class NotificationManager : INotificationManager
    {
        private static volatile object SyncLock = new object();
        private readonly ILog _log;

        private IDictionary<string, IList<INotification>> _notificationsToSend;

        public NotificationManager(ILog log)
        {
            _log = log;
            _log.Debug("Constructing NotificationManager");
            _notificationsToSend = new Dictionary<string, IList<INotification>>();
        }

        public void AddNotificationToBatch(INotification notification)
        {
            lock (SyncLock)
            {
                if (_notificationsToSend.ContainsKey(notification.GetRecipient()))
                {
                    _log.Debug("merging");
                    _notificationsToSend[notification.GetRecipient()].Add(notification);
                }
                else
                {
                    _log.Debug("adding");
                    _notificationsToSend.Add(notification.GetRecipient(), new List<INotification> { notification });
                }
            }
        }

        public IDictionary<string, IList<INotification>> GetNotificationsToSend()
        {
            Dictionary<string, IList<INotification>> current;
            lock (SyncLock)
            {
                current = new Dictionary<string, IList<INotification>>(_notificationsToSend);
                _notificationsToSend.Clear();
            }
            return current;

        }
    }
}