using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Blob.Contracts.Dto.ViewModels
{
    [DataContract]
    public class StatusRecordSingleVm
    {
        [DataMember]
        [Display(Name = "Id")]
        [Required]
        public long RecordId { get; set; }

        [DataMember]
        [Display(Name = "Monitor name")]
        [Required]
        public string MonitorName { get; set; }

        [DataMember]
        [Display(Name = "Monitor description")]
        [Required]
        public string MonitorDescription { get; set; }

        [DataMember]
        [Display(Name = "Status")]
        [Required]
        public int Status { get; set; }

        [DataMember]
        [Display(Name = "Time generated")]
        [Required]
        public DateTime TimeGenerated { get; set; }

        [DataMember]
        public IEnumerable<PerformanceRecordListItemVm> PerformanceRecords { get; set; }
    }
}
