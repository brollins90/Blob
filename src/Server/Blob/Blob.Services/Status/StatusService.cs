using Blob.Contracts.Models;
using Blob.Contracts.Status;
using Blob.Managers.Status;
using log4net;

namespace Blob.Services.Status
{
    public class StatusService : IStatusService
    {
        private readonly ILog _log;
        private readonly IStatusManager _statusManager;

        public StatusService(IStatusManager statusManager, ILog log)
        {
            _log = log;
            _statusManager = statusManager;
        }

        public void SendStatusToServer(StatusData statusData)
        {
            _log.Debug("Server received status: " + statusData);
            _statusManager.StoreStatusData(statusData);
        }

        public void SendStatusPerformanceToServer(StatusPerformanceData statusPerformanceData)
        {
            _log.Debug("Server received perf: " + statusPerformanceData);
            _statusManager.StoreStatusPerformanceData(statusPerformanceData);
        }
    }
}
