using System;
using System.Runtime.Serialization;

namespace Blob.Contracts.Models
{
    [DataContract]
    public class CheckDeviceRegistrationDto
    {
        [DataMember]
        public string DeviceId { get; set; }

        [DataMember]
        public string DeviceKey { get; set; }

        [DataMember]
        public int DeviceKeyFormat { get; set; }

        [DataMember]
        public DateTime TimeSent { get; set; }
    }
}
