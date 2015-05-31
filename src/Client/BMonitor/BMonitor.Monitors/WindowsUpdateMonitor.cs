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
            catch (Exception ex)
            {
                alertLevel = AlertLevel.CRITICAL;
            }

            //Type t = Type.GetTypeFromProgID("Microsoft.Update.Session", "remotehostname");
            //UpdateSession session = (UpdateSession)Activator.CreateInstance(t);
            //IUpdateSearcher updateSearcher = session.CreateUpdateSearcher();
            //int count = updateSearcher.GetTotalHistoryCount();
            //IUpdateHistoryEntryCollection history = updateSearcher.QueryHistory(0, count);
            //for (int i = 0; i < count; ++i)
            //{
            //    Console.WriteLine(string.Format("Title: {0}\tSupportURL: {1}\tDate: {2}\tResult Code: {3}\tDescription: {4}\r\n", history[i].Title, history[i].SupportUrl, history[i].Date, history[i].ResultCode, history[i].Description));
            //}

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
