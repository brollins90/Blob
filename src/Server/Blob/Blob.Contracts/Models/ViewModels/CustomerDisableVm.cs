using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Blob.Contracts.Models.ViewModels
{
    [DataContract]
    public class CustomerDisableVm
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

        public DisableCustomerDto ToDto()
        {
            return new DisableCustomerDto { CustomerId = CustomerId };
        }
    }
}
