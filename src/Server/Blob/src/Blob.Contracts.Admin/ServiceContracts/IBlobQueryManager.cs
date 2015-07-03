namespace Blob.Contracts.ServiceContracts
{
    using System;
    using System.Collections.Generic;
    using System.ServiceModel;
    using System.Threading.Tasks;
    using ViewModel;

    [ServiceContract]
    public interface IBlobQueryManager
    {
        // Customer
        [OperationContract]
        Task<CustomerDisableViewModel> GetCustomerDisableVmAsync(Guid customerId);
        [OperationContract]
        Task<CustomerEnableViewModel> GetCustomerEnableVmAsync(Guid customerId);
        [OperationContract]
        Task<CustomerPageViewModel> GetCustomerPageVmAsync(Guid searchId, int pageNum = 1, int pageSize = 10);
        [OperationContract]
        Task<CustomerSingleViewModel> GetCustomerSingleVmAsync(Guid customerId);
        [OperationContract]
        Task<CustomerUpdateViewModel> GetCustomerUpdateVmAsync(Guid customerId);
        [OperationContract]
        Task<IEnumerable<CustomerGroupRoleListItem>> GetCustomerRolesAsync(Guid customerId);
        [OperationContract]
        Task<CustomerGroupPageViewModel> GetCustomerGroupPageVmAsync(Guid customerId, int pageNum = 1, int pageSize = 10);

        // Customer Group
        [OperationContract]
        Task<CustomerGroupCreateViewModel> GetCustomerGroupCreateVmAsync(Guid customerId);
        [OperationContract]
        Task<CustomerGroupDeleteViewModel> GetCustomerGroupDeleteVmAsync(Guid groupId);
        [OperationContract]
        Task<CustomerGroupSingleViewModel> GetCustomerGroupSingleVmAsync(Guid groupId);
        [OperationContract]
        Task<CustomerGroupUpdateViewModel> GetCustomerGroupUpdateVmAsync(Guid groupId);
        [OperationContract]
        Task<IEnumerable<CustomerGroupRoleListItem>> GetCustomerGroupRolesAsync(Guid groupId);
        [OperationContract]
        Task<IEnumerable<CustomerGroupUserListItem>> GetCustomerGroupUsersAsync(Guid groupId);

        [OperationContract]
        Task<DashCurrentConnectionsLargeViewModel> GetDashCurrentConnectionsLargeVmAsync(Guid searchId, int pageNum = 1, int pageSize = 10);
        [OperationContract]
        Task<DashDevicesLargeViewModel> GetDashDevicesLargeVmAsync(Guid searchId, int pageNum = 1, int pageSize = 10);


        [OperationContract]
        IEnumerable<DeviceCommandViewModel> GetDeviceCommandVmList();
        [OperationContract]
        DeviceCommandIssueViewModel GetDeviceCommandIssueVm(Guid deviceId, string commandType);

        [OperationContract]
        Task<DeviceDisableViewModel> GetDeviceDisableVmAsync(Guid deviceId);
        [OperationContract]
        Task<DeviceEnableViewModel> GetDeviceEnableVmAsync(Guid deviceId);
        [OperationContract]
        Task<DevicePageViewModel> GetDevicePageVmAsync(Guid customerId, int pageNum = 1, int pageSize = 10);
        [OperationContract]
        Task<DeviceSingleViewModel> GetDeviceSingleVmAsync(Guid deviceId);
        [OperationContract]
        Task<DeviceUpdateViewModel> GetDeviceUpdateVmAsync(Guid deviceId);

        [OperationContract]
        Task<PerformanceRecordDeleteViewModel> GetPerformanceRecordDeleteVmAsync(long recordId);
        [OperationContract]
        Task<PerformanceRecordPageViewModel> GetPerformanceRecordPageVmAsync(Guid deviceId, int pageNum = 1, int pageSize = 10);

        [OperationContract]
        Task<PerformanceRecordPageViewModel> GetPerformanceRecordPageVmForStatusAsync(long recordId, int pageNum = 1, int pageSize = 10);
        [OperationContract]
        Task<PerformanceRecordSingleViewModel> GetPerformanceRecordSingleVmAsync(long recordId);

        [OperationContract]
        Task<StatusRecordDeleteViewModel> GetStatusRecordDeleteVmAsync(long recordId);
        [OperationContract]
        Task<StatusRecordPageViewModel> GetStatusRecordPageVmAsync(Guid deviceId, int pageNum = 1, int pageSize = 10);
        [OperationContract]
        Task<StatusRecordSingleViewModel> GetStatusRecordSingleVmAsync(long recordId);

        [OperationContract]
        Task<MonitorListViewModel> GetMonitorListVmAsync(Guid deviceId);

        [OperationContract]
        Task<UserDisableViewModel> GetUserDisableVmAsync(Guid userId);
        [OperationContract]
        Task<UserEnableViewModel> GetUserEnableVmAsync(Guid userId);
        [OperationContract]
        Task<UserPageViewModel> GetUserPageVmAsync(Guid customerId, int pageNum = 1, int pageSize = 10);
        [OperationContract]
        Task<UserSingleViewModel> GetUserSingleVmAsync(Guid userId);
        [OperationContract]
        Task<UserUpdateVm> GetUserUpdateVmAsync(Guid userId);
        [OperationContract]
        Task<UserUpdatePasswordViewModel> GetUserUpdatePasswordVmAsync(Guid userId);
    }
}