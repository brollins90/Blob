using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Blob.Contracts.Models.ViewModels
{
    [DataContract]
    public class CustomerGroupDeleteVm
    {
        [DataMember]
        [Display(Name = "Id")]
        [Required]
        public Guid GroupId { get; set; }

        [DataMember]
        [Required]
        public string Name { get; set; }

        public DeleteCustomerGroupDto ToDto()
        {
            return new DeleteCustomerGroupDto { GroupId = GroupId };
        }
    }
}
