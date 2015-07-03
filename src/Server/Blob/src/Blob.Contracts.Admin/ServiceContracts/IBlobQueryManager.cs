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
        Task<CustomerDisableViewModel> GetCustomerDisableViewModelAsync(Guid customerId);
        [OperationContract]
        Task<CustomerEnableViewModel> GetCustomerEnableViewModelAsync(Guid customerId);
        [OperationContract]
        Task<CustomerPageViewModel> GetCustomerPageViewModelAsync(Guid searchId, int pageNum = 1, int pageSize = 10);
        [OperationContract]
        Task<CustomerSingleViewModel> GetCustomerSingleViewModelAsync(Guid customerId);
        [OperationContract]
        Task<CustomerUpdateViewModel> GetCustomerUpdateViewModelAsync(Guid customerId);
        [OperationContract]
        Task<IEnumerable<CustomerGroupRoleListItem>> GetCustomerRolesAsync(Guid customerId);
        [OperationContract]
        Task<CustomerGroupPageViewModel> GetCustomerGroupPageViewModelAsync(Guid customerId, int pageNum = 1, int pageSize = 10);

        // Customer Group
        [OperationContract]
        Task<CustomerGroupCreateViewModel> GetCustomerGroupCreateViewModelAsync(Guid customerId);
        [OperationContract]
        Task<CustomerGroupDeleteViewModel> GetCustomerGroupDeleteViewModelAsync(Guid groupId);
        [OperationContract]
        Task<CustomerGroupSingleViewModel> GetCustomerGroupSingleViewModelAsync(Guid groupId);
        [OperationContract]
        Task<CustomerGroupUpdateViewModel> GetCustomerGroupUpdateViewModelAsync(Guid groupId);
        [OperationContract]
        Task<IEnumerable<CustomerGroupRoleListItem>> GetCustomerGroupRolesAsync(Guid groupId);
        [OperationContract]
        Task<IEnumerable<CustomerGroupUserListItem>> GetCustomerGroupUsersAsync(Guid groupId);

        [OperationContract]
        Task<DashCurrentConnectionsLargeViewModel> GetDashCurrentConnectionsLargeViewModelAsync(Guid searchId, int pageNum = 1, int pageSize = 10);
        [OperationContract]
        Task<DashDevicesLargeViewModel> GetDashDevicesLargeViewModelAsync(Guid searchId, int pageNum = 1, int pageSize = 10);


        [OperationContract]
        IEnumerable<DeviceCommandViewModel> GetDeviceCommandViewModelList();
        [OperationContract]
        DeviceCommandIssueViewModel GetDeviceCommandIssueViewModel(Guid deviceId, string commandType);

        [OperationContract]
        Task<DeviceDisableViewModel> GetDeviceDisableViewModelAsync(Guid deviceId);
        [OperationContract]
        Task<DeviceEnableViewModel> GetDeviceEnableViewModelAsync(Guid deviceId);
        [OperationContract]
        Task<DevicePageViewModel> GetDevicePageViewModelAsync(Guid customerId, int pageNum = 1, int pageSize = 10);
        [OperationContract]
        Task<DeviceSingleViewModel> GetDeviceSingleViewModelAsync(Guid deviceId);
        [OperationContract]
        Task<DeviceUpdateViewModel> GetDeviceUpdateViewModelAsync(Guid deviceId);

        [OperationContract]
        Task<PerformanceRecordDeleteViewModel> GetPerformanceRecordDeleteViewModelAsync(long recordId);
        [OperationContract]
        Task<PerformanceRecordPageViewModel> GetPerformanceRecordPageViewModelAsync(Guid deviceId, int pageNum = 1, int pageSize = 10);

        [OperationContract]
        Task<PerformanceRecordPageViewModel> GetPerformanceRecordPageViewModelForStatusAsync(long recordId, int pageNum = 1, int pageSize = 10);
        [OperationContract]
        Task<PerformanceRecordSingleViewModel> GetPerformanceRecordSingleViewModelAsync(long recordId);

        [OperationContract]
        Task<StatusRecordDeleteViewModel> GetStatusRecordDeleteViewModelAsync(long recordId);
        [OperationContract]
        Task<StatusRecordPageViewModel> GetStatusRecordPageViewModelAsync(Guid deviceId, int pageNum = 1, int pageSize = 10);
        [OperationContract]
        Task<StatusRecordSingleViewModel> GetStatusRecordSingleViewModelAsync(long recordId);

        [OperationContract]
        Task<MonitorListViewModel> GetMonitorListViewModelAsync(Guid deviceId);

        [OperationContract]
        Task<UserDisableViewModel> GetUserDisableViewModelAsync(Guid userId);
        [OperationContract]
        Task<UserEnableViewModel> GetUserEnableViewModelAsync(Guid userId);
        [OperationContract]
        Task<UserPageViewModel> GetUserPageViewModelAsync(Guid customerId, int pageNum = 1, int pageSize = 10);
        [OperationContract]
        Task<UserSingleViewModel> GetUserSingleViewModelAsync(Guid userId);
        [OperationContract]
        Task<UserUpdateViewModel> GetUserUpdateViewModelAsync(Guid userId);
        [OperationContract]
        Task<UserUpdatePasswordViewModel> GetUserUpdatePasswordViewModelAsync(Guid userId);
    }
}