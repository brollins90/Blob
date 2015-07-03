namespace Blob.Contracts.ViewModel
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.Serialization;

    [DataContract]
    public class RoleCreateViewModel
    {
        [DataMember]
        [Display(Name = "Id")]
        [Required]
        public Guid RoleId { get; set; } = Guid.NewGuid();

        [DataMember]
        [Display(Name = "Name")]
        [Required]
        public string Name { get; set; }

        [DataMember]
        [Display(Name = "Description")]
        [Required]
        public string Description { get; set; }
    }
}