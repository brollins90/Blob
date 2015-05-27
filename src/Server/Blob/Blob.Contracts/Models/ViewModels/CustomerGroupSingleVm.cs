using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Blob.Contracts.Models.ViewModels
{
    [DataContract]
    public class CustomerGroupSingleVm
    {
        [DataMember]
        [Display(Name = "Id")]
        [Required]
        public Guid GroupId { get; set; }

        [DataMember]
        [Required]
        public Guid CustomerId { get; set; }

        [DataMember]
        [Required]
        public string Name { get; set; }
        
        [DataMember]
        [Required]
        public string Description { get; set; }

        [DataMember]
        [Display(Name = "Role count")]
        [Required]
        public int RoleCount { get; set; }
        [DataMember]
        public IEnumerable<CustomerRoleListItemVm> Roles { get; set; }

        [DataMember]
        [Display(Name = "User count")]
        [Required]
        public int UserCount { get; set; }
        [DataMember]
        public IEnumerable<CustomerUserListItemVm> Users { get; set; } 
    }
}
