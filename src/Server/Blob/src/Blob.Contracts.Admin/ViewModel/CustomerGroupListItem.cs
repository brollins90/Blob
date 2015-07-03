namespace Blob.Contracts.ViewModel
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.Serialization;

    [DataContract]
    public class CustomerGroupListItem
    {
        [DataMember]
        [Display(Name = "Id")]
        [Required]
        public Guid GroupId { get; set; }

        [DataMember]
        [Display(Name = "Name")]
        [Required]
        public string Name { get; set; }
    }
}