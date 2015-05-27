using System;
using System.Runtime.Serialization;

namespace Blob.Contracts.Models
{
    [DataContract]
    public class UpdateCustomerGroupDto
    {
        [DataMember]
        public Guid GroupId { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public string[] RolesIdStrings { get; set; }
    }
}
