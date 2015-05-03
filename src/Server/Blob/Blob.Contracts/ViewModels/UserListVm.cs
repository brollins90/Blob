using System;
using System.Runtime.Serialization;

namespace Blob.Contracts.ViewModels
{
    [DataContract]
    public class UserListVm
    {
        [DataMember]
        public Guid UserId { get; set; }

        [DataMember]
        public string UserName { get; set; }
    }
}
