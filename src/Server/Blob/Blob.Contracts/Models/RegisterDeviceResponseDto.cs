using System;
using System.Runtime.Serialization;

namespace Blob.Contracts.Models
{
    public class RegisterDeviceResponseDto : BlobResultDto
    {
        //[DataMember]
        //public bool Succeeded { get; set; }

        [DataMember]
        public Guid DeviceId { get; set; }

        [DataMember]
        public DateTime TimeSent { get; set; }
    }
}
