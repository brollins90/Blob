﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Blob.Contracts.Models.ViewModels
{
    [DataContract]
    public class DashCurrentConnectionsListItemVm
    {
        [DataMember]
        [Display(Name = "Customer id")]
        [Required]
        public Guid CustomerId { get; set; }

        [DataMember]
        [Display(Name = "Customer name")]
        [Required]
        public string CustomerName { get; set; }

        [DataMember]
        [Display(Name = "Device id")]
        [Required]
        public Guid DeviceId { get; set; }

        [DataMember]
        [Display(Name = "Device name")]
        [Required]
        public string DeviceName { get; set; }

        [DataMember]
        [Display(Name = "Status")]
        [Required]
        public int Status { get; set; }

        [DataMember]
        [Display(Name = "Available commands")]
        public IEnumerable<DeviceCommandVm> AvailableCommands { get; set; }
    }
}
