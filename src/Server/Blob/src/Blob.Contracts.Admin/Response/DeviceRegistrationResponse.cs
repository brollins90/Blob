namespace Blob.Contracts.Response
{
    using System;
    using System.Runtime.Serialization;

    public class DeviceRegistrationResponse
    {
        [DataMember]
        public DateTime TimeSent { get; set; }
    }
}