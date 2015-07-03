using System;
using System.Runtime.Serialization;

namespace Blob.Contracts.Models
{
    public class CheckDeviceRegistrationResponseDto : BlobResult
    {
        [DataMember]
        public DateTime TimeSent { get; set; }
    }
}
