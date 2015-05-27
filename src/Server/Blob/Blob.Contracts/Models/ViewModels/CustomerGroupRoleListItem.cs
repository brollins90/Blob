using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Blob.Contracts.Models.ViewModels
{
    [DataContract]
    public class CustomerGroupRoleListItem
    {
        [DataMember]
        [Display(Name = "Id")]
        [Required]
        public Guid RoleId { get; set; }

        [DataMember]
        [Display(Name = "Name")]
        [Required]
        public string Name { get; set; }

        [DataMember]
        public IEnumerable<string> Users { get; set; }
    }
}
