using System;
using BMonitor.Common.Models;

namespace BMonitor.Monitors.Custom
{
    public class PingMonitor : BaseMonitor
    {
        protected override string MonitorId { get { return MonitorName + RemoteDevice; } }
        protected override string MonitorName { get { return "PingMonitor"; } }
        protected override string MonitorDescription { get { return "Checks if the service can ping the remote device"; } }

        public string RemoteDevice { get; set; }

        public PingMonitor() : this("blobservice.rritc.com") { }

        public PingMonitor(string remoteDevice)
        {
            RemoteDevice = remoteDevice;
        }

        public override ResultData Execute(bool collectPerfData = false)
        {
            return new ResultData
                   {
                       AlertLevel = 0,
                       MonitorDescription = MonitorDescription,
                       MonitorId = MonitorId,
                       MonitorName = MonitorName,
                       Perf = null,
                       TimeGenerated = DateTime.UtcNow,
                       UnitOfMeasure = null,
                       Value = string.Empty
                   };
        }
    }
}
