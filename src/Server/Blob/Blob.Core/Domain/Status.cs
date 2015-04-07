using System;

namespace Blob.Core.Domain
{
    public class Status
    {
        public long Id { get; set; }
        public Guid DeviceId { get; set; }
        public string MonitorName { get; set; }
        public string MonitorDescription { get; set; }
        public int AlertLevel { get; set; }
        public DateTime TimeGenerated { get; set; }
        public DateTime TimeSent { get; set; }
        public string CurrentValue { get; set; }
    }
}
