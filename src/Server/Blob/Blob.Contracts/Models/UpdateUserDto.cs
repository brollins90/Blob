﻿using System;
using System.Runtime.Serialization;

namespace Blob.Contracts.Models
{
    [DataContract]
    public class UpdateUserDto
    {
        [DataMember]
        public Guid UserId { get; set; }
        [DataMember]
        public string UserName { get; set; }

        public override string ToString()
        {
            return string.Format("UpdateUserDto("
                                 + "UserId: " + UserId
                                 + ", UserName: " + UserName
                                 + ")");
        }
    }
}