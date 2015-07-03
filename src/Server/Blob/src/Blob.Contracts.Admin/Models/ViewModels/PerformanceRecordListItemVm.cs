using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Blob.Contracts.Models.ViewModels
{
    [DataContract]
    public class PerformanceRecordListItemVm
    {
        [DataMember]
        [Display(Name = "Id")]
        [Required]
        public long RecordId { get; set; }

        [DataMember]
        public string Critical { get; set; }

        [DataMember]
        [Required]
        public string Label { get; set; }

        [DataMember]
        public string Max { get; set; }

        [DataMember]
        public string Min { get; set; }

        [DataMember]
        [Display(Name = "Monitor name")]
        [Required]
        public string MonitorName { get; set; }

        [DataMember]
        [Display(Name = "Monitor description")]
        [Required]
        public string MonitorDescription { get; set; }

        [DataMember]
        [Display(Name = "Time generated")]
        [Required]
        public DateTime TimeGenerated { get; set; }
        
        [DataMember]
        public string Unit { get; set; }
        
        [DataMember]
        public string Value { get; set; }

        [DataMember]
        public string Warning { get; set; }
    }
}
