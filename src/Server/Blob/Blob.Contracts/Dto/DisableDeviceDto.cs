using System;
using System.Runtime.Serialization;

namespace Blob.Contracts.Dto
{
    [DataContract]
    public class DisableDeviceDto
    {
        [DataMember]
        public Guid DeviceId { get; set; }
    }
}
