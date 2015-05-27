using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Blob.Contracts.Models.ViewModels
{
    [DataContract]
    public class CustomerGroupUserListItem
    {
        [DataMember]
        [Display(Name = "Id")]
        [Required]
        public Guid RoleId { get; set; }

        [DataMember]
        [Display(Name = "Name")]
        [Required]
        public string UserName { get; set; }

        [DataMember]
        public IEnumerable<string> Roles { get; set; }
    }
}
