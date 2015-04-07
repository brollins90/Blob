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
            _monitors.Add(new FreeDiskSpace(
                driveLetter: "C",
                driveDescription: "OS",
                unitOfMeasure: UnitOfMeasure.PERCENT,
                warningLevel: 20,
                criticalLevel: 10));
        }

        public void MonitorTick()
        {
            foreach (IMonitor monitor in _monitors)
            {
                MonitorResult result = monitor.Execute();
                StatusData statusData = new StatusData()
                             {
                                 AlertLevel = (int) result.AlertLevel,
                                 CurrentValue = result.CurrentValue,
                                 MonitorDescription = result.MonitorDescription,
                                 MonitorName = result.MonitorName,
                                 TimeGenerated = result.TimeGenerated,

                                 DeviceId = _deviceId,
                                 TimeSent = DateTime.Now
                             };

                Console.WriteLine(statusData.CurrentValue);

                Service<IStatusService>.Use(statusService =>
                {
                    statusService.SendStatusToServer(statusData);
                });
            }
        }
    }
}
