using System;
using System.Runtime.Serialization;

namespace Blob.Contracts.Dto
{
    [DataContract]
    public class DisableUserDto
    {
        [DataMember]
        public Guid UserId { get; set; }
    }
}
