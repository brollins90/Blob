using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Blob.Contracts.Models;
using Blob.Contracts.Models.ViewModels;

namespace Blob.Contracts.Services
{
    public interface IDeviceService
    {
        // Command
        Task<BlobResult> DisableDeviceAsync(DisableDeviceDto dto);
        Task<BlobResult> EnableDeviceAsync(EnableDeviceDto dto);
        Task<RegisterDeviceResponse> RegisterDeviceAsync(RegisterDeviceRequest dto);
        Task<BlobResult> UpdateDeviceAsync(UpdateDeviceDto dto);
        
        // Query
        Task<DeviceDisableVm> GetDeviceDisableVmAsync(Guid deviceId);
        Task<DeviceEnableVm> GetDeviceEnableVmAsync(Guid deviceId);
        Task<DeviceSingleVm> GetDeviceSingleVmAsync(Guid deviceId);
        Task<DeviceUpdateVm> GetDeviceUpdateVmAsync(Guid deviceId);

        //Task<IEnumerable<CustomerGroupRoleListItem>> GetCustomerRolesAsync(Guid customerId);
        Task<DevicePageVm> GetDevicePageVmAsync(Guid customerId, int pageNum = 1, int pageSize = 10);

        int CalculateDeviceAlertLevel(Guid deviceId);


        Task<BlobResult> AuthenticateDeviceAsync(AuthenticateDeviceRequest dto);
    }
}
