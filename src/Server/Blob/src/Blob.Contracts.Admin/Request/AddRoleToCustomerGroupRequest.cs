namespace Blob.Contracts.Request
{
    using System;
    using System.Runtime.Serialization;

    [DataContract]
    public class AddRoleToCustomerGroupRequest
    {
        [DataMember]
        public Guid GroupId { get; set; }

        [DataMember]
        public Guid RoleId { get; set; }
    }
}