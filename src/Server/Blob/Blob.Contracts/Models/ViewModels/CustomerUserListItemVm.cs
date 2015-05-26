using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Blob.Contracts.Models.ViewModels
{
    [DataContract]
    public class CustomerUserListItemVm
    {
        [DataMember]
        [Display(Name = "Id")]
        [Required]
        public Guid UserId { get; set; }

        [EmailAddress]
        [DataMember]
        [Display(Name = "Email")]
        [Required]
        public string Email { get; set; }

        [DataMember]
        //[Required]
        public bool RoleName { get; set; }
    }
}
