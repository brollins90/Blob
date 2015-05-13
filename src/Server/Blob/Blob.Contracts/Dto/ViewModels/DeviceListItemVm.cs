using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Blob.Contracts.Dto.ViewModels
{
    [DataContract]
    public class DeviceListItemVm
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
        public string DeviceType { get; set; }

        [DataMember]
        [Display(Name = "Enabled")]
        [Required]
        public bool Enabled { get; set; }

        [DataMember]
        [Display(Name = "Last activity")]
        [Required]
        public DateTime LastActivityDate { get; set; }

        [DataMember]
        [Display(Name = "Status")]
        [Required]
        public int Status { get; set; }

        [DataMember]
        public IEnumerable<DeviceCommandVm> AvailableCommands { get; set; }
    }
}
