using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Blob.Contracts.Models
{
    [DataContract]
    public class StatusPerformanceData
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
        public List<PerformanceDataValue> Data { get; set;}

        public override string ToString()
        {
            return string.Format("StatusPerformanceData:\n"
                + "DeviceId: {0}\n"
                + "MonitorName: {1}\n"
                + "MonitorDescription: {2}\n"
                + "TimeGenerated: {3}\n"
                + "TimeSent: {4}\n",
                                 DeviceId,
                                 MonitorName,
                                 MonitorDescription,
                                 TimeGenerated,
                                 TimeSent);
        }
    }
}