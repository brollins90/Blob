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
        Task<BlobResult> DisableDeviceAsync(DisableDeviceRequest request);
        Task<BlobResult> EnableDeviceAsync(EnableDeviceRequest request);
        Task<RegisterDeviceResponse> RegisterDeviceAsync(RegisterDeviceRequest request);
        Task<BlobResult> UpdateDeviceAsync(UpdateDeviceRequest request);

        // Query
        Task<DeviceDisableViewModel> GetDeviceDisableViewModelAsync(Guid id);
        Task<DeviceEnableViewModel> GetDeviceEnableViewModelAsync(Guid id);
        Task<DeviceSingleViewModel> GetDeviceSingleViewModelAsync(Guid id);
        Task<DeviceUpdateViewModel> GetDeviceUpdateViewModelAsync(Guid id);

        //Task<IEnumerable<CustomerGroupRoleListItem>> GetCustomerRolesAsync(Guid customerId);
        Task<DevicePageViewModel> GetDevicePageViewModelAsync(Guid customerId, int pageNum = 1, int pageSize = 10);

        int CalculateDeviceAlertLevel(Guid id);

        Task<BlobResult> AuthenticateDeviceAsync(AuthenticateDeviceRequest request);
    }
}