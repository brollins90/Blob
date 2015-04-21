using System;
using System.Runtime.Serialization;

namespace Blob.Contracts.Models
{
    public class RegistrationInformation
    {
        [DataMember]
        public string DeviceId { get; set; }

        [DataMember]
        public DateTime TimeSent { get; set; }

        public override string ToString()
        {
            return string.Format("RegistrationInformation("
                                 + "DeviceId: " + DeviceId
                                 + ", TimeSent: " + TimeSent
                                 + ")");
        }
    }
}