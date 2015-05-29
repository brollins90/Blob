using System;
using BMonitor.Common.Interfaces;
using BMonitor.Common.Models;

namespace BMonitor.Monitors
{
    public abstract class BaseMonitor : IMonitor, IDisposable
    {
        protected abstract string MonitorId { get; }
        protected abstract string MonitorName { get; }
        protected abstract string MonitorDescription { get; }

        protected virtual Threshold MonitorThreshold { get; set; }
        protected virtual UoM Unit { get; set; }
        protected virtual Range Warning { get; set; }
        protected virtual Range Critical { get; set; }
        
        public BaseMonitor()
        {
            Unit = UoM.Percent;

            MonitorThreshold = new Threshold
                (
                    critical: Critical,
                    warning: Warning
                );
        }

        public abstract ResultData Execute(bool collectPerfData = false);

        public void Dispose()
        {
        }
    }
}
