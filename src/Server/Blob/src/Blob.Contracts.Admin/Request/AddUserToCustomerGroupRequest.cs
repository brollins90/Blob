namespace Blob.Contracts.Request
{
    using System;
    using System.Runtime.Serialization;

    [DataContract]
    public class AddUserToCustomerGroupRequest
    {
        [DataMember]
        public Guid GroupId { get; set; }

        [DataMember]
        public Guid UserId { get; set; }
    }
}