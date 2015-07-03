namespace Blob.Contracts.Request
{
    using System;
    using System.Runtime.Serialization;

    [DataContract]
    public class DisableDeviceRequest
    {
        [DataMember]
        public Guid DeviceId { get; set; }
    }
}