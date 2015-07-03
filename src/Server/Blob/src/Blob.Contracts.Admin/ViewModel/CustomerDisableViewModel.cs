namespace Blob.Contracts.ViewModel
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.Serialization;
    using Request;

    [DataContract]
    public class CustomerDisableViewModel
    {
        [DataMember]
        [Display(Name = "Id")]
        [Required]
        public Guid CustomerId { get; set; }

        [DataMember]
        [Display(Name = "Customer name")]
        [Required]
        public string CustomerName { get; set; }

        [DataMember]
        [Display(Name = "Enabled")]
        [Required]
        public bool Enabled { get; set; }

        public DisableCustomerRequest ToRequest()
        {
            return new DisableCustomerRequest { CustomerId = CustomerId };
        }
    }
}