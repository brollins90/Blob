using System;

namespace BMonitor.Common.Interfaces
{
    public class MonitorResult
    {
        public AlertLevel AlertLevel { get; set; }
        public string CurrentValue { get; set; }
        public string MonitorDescription { get; set; }
        public string MonitorName { get; set; }
        public DateTime TimeGenerated { get; set; }
    }
}
