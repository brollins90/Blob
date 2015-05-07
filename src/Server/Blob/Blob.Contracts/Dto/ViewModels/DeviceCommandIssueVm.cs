using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Blob.Contracts.Dto.ViewModels
{
    public class DeviceCommandIssueVm
    {
        [DataMember]
        [Display(Name = "Device Id")]
        [Required]
        public Guid DeviceId { get; set; }

        //[DataMember]
        //[Required]
        //public DeviceCommandVm SelectedCommand { get; set; }

        [DataMember]
        public string CommandType { get; set; }

        [DataMember]
        public string CommandData { get; set; }

        [DataMember]
        public IEnumerable<DeviceCommandVm> AvailableCommands { get; set; }

        public IssueDeviceCommandDto ToDto()
        {
            return new IssueDeviceCommandDto { DeviceId = DeviceId, Command = CommandType, CommandParameters = new Dictionary<string, string> { { "CommandData", CommandData } }, TimeSent = DateTime.Now };
        }
    }
}
