using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Blob.Contracts.Models
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

        public void AddPerformanceDataValue(PerformanceRecordValue value)
        {
            Data.Add(value);
        }

        public override string ToString()
        {
            return string.Format("AddPerformanceRecordDto("
                                 + "DeviceId: " + DeviceId
                                 + ", MonitorName: " + MonitorName
                                 + ", MonitorDescription: " + MonitorDescription
                                 + ", TimeGenerated: " + TimeGenerated
                                 + ", TimeSent: " + TimeSent
                                 + ", Count: " + Data.Count
                                 + ")");
        }
    }
}