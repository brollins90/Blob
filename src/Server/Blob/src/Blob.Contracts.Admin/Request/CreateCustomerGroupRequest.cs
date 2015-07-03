namespace Blob.Contracts.Request
{
    using System;
    using System.Runtime.Serialization;

    [DataContract]
    public class CreateCustomerGroupRequest
    {
        [DataMember]
        public Guid CustomerId { get; set; }

        [DataMember]
        public Guid GroupId { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Description { get; set; }
    }
}