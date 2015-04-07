using System;
using System.Runtime.Serialization;

namespace Blob.Contracts.Models
{
    [DataContract]
    public class StatusData
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

        public override string ToString()
        {
            return string.Format("StatusMessage:\n"
                + "DeviceId: {0}\n"
                + "MonitorName: {1}\n"
                + "MonitorDescription: {2}\n"
                + "AlertLevel: {3}\n"
                + "TimeGenerated: {4}\n"
                + "TimeSent: {5}\n"
                + "CurrentValue: {6}\n",
                                 DeviceId,
                                 MonitorName,
                                 MonitorDescription,
                                 AlertLevel,
                                 TimeGenerated,
                                 TimeSent,
                                 CurrentValue);
        }
    }
}