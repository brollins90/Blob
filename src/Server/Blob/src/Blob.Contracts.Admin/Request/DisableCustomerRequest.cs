namespace Blob.Contracts.Request
{
    using System;
    using System.Runtime.Serialization;

    [DataContract]
    public class DisableCustomerRequest
    {
        [DataMember]
        public Guid CustomerId { get; set; }
    }
}