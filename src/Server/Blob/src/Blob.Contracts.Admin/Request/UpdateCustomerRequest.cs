namespace Blob.Contracts.Request
{
    using System;
    using System.Runtime.Serialization;

    [DataContract]
    public class UpdateCustomerRequest
    {
        [DataMember]
        public Guid CustomerId { get; set; }

        [DataMember]
        public string Name { get; set; }
    }
}