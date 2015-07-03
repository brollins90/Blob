namespace Blob.Contracts.ViewModel
{
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.Serialization;

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