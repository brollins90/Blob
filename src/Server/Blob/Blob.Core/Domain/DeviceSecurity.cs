using System;

namespace Blob.Core.Domain
{
    public enum DeviceSecurityKeyFormat
    {
        CLEAR = 0
    }

    public class DeviceSecurity
    {
        public Guid DeviceId { get; set; }
        public virtual Device Device { get; set; }

        public string Key1 { get; set; }
        public int Key1Format { get; set; }
        public string Key1Salt { get; set; }
        public string Key2 { get; set; }
        public int Key2Format { get; set; }
        public string Key2Salt { get; set; }
        public bool IsApproved { get; set; }
        public bool IsLockedOut { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime LastLoginDate { get; set; }
        public DateTime LastKey1ChangedDate { get; set; }
        public DateTime LastKey2ChangedDate { get; set; }
        public int FailedLoginAttemptCount { get; set; }
        public DateTime FailedLoginAttemptWindowStart { get; set; }
        public string Comment { get; set; } 
    }
}
