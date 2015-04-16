using BMonitor.Common.Interfaces;
using BMonitor.Monitors.Default;
using Blob.Contracts.Models;
using Blob.Contracts.Status;
using Blob.Proxies;
using System;
using System.Collections.Generic;
using System.Linq;
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
                MonitorResult result = monitor.Execute(collectPerfData: true);
                StatusData statusData = new StatusData()
                             {
                                 AlertLevel = (int) result.AlertLevel,
                                 CurrentValue = result.Value,
                                 DeviceId = _deviceId,
                                 MonitorDescription = result.MonitorDescription,
                                 MonitorName = result.MonitorName,
                                 TimeGenerated = result.TimeGenerated,
                                 TimeSent = DateTime.Now
                             };
                StatusPerformanceData spd = new StatusPerformanceData
                {
                    DeviceId = _deviceId,
                    MonitorDescription = result.MonitorDescription,
                    MonitorName = result.MonitorName,
                    TimeGenerated = result.TimeGenerated,
                    TimeSent = DateTime.Now
                };

                foreach (MonitorPerf perf in result.Perf)
                {
                    spd.AddPerformanceDataValue(new PerformanceDataValue
                    {
                        Critical = perf.Critical,
                        Label = perf.Label,
                        Max = perf.Max,
                        Min = perf.Min,
                        UnitOfMeasure = perf.UnitOfMeasure.ToString(),
                        Value = perf.Value,
                        Warning = perf.Warning
                    });
                }

                Console.WriteLine(statusData.CurrentValue + "|" + result.Perf.First().ToString());

                StatusClient statusClient = new StatusClient("StatusService");
                statusClient.ClientCredentials.UserName.UserName = "customerUser1";
                statusClient.ClientCredentials.UserName.Password = "password";
                statusClient.ClientCredentials.ServiceCertificate.Authentication.CertificateValidationMode = X509CertificateValidationMode.None;

                // send status
                Task.Run(() => statusClient.SendStatusToServer(statusData));

                // send perf
                Task.Run(() => statusClient.SendStatusPerformanceToServer(spd));
                
            }
        }
    }
}
