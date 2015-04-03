using Blob.Core.Data;
using log4net;

namespace Blob.Managers.Status
{
    public class StatusManager : IStatusManager
    {
        private readonly ILog _log;
        private readonly IRepository<Core.Domain.Status> _statusRepository;

        public StatusManager(IRepository<Core.Domain.Status> statusRepository, ILog log)
        {
            _log = log;
            _statusRepository = statusRepository;
        }

        public void StoreStatusData(Contracts.Models.StatusData statusData)
        {
            _log.Debug("Storing status data " + statusData);
            _statusRepository.Insert(new Core.Domain.Status()
                                     {
                                         // todo: Right now the objects are identical.  I will change this later
                                         CurrentValue = statusData.CurrentValue,
                                         DeviceId = statusData.DeviceId,
                                         MonitorDescription = statusData.MonitorDescription,
                                         MonitorName = statusData.MonitorName,
                                         PreviousValue = statusData.PreviousValue,
                                         TimeGenerated = statusData.TimeGenerated,
                                         TimeSent = statusData.TimeSent
                                     });
        }
    }
}
