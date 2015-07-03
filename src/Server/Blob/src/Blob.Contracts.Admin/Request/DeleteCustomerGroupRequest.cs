namespace Blob.Contracts.Request
{
    using System;
    using System.Runtime.Serialization;

    [DataContract]
    public class DeleteCustomerGroupRequest
    {
        [DataMember]
        public Guid GroupId { get; set; }
    }
}