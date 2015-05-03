using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Blob.Contracts.ViewModels
{
    [DataContract]
    public class DeviceTypeSingleVm
    {
        [DataMember]
        [Display(Name = "Id")]
        public Guid DeviceTypeId { get; set; }

        [DataMember]
        [Display(Name = "Device type")]
        public string Value { get; set; }
    }
}
