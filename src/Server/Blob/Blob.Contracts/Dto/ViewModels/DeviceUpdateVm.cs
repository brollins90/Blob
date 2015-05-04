using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Blob.Contracts.Dto.ViewModels
{
    [DataContract]
    public class DeviceUpdateVm
    {
        [DataMember]
        [Display(Name = "Id")]
        [Required]
        public Guid DeviceId { get; set; }


        [DataMember]
        [Display(Name = "Device name")]
        [Required]
        public string DeviceName { get; set; }
        
        [DataMember]
        [Display(Name = "Device type")]
        [Required]
        public Guid DeviceTypeId { get; set; }

        [DataMember]
        public IEnumerable<DeviceTypeSingleVm> AvailableTypes { get; set; }

        public UpdateDeviceDto ToDto()
        {
            return new UpdateDeviceDto { DeviceId = this.DeviceId, DeviceTypeId = this.DeviceTypeId, Name = this.DeviceName };
        }
    }
}
