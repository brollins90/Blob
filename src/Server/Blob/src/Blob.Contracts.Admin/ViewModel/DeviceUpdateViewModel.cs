namespace Blob.Contracts.ViewModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.Serialization;
    using Request;

    [DataContract]
    public class DeviceUpdateViewModel
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
        public IEnumerable<DeviceTypeSingleViewModel> AvailableTypes { get; set; }

        public UpdateDeviceRequest ToRequest()
        {
            return new UpdateDeviceRequest { DeviceId = DeviceId, DeviceTypeId = DeviceTypeId, Name = DeviceName };
        }
    }
}