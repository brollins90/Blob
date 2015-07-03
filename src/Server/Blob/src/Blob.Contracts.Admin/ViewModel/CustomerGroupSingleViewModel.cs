namespace Blob.Contracts.ViewModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.Serialization;

    [DataContract]
    public class CustomerGroupSingleViewModel
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
        public IEnumerable<CustomerGroupRoleListItem> Roles { get; set; }

        [DataMember]
        [Display(Name = "User count")]
        [Required]
        public int UserCount { get; set; }
        [DataMember]
        public IEnumerable<CustomerGroupUserListItem> Users { get; set; }
    }
}