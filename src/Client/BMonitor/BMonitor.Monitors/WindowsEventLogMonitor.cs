//using BMonitor.Common.Models;

//namespace BMonitor.Monitors
//{
//    public class WindowsEventLogMonitor : BaseMonitor
//    {
//        protected override string MonitorId { get { return MonitorName + EventLogName; } }
//        protected override string MonitorName { get { return "WindowsEventLogMonitor"; } }
//        protected override string MonitorDescription { get { return "Checks for Windows event log errors"; } }

//        public string EventLogName { get; set; }

//        public WindowsEventLogMonitor() : this("all") { }
        
//        public WindowsEventLogMonitor(string eventLogName)
//        {
//            EventLogName = eventLogName;
//        }

//        public override ResultData Execute(bool collectPerfData = false)
//        {
//            return base.Execute();
//        }
//    }
//}
