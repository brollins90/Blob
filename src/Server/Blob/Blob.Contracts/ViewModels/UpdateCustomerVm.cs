using System;
using System.Runtime.Serialization;

namespace Blob.Contracts.ViewModels
{
    [DataContract]
    public class UpdateCustomerVm
    {
        [DataMember]
        public Guid CustomerId { get; set; }
        [DataMember]
        public string Name { get; set; }

        public override string ToString()
        {
            return string.Format("UpdateCustomerVm("
                                 + "CustomerId: " + CustomerId
                                 + ", Name: " + Name
                                 + ")");
        }
    }
}