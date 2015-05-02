using System;
using System.ComponentModel.DataAnnotations;

namespace Before.ViewModels
{
    public class PerformanceRecordViewModel
    {
        public long Id { get; set; }

        [Display(Name = "Monitor name")]
        public string MonitorName { get; set; }

        [Display(Name = "Monitor description")]
        public string MonitorDescription { get; set; }

        public DateTime TimeGenerated { get; set; }
        public string Unit { get; set; }
        public string Label { get; set; }
        public string Value { get; set; }
        public string Critical { get; set; }
        public string Max { get; set; }
        public string Min { get; set; }
        public string Warning { get; set; }
    }
}
