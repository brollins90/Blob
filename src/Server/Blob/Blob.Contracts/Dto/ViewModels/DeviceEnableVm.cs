using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Blob.Contracts.Dto.ViewModels
{
    [DataContract]
    public class DeviceEnableVm
    {
        [DataMember]
        [Display(Name = "Id")]
        [Required]
        public Guid DeviceId { get; set; }

        [DataMember]
        [Display(Name = "Device name")]
        [Required]
        public string Name { get; set; }

        public EnableDeviceDto ToDto()
        {
            return new EnableDeviceDto { DeviceId = this.DeviceId, Name = this.Name };
        }
    }
}
