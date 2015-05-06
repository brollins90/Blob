using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Blob.Contracts.Dto
{
    [DataContract]
    public class AddPerformanceRecordDto
    {
        [DataMember]
        public Guid DeviceId { get; set; }

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
        public List<PerformanceRecordValue> Data
        {
            get { return _data ?? (_data = new List<PerformanceRecordValue>()); }
            set { _data = value; }
        }
        private List<PerformanceRecordValue> _data;
    }


    [DataContract]
    public class PerformanceRecordValue
    {
        [DataMember]
        public string Label { get; set; }

        [DataMember]
        public string Value { get; set; }

        [DataMember]
        public string UnitOfMeasure { get; set; }

        [DataMember]
        public string Warning { get; set; }

        [DataMember]
        public string Critical { get; set; }

        [DataMember]
        public string Min { get; set; }

        [DataMember]
        public string Max { get; set; }
    }
}
