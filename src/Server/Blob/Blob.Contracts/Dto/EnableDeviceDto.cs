using System;
using System.Runtime.Serialization;

namespace Blob.Contracts.Dto
{
    [DataContract]
    public class EnableDeviceDto
    {
        [DataMember]
        public Guid DeviceId { get; set; }

        [DataMember]
        public string Name { get; set; }
    }
}
