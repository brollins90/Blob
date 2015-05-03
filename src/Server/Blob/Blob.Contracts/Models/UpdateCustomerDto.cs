using System;
using System.Runtime.Serialization;

namespace Blob.Contracts.Models
{
    [DataContract]
    public class UpdateCustomerDto
    {
        [DataMember]
        public Guid CustomerId { get; set; }
        [DataMember]
        public string Name { get; set; }

        public override string ToString()
        {
            return string.Format("UpdateCustomerDto("
                                 + "CustomerId: " + CustomerId
                                 + ", Name: " + Name
                                 + ")");
        }
    }
}