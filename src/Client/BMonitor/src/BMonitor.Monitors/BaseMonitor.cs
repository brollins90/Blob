using System;
using System.Collections.Generic;
using BMonitor.Common.Interfaces;
using BMonitor.Common.Models;
using BMonitor.Common.Operations;

namespace BMonitor.Monitors
{
    public abstract class BaseMonitor : IMonitor, IDisposable
    {
        /// <summary>
        /// Uniquely identifies the instance of the monitor on the monitored computer
        /// </summary>
        protected abstract string MonitorId { get; }

        /// <summary>
        /// Generic name of the monitor
        /// </summary>
        protected abstract string MonitorName { get; }

        /// <summary>
        /// Generic purpose for using the monitor
        /// </summary>
        protected abstract string MonitorDescription { get; }

        /// <summary>
        /// Text that describes this instance of the monitor
        /// </summary>
        protected abstract string MonitorLabel { get; }


        /// <summary>
        /// The operation that defines the alert value
        /// </summary>
        public virtual EvaluationOperation Operation { get; set; } = EvaluationOperation.LessThan;

        /// <summary>
        /// The warning threshold
        /// </summary>
        public virtual double Warning { get; set; } = 20d;

        /// <summary>
        /// The critical threshold
        /// </summary>
        public virtual double Critical { get; set; } = 10d;


        public virtual ResultData Execute(bool collectPerfData = false)
        {
            return new ResultData
                   {
                       AlertLevel = AlertLevel.OK,
                       MonitorDescription = MonitorDescription,
                       MonitorId = MonitorId,
                       MonitorName = MonitorName,
                       MonitorLabel = MonitorLabel,
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
