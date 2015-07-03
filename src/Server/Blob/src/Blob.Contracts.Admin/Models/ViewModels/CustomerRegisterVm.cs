using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Blob.Contracts.Models.ViewModels
{
    [DataContract]
    public class CustomerRegisterVm
    {
        public CustomerRegisterVm()
        {
            CustomerId = Guid.NewGuid();
        }

        [DataMember]
        public Guid CustomerId { get; set; }

        [DataMember]
        [Display(Name = "Customer name")]
        [Required]
        public string CustomerName { get; set; }

        [DataMember]
        [Required]
        public UserRegisterVm UserRegistration { get; set; }

        public RegisterCustomerDto ToDto()
        {
            return new RegisterCustomerDto
                   {
                       CustomerId = CustomerId, CustomerName = CustomerName,
                       DefaultUser = new CreateUserDto
                                     {
                                         CustomerId = CustomerId, 
                                         Email = UserRegistration.Email, 
                                         Password = UserRegistration.Password,
                                         UserId = UserRegistration.UserId,
                                         UserName = UserRegistration.UserName
                                     }
                   };
        }
    }
}
