using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Blob.Contracts.Models.ViewModels
{
    [DataContract]
    public class CustomerGroupCreateVm
    {
        [DataMember]
        [Display(Name = "Id")]
        [Required]
        public Guid GroupId { get; set; }

        [DataMember]
        [Required]
        public Guid CustomerId { get; set; }

        [DataMember]
        [Required]
        public string Name { get; set; }
        
        [DataMember]
        [Required]
        public string Description { get; set; }

        public CreateCustomerGroupDto ToDto()
        {
            return new CreateCustomerGroupDto { GroupId = GroupId, CustomerId = CustomerId, Name = Name, Description = Description };
        }
    }
}
