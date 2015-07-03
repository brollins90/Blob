namespace Blob.Contracts.Request
{
    using System;
    using System.Runtime.Serialization;

    [DataContract]
    public class RegisterDeviceRequest
    {
        [DataMember]
        public string CustomerId { get; set; }

        [DataMember]
        public string DeviceId { get; set; }

        [DataMember]
        public string DeviceKey1 { get; set; }

        [DataMember]
        public int DeviceKey1Format { get; set; }

        [DataMember]
        public string DeviceKey2 { get; set; }

        [DataMember]
        public int DeviceKey2Format { get; set; }

        [DataMember]
        public string DeviceName { get; set; }

        [DataMember]
        public string DeviceType { get; set; }

        [DataMember]
        public DateTime TimeSent { get; set; }
    }
}