using System.Security.Permissions;
using System.ServiceModel;
using System.Threading.Tasks;
using Blob.Contracts.Blob;
using Blob.Contracts.Device;
using Blob.Contracts.Dto;
using log4net;

namespace Blob.Services.Device
{
    [ServiceBehavior]
    //[PrincipalPermission(SecurityAction.Demand, Authenticated = true)]
    public class DeviceStatusService : IDeviceStatusService
    {
        private readonly ILog _log;
        private readonly IBlobCommandManager _blobCommandManager;

        public DeviceStatusService(IBlobCommandManager blobCommandManager, ILog log)
        {
            _log = log;
            _blobCommandManager = blobCommandManager;
        }

        #region Customer

        [OperationBehavior]
        [PrincipalPermission(SecurityAction.Demand, Role = "Customer")]
        public async Task<RegisterDeviceResponseDto> RegisterDeviceAsync(RegisterDeviceDto dto)
        {
            _log.Debug("RegistrationService received registration message: " + dto);
            return await _blobCommandManager.RegisterDeviceAsync(dto).ConfigureAwait(false);
        }

        #endregion


        #region Device

        [OperationBehavior]
        [PrincipalPermission(SecurityAction.Assert, Role = "Device")]
        public async Task AddStatusRecordAsync(AddStatusRecordDto dto)
        {
            _log.Debug("Server received status: " + dto);
            await _blobCommandManager.AddStatusRecordAsync(dto).ConfigureAwait(false);
        }

        [OperationBehavior]
        [PrincipalPermission(SecurityAction.Assert, Role = "Device")]
        public async Task AddPerformanceRecordAsync(AddPerformanceRecordDto dto)
        {
            _log.Debug("Server received perf: " + dto);
            await _blobCommandManager.AddPerformanceRecordAsync(dto).ConfigureAwait(false);
        }

        #endregion
    }
}
