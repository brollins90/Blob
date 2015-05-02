using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Before.ViewModels
{
    public class DeviceViewModel
    {
        [Display(Name = "Current status")]
        public StatusValue CurrentStatus { get; set; }

        public string CssClass
        {
            get
            {
                return (CurrentStatus == StatusValue.Critical)
                           ? "status-critical"
                           : (CurrentStatus == StatusValue.Information)
                                 ? "status-information"
                                 : (CurrentStatus == StatusValue.Ok)
                                       ? "status-ok"
                                       : (CurrentStatus == StatusValue.Unknown)
                                             ? "status-unknown"
                                             : "status-warning";
            }
        }

        public Guid Id { get; set; }

        [Display(Name = "Device name")]
        public string DeviceName { get; set; }

        [Display(Name = "Last activity")]
        public DateTime LastActivityDate { get; set; }

        [Display(Name = "Device type")]
        public string DeviceType { get; set; }
        public virtual ICollection<StatusRecordViewModel> StatusRecords { get; set; }
        public virtual ICollection<PerformanceRecordViewModel> PerformanceRecords { get; set; }
    }
}
