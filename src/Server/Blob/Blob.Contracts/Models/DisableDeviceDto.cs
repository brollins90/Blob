using System;
using System.Runtime.Serialization;

namespace Blob.Contracts.Models
{
    [DataContract]
    public class DisableDeviceDto
    {
        [DataMember]
        public Guid DeviceId { get; set; }

        public override string ToString()
        {
            return string.Format("DisableDeviceDto("
                                 + "DeviceId: " + DeviceId
                                 + ")");
        }
    }
}