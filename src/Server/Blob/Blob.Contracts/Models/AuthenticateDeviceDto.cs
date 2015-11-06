using System;
using System.Runtime.Serialization;

namespace Blob.Contracts.Models
{
    [DataContract]
    public class AuthenticateDeviceDto
    {
        [DataMember]
        public Guid DeviceId { get; set; }

        [DataMember]
        public string Key { get; set; }

        [DataMember]
        public int KeyFormat { get; set; }
    }
}
