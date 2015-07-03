namespace Blob.Contracts.ViewModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.Serialization;
    using Request;

    [DataContract]
    public class CustomerGroupUpdateViewModel
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

        public UpdateCustomerGroupRequest ToRequest()
        {
            return new UpdateCustomerGroupRequest { GroupId = GroupId, Name = Name, Description = Description };
        }
    }
}