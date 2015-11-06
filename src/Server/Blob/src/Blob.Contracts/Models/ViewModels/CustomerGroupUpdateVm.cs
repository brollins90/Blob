using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Blob.Contracts.Models.ViewModels
{
    [DataContract]
    public class CustomerGroupUpdateVm
    {
        [DataMember]
        [Required]
        public Guid CustomerId { get; set; }

        [DataMember]
        [Required]
        public Guid GroupId { get; set; }

        [DataMember]
        [Required]
        public string Name { get; set; }
        
        [DataMember]
        [Required]
        public string Description { get; set; }

        [DataMember]
        public IEnumerable<CustomerGroupRoleListItem> AvailableRoles { get; set; }

        public UpdateCustomerGroupDto ToDto()
        {
            return new UpdateCustomerGroupDto { GroupId = GroupId, Name = Name, Description = Description };
        }
    }
}
