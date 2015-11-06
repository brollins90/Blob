using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Blob.Contracts.Models.ViewModels
{
    [DataContract]
    public class MonitorListListItemVm
    {
        [DataMember]
        [Display(Name = "Monitor id")]
        public string MonitorId { get; set; }

        [DataMember]
        [Display(Name = "Monitor name")]
        [Required]
        public string MonitorName { get; set; }

        [DataMember]
        [Display(Name = "Monitor label")]
        [Required]
        public string MonitorLabel { get; set; }

        [DataMember]
        [Display(Name = "Monitor description")]
        [Required]
        public string MonitorDescription { get; set; }

        [DataMember]
        [Display(Name = "Current")]
        [Required]
        public string CurrentValue { get; set; }

        [DataMember]
        [Display(Name = "Status")]
        [Required]
        public int Status { get; set; }

        [DataMember]
        [Display(Name = "Time generated")]
        [Required]
        public DateTime TimeGenerated { get; set; }
    }
}
