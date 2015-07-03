namespace Blob.Contracts.ViewModel
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.Serialization;
    using Request;

    [DataContract]
    public class UserCreateViewModel
    {
        [DataMember]
        public Guid CustomerId { get; set; }

        [DataMember]
        [Display(Name = "Id")]
        [Required]
        public Guid UserId { get; set; } = Guid.NewGuid();

        [EmailAddress]
        [DataMember]
        [Display(Name = "Email")]
        [Required]
        public string Email { get; set; }

        [DataMember]
        [Display(Name = "Username")]
        [Required]
        public string UserName { get; set; }

        public CreateUserRequest ToRequest()
        {
            return new CreateUserRequest { UserId = UserId, Email = Email, UserName = UserName, CustomerId = CustomerId };
        }
    }
}