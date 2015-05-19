﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Blob.Contracts.Models.ViewModels
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
        [Display(Name = "Device count")]
        [Required]
        public int DeviceCount { get; set; }

        [DataMember]
        [Display(Name = "User count")]
        [Required]
        public int UserCount { get; set; }
    }
}
