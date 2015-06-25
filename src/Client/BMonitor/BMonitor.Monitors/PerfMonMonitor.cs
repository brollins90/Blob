using System;
using System.Collections.Generic;
using BMonitor.Common.Models;

namespace BMonitor.Monitors
{
    public class PerfMonMonitor : BaseMonitor
    {
        protected override string MonitorId => MonitorName + _counterKey;
        protected override string MonitorName => "PerfMonMonitor";
        protected override string MonitorDescription => "Checks the status in the Windows Performance Monitor";
        protected override string MonitorLabel => Label ?? MonitorName;

        public string Label { get; set; }
        public string CategoryName { get; set; }
        public string CounterName { get; set; }
        public string InstanceName { get; set; }

        private PerfmonCounterKey _counterKey;
        private PerfmonCounterManager _manager;

        public PerfMonMonitor(PerfmonCounterManager manager)
        {
            _manager = manager;
        }

        public override ResultData Execute(bool collectPerfData = false)
        {
            _counterKey = new PerfmonCounterKey(categoryName: CategoryName, counterName: CounterName, instanceName: InstanceName);

            double executionValue = _manager.NextValue(_counterKey);
            AlertLevel alertLevel = CheckAlertLevel(executionValue);

            string currentValueString = string.Empty;
            switch (alertLevel)
            {
                case AlertLevel.CRITICAL:
                    currentValueString = $"{_counterKey}: CRITICAL ({executionValue} {Operation} {Critical})";
                    break;
                case AlertLevel.OK:
                    currentValueString = $"{_counterKey}: OK ({executionValue})";
                    break;
                case AlertLevel.UNKNOWN:
                    currentValueString = "UNKNOWN";
                    break;
                case AlertLevel.WARNING:
                    currentValueString = $"{_counterKey}: WARNING ({executionValue} {Operation} {Warning})";
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
                    Label = _counterKey.ToString(),
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
