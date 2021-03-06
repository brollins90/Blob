﻿using System;

namespace Blob.Core.Models
{
    public class UserProfile
    {
        public Guid UserId { get; set; }
        public virtual User User { get; set; }

        public bool SendEmailNotifications { get; set; }
        public Guid EmailNotificationScheduleId { get; set; }
        public NotificationSchedule EmailNotificationSchedule { get; set; }
    }
}
