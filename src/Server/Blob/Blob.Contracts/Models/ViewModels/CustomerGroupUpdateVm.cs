using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Blob.Contracts.Models.ViewModels
{
    [DataContract]
    public class CustomerGroupUpdateVm
    {
        [DataMember]
        [Display(Name = "Id")]
        [Required]
        public Guid GroupId { get; set; }

        [DataMember]
        [Required]
        public string Name { get; set; }
        
        [DataMember]
        [Required]
        public string Description { get; set; }

        public UpdateCustomerGroupDto ToDto()
        {
            return new UpdateCustomerGroupDto { GroupId = GroupId, Name = Name, Description = Description };
        }
    }
}
