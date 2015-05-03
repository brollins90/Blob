using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Blob.Contracts.ViewModels
{
    [DataContract]
    public class DeviceListVm
    {
        [DataMember]
        [Display(Name = "Id")]
        public Guid DeviceId { get; set; }

        [DataMember]
        [Display(Name = "Status")]
        public string Status { get; set; }

        [DataMember]
        [Display(Name = "Device name")]
        public string DeviceName { get; set; }

        [DataMember]
        [Display(Name = "Device type")]
        public string DeviceType { get; set; }

        [DataMember]
        [Display(Name = "Last activity")]
        public DateTime LastActivityDate { get; set; }
    }
}
