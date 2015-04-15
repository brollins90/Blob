using System;
using System.Runtime.Serialization;

namespace Blob.Contracts.Models
{
    [DataContract]
    public class RegistrationMessage
    {
        [DataMember]
        public string DeviceId { get; set; }

        [DataMember]
        public string DeviceKey1 { get; set; }

        [DataMember]
        public int DeviceKey1Format { get; set; }

        [DataMember]
        public string DeviceKey2 { get; set; }

        [DataMember]
        public int DeviceKey2Format { get; set; }

        [DataMember]
        public string DeviceName { get; set; }

        [DataMember]
        public string DeviceType { get; set; }

        [DataMember]
        public DateTime TimeSent { get; set; }

        public override string ToString()
        {
            return string.Format("RegistrationMessage("
                                 + "DeviceId: " + DeviceId
                                 + ", DeviceKey1: " + DeviceKey1
                                 + ", DeviceKey1Format: " + DeviceKey1Format
                                 + ", DeviceKey2: " + DeviceKey2
                                 + ", DeviceKey2Format: " + DeviceKey2Format
                                 + ", DeviceName: " + DeviceName
                                 + ", DeviceType: " + DeviceType
                                 + ", TimeSent: " + TimeSent
                                 + ")");
        }
    }
}