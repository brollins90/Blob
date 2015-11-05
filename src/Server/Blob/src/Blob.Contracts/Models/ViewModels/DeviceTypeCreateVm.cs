using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Blob.Contracts.Models.ViewModels
{
    [DataContract]
    public class DeviceTypeCreateVm
    {
        [DataMember]
        [Display(Name = "Device type")]
        [Required]
        public string Value { get; set; }
    }
}
