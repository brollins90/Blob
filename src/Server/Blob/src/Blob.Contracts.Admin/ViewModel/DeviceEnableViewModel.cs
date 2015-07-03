namespace Blob.Contracts.ViewModel
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.Serialization;
    using Request;

    [DataContract]
    public class DeviceEnableViewModel
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
        [Display(Name = "Enabled")]
        [Required]
        public bool Enabled { get; set; }

        public EnableDeviceRequest ToRequest()
        {
            return new EnableDeviceRequest { DeviceId = DeviceId };
        }
    }
}