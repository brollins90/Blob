namespace Blob.Contracts.Request
{
    using System;
    using System.Runtime.Serialization;

    [DataContract]
    public class DisableUserRequest
    {
        [DataMember]
        public Guid UserId { get; set; }
    }
}