using System;
using System.Runtime.Serialization;

namespace Blob.Contracts.Models
{
    [DataContract]
    public class EnableCustomerDto
    {
        [DataMember]
        public Guid CustomerId { get; set; }
    }
}
