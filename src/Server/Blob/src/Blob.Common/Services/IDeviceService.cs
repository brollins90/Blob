namespace Blob.Common.Services
{
    using System;
    using System.Threading.Tasks;
    using Contracts.Request;
    using Contracts.Response;
    using Contracts.ViewModel;

    public interface IDeviceService
    {
        // Command
        Task<BlobResult> DisableDeviceAsync(DisableDeviceRequest dto);
        Task<BlobResult> EnableDeviceAsync(EnableDeviceRequest dto);
        Task<RegisterDeviceResponse> RegisterDeviceAsync(RegisterDeviceRequest dto);
        Task<BlobResult> UpdateDeviceAsync(UpdateDeviceRequest dto);

        // Query
        Task<DeviceDisableViewModel> GetDeviceDisableVmAsync(Guid deviceId);
        Task<DeviceEnableViewModel> GetDeviceEnableVmAsync(Guid deviceId);
        Task<DeviceSingleViewModel> GetDeviceSingleVmAsync(Guid deviceId);
        Task<DeviceUpdateViewModel> GetDeviceUpdateVmAsync(Guid deviceId);

        //Task<IEnumerable<CustomerGroupRoleListItem>> GetCustomerRolesAsync(Guid customerId);
        Task<DevicePageViewModel> GetDevicePageVmAsync(Guid customerId, int pageNum = 1, int pageSize = 10);

        int CalculateDeviceAlertLevel(Guid deviceId);

        Task<BlobResult> AuthenticateDeviceAsync(AuthenticateDeviceRequest dto);
    }
}