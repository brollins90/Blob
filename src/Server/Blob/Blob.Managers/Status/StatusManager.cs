using Blob.Contracts.Models;
using Blob.Core.Data;
using log4net;
using System.Threading.Tasks;

namespace Blob.Managers.Status
{
    public class StatusManager : IStatusManager
    {
        private readonly ILog _log;
        private readonly IRepositoryAsync<Core.Domain.Status> _statusRepository;
        private readonly IRepositoryAsync<Core.Domain.StatusPerf> _statusPerfRepository;

        public StatusManager(IRepositoryAsync<Core.Domain.Status> statusRepository, IRepositoryAsync<Core.Domain.StatusPerf> statusPerfRepository, ILog log)
        {
            _log = log;
            _statusRepository = statusRepository;
            _statusPerfRepository = statusPerfRepository;
        }

        public async Task StoreStatusData(StatusData statusData)
        {
            _log.Debug("Storing status data " + statusData);
            await _statusRepository.InsertAsync(new Core.Domain.Status()
                                     {
                                         CurrentValue = statusData.CurrentValue,
                                         DeviceId = statusData.DeviceId,
                                         MonitorDescription = statusData.MonitorDescription,
                                         MonitorName = statusData.MonitorName,
                                         TimeGenerated = statusData.TimeGenerated,
                                         TimeSent = statusData.TimeSent
                                     });
        }

        public async Task StoreStatusPerformanceData(StatusPerformanceData statusPerformanceData)
        {
            _log.Debug("Storing status perf data " + statusPerformanceData);

            foreach (PerformanceDataValue value in statusPerformanceData.Data)
            {
                await _statusPerfRepository.InsertAsync(new Core.Domain.StatusPerf()
                                                        {
                                                            Critical = value.Critical,
                                                            DeviceId = statusPerformanceData.DeviceId,
                                                            Label = value.Label,
                                                            Max = value.Max,
                                                            Min = value.Min,
                                                            MonitorDescription = statusPerformanceData.MonitorDescription,
                                                            MonitorName = statusPerformanceData.MonitorName,
                                                            TimeGenerated = statusPerformanceData.TimeGenerated,
                                                            TimeSent = statusPerformanceData.TimeSent,
                                                            UnitOfMeasure = value.UnitOfMeasure,
                                                            Value = value.Value,
                                                            Warning = value.Warning
                                                        });
            }
        }
    }
}
