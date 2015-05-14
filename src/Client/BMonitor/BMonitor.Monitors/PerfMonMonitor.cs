using System;
using System.Collections.Generic;
using System.Diagnostics;
using BMonitor.Common.Models;

namespace BMonitor.Monitors
{
    public class PerfMonMonitor : BaseMonitor
    {
        private readonly PerfmonCounterManager _manager;
        private PerformanceCounter _counter;
        private string _counterKey;

        public PerfMonMonitor(params string[] strings)
        {
            _counterKey = string.Join("_", strings);
            _manager = PerfmonCounterManager.Instance;
            _counter = _manager.GetCounter(strings);
        }

        protected override string MonitorName { get { return "PerfMonMonitor"; } }
        protected override string MonitorDescription { get { return "PerfMonMonitor Description"; } }


        public override ResultData Execute(bool collectPerfData = false)
        {

            double executionValue = _counter.NextValue();

            //string driveLetter = string.Format("{0}:", _driveLetter.ToUpper());
            //string driveName;
            //string driveLabel;
            //long totalFreeSpace;
            //long totalSize;
            //double freePercent;
            ////double executionValue;

            //DriveInfo driveInfo = new DriveInfo(driveLetter);
            //driveName = driveInfo.Name;
            //driveLabel = driveInfo.VolumeLabel;
            //totalFreeSpace = driveInfo.TotalFreeSpace;
            //totalSize = driveInfo.TotalSize;
            //freePercent = Math.Round(((double)totalFreeSpace / (double)totalSize) * 100);

            ////executionValue = 7.0d;//freePercent;


            AlertLevel alertLevel = MonitorThreshold.CheckAlertLevel(executionValue);

            string currentValueString = string.Empty;
            switch (alertLevel)
            {
                case AlertLevel.CRITICAL:
                    currentValueString = string.Format("{0}: {1} ({2} < {3})",
                        _counterKey,
                        "CRITICAL",
                        executionValue,
                        MonitorThreshold.Critical.Limit);
                    break;
                case AlertLevel.OK:
                    currentValueString = string.Format("{0}: {1} ({2})",
                        _counterKey,
                        "OK",
                        executionValue);
                    break;
                case AlertLevel.UNKNOWN:
                    currentValueString = string.Format("UNKNOWN");
                    break;
                case AlertLevel.WARNING:
                    currentValueString = string.Format("{0}: {1} ({2} < {3})",
                        _counterKey,
                        "WARNING",
                        executionValue,
                        MonitorThreshold.Warning.Limit);
                    break;
            }

            ResultData result = new ResultData()
                                {
                                    AlertLevel = alertLevel,
                                    MonitorDescription = MonitorDescription,
                                    MonitorName = MonitorName,
                                    Perf = new List<PerformanceData>(),
                                    TimeGenerated = DateTime.Now,
                                    UnitOfMeasure = Unit.ShortName,
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
                                           Critical = MonitorThreshold.Critical.Limit.ToString(),
                                           Label = _counterKey,
                                           Max = "0",//.BytesToGb().ToString(), // the largest a %value can be (not required for %)
                                           Min = "0", // the smallest a %value can be (not required for %)
                                           UnitOfMeasure = "_",
                                           Value = executionValue.ToString(),
                                           Warning = MonitorThreshold.Warning.Limit.ToString()
                                       };
                result.Perf.Add(perf);

            }
            return result;
        }
    }
}
