using System;
using System.Runtime.Serialization;

namespace Blob.Contracts.Models
{
    [DataContract]
    public class EnableUserDto
    {
        [DataMember]
        public Guid UserId { get; set; }
    }
}
