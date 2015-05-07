using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Blob.Contracts.Dto.ViewModels
{
    [DataContract]
    public class DeviceCommandVm
    {
        [DataMember]
        [Display(Name = "Command type")]
        [Required]
        public string CommandType { get; set; }

        [DataMember]
        [Display(Name = "Command parameters")]
        public Dictionary<string, string> CommandParamters { get; set; }
    }
}
