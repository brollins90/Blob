using System;
using System.Threading.Tasks;
using Blob.Contracts.Dto.ViewModels;

namespace Blob.Contracts.Blob
{
    public interface IBlobQueryManager
    {
        Task<CustomerDisableVm> GetCustomerDisableVmAsync(Guid customerId);
        Task<CustomerEnableVm> GetCustomerEnableVmAsync(Guid customerId);
        Task<CustomerSingleVm> GetCustomerSingleVmAsync(Guid customerId);
        Task<CustomerUpdateVm> GetCustomerUpdateVmAsync(Guid customerId);

        Task<DeviceDisableVm> GetDeviceDisableVmAsync(Guid deviceId);
        Task<DeviceEnableVm> GetDeviceEnableVmAsync(Guid deviceId);
        Task<DeviceSingleVm> GetDeviceSingleVmAsync(Guid deviceId);
        Task<DeviceUpdateVm> GetDeviceUpdateVmAsync(Guid deviceId);

        Task<StatusRecordSingleVm> GetStatusRecordSingleVmAsync(long recordId);

        Task<PerformanceRecordSingleVm> GetPerformanceRecordSingleVmAsync(long recordId);

        Task<UserDisableVm> GetUserDisableVmAsync(Guid UserId);
        Task<UserEnableVm> GetUserEnableVmAsync(Guid UserId);
        Task<UserSingleVm> GetUserSingleVmAsync(Guid UserId);
        Task<UserUpdateVm> GetUserUpdateVmAsync(Guid UserId);
    }
}
