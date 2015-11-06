using Blob.Contracts.Models;
using Blob.Contracts.ServiceContracts;
using Blob.Contracts.Services;
using log4net;
using System.Threading.Tasks;

namespace Blob.Services
{
    public class DeviceStatusService : IDeviceStatusService
    {
        private readonly ILog _log;
        private IDeviceService _deviceService;
        private IPerformanceRecordService _performanceRecordService;
        private IStatusRecordService _statusRecordService;

        public DeviceStatusService(
            ILog log, IDeviceService deviceService,
            IPerformanceRecordService performanceRecordService,
            IStatusRecordService statusRecordService)
        {
            _log = log;
            _deviceService = deviceService;
            _performanceRecordService = performanceRecordService;
            _statusRecordService = statusRecordService;
        }

        #region Customer

        public async Task<RegisterDeviceResponseDto> RegisterDeviceAsync(RegisterDeviceDto dto)
        {
            _log.Debug("RegistrationService received registration message: " + dto);
            return await _deviceService.RegisterDeviceAsync(dto).ConfigureAwait(false);
        }

        #endregion


        #region Device

        public async Task AddPerformanceRecordAsync(AddPerformanceRecordDto dto)
        {
            _log.Debug("Server received perf: " + dto);
            await _performanceRecordService.AddPerformanceRecordAsync(dto).ConfigureAwait(false);
        }

        public async Task AddStatusRecordAsync(AddStatusRecordDto dto)
        {
            _log.Debug("Server received status: " + dto);
            await _statusRecordService.AddStatusRecordAsync(dto).ConfigureAwait(false);
        }

        #endregion

        public async Task<BlobResultDto> AuthenticateDeviceAsync(AuthenticateDeviceDto dto)
        {
            return await _deviceService.AuthenticateDeviceAsync(dto).ConfigureAwait(false);
        }
    }
}
