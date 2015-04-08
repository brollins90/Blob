using System;

namespace Blob.Core.Domain
{
    public class StatusPerf
    {
        public long Id { get; set; }
        public Guid DeviceId { get; set; }
        public string MonitorName { get; set; }
        public string MonitorDescription { get; set; }
        public DateTime TimeGenerated { get; set; }
        public DateTime TimeSent { get; set; }
        public string Label { get; set; }
        public string Value { get; set; }
        public string UnitOfMeasure { get; set; }
        public string Warning { get; set; }
        public string Critical { get; set; }
        public string Min { get; set; }
        public string Max { get; set; }
    }
}
