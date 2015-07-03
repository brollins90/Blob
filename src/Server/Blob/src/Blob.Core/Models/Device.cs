namespace Blob.Core.Models
{
    using System;
    using System.Collections.Generic;

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

        public virtual ICollection<StatusRecord> StatusRecords { get; set; } = new List<StatusRecord>();
        public virtual ICollection<PerformanceRecord> PerformanceRecords { get; set; } = new List<PerformanceRecord>();
    }
}