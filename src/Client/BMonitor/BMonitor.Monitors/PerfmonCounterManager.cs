using System;
using System.Collections.Concurrent;
using System.Diagnostics;

namespace BMonitor.Monitors
{
    public class PerfmonCounterKey
    {
        public string CategoryName { get; set; }
        public string CounterName { get; set; }
        public string InstanceName { get; set; }
        public string Key { get; private set; }

        public PerfmonCounterKey(string categoryName, string counterName, string instanceName = null)
        {
            CategoryName = categoryName;
            CounterName = counterName;
            InstanceName = instanceName;
            Key = string.Format("{0}_{1}_{2}", categoryName, counterName, instanceName);
        }
    }

    public class PerfmonCounterManager : IDisposable
    {
        private static ConcurrentDictionary<string, PerformanceCounter> _counters = new ConcurrentDictionary<string, PerformanceCounter>();

        public static PerfmonCounterManager Instance
        {
            get { return _instance ?? (_instance = new PerfmonCounterManager()); }
        }
        private static PerfmonCounterManager _instance;


        //public CounterSample NextSample(string categoryName, string counterName, string instanceName = null)
        //{
        //    PerformanceCounter counter = GetCounter(categoryName, counterName, instanceName);
        //    return counter.NextSample();
        //}

        public float NextValue(string categoryName, string counterName, string instanceName = null)
        {
            PerformanceCounter counter = GetCounter(categoryName, counterName, instanceName);
            return counter.NextValue();
        }

        //public long RawValue(string categoryName, string counterName, string instanceName = null)
        //{
        //    PerformanceCounter counter = GetCounter(categoryName, counterName, instanceName);
        //    return counter.RawValue;
        //}

        private PerformanceCounter GetCounter(string categoryName, string counterName, string instanceName)
        {
            PerfmonCounterKey key = new PerfmonCounterKey(categoryName, counterName, instanceName);
            if (!_counters.ContainsKey(key.Key))
            {

                PerformanceCounter counter = (string.IsNullOrEmpty(instanceName))
                                                 ? new PerformanceCounter(categoryName, counterName)
                                                 : new PerformanceCounter(categoryName, counterName, instanceName);

                _counters.TryAdd(key.Key, counter);
            }
            return _counters[key.Key];
        }

        public void Dispose()
        {
            foreach (var counter in _counters.Values)
            {
                counter.Dispose();
            }
            _counters = null;
        }
    }
}
