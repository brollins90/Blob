using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;
using Blob.Contracts.Models.ViewModels;
using Blob.Contracts.Services;

namespace Blob.Contracts.ServiceContracts
{
    [ServiceContract]
    public interface IBlobQueryManager : ICustomerQueryManager, ICustomerGroupQueryManager
    {
        [OperationContract]
        Task<DashCurrentConnectionsLargeVm> GetDashCurrentConnectionsLargeVmAsync(Guid searchId, int pageNum = 1, int pageSize = 10);
        [OperationContract]
        Task<DashDevicesLargeVm> GetDashDevicesLargeVmAsync(Guid searchId, int pageNum = 1, int pageSize = 10);
        
        
        [OperationContract]
        IEnumerable<DeviceCommandVm> GetDeviceCommandVmList();
        [OperationContract]
        DeviceCommandIssueVm GetDeviceCommandIssueVm(Guid deviceId, string commandType);

        [OperationContract]
        Task<DeviceDisableVm> GetDeviceDisableVmAsync(Guid deviceId);
        [OperationContract]
        Task<DeviceEnableVm> GetDeviceEnableVmAsync(Guid deviceId);
        [OperationContract]
        Task<DevicePageVm> GetDevicePageVmAsync(Guid customerId, int pageNum = 1, int pageSize = 10);
        [OperationContract]
        Task<DeviceSingleVm> GetDeviceSingleVmAsync(Guid deviceId);
        [OperationContract]
        Task<DeviceUpdateVm> GetDeviceUpdateVmAsync(Guid deviceId);

        [OperationContract]
        Task<PerformanceRecordDeleteVm> GetPerformanceRecordDeleteVmAsync(long recordId);
        [OperationContract]
        Task<PerformanceRecordPageVm> GetPerformanceRecordPageVmAsync(Guid deviceId, int pageNum = 1, int pageSize = 10);

        [OperationContract]
        Task<PerformanceRecordPageVm> GetPerformanceRecordPageVmForStatusAsync(long recordId, int pageNum = 1, int pageSize = 10);
        [OperationContract]
        Task<PerformanceRecordSingleVm> GetPerformanceRecordSingleVmAsync(long recordId);

        [OperationContract]
        Task<StatusRecordDeleteVm> GetStatusRecordDeleteVmAsync(long recordId);
        [OperationContract]
        Task<StatusRecordPageVm> GetStatusRecordPageVmAsync(Guid deviceId, int pageNum = 1, int pageSize = 10);
        [OperationContract]
        Task<StatusRecordSingleVm> GetStatusRecordSingleVmAsync(long recordId);

        [OperationContract]
        Task<UserDisableVm> GetUserDisableVmAsync(Guid userId);
        [OperationContract]
        Task<UserEnableVm> GetUserEnableVmAsync(Guid userId);
        [OperationContract]
        Task<UserPageVm> GetUserPageVmAsync(Guid customerId, int pageNum = 1, int pageSize = 10);
        [OperationContract]
        Task<UserSingleVm> GetUserSingleVmAsync(Guid userId);
        [OperationContract]
        Task<UserUpdateVm> GetUserUpdateVmAsync(Guid userId);
        [OperationContract]
        Task<UserUpdatePasswordVm> GetUserUpdatePasswordVmAsync(Guid userId);
        
    }
}
