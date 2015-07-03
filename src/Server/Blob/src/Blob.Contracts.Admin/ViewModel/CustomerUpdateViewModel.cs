namespace Blob.Contracts.ViewModel
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.Serialization;
    using Request;

    public class CustomerUpdateViewModel
    {
        [DataMember]
        [Display(Name = "Id")]
        [Required]
        public Guid CustomerId { get; set; }

        [DataMember]
        public string CustomerName { get; set; }

        public UpdateCustomerRequest ToRequest()
        {
            return new UpdateCustomerRequest { CustomerId = CustomerId, Name = CustomerName };
        }
    }
}