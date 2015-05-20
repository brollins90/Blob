using Blob.Managers.Notification;

namespace Behind.Service
{
    public class EmailNotification : INotification
    {
        public string GetRecipient()
        {
            return Recipient;
        }
        public string Recipient { get; set; }

        public string GetMessage()
        {
            return Message;
        }
        public string Message { get; set; }
    }
}
