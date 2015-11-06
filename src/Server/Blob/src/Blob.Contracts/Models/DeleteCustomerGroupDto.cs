using System;
using System.Runtime.Serialization;

namespace Blob.Contracts.Models
{
    [DataContract]
    public class DeleteCustomerGroupDto
    {
        [DataMember]
        public Guid GroupId { get; set; }
    }
}
