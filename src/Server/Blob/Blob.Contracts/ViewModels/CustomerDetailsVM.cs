using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Blob.Contracts.ViewModels
{
    [DataContract]
    public class CustomerDetailsVm
    {
        [DataMember]
        public Guid CustomerId { get; set; }
        
        [DataMember]
        public string Name { get; set; }
        
        [DataMember]
        public DateTime CreateDate { get; set; }
        
        [DataMember]
        public IEnumerable<UserListVm> Users { get; set; }

        [DataMember]
        public IEnumerable<DeviceListVm> Devices { get; set; }
    }
}
