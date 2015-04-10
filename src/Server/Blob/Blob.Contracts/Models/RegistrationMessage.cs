using System;
using System.Runtime.Serialization;

namespace Blob.Contracts.Models
{
    [DataContract]
    public class RegistrationMessage
    {
        [DataMember]
        public string DeviceId { get; set; }

        [DataMember]
        public string DeviceName { get; set; }

        [DataMember]
        public string DeviceType { get; set; }

        [DataMember]
        public DateTime TimeSent { get; set; }

        [DataMember]
        public string Username { get; set; }

        [DataMember]
        public string PasswordHash { get; set; }

        [DataMember]
        public string Key { get; set; }

        public override string ToString()
        {
            return string.Format("RegistrationMessage:\n" +
                                 "DeviceName: {0}\n" +
                                 "DeviceType: {1}\n" +
                                 "TimeSent: {2}\n" +
                                 "Username: {3}\n" +
                                 "PasswordHash: {4}\n" +
                                 "Key{5}",
                                 DeviceName,
                                 DeviceType,
                                 TimeSent,
                                 Username,
                                 PasswordHash,
                                 Key);
        }
    }
}