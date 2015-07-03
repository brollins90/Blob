namespace Blob.Contracts.ViewModel
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.Serialization;
    using Request;

    [DataContract]
    public class CustomerGroupDeleteViewModel
    {
        [DataMember]
        [Display(Name = "Id")]
        [Required]
        public Guid GroupId { get; set; }

        [DataMember]
        [Required]
        public string Name { get; set; }

        public DeleteCustomerGroupRequest ToRequest()
        {
            return new DeleteCustomerGroupRequest { GroupId = GroupId };
        }
    }
}