using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Blob.Contracts.Dto.ViewModels
{
    [DataContract]
    public class DeviceTypeSingleVm
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
