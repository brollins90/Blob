using System;
using System.Collections.Generic;

namespace Blob.Core.Models
{
    public class StatusRecord
    {
        public long Id { get; set; }
        public string MonitorId { get; set; }
        public string MonitorName { get; set; }
        public string MonitorDescription { get; set; }
        public int AlertLevel { get; set; }
        public DateTime TimeGeneratedUtc { get; set; }
        public DateTime TimeSentUtc { get; set; }
        public string CurrentValue { get; set; }

        public Guid DeviceId { get; set; }
        public virtual Device Device { get; set; }

        public virtual ICollection<PerformanceRecord> PerformanceRecords { get; set; }
    }
}
