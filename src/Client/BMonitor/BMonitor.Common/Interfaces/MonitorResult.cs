using System;
using System.Collections.Generic;

namespace BMonitor.Common.Interfaces
{
    public class MonitorResult
    {
        public AlertLevel AlertLevel { get; set; }
        public string MonitorDescription { get; set; }
        public string MonitorName { get; set; }
        public DateTime TimeGenerated { get; set; }
        public UnitOfMeasure UnitOfMeasure { get; set; }
        public string Value { get; set; }

        private ICollection<MonitorPerf> _perf;
        public ICollection<MonitorPerf> Perf
        {
            get { return _perf ?? (_perf = new List<MonitorPerf>()); }
            set { _perf = value; }
        }
    }
}
