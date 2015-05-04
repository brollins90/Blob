using System;
using System.Runtime.Serialization;

namespace Blob.Contracts.Dto
{
    [DataContract]
    public class AddStatusRecordDto
    {
        [DataMember]
        public Guid DeviceId { get; set; }

        [DataMember]
        public string MonitorName { get; set; }

        [DataMember]
        public string MonitorDescription { get; set; }

        [DataMember]
        public int AlertLevel { get; set; }

        [DataMember]
        public DateTime TimeGenerated { get; set; }

        [DataMember]
        public DateTime TimeSent { get; set; }

        [DataMember]
        public string CurrentValue { get; set; }
    }
}
