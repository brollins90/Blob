namespace Blob.Contracts.Request
{
    using System;
    using System.Runtime.Serialization;

    [DataContract]
    public class CreateUserIdentityRequest
    {
        [DataMember]
        public Guid UserId { get; set; }

        [DataMember]
        public string UserName { get; set; }

        [DataMember]
        public string Password { get; set; }

        [DataMember]
        public string AuthenticationType { get; set; }
    }
}