using System.Security.Permissions;
using System.ServiceModel;
using System.Threading.Tasks;
using Blob.Contracts.Models;
using Blob.Contracts.Status;
using Blob.Managers.Status;
using log4net;

namespace Blob.Services.Status
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    //[PrincipalPermission(SecurityAction.Demand, Authenticated = true)]
    public class StatusService : IStatusService
    {
        private readonly ILog _log;
        private readonly IStatusManager _statusManager;

        public StatusService(IStatusManager statusManager, ILog log)
        {
            _log = log;
            _statusManager = statusManager;
        }

        [OperationBehavior]
        [PrincipalPermission(SecurityAction.Assert, Role = "Device")]
        public async Task SendStatusToServer(AddStatusRecordDto statusData)
        {
            _log.Debug("Server received status: " + statusData);
            await _statusManager.StoreStatusData(statusData).ConfigureAwait(false);
        }

        [OperationBehavior]
        [PrincipalPermission(SecurityAction.Assert, Role = "Device")]
        public async Task SendStatusPerformanceToServer(AddPerformanceRecordDto statusPerformanceData)
        {
            _log.Debug("Server received perf: " + statusPerformanceData);
            await _statusManager.StoreStatusPerformanceData(statusPerformanceData).ConfigureAwait(false);
        }
    }
}
