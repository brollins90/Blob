﻿using System;
using System.Runtime.Serialization;

namespace Blob.Contracts.Dto
{
    public class RegisterDeviceResponseDto
    {
        [DataMember]
        public string DeviceId { get; set; }

        [DataMember]
        public DateTime TimeSent { get; set; }
    }
}
