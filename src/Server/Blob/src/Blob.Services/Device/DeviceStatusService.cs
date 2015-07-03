namespace Blob.Services.Device
{
    using System.IdentityModel.Services;
    using System.Security.Permissions;
    using System.ServiceModel;
    using System.Threading.Tasks;
    using Common.Services;
    using Contracts.Request;
    using Contracts.Response;
    using Contracts.ServiceContracts;
    using log4net;

    [ServiceBehavior]
    [GlobalErrorBehavior(typeof(GlobalErrorHandler))]
    //[ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "service", Operation = "create")]
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

        //[OperationBehavior]
        //public async Task<CheckDeviceRegistrationResponseDto> CheckDeviceRegistration(CheckDeviceRegistrationDto dto)
        //{
        //    return await _blobCommandManager.CheckDeviceRegistration(dto).ConfigureAwait(false);
        //}

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "device", Operation = "create")]
        public async Task<RegisterDeviceResponse> RegisterDeviceAsync(RegisterDeviceRequest dto)
        {
            _log.Debug("RegistrationService received registration message: " + dto);
            return await _blobCommandManager.RegisterDeviceAsync(dto).ConfigureAwait(false);
        }

        #endregion


        #region Device

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "performance", Operation = "add")]
        public async Task AddPerformanceRecordAsync(AddPerformanceRecordRequest dto)
        {
            _log.Debug("Server received perf: " + dto);
            await _blobCommandManager.AddPerformanceRecordAsync(dto).ConfigureAwait(false);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "status", Operation = "add")]
        public async Task AddStatusRecordAsync(AddStatusRecordRequest dto)
        {
            _log.Debug("Server received status: " + dto);
            await _blobCommandManager.AddStatusRecordAsync(dto).ConfigureAwait(false);
        }

        #endregion

        [OperationBehavior]
        public async Task<BlobResult> AuthenticateDeviceAsync(AuthenticateDeviceRequest dto)
        {
            return await _deviceService.AuthenticateDeviceAsync(dto).ConfigureAwait(false);
        }
    }
}