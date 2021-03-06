using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Blob.Contracts.Models.ViewModels
{
    [DataContract]
    public class UserListItemVm
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
        [Required]
        public bool Enabled { get; set; }

        [DataMember]
        [Display(Name = "Username")]
        [Required]
        public string UserName { get; set; }
    }
}
