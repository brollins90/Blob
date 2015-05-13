using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;
using Blob.Contracts.Dto.ViewModels;

namespace Blob.Contracts.Blob
{
    [ServiceContract]
    public interface IBlobQueryManager
    {
        [OperationContract]
        Task<CustomerDisableVm> GetCustomerDisableVmAsync(Guid customerId);
        [OperationContract]
        Task<CustomerEnableVm> GetCustomerEnableVmAsync(Guid customerId);
        [OperationContract]
        Task<CustomerSingleVm> GetCustomerSingleVmAsync(Guid customerId);
        [OperationContract]
        Task<CustomerUpdateVm> GetCustomerUpdateVmAsync(Guid customerId);

        [OperationContract]
        IEnumerable<DeviceCommandVm> GetDeviceCommandVmList();
        [OperationContract]
        DeviceCommandIssueVm GetDeviceCommandIssueVm(Guid deviceId, string commandType);

        [OperationContract]
        Task<DeviceDisableVm> GetDeviceDisableVmAsync(Guid deviceId);
        [OperationContract]
        Task<DeviceEnableVm> GetDeviceEnableVmAsync(Guid deviceId);
        [OperationContract]
        Task<DeviceSingleVm> GetDeviceSingleVmAsync(Guid deviceId);
        [OperationContract]
        Task<DeviceUpdateVm> GetDeviceUpdateVmAsync(Guid deviceId);

        [OperationContract]
        Task<PerformanceRecordDeleteVm> GetPerformanceRecordDeleteVmAsync(long recordId);
        [OperationContract]
        Task<PerformanceRecordSingleVm> GetPerformanceRecordSingleVmAsync(long recordId);

        [OperationContract]
        Task<StatusRecordDeleteVm> GetStatusRecordDeleteVmAsync(long recordId);
        [OperationContract]
        Task<StatusRecordSingleVm> GetStatusRecordSingleVmAsync(long recordId);

        [OperationContract]
        Task<UserDisableVm> GetUserDisableVmAsync(Guid userId);
        [OperationContract]
        Task<UserEnableVm> GetUserEnableVmAsync(Guid userId);
        [OperationContract]
        Task<UserSingleVm> GetUserSingleVmAsync(Guid userId);
        [OperationContract]
        Task<UserUpdateVm> GetUserUpdateVmAsync(Guid userId);
        [OperationContract]
        Task<UserUpdatePasswordVm> GetUserUpdatePasswordVmAsync(Guid userId);
        
    }
}
