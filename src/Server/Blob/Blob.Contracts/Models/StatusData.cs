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
        public DateTime TimeGenerated { get; set; }

        [DataMember]
        public DateTime TimeSent { get; set; }

        [DataMember]
        public string CurrentValue { get; set; }

        [DataMember]
        public string PreviousValue { get; set; }

        public override string ToString()
        {
            return string.Format("StatusMessage:\n"
                + "DeviceId: {0}\n"
                + "MonitorName: {1}\n"
                + "MonitorDescription: {2}\n"
                + "TimeGenerated: {3}\n"
                + "TimeSent: {4}\n"
                + "CurrentValue: {5}\n"
                + "PreviousValue{6}",
                                 DeviceId,
                                 MonitorName,
                                 MonitorDescription,
                                 TimeGenerated,
                                 TimeSent,
                                 CurrentValue,
                                 PreviousValue);
        }
    }
}