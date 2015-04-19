using System;

namespace Blob.Core.Domain
{
    public class StatusPerf
    {
        public virtual long Id { get; set; }
        public virtual string MonitorName { get; set; }
        public virtual string MonitorDescription { get; set; }
        public virtual DateTime TimeGenerated { get; set; }
        public virtual string Label { get; set; }
        public virtual string UnitOfMeasure { get; set; }
        public virtual decimal Value { get; set; }
        public virtual decimal? Warning { get; set; }
        public virtual decimal? Critical { get; set; }
        public virtual decimal? Min { get; set; }
        public virtual decimal? Max { get; set; }

        public virtual Guid DeviceId { get; set; }
        public virtual Device Device { get; set; }
    }
}
