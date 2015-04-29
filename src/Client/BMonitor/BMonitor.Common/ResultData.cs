using System;
using System.Collections.Generic;

namespace BMonitor.Common
{

    public class ExecutionResult
    {
        public dynamic Value { get; set; }
        public Dictionary<string, dynamic> Other { get; set; }

        public ExecutionResult()
        {
            Other = new Dictionary<string, dynamic>();
        }
    }

    public class ResultData
    {
        public AlertLevel AlertLevel { get; set; }
        public string MonitorDescription { get; set; }
        public string MonitorName { get; set; }
        public DateTime TimeGenerated { get; set; }
        public string UnitOfMeasure { get; set; }
        public string Value { get; set; }

        private ICollection<PerformanceData> _perf;
        public ICollection<PerformanceData> Perf
        {
            get { return _perf ?? (_perf = new List<PerformanceData>()); }
            set { _perf = value; }
        }
    }
    ////public class MonitorResult : MonitorResult<string> { }

    //public class MonitorResult<TUnit, TValue>
    //    where TUnit : UnitOfMeasure
    //{
    //    public AlertLevel AlertLevel { get; set; }
    //    public string MonitorDescription { get; set; }
    //    public string MonitorName { get; set; }
    //    public DateTime TimeGenerated { get; set; }
    //    public TUnit UnitOfMeasure { get; set; }
    //    public TValue Value { get; set; }

    //    private ICollection<MonitorPerf<TUnit, TValue>> _perf;
    //    public ICollection<MonitorPerf<TUnit, TValue>> Perf
    //    {
    //        get { return _perf ?? (_perf = new List<MonitorPerf<TUnit, TValue>>()); }
    //        set { _perf = value; }
    //    }
    //}
}
