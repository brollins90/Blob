using System;
using System.Runtime.Serialization;

namespace Blob.Contracts.Models.ViewModels
{
    [DataContract]
    public class CustomerRoleListItemVm
    {
        [DataMember]
        public Guid RoleId { get; set; }
        [DataMember]
        public string RoleName { get; set; }
    }
}
