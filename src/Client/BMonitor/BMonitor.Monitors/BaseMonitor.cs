using System;
using System.Collections.Generic;
using BMonitor.Common.Interfaces;
using BMonitor.Common.Models;

namespace BMonitor.Monitors
{
    public abstract class BaseMonitor : IMonitor, IDisposable
    {
        protected abstract string MonitorId { get; }
        protected abstract string MonitorName { get; }
        protected abstract string MonitorDescription { get; }

        public virtual EvaluationOperation Operation { get; set; }
        public virtual double Warning { get; set; }
        public virtual double Critical { get; set; }
        
        public virtual ResultData Execute(bool collectPerfData = false) { 
            return new ResultData
                   {
                       AlertLevel = AlertLevel.OK,
                       MonitorDescription = MonitorDescription,
                       MonitorId = MonitorId,
                       MonitorName = MonitorName,
                       Perf = new List<PerformanceData>(),
                       TimeGenerated = DateTime.UtcNow,
                       UnitOfMeasure = null,
                       Value = string.Empty
                   };
        }

        public AlertLevel CheckAlertLevel(double actual)
        {
            if (Operation.LimitBroken(Critical, actual))
            {
                return AlertLevel.CRITICAL;
            }
            else if (Operation.LimitBroken(Warning, actual))
            {
                return (AlertLevel.WARNING);
            }
            else
            {
                return AlertLevel.OK;
            }
        }

        public void Dispose()
        {
        }
    }
}
