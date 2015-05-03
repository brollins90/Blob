using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Blob.Contracts.ViewModels
{
    [DataContract]
    public class DeviceSingleVm
    {
        [DataMember]
        [Display(Name = "Id")]
        public Guid DeviceId { get; set; }

        [DataMember]
        [Display(Name = "Device name")]
        public string DeviceName { get; set; }

        [DataMember]
        [Display(Name = "Device type")]
        public string DeviceType { get; set; }

        [DataMember]
        [Display(Name = "Last activity")]
        public DateTime LastActivityDate { get; set; }

        [DataMember]
        [Display(Name = "Create date")]
        public DateTime CreateDate { get; set; }

        [DataMember]
        [Display(Name = "Enabled")]
        public bool Enabled { get; set; }

        [DataMember]
        [Display(Name = "Status")]
        public int Status { get; set; }
        
        [DataMember]
        public IEnumerable<StatusRecordSingleVm> StatusRecords { get; set; }

        [DataMember]
        public IEnumerable<PerformanceRecordListVm> PerformanceRecords { get; set; }
    }
}
