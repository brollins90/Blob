﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Blob.Contracts.ViewModels
{
    [DataContract]
    public class PerformanceRecordListVm
    {
        [DataMember]
        public int PageNumber { get; set; }

        [DataMember]
        public int PageSize { get; set; }

        [DataMember]
        [Display(Name = "Id")]
        public long RecordId { get; set; }

        [DataMember]
        [Display(Name = "Monitor name")]
        public string MonitorName { get; set; }

        [DataMember]
        [Display(Name = "Monitor description")]
        public string MonitorDescription { get; set; }

        [DataMember]
        [Display(Name = "Time generated")]
        public DateTime TimeGenerated { get; set; }
        
        [DataMember]
        public string Unit { get; set; }
        
        [DataMember]
        public string Label { get; set; }
        
        [DataMember]
        public string Value { get; set; }
        
        [DataMember]
        public string Critical { get; set; }
        
        [DataMember]
        public string Max { get; set; }

        [DataMember]
        public string Min { get; set; }

        [DataMember]
        public string Warning { get; set; }
    }
}
