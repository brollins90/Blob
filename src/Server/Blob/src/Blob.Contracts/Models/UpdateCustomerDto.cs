﻿using System;
using System.Runtime.Serialization;

namespace Blob.Contracts.Models
{
    [DataContract]
    public class UpdateCustomerDto
    {
        [DataMember]
        public Guid CustomerId { get; set; }

        [DataMember]
        public string Name { get; set; }
    }
}
