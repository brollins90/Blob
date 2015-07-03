namespace Blob.Contracts.Request
{
    using System;
    using System.Runtime.Serialization;

    [DataContract]
    public class CheckDeviceRegistrationRequest
    {
        [DataMember]
        public string DeviceId { get; set; }

        [DataMember]
        public string DeviceKey { get; set; }

        [DataMember]
        public int DeviceKeyFormat { get; set; }

        [DataMember]
        public DateTime TimeSent { get; set; }
    }
}