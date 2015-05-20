using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blob.Core.Domain
{
    public class UserProfile
    {
        public Guid UserId { get; set; }
        public virtual User User { get; set; }

        public bool SendEmailNotifications { get; set; }
        public string EmailNotificationSchedule { get; set; }
    }
}
