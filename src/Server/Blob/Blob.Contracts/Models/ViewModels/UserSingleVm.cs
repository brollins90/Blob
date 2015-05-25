using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Blob.Contracts.Models.ViewModels
{
    [DataContract]
    public class UserSingleVm
    {
        [DataMember]
        [Display(Name = "Id")]
        [Required]
        public Guid UserId { get; set; }

        [DataMember]
        [Display(Name = "Create date")]
        [Required]
        public DateTime CreateDate { get; set; }

        [DataMember]
        [Display(Name = "Customer name")]
        [Required]
        public string CustomerName { get; set; }

        [EmailAddress]
        [DataMember]
        [Display(Name = "Email")]
        [Required]
        public string Email { get; set; }

        [DataMember]
        [Required]
        public bool EmailConfirmed { get; set; }

        [DataMember]
        [Required]
        public bool Enabled { get; set; }

        [DataMember]
        [Required]
        public bool HasPassword { get; set; }

        [DataMember]
        [Required]
        public bool HasSecurityStamp { get; set; }

        [DataMember]
        [Display(Name = "Last activity")]
        public DateTime? LastActivityDate { get; set; }

        [DataMember]
        [Display(Name = "Username")]
        [Required]
        public string UserName { get; set; }

        //[DataMember]
        //[Display(Name = "Connected Customers")]
        //public ICollection<IdNameOtherTupleVm> Customers { get; set; }
    }
}
