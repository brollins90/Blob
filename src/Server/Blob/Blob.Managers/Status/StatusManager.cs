using Blob.Core.Data;
using log4net;

namespace Blob.Managers.Status
{
    public class StatusManager : IStatusManager
    {
        private readonly ILog _log;
        private readonly IRepositoryAsync<Core.Domain.Status> _statusRepository;

        public StatusManager(IRepositoryAsync<Core.Domain.Status> statusRepository, ILog log)
        {
            _log = log;
            _statusRepository = statusRepository;
        }

        public async void StoreStatusData(Contracts.Models.StatusData statusData)
        {
            _log.Debug("Storing status data " + statusData);
            await _statusRepository.InsertAsync(new Core.Domain.Status()
                                     {
                                         // todo: Right now the objects are identical.  I will change this later
                                         CurrentValue = statusData.CurrentValue,
                                         DeviceId = statusData.DeviceId,
                                         MonitorDescription = statusData.MonitorDescription,
                                         MonitorName = statusData.MonitorName,
                                         TimeGenerated = statusData.TimeGenerated,
                                         TimeSent = statusData.TimeSent
                                     });
        }
    }
}
