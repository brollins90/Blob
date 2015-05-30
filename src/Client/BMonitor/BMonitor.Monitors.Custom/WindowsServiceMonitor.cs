using BMonitor.Common.Models;

namespace BMonitor.Monitors.Custom
{
    public class WindowsServiceMonitor : BaseMonitor
    {
        protected override string MonitorId { get { return MonitorName + ServiceName; } }
        protected override string MonitorName { get { return "WindowsServiceMonitor"; } }
        protected override string MonitorDescription { get { return "Checks the windows service status"; } }

        public string ServiceName { get; set; }
        
        public WindowsServiceMonitor() : this("BMonitor") { }

        public WindowsServiceMonitor(string serviceName)
        {
            ServiceName = serviceName;
        }

        public override ResultData Execute(bool collectPerfData = false)
        {
            return base.Execute();
        }
    }
}
