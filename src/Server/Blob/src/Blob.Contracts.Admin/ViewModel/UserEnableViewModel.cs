namespace Blob.Contracts.ViewModel
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.Serialization;
    using Request;

    [DataContract]
    public class UserEnableViewModel
    {
        [DataMember]
        [Display(Name = "Id")]
        [Required]
        public Guid UserId { get; set; }

        [DataMember]
        [Display(Name = "Email")]
        [EmailAddress]
        [Required]
        public string Email { get; set; }

        [DataMember]
        [Display(Name = "Enabled")]
        [Required]
        public bool Enabled { get; set; }

        [DataMember]
        [Display(Name = "User name")]
        [Required]
        public string UserName { get; set; }

        public EnableUserRequest ToRequest()
        {
            return new EnableUserRequest { UserId = UserId };
        }
    }
}