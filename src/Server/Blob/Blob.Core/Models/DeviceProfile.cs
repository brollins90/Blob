using System;

namespace Blob.Core.Models
{
    public class DeviceProfile
    {
        public Guid DeviceId { get; set; }
        public virtual Device Device { get; set; }

        public DateTime ProfileUpdateTimeUtc { get; set; }
        public string OperatingSystem { get; set; }
    }
}
