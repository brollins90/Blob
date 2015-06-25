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
        public string Key => $"{CategoryName}_{CounterName}_{InstanceName}";

        public PerfmonCounterKey(string categoryName, string counterName, string instanceName = null)
        {
            CategoryName = categoryName.ToLowerInvariant();
            CounterName = counterName.ToLowerInvariant();
            InstanceName = instanceName?.ToLowerInvariant() ?? string.Empty;
        }

        public override string ToString() => Key;
    }

    public class PerfmonCounterManager : IDisposable
    {
        private static ConcurrentDictionary<string, PerformanceCounter> _counters = new ConcurrentDictionary<string, PerformanceCounter>();

        public float NextValue(PerfmonCounterKey key)
        {
            PerformanceCounter counter = GetCounter(key);
            return counter.NextValue();
        }

        public float NextValue(string categoryName, string counterName, string instanceName = null)
        {
            PerformanceCounter counter = GetCounter(categoryName, counterName, instanceName);
            return counter.NextValue();
        }

        private PerformanceCounter GetCounter(string categoryName, string counterName, string instanceName)
        {
            PerfmonCounterKey key = new PerfmonCounterKey(categoryName, counterName, instanceName);
            return GetCounter(key);
        }

        private PerformanceCounter GetCounter(PerfmonCounterKey key)
        {
            if (!_counters.ContainsKey(key.Key))
            {

                PerformanceCounter counter = (string.IsNullOrEmpty(key.InstanceName))
                                                 ? new PerformanceCounter(key.CategoryName, key.CounterName)
                                                 : new PerformanceCounter(key.CategoryName, key.CounterName, key.InstanceName);

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
