using System;
using System.Collections.Generic;

namespace Blob.Core.Models
{
    public class Device
    {
        public Guid Id { get; set; }
        public string DeviceName { get; set; }
        public DateTime LastActivityDateUtc { get; set; }
        public int AlertLevel { get; set; }
        public DateTime CreateDateUtc { get; set; }
        public bool Enabled { get; set; }

        public Guid DeviceTypeId { get; set; }
        public virtual DeviceType DeviceType { get; set; }

        public Guid CustomerId { get; set; }
        public virtual Customer Customer { get; set; }

        public virtual ICollection<StatusRecord> StatusRecords { get; set; }
        public virtual ICollection<PerformanceRecord> PerformanceRecords { get; set; }
    }
}
