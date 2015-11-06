using System;
using System.Runtime.Serialization;

namespace Blob.Contracts.Models
{
    public class CheckDeviceRegistrationResponseDto : BlobResultDto
    {
        [DataMember]
        public DateTime TimeSent { get; set; }
    }
}
