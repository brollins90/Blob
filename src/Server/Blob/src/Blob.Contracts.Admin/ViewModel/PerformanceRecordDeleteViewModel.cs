namespace Blob.Contracts.ViewModel
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.Serialization;
    using Request;

    [DataContract]
    public class PerformanceRecordDeleteViewModel
    {
        [DataMember]
        [Display(Name = "Id")]
        [Required]
        public long RecordId { get; set; }

        [DataMember]
        [Display(Name = "Device name")]
        [Required]
        public string DeviceName { get; set; }

        [DataMember]
        [Display(Name = "Monitor name")]
        [Required]
        public string MonitorName { get; set; }

        [DataMember]
        [Display(Name = "Time generated")]
        [Required]
        public DateTime TimeGenerated { get; set; }

        public DeletePerformanceRecordRequest ToRequest()
        {
            return new DeletePerformanceRecordRequest { RecordId = RecordId };
        }
    }
}