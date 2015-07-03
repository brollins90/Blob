namespace Blob.Core.Models
{
    using System;

    public class PerformanceRecord
    {
        public long Id { get; set; }
        public string MonitorName { get; set; }
        public string MonitorDescription { get; set; }
        public DateTime TimeGeneratedUtc { get; set; }
        public string Label { get; set; }
        public string UnitOfMeasure { get; set; }
        public decimal Value { get; set; }
        public decimal? Warning { get; set; }
        public decimal? Critical { get; set; }
        public decimal? Min { get; set; }
        public decimal? Max { get; set; }

        public Guid DeviceId { get; set; }
        public virtual Device Device { get; set; }

        public long? StatusId { get; set; }
        public virtual StatusRecord Status { get; set; }
    }
}