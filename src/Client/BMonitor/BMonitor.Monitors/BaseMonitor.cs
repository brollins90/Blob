using System;
using BMonitor.Common;
using BMonitor.Common.Interfaces;
using BMonitor.Common.Models;

namespace BMonitor.Monitors
{
    public abstract class BaseMonitor : IMonitor, IDisposable
    {
        protected abstract string MonitorName { get; }
        protected abstract string MonitorDescription { get; }

        //protected virtual string CriticalTemplate { get; set; }
        //protected virtual string DescriptionTemplate { get; set; }
        //protected virtual string OkTemplate { get; set; }
        //protected virtual string PerformanceTemplate { get; set; }
        //protected virtual string UnknownTemplate { get; set; }
        //protected virtual string WarningTemplate { get; set; }

        protected virtual UoM Unit { get; set; }

        protected virtual Threshold MonitorThreshold { get; set; }

        public BaseMonitor()
        {
            //CriticalTemplate = "Critical: {0}";
            //DescriptionTemplate = "Description: {0}";
            //OkTemplate = "Ok: {0}";
            //PerformanceTemplate = "Performance: {0}";
            //UnknownTemplate = "Unknown: {0}";
            //WarningTemplate = "Warning: {0}";

            Unit = UoM.Percent;

            MonitorThreshold = new Threshold
                (
                    critical: new Range(10d, double.PositiveInfinity, true),
                    warning: new Range(80d, double.PositiveInfinity, true)
                );
        }

        public abstract ResultData Execute(bool collectPerfData = false);

        public void Dispose()
        {
        }
    }
}
