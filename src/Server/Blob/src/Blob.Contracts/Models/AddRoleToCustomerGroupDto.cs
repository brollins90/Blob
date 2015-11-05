using System;
using System.Runtime.Serialization;

namespace Blob.Contracts.Models
{
    [DataContract]
    public class AddRoleToCustomerGroupDto
    {
        [DataMember]
        public Guid GroupId { get; set; }

        [DataMember]
        public Guid RoleId { get; set; }
    }

    [DataContract]
    public class AddUserToCustomerGroupDto
    {
        [DataMember]
        public Guid GroupId { get; set; }

        [DataMember]
        public Guid UserId { get; set; }
    }

    [DataContract]
    public class RemoveRoleFromCustomerGroupDto
    {
        [DataMember]
        public Guid GroupId { get; set; }

        [DataMember]
        public Guid RoleId { get; set; }
    }

    [DataContract]
    public class RemoveUserFromCustomerGroupDto
    {
        [DataMember]
        public Guid GroupId { get; set; }

        [DataMember]
        public Guid UserId { get; set; }
    }
}
