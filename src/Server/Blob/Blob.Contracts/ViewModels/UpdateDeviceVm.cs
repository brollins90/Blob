using System;
using System.Runtime.Serialization;

namespace Blob.Contracts.ViewModels
{
    [DataContract]
    public class UpdateDeviceVm
    {
        [DataMember]
        public Guid DeviceId { get; set; }
        [DataMember]
        public string Name { get; set; }

        public override string ToString()
        {
            return string.Format("UpdateDeviceVm("
                                 + "DeviceId: " + DeviceId
                                 + ", Name: " + Name
                                 + ")");
        }
    }
}