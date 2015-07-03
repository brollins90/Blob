namespace Blob.Services.Before
{
    using System;
    using System.Collections.Generic;
    using System.IdentityModel.Services;
    using System.Security.Claims;
    using System.Security.Permissions;
    using System.ServiceModel;
    using System.Threading.Tasks;
    using Contracts.ServiceContracts;
    using Contracts.ViewModel;
    using Core.Extensions;

    [ServiceBehavior]
    [GlobalErrorBehavior(typeof(GlobalErrorHandler))]
    //[ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "service", Operation = "create")]
    public class BeforeQueryService : IBlobQueryManager
    {
        private readonly IBlobAuditor _blobAuditor;
        private readonly IBlobQueryManager _blobQueryManager;

        public BeforeQueryService(IBlobQueryManager blobQueryManager, IBlobAuditor blobAuditor)
        {
            _blobQueryManager = blobQueryManager;
            _blobAuditor = blobAuditor;
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "customer", Operation = "view")]
        public async Task<DashCurrentConnectionsLargeViewModel> GetDashCurrentConnectionsLargeViewModelAsync(Guid customerId, int pageNum, int pageSize)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            await _blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), Blob.Contracts.ServiceContracts.AuditLevel.View, "view", "customer", customerId.ToString());
            return await _blobQueryManager.GetDashCurrentConnectionsLargeViewModelAsync(customerId, pageNum, pageSize).ConfigureAwait(false);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "customer", Operation = "view")]
        public async Task<DashDevicesLargeViewModel> GetDashDevicesLargeViewModelAsync(Guid customerId, int pageNum, int pageSize)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            await _blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), Blob.Contracts.ServiceContracts.AuditLevel.View, "view", "customer", customerId.ToString());
            return await _blobQueryManager.GetDashDevicesLargeViewModelAsync(customerId, pageNum, pageSize).ConfigureAwait(false);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "customer", Operation = "disable")]
        public async Task<CustomerDisableViewModel> GetCustomerDisableViewModelAsync(Guid customerId)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            await _blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), Blob.Contracts.ServiceContracts.AuditLevel.View, "disableview", "customer", customerId.ToString());
            return await _blobQueryManager.GetCustomerDisableViewModelAsync(customerId).ConfigureAwait(false);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "customer", Operation = "enable")]
        public async Task<CustomerEnableViewModel> GetCustomerEnableViewModelAsync(Guid customerId)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            await _blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), Blob.Contracts.ServiceContracts.AuditLevel.View, "enableview", "customer", customerId.ToString());
            return await _blobQueryManager.GetCustomerEnableViewModelAsync(customerId).ConfigureAwait(false);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "customer", Operation = "view")]
        public async Task<CustomerSingleViewModel> GetCustomerSingleViewModelAsync(Guid customerId)//, int devicePageSize, int devicePageNum, int userPageSize, int userPageNum)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            await _blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), Blob.Contracts.ServiceContracts.AuditLevel.View, "view", "customer", customerId.ToString());
            return await _blobQueryManager.GetCustomerSingleViewModelAsync(customerId).ConfigureAwait(false);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "customer", Operation = "update")]
        public async Task<CustomerUpdateViewModel> GetCustomerUpdateViewModelAsync(Guid customerId)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            await _blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), Blob.Contracts.ServiceContracts.AuditLevel.View, "editview", "customer", customerId.ToString());
            return await _blobQueryManager.GetCustomerUpdateViewModelAsync(customerId).ConfigureAwait(false);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "device", Operation = "issueCommand")]
        public IEnumerable<DeviceCommandViewModel> GetDeviceCommandViewModelList()
        {
            var identity = ClaimsPrincipal.Current.Identity;
            Task.WaitAll(_blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), Blob.Contracts.ServiceContracts.AuditLevel.View, "list", "deviceCommand", "all"));
            return _blobQueryManager.GetDeviceCommandViewModelList();
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "device", Operation = "issueCommand")]
        public DeviceCommandIssueViewModel GetDeviceCommandIssueViewModel(Guid deviceId, string commandType)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            Task.WaitAll(_blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), Blob.Contracts.ServiceContracts.AuditLevel.View, "view", "deviceCommand", commandType));
            return _blobQueryManager.GetDeviceCommandIssueViewModel(deviceId, commandType);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "device", Operation = "disable")]
        public async Task<DeviceDisableViewModel> GetDeviceDisableViewModelAsync(Guid deviceId)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            await _blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), Blob.Contracts.ServiceContracts.AuditLevel.View, "disableview", "device", deviceId.ToString());
            return await _blobQueryManager.GetDeviceDisableViewModelAsync(deviceId).ConfigureAwait(false);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "device", Operation = "enable")]
        public async Task<DeviceEnableViewModel> GetDeviceEnableViewModelAsync(Guid deviceId)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            await _blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), Blob.Contracts.ServiceContracts.AuditLevel.View, "enableview", "device", deviceId.ToString());
            return await _blobQueryManager.GetDeviceEnableViewModelAsync(deviceId).ConfigureAwait(false);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "device", Operation = "view")]
        public async Task<DevicePageViewModel> GetDevicePageViewModelAsync(Guid customerId, int pageNum, int pageSize)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            await _blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), Blob.Contracts.ServiceContracts.AuditLevel.View, "view", "device", customerId.ToString());
            return await _blobQueryManager.GetDevicePageViewModelAsync(customerId, pageNum, pageSize).ConfigureAwait(false);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "device", Operation = "view")]
        public async Task<DeviceSingleViewModel> GetDeviceSingleViewModelAsync(Guid deviceId)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            await _blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), Blob.Contracts.ServiceContracts.AuditLevel.View, "view", "device", deviceId.ToString());
            return await _blobQueryManager.GetDeviceSingleViewModelAsync(deviceId).ConfigureAwait(false);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "device", Operation = "update")]
        public async Task<DeviceUpdateViewModel> GetDeviceUpdateViewModelAsync(Guid deviceId)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            await _blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), Blob.Contracts.ServiceContracts.AuditLevel.View, "updateview", "device", deviceId.ToString());
            return await _blobQueryManager.GetDeviceUpdateViewModelAsync(deviceId).ConfigureAwait(false);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "performance", Operation = "delete")]
        public async Task<PerformanceRecordDeleteViewModel> GetPerformanceRecordDeleteViewModelAsync(long recordId)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            await _blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), Blob.Contracts.ServiceContracts.AuditLevel.View, "deleteview", "performance", recordId.ToString());
            return await _blobQueryManager.GetPerformanceRecordDeleteViewModelAsync(recordId).ConfigureAwait(false);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "performance", Operation = "view")]
        public async Task<PerformanceRecordPageViewModel> GetPerformanceRecordPageViewModelAsync(Guid deviceId, int pageNum, int pageSize)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            await _blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), Blob.Contracts.ServiceContracts.AuditLevel.View, "view", "performance", deviceId.ToString());
            return await _blobQueryManager.GetPerformanceRecordPageViewModelAsync(deviceId, pageNum, pageSize).ConfigureAwait(false);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "performance", Operation = "view")]
        public async Task<PerformanceRecordPageViewModel> GetPerformanceRecordPageViewModelForStatusAsync(long recordId, int pageNum, int pageSize)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            await _blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), Blob.Contracts.ServiceContracts.AuditLevel.View, "view", "performance", recordId.ToString());
            return await _blobQueryManager.GetPerformanceRecordPageViewModelForStatusAsync(recordId, pageNum, pageSize).ConfigureAwait(false);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "performance", Operation = "view")]
        public async Task<PerformanceRecordSingleViewModel> GetPerformanceRecordSingleViewModelAsync(long recordId)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            await _blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), Blob.Contracts.ServiceContracts.AuditLevel.View, "view", "performance", recordId.ToString());
            return await _blobQueryManager.GetPerformanceRecordSingleViewModelAsync(recordId).ConfigureAwait(false);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "status", Operation = "delete")]
        public async Task<StatusRecordDeleteViewModel> GetStatusRecordDeleteViewModelAsync(long recordId)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            await _blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), Blob.Contracts.ServiceContracts.AuditLevel.View, "deleteview", "status", recordId.ToString());
            return await _blobQueryManager.GetStatusRecordDeleteViewModelAsync(recordId).ConfigureAwait(false);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "status", Operation = "view")]
        public async Task<StatusRecordPageViewModel> GetStatusRecordPageViewModelAsync(Guid deviceId, int pageNum, int pageSize)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            await _blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), Blob.Contracts.ServiceContracts.AuditLevel.View, "view", "status", deviceId.ToString());
            return await _blobQueryManager.GetStatusRecordPageViewModelAsync(deviceId, pageNum, pageSize).ConfigureAwait(false);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "status", Operation = "view")]
        public async Task<StatusRecordSingleViewModel> GetStatusRecordSingleViewModelAsync(long recordId)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            await _blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), Blob.Contracts.ServiceContracts.AuditLevel.View, "view", "status", recordId.ToString());
            return await _blobQueryManager.GetStatusRecordSingleViewModelAsync(recordId).ConfigureAwait(false);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "user", Operation = "disable")]
        public async Task<UserDisableViewModel> GetUserDisableViewModelAsync(Guid userId)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            await _blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), Blob.Contracts.ServiceContracts.AuditLevel.View, "disableview", "user", userId.ToString());
            return await _blobQueryManager.GetUserDisableViewModelAsync(userId).ConfigureAwait(false);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "user", Operation = "enable")]
        public async Task<UserEnableViewModel> GetUserEnableViewModelAsync(Guid userId)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            await _blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), Blob.Contracts.ServiceContracts.AuditLevel.View, "enableview", "user", userId.ToString());
            return await _blobQueryManager.GetUserEnableViewModelAsync(userId).ConfigureAwait(false);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "user", Operation = "view")]
        public async Task<UserPageViewModel> GetUserPageViewModelAsync(Guid customerId, int pageNum = 1, int pageSize = 10)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            await _blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), Blob.Contracts.ServiceContracts.AuditLevel.View, "view", "user", customerId.ToString());
            return await _blobQueryManager.GetUserPageViewModelAsync(customerId, pageNum, pageSize).ConfigureAwait(false);
        }
        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "user", Operation = "view")]
        public async Task<UserSingleViewModel> GetUserSingleViewModelAsync(Guid userId)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            await _blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), Blob.Contracts.ServiceContracts.AuditLevel.View, "view", "user", userId.ToString());
            return await _blobQueryManager.GetUserSingleViewModelAsync(userId).ConfigureAwait(false);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "user", Operation = "update")]
        public async Task<UserUpdateViewModel> GetUserUpdateViewModelAsync(Guid userId)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            await _blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), Blob.Contracts.ServiceContracts.AuditLevel.View, "updateview", "user", userId.ToString());
            return await _blobQueryManager.GetUserUpdateViewModelAsync(userId).ConfigureAwait(false);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "user", Operation = "update")]
        public async Task<UserUpdatePasswordViewModel> GetUserUpdatePasswordViewModelAsync(Guid userId)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            await _blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), Blob.Contracts.ServiceContracts.AuditLevel.View, "updateview", "user", userId.ToString());
            return await _blobQueryManager.GetUserUpdatePasswordViewModelAsync(userId).ConfigureAwait(false);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "group", Operation = "delete")]
        public async Task<CustomerGroupDeleteViewModel> GetCustomerGroupDeleteViewModelAsync(Guid groupId)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            await _blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), Blob.Contracts.ServiceContracts.AuditLevel.View, "delete", "group", groupId.ToString());
            return await _blobQueryManager.GetCustomerGroupDeleteViewModelAsync(groupId).ConfigureAwait(false);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "group", Operation = "view")]
        public async Task<CustomerGroupSingleViewModel> GetCustomerGroupSingleViewModelAsync(Guid groupId)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            await _blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), Blob.Contracts.ServiceContracts.AuditLevel.View, "view", "group", groupId.ToString());
            return await _blobQueryManager.GetCustomerGroupSingleViewModelAsync(groupId).ConfigureAwait(false);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "group", Operation = "update")]
        public async Task<CustomerGroupUpdateViewModel> GetCustomerGroupUpdateViewModelAsync(Guid groupId)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            await _blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), Blob.Contracts.ServiceContracts.AuditLevel.View, "update", "group", groupId.ToString());
            return await _blobQueryManager.GetCustomerGroupUpdateViewModelAsync(groupId).ConfigureAwait(false);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "role", Operation = "view")]
        public async Task<IEnumerable<CustomerGroupRoleListItem>> GetCustomerRolesAsync(Guid customerId)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            await _blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), Blob.Contracts.ServiceContracts.AuditLevel.View, "view", "role", customerId.ToString());
            return await _blobQueryManager.GetCustomerRolesAsync(customerId).ConfigureAwait(false);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "group", Operation = "view")]
        public async Task<CustomerGroupPageViewModel> GetCustomerGroupPageViewModelAsync(Guid customerId, int pageNum = 1, int pageSize = 10)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            await _blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), Blob.Contracts.ServiceContracts.AuditLevel.View, "view", "group", customerId.ToString());
            return await _blobQueryManager.GetCustomerGroupPageViewModelAsync(customerId, pageNum, pageSize).ConfigureAwait(false);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "group", Operation = "create")]
        public async Task<CustomerGroupCreateViewModel> GetCustomerGroupCreateViewModelAsync(Guid customerId)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            await _blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), Blob.Contracts.ServiceContracts.AuditLevel.View, "create", "group", customerId.ToString());
            return await _blobQueryManager.GetCustomerGroupCreateViewModelAsync(customerId).ConfigureAwait(false);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "group", Operation = "view")]
        public async Task<IEnumerable<CustomerGroupRoleListItem>> GetCustomerGroupRolesAsync(Guid groupId)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            await _blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), Blob.Contracts.ServiceContracts.AuditLevel.View, "view", "group", groupId.ToString());
            return await _blobQueryManager.GetCustomerGroupRolesAsync(groupId).ConfigureAwait(false);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "group", Operation = "view")]
        public async Task<IEnumerable<CustomerGroupUserListItem>> GetCustomerGroupUsersAsync(Guid groupId)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            await _blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), Blob.Contracts.ServiceContracts.AuditLevel.View, "view", "group", groupId.ToString());
            return await _blobQueryManager.GetCustomerGroupUsersAsync(groupId).ConfigureAwait(false);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "device", Operation = "view")]
        public async Task<MonitorListViewModel> GetMonitorListViewModelAsync(Guid deviceId)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            await _blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), Blob.Contracts.ServiceContracts.AuditLevel.View, "view", "device", deviceId.ToString());
            return await _blobQueryManager.GetMonitorListViewModelAsync(deviceId).ConfigureAwait(false);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "customer", Operation = "view")]
        public async Task<CustomerPageViewModel> GetCustomerPageViewModelAsync(Guid searchId, int pageNum = 1, int pageSize = 10)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            await _blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), Blob.Contracts.ServiceContracts.AuditLevel.View, "view", "device", searchId.ToString());
            return await _blobQueryManager.GetCustomerPageViewModelAsync(searchId, pageNum, pageSize).ConfigureAwait(false);
        }
    }
}