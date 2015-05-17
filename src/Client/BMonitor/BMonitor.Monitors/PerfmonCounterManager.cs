using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace BMonitor.Monitors
{
    public class PerfmonCounterManager : IDisposable
    {
        // http://blogs.msdn.com/b/bclteam/archive/2006/06/02/618156.aspx
        // http://geekswithblogs.net/akraus1/archive/2009/05/27/132456.aspx
        private PerfmonCounterManager()
        {
            Counters = new Dictionary<string, PerformanceCounter>();

            GetCounter("Memory", "Available Bytes", string.Empty);
            GetCounter("Processor", "% Processor Time", "_Total");
            //PerformanceCounter _memoryCounter = new PerformanceCounter("Memory", "Available Bytes");
            //PerformanceCounter _processorCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
        }

        public static PerfmonCounterManager Instance
        {
            get { return _instance ?? (_instance = new PerfmonCounterManager()); }
        }
        private static PerfmonCounterManager _instance;

        public Dictionary<string, PerformanceCounter> Counters { get; private set; }

        internal PerformanceCounter GetCounter(string categoryName, string counterName, string instanceName)
        {
            string key = string.Format("{0}_{1}_{2}", categoryName, counterName, instanceName);
            if (Counters.ContainsKey(key))
            {
                return Counters[key];
            }

            PerformanceCounter counter = (string.IsNullOrEmpty(instanceName))
                                             ? new PerformanceCounter(categoryName, counterName)
                                             : new PerformanceCounter(categoryName, counterName, instanceName);

            if (counter == null) throw new ArgumentOutOfRangeException();

            Counters.Add(key, counter);
            return Counters[key];
        }

        public void Dispose()
        {
            foreach (var counter in Counters.Values)
            {
                counter.Dispose();
            }
            Counters = null;
        }
    }
}
