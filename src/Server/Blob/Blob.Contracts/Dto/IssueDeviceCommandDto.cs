using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Blob.Contracts.Dto
{
    [DataContract]
    public class IssueDeviceCommandDto
    {
        [DataMember]
        public Guid DeviceId { get; set; }

        [DataMember]
        public string Command { get; set; }

        [DataMember]
        public Dictionary<string, string> CommandParameters { get; set; }

        [DataMember]
        public DateTime TimeSent { get; set; }
    }
}
