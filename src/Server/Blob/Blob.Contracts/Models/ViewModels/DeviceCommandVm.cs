using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Blob.Contracts.Models.ViewModels
{
    [DataContract]
    public class DeviceCommandVm
    {
        [DataMember]
        [Display(Name = "Command type")]
        [Required]
        public string CommandType { get; set; }

        [DataMember]
        [Display(Name = "Short name")]
        [Required]
        public string ShortName { get; set; }

        [DataMember]
        [Display(Name = "Command parameters")]
        public IEnumerable<DeviceCommandParameterPairVm> CommandParamters { get; set; }
    }

    [DataContract]
    public class DeviceCommandParameterPairVm
    {
        [DataMember]
        public string Key { get; set; }

        [DataMember]
        public string Value { get; set; }
    }
}
