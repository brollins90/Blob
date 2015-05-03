using System;
using System.Runtime.Serialization;

namespace Blob.Contracts.Models
{
    [DataContract]
    public class UpdateDeviceDto
    {
        [DataMember]
        public Guid DeviceId { get; set; }

        [DataMember]
        public string Name { get; set; }

        public override string ToString()
        {
            return string.Format("UpdateDeviceDto("
                                 + "DeviceId: " + DeviceId
                                 + ", Name: " + Name
                                 + ")");
        }
    }
}