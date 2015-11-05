using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Blob.Contracts.Models.ViewModels
{
    [DataContract]
    public class RoleCreateVm
    {
        public RoleCreateVm()
        {
            RoleId = Guid.NewGuid();
        }

        [DataMember]
        [Display(Name = "Id")]
        [Required]
        public Guid RoleId { get; set; }

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
