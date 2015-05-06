using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Blob.Contracts.Dto.ViewModels
{
    [DataContract]
    public class CustomerSingleVm
    {
        [DataMember]
        [Display(Name = "Id")]
        [Required]
        public Guid CustomerId { get; set; }

        [DataMember]
        [Required]
        public DateTime CreateDate { get; set; }

        [DataMember]
        [Required]
        public bool Enabled { get; set; }
        
        [DataMember]
        [Display(Name = "Customer name")]
        [Required]
        public string Name { get; set; }
        
        [DataMember]
        public IEnumerable<UserListItemVm> Users { get; set; }

        [DataMember]
        public IEnumerable<DeviceListItemVm> Devices { get; set; }
    }
}
