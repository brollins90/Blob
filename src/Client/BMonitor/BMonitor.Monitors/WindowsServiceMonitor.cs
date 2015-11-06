using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using BMonitor.Common.Models;

namespace BMonitor.Monitors
{
    public class WindowsServiceMonitor : BaseMonitor
    {
        protected override string MonitorId { get { return MonitorName + ServiceName; } }
        protected override string MonitorName { get { return "WindowsServiceMonitor"; } }
        protected override string MonitorDescription { get { return "Checks the windows service status"; } }
        protected override string MonitorLabel { get { return Label ?? MonitorName; } }

        public string Label { get; set; }
        public string ServiceName { get; set; }

        public WindowsServiceMonitor() : this("BMonitor") { }

        public WindowsServiceMonitor(string serviceName)
        {
            ServiceName = serviceName;
        }

        public override ResultData Execute(bool collectPerfData = false)
        {
            var ctl = ServiceController.GetServices().FirstOrDefault(s => s.ServiceName.Equals(ServiceName));
            if (ctl == null) throw new Exception(string.Format("Service with name [{0}] was not found", ServiceName));

            ServiceControllerStatus executionValue = ctl.Status;
            AlertLevel alertLevel = CheckAlertLevelServiceStatus(executionValue);

            string currentValueString = string.Empty;
            switch (alertLevel)
            {
                case AlertLevel.CRITICAL:
                    currentValueString = string.Format("{1}: {2} ({2})",
                        ServiceName,
                        "CRITICAL",
                        executionValue.ToString());
                    break;
                case AlertLevel.OK:
                    currentValueString = string.Format("{0}: {1} ({2})",
                        ServiceName,
                        "OK",
                        executionValue.ToString());
                    break;
                case AlertLevel.UNKNOWN:
                    currentValueString = string.Format("UNKNOWN");
                    break;
                case AlertLevel.WARNING:
                    currentValueString = string.Format("{0}: {1} ({2} {3} {4})",
                        ServiceName,
                        "WARNING",
                        executionValue.ToString(),
                        Operation,
                        base.Warning);
                    break;
            }

            ResultData result = new ResultData()
            {
                AlertLevel = alertLevel,
                MonitorDescription = MonitorDescription,
                MonitorId = MonitorId,
                MonitorLabel = MonitorLabel,
                MonitorName = MonitorName,
                Perf = new List<PerformanceData>(),
                TimeGenerated = DateTime.Now,
                UnitOfMeasure = "",//Unit.ShortName,
                Value = currentValueString
            };
            // todo: perf
            return result;
        }

        private AlertLevel CheckAlertLevelServiceStatus(ServiceControllerStatus executionValue)
        {
            switch (executionValue)
            {
                case ServiceControllerStatus.Running:
                    return AlertLevel.OK;
                //case ServiceControllerStatus.ContinuePending:
                //case ServiceControllerStatus.Paused:
                //case ServiceControllerStatus.PausePending:
                //case ServiceControllerStatus.StartPending:
                //case ServiceControllerStatus.Stopped:
                //case ServiceControllerStatus.StopPending:
                default:
                    return AlertLevel.CRITICAL;
            }
        }
    }
}
