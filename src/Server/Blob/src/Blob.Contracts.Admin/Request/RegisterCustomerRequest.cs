namespace Blob.Contracts.Request
{
    using System;
    using System.Runtime.Serialization;

    [DataContract]
    public class RegisterCustomerRequest
    {
        [DataMember]
        public Guid CustomerId { get; set; } = Guid.NewGuid();

        [DataMember]
        public string CustomerName { get; set; }

        [DataMember]
        public CreateUserRequest DefaultUser { get; set; }
    }
}