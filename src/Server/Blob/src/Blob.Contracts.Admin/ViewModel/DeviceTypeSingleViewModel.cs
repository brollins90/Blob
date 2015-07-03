namespace Blob.Contracts.ViewModel
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.Serialization;

    [DataContract]
    public class DeviceTypeSingleViewModel
    {
        [DataMember]
        [Display(Name = "Id")]
        [Required]
        public Guid DeviceTypeId { get; set; }

        [DataMember]
        [Display(Name = "Device type")]
        [Required]
        public string Value { get; set; }
    }
}