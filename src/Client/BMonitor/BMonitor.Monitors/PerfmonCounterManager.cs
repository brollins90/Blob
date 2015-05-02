using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMonitor.Monitors
{
    public class PerfmonCounterManager : IDisposable
    {
        // http://blogs.msdn.com/b/bclteam/archive/2006/06/02/618156.aspx
        // http://geekswithblogs.net/akraus1/archive/2009/05/27/132456.aspx
        private PerfmonCounterManager()
        {
            Counters = new Dictionary<string, PerformanceCounter>();

            GetCounter(new[] { "Memory", "Available Bytes" });
            GetCounter(new[] { "Processor", "% Processor Time", "_Total" });
            //PerformanceCounter _memoryCounter = new PerformanceCounter("Memory", "Available Bytes");
            //PerformanceCounter _processorCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
        }

        public static PerfmonCounterManager Instance
        {
            get { return _instance ?? (_instance = new PerfmonCounterManager()); }   
        }
        private static PerfmonCounterManager _instance;

        public Dictionary<string, PerformanceCounter> Counters { get; private set; }

        internal PerformanceCounter GetCounter(string[] strings)
        {
            string s = string.Join("_", strings);
            if (Counters.ContainsKey(s))
            {
                return Counters[s];
            }

            PerformanceCounter counter = (strings.Length == 2)
                                             ? new PerformanceCounter(strings[0], strings[1])
                                             : (strings.Length == 3)
                                                   ? new PerformanceCounter(strings[0], strings[1], strings[2])
                                                   : null;
            
            if (counter == null) throw new ArgumentOutOfRangeException();

            Counters.Add(s, counter);
            return Counters[s];
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
