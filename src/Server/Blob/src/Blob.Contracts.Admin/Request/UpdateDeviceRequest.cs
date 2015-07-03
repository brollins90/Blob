namespace Blob.Contracts.Request
{
    using System;
    using System.Runtime.Serialization;

    [DataContract]
    public class UpdateDeviceRequest
    {
        [DataMember]
        public Guid DeviceId { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public Guid DeviceTypeId { get; set; }
    }
}