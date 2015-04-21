using Blob.Contracts.Models;
using Blob.Core.Data;
using Blob.Core.Domain;
using Blob.Managers.Extensions;
using log4net;
using System.Threading.Tasks;

namespace Blob.Managers.Status
{
    public class StatusManager : IStatusManager
    {
        private readonly ILog _log;
        private readonly IStatusRepository _statusRepository;

        public StatusManager(IStatusRepository statusRepository, ILog log)
        {
            _log = log;
            _log.Debug("Constructing StatusManager");
            _statusRepository = statusRepository;
        }

        public async Task StoreStatusData(StatusData statusData)
        {
            _log.Debug("Storing status data " + statusData);
            Device device = await _statusRepository.FindDeviceByIdAsync(statusData.DeviceId);

            await _statusRepository.AddStatusAsync(device, new Core.Domain.Status
                                                           {
                                                               AlertLevel = statusData.AlertLevel,
                                                               CurrentValue = statusData.CurrentValue,
                                                               DeviceId = device.Id,
                                                               MonitorDescription = statusData.MonitorDescription,
                                                               MonitorName = statusData.MonitorName,
                                                               TimeGenerated = statusData.TimeGenerated,
                                                               TimeSent = statusData.TimeSent
                                                           })
                                                           .ConfigureAwait(false);
        }

        public async Task StoreStatusPerformanceData(StatusPerformanceData statusPerformanceData)
        {
            _log.Debug("Storing status perf data " + statusPerformanceData);
            Device device = await _statusRepository.FindDeviceByIdAsync(statusPerformanceData.DeviceId);

            if (device != null)
            {
                foreach (PerformanceDataValue value in statusPerformanceData.Data)
                {
                    await _statusRepository.AddPerformanceAsync(device, new StatusPerf
                    {
                        Critical = value.Critical.ToNullableDecimal(),
                        DeviceId = device.Id,
                        Label = value.Label,
                        Max = value.Max.ToNullableDecimal(),
                        Min = value.Min.ToNullableDecimal(),
                        MonitorDescription = statusPerformanceData.MonitorDescription,
                        MonitorName = statusPerformanceData.MonitorName,
                        TimeGenerated = statusPerformanceData.TimeGenerated,
                        UnitOfMeasure = value.UnitOfMeasure,
                        Value = value.Value.ToDecimal(),
                        Warning = value.Warning.ToNullableDecimal()
                    })
                    .ConfigureAwait(false);
                }
            }
        }
    }
}
