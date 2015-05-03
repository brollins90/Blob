using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Blob.Contracts.ViewModels
{
    [DataContract]
    public class DeviceUpdateVm
    {
        [DataMember]
        public Guid DeviceId { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string DeviceTypeId { get; set; }

        [DataMember]
        public IEnumerable<DeviceTypeSingleVm> AvailableTypes { get; set; }
    }
}