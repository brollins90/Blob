using System;
using System.Collections.Generic;
using BMonitor.Common.Interfaces;
using BMonitor.Monitors.Default;

namespace BMonitor.Service
{
    public class MonitorManager
    {
        private string _monitorPath;
        private readonly ICollection<IMonitor> _monitors;

        public MonitorManager(string monitorPath)
        {
            _monitorPath = monitorPath;
            _monitors = new List<IMonitor>();

            // todo: maybe move the init, but then I have to remember to call it...
            Initialize();
        }

        public void Initialize()
        {
            _monitors.Add(new FreeDiskSpace());
        }

        public void MonitorTick()
        {
            foreach (IMonitor monitor in _monitors)
            {
                Console.WriteLine(monitor.Execute());
            }
        }
    }
}
