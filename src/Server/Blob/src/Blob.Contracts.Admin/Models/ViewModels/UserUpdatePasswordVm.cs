using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Blob.Contracts.Models.ViewModels
{
    [DataContract]
    public class UserUpdatePasswordVm
    {
        [DataMember]
        [Display(Name = "Id")]
        [Required]
        public Guid UserId { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        [Required]
        public string OldPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        public string NewPassword { get; set; }

        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        public string ConfirmPassword { get; set; }
    }
}
