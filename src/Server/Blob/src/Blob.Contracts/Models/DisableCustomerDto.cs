using System;
using System.Runtime.Serialization;

namespace Blob.Contracts.Models
{
    [DataContract]
    public class DisableCustomerDto
    {
        [DataMember]
        public Guid CustomerId { get; set; }
    }
}
