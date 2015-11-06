using Blob.Contracts.Models;
using Blob.Contracts.ServiceContracts;
using System.IdentityModel.Services;
using System.Security.Permissions;
using System.ServiceModel;
using System.Threading.Tasks;

namespace Blob.WcfHost.Service
{
    [ServiceBehavior]
    [GlobalErrorBehavior(typeof(GlobalErrorHandler))]
    public class DeviceStatusWcfService : IDeviceStatusService
    {
        IDeviceStatusService _deviceStatusService;

        public DeviceStatusWcfService(IDeviceStatusService deviceStatusService)
        {
            _deviceStatusService = deviceStatusService;
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "device", Operation = "create")]
        public async Task<RegisterDeviceResponseDto> RegisterDeviceAsync(RegisterDeviceDto dto)
        {
            return await _deviceStatusService.RegisterDeviceAsync(dto);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "performance", Operation = "add")]
        public async Task AddPerformanceRecordAsync(AddPerformanceRecordDto dto)
        {
            await _deviceStatusService.AddPerformanceRecordAsync(dto);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "status", Operation = "add")]
        public async Task AddStatusRecordAsync(AddStatusRecordDto dto)
        {
            await _deviceStatusService.AddStatusRecordAsync(dto);
        }

        [OperationBehavior]
        public async Task<BlobResultDto> AuthenticateDeviceAsync(AuthenticateDeviceDto dto)
        {
            return await _deviceStatusService.AuthenticateDeviceAsync(dto);
        }
    }
}