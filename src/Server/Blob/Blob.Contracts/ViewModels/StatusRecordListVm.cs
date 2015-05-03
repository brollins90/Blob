using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Blob.Contracts.ViewModels
{
    [DataContract]
    public class StatusRecordListVm
    {
        [DataMember]
        public int PageNumber { get; set; }

        [DataMember]
        public int PageSize { get; set; }

        [DataMember]
        [Display(Name = "Id")]
        public long RecordId { get; set; }

        [DataMember]
        [Display(Name = "Status")]
        public int Status { get; set; }

        [DataMember]
        [Display(Name = "Monitor name")]
        public string MonitorName { get; set; }

        [DataMember]
        [Display(Name = "Monitor description")]
        public string MonitorDescription { get; set; }

        [DataMember]
        [Display(Name = "Time generated")]
        public DateTime TimeGenerated { get; set; }
    }
}
