namespace Blob.Contracts.Request
{
    using System;
    using System.Runtime.Serialization;

    [DataContract]
    public class UpdateCustomerGroupRequest
    {
        [DataMember]
        public Guid GroupId { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public string[] RolesIdStrings { get; set; }

        [DataMember]
        public string[] UsersIdStrings { get; set; }
    }
}