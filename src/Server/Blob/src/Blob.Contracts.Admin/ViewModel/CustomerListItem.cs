namespace Blob.Contracts.ViewModel
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.Serialization;

    [DataContract]
    public class CustomerListItem
    {
        [DataMember]
        [Display(Name = "Id")]
        [Required]
        public Guid CustomerId { get; set; }

        [DataMember]
        [Required]
        public DateTime CreateDate { get; set; }

        [DataMember]
        [Required]
        public bool Enabled { get; set; }

        [DataMember]
        [Display(Name = "Customer name")]
        [Required]
        public string Name { get; set; }
    }
}