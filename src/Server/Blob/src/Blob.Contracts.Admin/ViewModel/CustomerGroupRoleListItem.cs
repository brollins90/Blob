namespace Blob.Contracts.ViewModel
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.Serialization;

    [DataContract]
    public class CustomerGroupRoleListItem
    {
        [DataMember]
        [Display(Name = "Id")]
        [Required]
        public Guid RoleId { get; set; }

        [DataMember]
        [Display(Name = "Name")]
        [Required]
        public string Name { get; set; }

        [DataMember]
        public bool Selected { get; set; }
    }
}