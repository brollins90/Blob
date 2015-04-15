using System.Security.Permissions;
using Blob.Contracts.Models;
using Blob.Contracts.Status;
using Blob.Managers.Status;
using log4net;
using System.ServiceModel;
using System.Threading.Tasks;

namespace Blob.Services.Status
{
    [ServiceBehavior(InstanceContextMode=InstanceContextMode.PerCall)]
    public class StatusService : IStatusService
    {
        private readonly ILog _log;
        private readonly IStatusManager _statusManager;

        public StatusService(IStatusManager statusManager, ILog log)
        {
            _log = log;
            _statusManager = statusManager;
        }

        [PrincipalPermission(SecurityAction.Assert, Authenticated=true, Role="Device")]
        public async Task SendStatusToServer(StatusData statusData)
        {
            _log.Debug("Server received status: " + statusData);
            await _statusManager.StoreStatusData(statusData).ConfigureAwait(false);
        }

        [PrincipalPermission(SecurityAction.Assert, Authenticated = true, Role = "Device")]
        public async Task SendStatusPerformanceToServer(StatusPerformanceData statusPerformanceData)
        {
            _log.Debug("Server received perf: " + statusPerformanceData);
            await _statusManager.StoreStatusPerformanceData(statusPerformanceData).ConfigureAwait(false);
        }
    }
}
