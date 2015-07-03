namespace Blob.Contracts.Request
{
    using System;
    using System.Runtime.Serialization;

    [DataContract]
    public class AuthenticateDeviceRequest
    {
        [DataMember]
        public Guid DeviceId { get; set; }

        [DataMember]
        public string Key { get; set; }

        [DataMember]
        public int KeyFormat { get; set; }
    }
}