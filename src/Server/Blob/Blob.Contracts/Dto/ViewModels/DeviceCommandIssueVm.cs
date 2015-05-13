using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;

namespace Blob.Contracts.Dto.ViewModels
{
    public class DeviceCommandIssueVm
    {
        [DataMember]
        [Display(Name = "Device Id")]
        [Required]
        public Guid DeviceId { get; set; }

        [DataMember]
        [Display(Name = "Command type")]
        public string CommandType { get; set; }

        [DataMember]
        [Display(Name = "Command")]
        public string ShortName { get; set; }

        [DataMember]
        [Display(Name = "Command parameters")]
        public IEnumerable<DeviceCommandParameterPairVm> CommandParamters { get; set; }

        public IssueDeviceCommandDto ToDto()
        {
            return new IssueDeviceCommandDto { DeviceId = DeviceId, Command = CommandType, 
                CommandParameters = CommandParamters.ToDictionary(deviceCommandParameterPairVm => deviceCommandParameterPairVm.Key, deviceCommandParameterPairVm => deviceCommandParameterPairVm.Value),
                TimeSent = DateTime.Now };
        }
    }
}
