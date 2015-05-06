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

        DeviceCommandIssueVm GetDeviceCommandIssueVm(Guid deviceId);

        Task<DeviceDisableVm> GetDeviceDisableVmAsync(Guid deviceId);
        Task<DeviceEnableVm> GetDeviceEnableVmAsync(Guid deviceId);
        Task<DeviceSingleVm> GetDeviceSingleVmAsync(Guid deviceId);
        Task<DeviceUpdateVm> GetDeviceUpdateVmAsync(Guid deviceId);

        Task<PerformanceRecordDeleteVm> GetPerformanceRecordDeleteVmAsync(long recordId);
        Task<PerformanceRecordSingleVm> GetPerformanceRecordSingleVmAsync(long recordId);

        Task<StatusRecordDeleteVm> GetStatusRecordDeleteVmAsync(long recordId);
        Task<StatusRecordSingleVm> GetStatusRecordSingleVmAsync(long recordId);

        Task<UserDisableVm> GetUserDisableVmAsync(Guid userId);
        Task<UserEnableVm> GetUserEnableVmAsync(Guid userId);
        Task<UserSingleVm> GetUserSingleVmAsync(Guid userId);
        Task<UserUpdateVm> GetUserUpdateVmAsync(Guid userId);
        Task<UserUpdatePasswordVm> GetUserUpdatePasswordVmAsync(Guid userId);
        
    }
}
