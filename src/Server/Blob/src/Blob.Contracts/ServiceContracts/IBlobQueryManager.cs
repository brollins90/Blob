using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;
using Blob.Contracts.Models.ViewModels;

namespace Blob.Contracts.ServiceContracts
{
    [ServiceContract]
    public interface IBlobQueryManager
    {
        // Customer
        [OperationContract]
        Task<CustomerDisableVm> GetCustomerDisableVmAsync(Guid customerId);
        [OperationContract]
        Task<CustomerEnableVm> GetCustomerEnableVmAsync(Guid customerId);
        [OperationContract]
        Task<CustomerPageVm> GetCustomerPageVmAsync(Guid searchId, int pageNum = 1, int pageSize = 10);
        [OperationContract]
        Task<CustomerSingleVm> GetCustomerSingleVmAsync(Guid customerId);
        [OperationContract]
        Task<CustomerUpdateVm> GetCustomerUpdateVmAsync(Guid customerId);
        [OperationContract]
        Task<IEnumerable<CustomerGroupRoleListItem>> GetCustomerRolesAsync(Guid customerId);
        [OperationContract]
        Task<CustomerGroupPageVm> GetCustomerGroupPageVmAsync(Guid customerId, int pageNum = 1, int pageSize = 10);

        // Customer Group
        [OperationContract]
        Task<CustomerGroupCreateVm> GetCustomerGroupCreateVmAsync(Guid customerId);
        [OperationContract]
        Task<CustomerGroupDeleteVm> GetCustomerGroupDeleteVmAsync(Guid groupId);
        [OperationContract]
        Task<CustomerGroupSingleVm> GetCustomerGroupSingleVmAsync(Guid groupId);
        [OperationContract]
        Task<CustomerGroupUpdateVm> GetCustomerGroupUpdateVmAsync(Guid groupId);
        [OperationContract]
        Task<IEnumerable<CustomerGroupRoleListItem>> GetCustomerGroupRolesAsync(Guid groupId);
        [OperationContract]
        Task<IEnumerable<CustomerGroupUserListItem>> GetCustomerGroupUsersAsync(Guid groupId);

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
        Task<MonitorListVm> GetMonitorListVmAsync(Guid deviceId);

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
