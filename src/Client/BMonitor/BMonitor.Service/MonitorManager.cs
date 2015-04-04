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
        private Guid _deviceId;
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
                string currentStatus = monitor.Execute();
                Console.WriteLine(currentStatus);

                StatusData statusData = new StatusData()
                                        {
                                            CurrentValue = currentStatus,
                                            DeviceId = _deviceId,
                                            MonitorDescription = "Free Disk Space: ",
                                            MonitorName = "FreeDiskSpace",
                                            TimeGenerated = DateTime.Now,
                                            TimeSent = DateTime.Now
                                        };

                Service<IStatusService>.Use(statusService =>
                {
                    statusService.SendStatusToServer(statusData);
                });
            }
        }
    }
}
