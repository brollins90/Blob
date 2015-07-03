namespace Blob.Contracts.Request
{
    using System;
    using System.Runtime.Serialization;

    [DataContract]
    public class AddStatusRecordRequest
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
        public string MonitorLabel { get; set; }

        [DataMember]
        public int AlertLevel { get; set; }

        [DataMember]
        public DateTime TimeGenerated { get; set; }

        [DataMember]
        public DateTime TimeSent { get; set; }

        [DataMember]
        public string CurrentValue { get; set; }

        [DataMember]
        public AddPerformanceRecordRequest PerformanceRecordDto { get; set; }
    }
}