using System;
using System.Runtime.Serialization;

namespace Blob.Contracts.Models
{
    [DataContract]
    public class RegisterCustomerDto
    {
        public RegisterCustomerDto()
        {
            CustomerId = Guid.NewGuid();
        }

        [DataMember]
        public Guid CustomerId { get; set; }

        [DataMember]
        public string CustomerName { get; set; }

        [DataMember]
        public CreateUserDto DefaultUser { get; set; }
    }
}
