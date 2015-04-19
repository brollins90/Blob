using System;

namespace Blob.Core.Domain
{
    public class Status
    {
        public virtual long Id { get; set; }
        public virtual string MonitorName { get; set; }
        public virtual string MonitorDescription { get; set; }
        public virtual int AlertLevel { get; set; }
        public virtual DateTime TimeGenerated { get; set; }
        public virtual DateTime TimeSent { get; set; }
        public virtual string CurrentValue { get; set; }

        public virtual Guid DeviceId { get; set; }
        public virtual Device Device { get; set; }
    }
}
