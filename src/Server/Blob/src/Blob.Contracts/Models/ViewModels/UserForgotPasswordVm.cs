﻿using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Blob.Contracts.Models.ViewModels
{
    [DataContract]
    public class UserForgotPasswordVm
    {
        [DataMember]
        [Display(Name = "Email")]
        [EmailAddress]
        [Required]
        public string Email { get; set; }
    }
}
