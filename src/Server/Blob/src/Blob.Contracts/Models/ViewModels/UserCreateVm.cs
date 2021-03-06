using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Blob.Contracts.Models.ViewModels
{
    [DataContract]
    public class UserCreateVm
    {
        public UserCreateVm()
        {
            UserId = Guid.NewGuid();
        }

        [DataMember]
        public Guid CustomerId { get; set; }

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
        [Display(Name = "Username")]
        [Required]
        public string UserName { get; set; }

        public CreateUserDto ToDto()
        {
            return new CreateUserDto { UserId = UserId, Email = Email, UserName = UserName, CustomerId = CustomerId };
        }
    }
}
