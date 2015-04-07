using System;
using System.Collections.Generic;
using Blob.Contracts.Models;
using Blob.Contracts.Status;
using BMonitor.Common.Interfaces;
using BMonitor.Monitors.Default;

namespace BMonitor.Service
{
    public class MonitorManager
    {
        private readonly Guid _deviceId;
        private string _monitorPath;
        private readonly ICollection<IMonitor> _monitors;

        public MonitorManager(Guid deviceId, string monitorPath)
        {
            _deviceId = deviceId;
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
                StatusData statusData = monitor.Execute();
                Console.WriteLine(statusData.CurrentValue);

                statusData.DeviceId = _deviceId;
                statusData.TimeSent = DateTime.Now;

                Service<IStatusService>.Use(statusService =>
                {
                    statusService.SendStatusToServer(statusData);
                });
            }
        }
    }
}
