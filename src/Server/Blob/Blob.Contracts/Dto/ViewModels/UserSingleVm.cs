using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Blob.Contracts.Dto.ViewModels
{
    [DataContract]
    public class UserSingleVm
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

        // audit logs?
        // permissions?
    }
}
