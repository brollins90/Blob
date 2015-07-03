namespace Blob.Contracts.Response
{
    using System;
    using System.Runtime.Serialization;

    public class RegisterDeviceResponse : BlobResult
    {
        [DataMember]
        public Guid DeviceId { get; set; }

        [DataMember]
        public DateTime TimeSent { get; set; }
    }
}