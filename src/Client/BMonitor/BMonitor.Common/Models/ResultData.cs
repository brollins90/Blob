using System;
using System.Collections.Generic;

namespace BMonitor.Common.Models
{
    public class ResultData
    {
        public AlertLevel AlertLevel { get; set; }
        public string MonitorDescription { get; set; }
        public string MonitorId { get; set; }
        public string MonitorName { get; set; }
        public string MonitorLabel { get; set; }
        public DateTime TimeGenerated { get; set; }
        public string UnitOfMeasure { get; set; }
        public string Value { get; set; }

        private ICollection<PerformanceData> _perf;
        public ICollection<PerformanceData> Perf
        {
            get { return _perf ?? (_perf = new List<PerformanceData>()); }
            set { _perf = value; }
        }
    }
}
