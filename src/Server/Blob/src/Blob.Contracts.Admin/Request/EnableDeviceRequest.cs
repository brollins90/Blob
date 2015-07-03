namespace Blob.Contracts.Request
{
    using System;
    using System.Runtime.Serialization;

    [DataContract]
    public class EnableDeviceRequest
    {
        [DataMember]
        public Guid DeviceId { get; set; }

        [DataMember]
        public string Name { get; set; }
    }
}