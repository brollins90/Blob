namespace Blob.Contracts.ViewModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.Serialization;

    [DataContract]
    public class CustomerSingleViewModel
    {
        [DataMember]
        [Display(Name = "Id")]
        [Required]
        public Guid CustomerId { get; set; }

        [DataMember]
        [Required]
        public DateTime CreateDate { get; set; }

        [DataMember]
        [Required]
        public bool Enabled { get; set; }

        [DataMember]
        [Display(Name = "Customer name")]
        [Required]
        public string Name { get; set; }

        [DataMember]
        [Display(Name = "Device count")]
        [Required]
        public int DeviceCount { get; set; }

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