using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Blob.Contracts.Dto.ViewModels
{
    [DataContract]
    public class DeviceDisableVm
    {
        [DataMember]
        [Display(Name = "Id")]
        [Required]
        public Guid DeviceId { get; set; }

        [DataMember]
        [Display(Name = "Device name")]
        [Required]
        public string Name { get; set; }

        public DisableDeviceDto ToDto()
        {
            return new DisableDeviceDto {DeviceId = this.DeviceId, Name = this.Name};
        }
    }
}
