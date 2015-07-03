namespace Blob.Contracts.ViewModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Runtime.Serialization;
    using Request;

    public class DeviceCommandIssueViewModel
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
        public IList<DeviceCommandParameterPair> CommandParameters { get; set; }

        public IssueDeviceCommandRequest ToRequest()
        {

            return new IssueDeviceCommandRequest
            {
                DeviceId = DeviceId,
                Command = CommandType,
                CommandParameters = (CommandParameters != null)
                    ? CommandParameters.ToDictionary(kvp => kvp.Key, deviceCommandParameterPairViewModel => deviceCommandParameterPairViewModel.Value)
                    : new Dictionary<string, string>(),
                TimeSent = DateTime.Now
            };
        }
    }
}