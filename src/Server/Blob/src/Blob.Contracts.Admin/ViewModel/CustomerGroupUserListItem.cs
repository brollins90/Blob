namespace Blob.Contracts.ViewModel
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.Serialization;

    [DataContract]
    public class CustomerGroupUserListItem
    {
        [DataMember]
        [Display(Name = "Id")]
        [Required]
        public Guid UserId { get; set; }

        [DataMember]
        [Display(Name = "Name")]
        [Required]
        public string UserName { get; set; }
    }
}