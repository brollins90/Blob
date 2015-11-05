using Blob.Contracts.Models;
using Blob.Contracts.ServiceContracts;
using Blob.Contracts.Services;
using log4net;
using System.Threading.Tasks;

namespace Blob.Services.Device
{
    public class DeviceStatusService : IDeviceStatusService
    {
        private readonly ILog _log;
        private readonly IBlobCommandManager _blobCommandManager;
        private IDeviceService _deviceService;

        public DeviceStatusService(IBlobCommandManager blobCommandManager, ILog log, IDeviceService deviceService)
        {
            _log = log;
            _deviceService = deviceService;
            _blobCommandManager = blobCommandManager;
        }

        #region Customer

        public async Task<RegisterDeviceResponseDto> RegisterDeviceAsync(RegisterDeviceDto dto)
        {
            _log.Debug("RegistrationService received registration message: " + dto);
            return await _blobCommandManager.RegisterDeviceAsync(dto).ConfigureAwait(false);
        }

        #endregion


        #region Device

        public async Task AddPerformanceRecordAsync(AddPerformanceRecordDto dto)
        {
            _log.Debug("Server received perf: " + dto);
            await _blobCommandManager.AddPerformanceRecordAsync(dto).ConfigureAwait(false);
        }

        public async Task AddStatusRecordAsync(AddStatusRecordDto dto)
        {
            _log.Debug("Server received status: " + dto);
            await _blobCommandManager.AddStatusRecordAsync(dto).ConfigureAwait(false);
        }

        #endregion

        public async Task<BlobResultDto> AuthenticateDeviceAsync(AuthenticateDeviceDto dto)
        {
            return await _deviceService.AuthenticateDeviceAsync(dto).ConfigureAwait(false);
        }
    }
}
