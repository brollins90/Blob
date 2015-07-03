namespace Blob.Contracts.ViewModel
{
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.Serialization;

    [DataContract]
    public class DeviceTypeCreateViewModel
    {
        [DataMember]
        [Display(Name = "Device type")]
        [Required]
        public string Value { get; set; }
    }
}