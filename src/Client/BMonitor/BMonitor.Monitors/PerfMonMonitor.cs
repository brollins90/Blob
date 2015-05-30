using System;
using System.Collections.Generic;
using System.Diagnostics;
using BMonitor.Common.Models;

namespace BMonitor.Monitors
{
    public class PerfMonMonitor : BaseMonitor
    {
        protected override string MonitorId { get { return MonitorName + CounterKey; } }
        protected override string MonitorName { get { return "PerfMonMonitor"; } }
        protected override string MonitorDescription { get { return "Checks the status in the Windows Performance Monitor"; } }

        private static PerfmonCounterManager Manager { get { return PerfmonCounterManager.Instance; } }
        private PerformanceCounter Counter { get { return Manager.GetCounter(CategoryName, CounterName, InstanceName); } }

        public string CategoryName { get; set; }
        public string CounterName { get; set; }
        public string InstanceName { get; set; }
        private string CounterKey { get { return string.Format("{0}_{1}_{2}", CategoryName, CounterName, InstanceName); } }

        public override ResultData Execute(bool collectPerfData = false)
        {
            double executionValue = Counter.NextValue();
            AlertLevel alertLevel = base.CheckAlertLevel(executionValue);

            string currentValueString = string.Empty;
            switch (alertLevel)
            {
                case AlertLevel.CRITICAL:
                    currentValueString = string.Format("{0}: {1} ({2} {3} {4})",
                        CounterKey,
                        "CRITICAL",
                        executionValue,
                        Operation,
                        base.Critical);
                    break;
                case AlertLevel.OK:
                    currentValueString = string.Format("{0}: {1} ({2})",
                        CounterKey,
                        "OK",
                        executionValue);
                    break;
                case AlertLevel.UNKNOWN:
                    currentValueString = string.Format("UNKNOWN");
                    break;
                case AlertLevel.WARNING:
                    currentValueString = string.Format("{0}: {1} ({2} {3} {4})",
                        CounterKey,
                        "WARNING",
                        executionValue,
                        Operation,
                        base.Warning);
                    break;
            }

            ResultData result = new ResultData()
                                {
                                    AlertLevel = alertLevel,
                                    MonitorDescription = MonitorDescription,
                                    MonitorName = MonitorName,
                                    Perf = new List<PerformanceData>(),
                                    TimeGenerated = DateTime.Now,
                                    UnitOfMeasure = "",//Unit.ShortName,
                                    Value = currentValueString
                                };

            if (collectPerfData)
            {
                // perfdata for this monitor is always in GB
                //long crit = (MonitorThreshold.Critical.Percent)
                //                ? ((long)(MonitorThreshold.Critical.Limit * .01d * totalSize))
                //                : ((long)(MonitorThreshold.Critical.Limit));
                //long warn = (MonitorThreshold.Warning.Percent)
                //                ? ((long)(MonitorThreshold.Warning.Limit * .01d * totalSize))
                //                : ((long)(MonitorThreshold.Warning.Limit));

                // also we want to invert the numbers in the results
                PerformanceData perf = new PerformanceData
                                       {
                                           Critical = base.Critical.ToString(),
                                           Label = CounterKey,
                                           Max = "0",//.BytesToGb().ToString(), // the largest a %value can be (not required for %)
                                           Min = "0", // the smallest a %value can be (not required for %)
                                           UnitOfMeasure = "_",
                                           Value = executionValue.ToString(),
                                           Warning = base.Warning.ToString()
                                       };
                result.Perf.Add(perf);

            }
            return result;
        }
    }
}
