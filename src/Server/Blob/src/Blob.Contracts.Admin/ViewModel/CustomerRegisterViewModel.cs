namespace Blob.Contracts.ViewModel
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.Serialization;
    using Request;

    [DataContract]
    public class CustomerRegisterViewModel
    {
        [DataMember]
        public Guid CustomerId { get; set; } = Guid.NewGuid();

        [DataMember]
        [Display(Name = "Customer name")]
        [Required]
        public string CustomerName { get; set; }

        [DataMember]
        [Required]
        public UserRegisterViewModel UserRegistration { get; set; }

        public RegisterCustomerRequest ToRequest()
        {
            return new RegisterCustomerRequest
            {
                CustomerId = CustomerId,
                CustomerName = CustomerName,
                DefaultUser = new CreateUserRequest
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