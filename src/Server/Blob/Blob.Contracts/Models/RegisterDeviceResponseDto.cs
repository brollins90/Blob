using System;
using System.Runtime.Serialization;

namespace Blob.Contracts.Models
{
    public class RegisterDeviceResponseDto
    {
        [DataMember]
        public bool Succeeded { get; set; }

        [DataMember]
        public string DeviceId { get; set; }

        [DataMember]
        public DateTime TimeSent { get; set; }
    }
}
