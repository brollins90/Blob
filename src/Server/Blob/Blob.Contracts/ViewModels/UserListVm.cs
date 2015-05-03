﻿using System;
using System.Runtime.Serialization;

namespace Blob.Contracts.ViewModels
{
    [DataContract]
    public class UserListVm
    {
        [DataMember]
        public int PageNumber { get; set; }

        [DataMember]
        public int PageSize { get; set; }

        [DataMember]
        public Guid UserId { get; set; }

        [DataMember]
        public string UserName { get; set; }

        [DataMember]
        public string Email { get; set; }
    }
}
