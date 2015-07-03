namespace Blob.Contracts.Models
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    [DataContract]
    public class AddPerformanceRecordRequest
    {
        [DataMember]
        public Guid DeviceId { get; set; }

        [DataMember]
        public string MonitorId { get; set; }

        [DataMember]
        public string MonitorName { get; set; }

        [DataMember]
        public string MonitorDescription { get; set; }

        [DataMember]
        public long? StatusRecordId { get; set; }

        [DataMember]
        public DateTime TimeGenerated { get; set; }

        [DataMember]
        public DateTime TimeSent { get; set; }

        [DataMember]
        public List<PerformanceRecordItem> Data { get; set; } = new List<PerformanceRecordItem>();
    }
}