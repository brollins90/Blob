using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Blob.Contracts.Models.ViewModels
{
    public class CustomerUpdateVm
    {
        [DataMember]
        [Display(Name = "Id")]
        [Required]
        public Guid CustomerId { get; set; }

        [DataMember]
        public string CustomerName { get; set; }

        public UpdateCustomerDto ToDto()
        {
            return new UpdateCustomerDto { CustomerId = CustomerId, Name = CustomerName };
        }
    }
}
