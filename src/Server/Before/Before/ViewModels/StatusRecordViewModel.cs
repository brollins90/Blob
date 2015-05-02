using System;
using System.ComponentModel.DataAnnotations;

namespace Before.ViewModels
{
    public class StatusRecordViewModel
    {
        [Display(Name = "Status")]
        public StatusValue Status { get; set; }

        public long Id { get; set; }

        [Display(Name = "Monitor name")]
        public string MonitorName { get; set; }

        [Display(Name = "Monitor description")]
        public string MonitorDescription { get; set; }

        public DateTime TimeGenerated { get; set; }
        public DateTime TimeSent { get; set; }
    }
}
