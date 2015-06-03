using System;
using System.Collections.Generic;
using BMonitor.Common.Models;
using WUApiLib;

namespace BMonitor.Monitors
{
    public class WindowsUpdateMonitor : BaseMonitor
    {
        protected override string MonitorId { get { return MonitorName; } }
        protected override string MonitorName { get { return "WindowsUpdateMonitor"; } }
        protected override string MonitorDescription { get { return "Checks the status of Windows Updates"; } }
        protected override string MonitorLabel { get { return Label ?? MonitorName; } }
        public string Label { get; set; }

        public override ResultData Execute(bool collectPerfData = false)
        {
            // http://blogs.technet.com/b/heyscriptingguy/archive/2009/03/11/how-can-i-search-for-download-and-install-an-update.aspx

            // https://msdn.microsoft.com/en-us/library/aa387102%28v=vs.85%29.aspx
            // search criteria  https://msdn.microsoft.com/en-us/library/windows/desktop/aa386526%28v=vs.85%29.aspx

            int neededUpdateCount = 0;
            AlertLevel alertLevel = AlertLevel.UNKNOWN;
            try{
                // Find updates for this computer
                IUpdateSession3 uSession = new UpdateSession();
                IUpdateSearcher uSearcher = uSession.CreateUpdateSearcher();
                ISearchResult uResult = uSearcher.Search("IsInstalled=0 and IsHidden=0");

                neededUpdateCount = uResult.Updates.Count;
                alertLevel = base.CheckAlertLevel(neededUpdateCount);
            }
            catch (Exception)
            {
                alertLevel = AlertLevel.CRITICAL;
            }

            string currentValueString = string.Empty;
            switch (alertLevel)
            {
                case AlertLevel.CRITICAL:
                    currentValueString = string.Format("{0}: {1} needed windows updates.",
                        "CRITICAL",
                        neededUpdateCount.ToString());
                    break;
                case AlertLevel.OK:
                    currentValueString = string.Format("{0}: {1} needed windows updates",
                        "OK",
                        neededUpdateCount.ToString());
                    break;
                case AlertLevel.UNKNOWN:
                    currentValueString = string.Format("UNKNOWN");
                    break;
                case AlertLevel.WARNING:
                    currentValueString = string.Format("{0}: {1} needed windows updates.",
                        "WARNING",
                        neededUpdateCount.ToString());
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
    }
}
