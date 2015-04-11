using BMonitor.Common.Interfaces;
using BMonitor.Monitors.Default;
using Blob.Contracts.Models;
using Blob.Contracts.Status;
using Blob.Proxies;
using System;
using System.Collections.Generic;
using System.ServiceModel.Description;
using System.ServiceModel.Security;
using System.Threading.Tasks;

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

                StatusClient statusClient = new StatusClient("StatusService");
                statusClient.ClientCredentials.UserName.UserName = "TestUser1";
                statusClient.ClientCredentials.UserName.Password = "TestPassword1";
                statusClient.ClientCredentials.ServiceCertificate.Authentication.CertificateValidationMode = X509CertificateValidationMode.None;

                Task.Run(() => statusClient.SendStatusToServer(statusData));
            }
        }
    }
}
