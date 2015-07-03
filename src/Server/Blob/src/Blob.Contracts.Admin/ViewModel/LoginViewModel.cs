namespace Blob.Contracts.ViewModel
{
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.Serialization;

    [DataContract]
    public class LoginViewModel
    {
        [DataMember]
        [Display(Name = "Username / Email")]
        [Required]
        public string UserNameEmail { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [DataMember]
        [Required]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }

        public string ReturnUrl { get; set; }
    }
}