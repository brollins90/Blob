using Blob.Contracts.Models.ViewModels;
using Blob.Contracts.ServiceContracts;
using Blob.Contracts.Services;
using Blob.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Blob.Services.Before
{
    public class BeforeQueryService : IBlobQueryManager
    {
        private readonly IBlobAuditor _blobAuditor;
        private readonly ICustomerService _customerManager;
        private readonly ICustomerGroupService _customerGroupManager;
        private readonly IDashboardService _dashboardManager;
        private readonly IDeviceService _deviceManager;
        private readonly IDeviceCommandService _deviceCommandManager;
        private readonly IPerformanceRecordService _performanceRecordManager;
        private readonly IStatusRecordService _statusRecordManager;
        private readonly IUserService _userManager2;

        public BeforeQueryService(
            IBlobAuditor blobAuditor,
            ICustomerService customerManager,
            ICustomerGroupService customerGroupManager,
            IDashboardService dashboardManager,
            IDeviceService deviceManager,
            IDeviceCommandService deviceCommandManager,
            IPerformanceRecordService performanceRecordManager,
            IStatusRecordService statusRecordManager,
            IUserService userManager2)
        {
            _blobAuditor = blobAuditor;

            _customerManager = customerManager;
            _customerGroupManager = customerGroupManager;
            _dashboardManager = dashboardManager;
            _deviceManager = deviceManager;
            _deviceCommandManager = deviceCommandManager;
            _performanceRecordManager = performanceRecordManager;
            _statusRecordManager = statusRecordManager;
            _userManager2 = userManager2;
        }

        public async Task<DashCurrentConnectionsLargeVm> GetDashCurrentConnectionsLargeVmAsync(Guid customerId, int pageNum, int pageSize)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            await _blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), Blob.Contracts.ServiceContracts.AuditLevel.View, "view", "customer", customerId.ToString());
            return await _dashboardManager.GetDashCurrentConnectionsLargeVmAsync(customerId, pageNum, pageSize).ConfigureAwait(false);
        }

        public async Task<DashDevicesLargeVm> GetDashDevicesLargeVmAsync(Guid customerId, int pageNum, int pageSize)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            await _blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), Blob.Contracts.ServiceContracts.AuditLevel.View, "view", "customer", customerId.ToString());
            return await _dashboardManager.GetDashDevicesLargeVmAsync(customerId, pageNum, pageSize).ConfigureAwait(false);
        }

        public async Task<CustomerDisableVm> GetCustomerDisableVmAsync(Guid customerId)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            await _blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), Blob.Contracts.ServiceContracts.AuditLevel.View, "disableview", "customer", customerId.ToString());
            return await _customerManager.GetCustomerDisableVmAsync(customerId).ConfigureAwait(false);
        }

        public async Task<CustomerEnableVm> GetCustomerEnableVmAsync(Guid customerId)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            await _blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), Blob.Contracts.ServiceContracts.AuditLevel.View, "enableview", "customer", customerId.ToString());
            return await _customerManager.GetCustomerEnableVmAsync(customerId).ConfigureAwait(false);
        }

        public async Task<CustomerSingleVm> GetCustomerSingleVmAsync(Guid customerId)//, int devicePageSize, int devicePageNum, int userPageSize, int userPageNum)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            await _blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), Blob.Contracts.ServiceContracts.AuditLevel.View, "view", "customer", customerId.ToString());
            return await _customerManager.GetCustomerSingleVmAsync(customerId).ConfigureAwait(false);
        }

        public async Task<CustomerUpdateVm> GetCustomerUpdateVmAsync(Guid customerId)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            await _blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), Blob.Contracts.ServiceContracts.AuditLevel.View, "editview", "customer", customerId.ToString());
            return await _customerManager.GetCustomerUpdateVmAsync(customerId).ConfigureAwait(false);
        }

        public IEnumerable<DeviceCommandVm> GetDeviceCommandVmList()
        {
            var identity = ClaimsPrincipal.Current.Identity;
            Task.WaitAll(_blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), Blob.Contracts.ServiceContracts.AuditLevel.View, "list", "deviceCommand", "all"));
            return _deviceCommandManager.GetDeviceCommandVmList();
        }

        public DeviceCommandIssueVm GetDeviceCommandIssueVm(Guid deviceId, string commandType)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            Task.WaitAll(_blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), Blob.Contracts.ServiceContracts.AuditLevel.View, "view", "deviceCommand", commandType));
            return _deviceCommandManager.GetDeviceCommandIssueVm(deviceId, commandType);
        }

        public async Task<DeviceDisableVm> GetDeviceDisableVmAsync(Guid deviceId)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            await _blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), Blob.Contracts.ServiceContracts.AuditLevel.View, "disableview", "device", deviceId.ToString());
            return await _deviceManager.GetDeviceDisableVmAsync(deviceId).ConfigureAwait(false);
        }

        public async Task<DeviceEnableVm> GetDeviceEnableVmAsync(Guid deviceId)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            await _blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), Blob.Contracts.ServiceContracts.AuditLevel.View, "enableview", "device", deviceId.ToString());
            return await _deviceManager.GetDeviceEnableVmAsync(deviceId).ConfigureAwait(false);
        }

        public async Task<DevicePageVm> GetDevicePageVmAsync(Guid customerId, int pageNum, int pageSize)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            await _blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), Blob.Contracts.ServiceContracts.AuditLevel.View, "view", "device", customerId.ToString());
            return await _deviceManager.GetDevicePageVmAsync(customerId, pageNum, pageSize).ConfigureAwait(false);
        }

        public async Task<DeviceSingleVm> GetDeviceSingleVmAsync(Guid deviceId)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            await _blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), Blob.Contracts.ServiceContracts.AuditLevel.View, "view", "device", deviceId.ToString());
            return await _deviceManager.GetDeviceSingleVmAsync(deviceId).ConfigureAwait(false);
        }

        public async Task<DeviceUpdateVm> GetDeviceUpdateVmAsync(Guid deviceId)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            await _blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), Blob.Contracts.ServiceContracts.AuditLevel.View, "updateview", "device", deviceId.ToString());
            return await _deviceManager.GetDeviceUpdateVmAsync(deviceId).ConfigureAwait(false);
        }

        public async Task<PerformanceRecordDeleteVm> GetPerformanceRecordDeleteVmAsync(long recordId)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            await _blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), Blob.Contracts.ServiceContracts.AuditLevel.View, "deleteview", "performance", recordId.ToString());
            return await _performanceRecordManager.GetPerformanceRecordDeleteVmAsync(recordId).ConfigureAwait(false);
        }

        public async Task<PerformanceRecordPageVm> GetPerformanceRecordPageVmAsync(Guid deviceId, int pageNum, int pageSize)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            await _blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), Blob.Contracts.ServiceContracts.AuditLevel.View, "view", "performance", deviceId.ToString());
            return await _performanceRecordManager.GetPerformanceRecordPageVmAsync(deviceId, pageNum, pageSize).ConfigureAwait(false);
        }

        public async Task<PerformanceRecordPageVm> GetPerformanceRecordPageVmForStatusAsync(long recordId, int pageNum, int pageSize)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            await _blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), Blob.Contracts.ServiceContracts.AuditLevel.View, "view", "performance", recordId.ToString());
            return await _performanceRecordManager.GetPerformanceRecordPageVmForStatusAsync(recordId, pageNum, pageSize).ConfigureAwait(false);
        }

        public async Task<PerformanceRecordSingleVm> GetPerformanceRecordSingleVmAsync(long recordId)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            await _blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), Blob.Contracts.ServiceContracts.AuditLevel.View, "view", "performance", recordId.ToString());
            return await _performanceRecordManager.GetPerformanceRecordSingleVmAsync(recordId).ConfigureAwait(false);
        }

        public async Task<StatusRecordDeleteVm> GetStatusRecordDeleteVmAsync(long recordId)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            await _blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), Blob.Contracts.ServiceContracts.AuditLevel.View, "deleteview", "status", recordId.ToString());
            return await _statusRecordManager.GetStatusRecordDeleteVmAsync(recordId).ConfigureAwait(false);
        }

        public async Task<StatusRecordPageVm> GetStatusRecordPageVmAsync(Guid deviceId, int pageNum, int pageSize)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            await _blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), Blob.Contracts.ServiceContracts.AuditLevel.View, "view", "status", deviceId.ToString());
            return await _statusRecordManager.GetStatusRecordPageVmAsync(deviceId, pageNum, pageSize).ConfigureAwait(false);
        }

        public async Task<StatusRecordSingleVm> GetStatusRecordSingleVmAsync(long recordId)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            await _blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), Blob.Contracts.ServiceContracts.AuditLevel.View, "view", "status", recordId.ToString());
            return await _statusRecordManager.GetStatusRecordSingleVmAsync(recordId).ConfigureAwait(false);
        }

        public async Task<UserDisableVm> GetUserDisableVmAsync(Guid userId)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            await _blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), Blob.Contracts.ServiceContracts.AuditLevel.View, "disableview", "user", userId.ToString());
            return await _userManager2.GetUserDisableVmAsync(userId).ConfigureAwait(false);
        }

        public async Task<UserEnableVm> GetUserEnableVmAsync(Guid userId)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            await _blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), Blob.Contracts.ServiceContracts.AuditLevel.View, "enableview", "user", userId.ToString());
            return await _userManager2.GetUserEnableVmAsync(userId).ConfigureAwait(false);
        }

        public async Task<UserPageVm> GetUserPageVmAsync(Guid customerId, int pageNum = 1, int pageSize = 10)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            await _blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), Blob.Contracts.ServiceContracts.AuditLevel.View, "view", "user", customerId.ToString());
            return await _userManager2.GetUserPageVmAsync(customerId, pageNum, pageSize).ConfigureAwait(false);
        }

        public async Task<UserSingleVm> GetUserSingleVmAsync(Guid userId)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            await _blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), Blob.Contracts.ServiceContracts.AuditLevel.View, "view", "user", userId.ToString());
            return await _userManager2.GetUserSingleVmAsync(userId).ConfigureAwait(false);
        }

        public async Task<UserUpdateVm> GetUserUpdateVmAsync(Guid userId)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            await _blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), Blob.Contracts.ServiceContracts.AuditLevel.View, "updateview", "user", userId.ToString());
            return await _userManager2.GetUserUpdateVmAsync(userId).ConfigureAwait(false);
        }

        public async Task<UserUpdatePasswordVm> GetUserUpdatePasswordVmAsync(Guid userId)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            await _blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), Blob.Contracts.ServiceContracts.AuditLevel.View, "updateview", "user", userId.ToString());
            return await _userManager2.GetUserUpdatePasswordVmAsync(userId).ConfigureAwait(false);
        }

        public async Task<CustomerGroupDeleteVm> GetCustomerGroupDeleteVmAsync(Guid groupId)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            await _blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), Blob.Contracts.ServiceContracts.AuditLevel.View, "delete", "group", groupId.ToString());
            return await _customerGroupManager.GetCustomerGroupDeleteVmAsync(groupId).ConfigureAwait(false);
        }

        public async Task<CustomerGroupSingleVm> GetCustomerGroupSingleVmAsync(Guid groupId)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            await _blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), Blob.Contracts.ServiceContracts.AuditLevel.View, "view", "group", groupId.ToString());
            return await _customerGroupManager.GetCustomerGroupSingleVmAsync(groupId).ConfigureAwait(false);
        }

        public async Task<CustomerGroupUpdateVm> GetCustomerGroupUpdateVmAsync(Guid groupId)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            await _blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), Blob.Contracts.ServiceContracts.AuditLevel.View, "update", "group", groupId.ToString());
            return await _customerGroupManager.GetCustomerGroupUpdateVmAsync(groupId).ConfigureAwait(false);
        }

        public async Task<IEnumerable<CustomerGroupRoleListItem>> GetCustomerRolesAsync(Guid customerId)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            await _blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), Blob.Contracts.ServiceContracts.AuditLevel.View, "view", "role", customerId.ToString());
            return await _customerGroupManager.GetCustomerRolesAsync(customerId).ConfigureAwait(false);
        }

        public async Task<CustomerGroupPageVm> GetCustomerGroupPageVmAsync(Guid customerId, int pageNum = 1, int pageSize = 10)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            await _blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), Blob.Contracts.ServiceContracts.AuditLevel.View, "view", "group", customerId.ToString());
            return await _customerGroupManager.GetCustomerGroupPageVmAsync(customerId, pageNum, pageSize).ConfigureAwait(false);
        }

        public async Task<CustomerGroupCreateVm> GetCustomerGroupCreateVmAsync(Guid customerId)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            await _blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), Blob.Contracts.ServiceContracts.AuditLevel.View, "create", "group", customerId.ToString());
            return await _customerGroupManager.GetCustomerGroupCreateVmAsync(customerId).ConfigureAwait(false);
        }

        public async Task<IEnumerable<CustomerGroupRoleListItem>> GetCustomerGroupRolesAsync(Guid groupId)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            await _blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), Blob.Contracts.ServiceContracts.AuditLevel.View, "view", "group", groupId.ToString());
            return await _customerGroupManager.GetCustomerGroupRolesAsync(groupId).ConfigureAwait(false);
        }

        public async Task<IEnumerable<CustomerGroupUserListItem>> GetCustomerGroupUsersAsync(Guid groupId)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            await _blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), Blob.Contracts.ServiceContracts.AuditLevel.View, "view", "group", groupId.ToString());
            return await _customerGroupManager.GetCustomerGroupUsersAsync(groupId).ConfigureAwait(false);
        }

        public async Task<MonitorListVm> GetMonitorListVmAsync(Guid deviceId)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            await _blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), Blob.Contracts.ServiceContracts.AuditLevel.View, "view", "device", deviceId.ToString());
            return await _statusRecordManager.GetMonitorListVmAsync(deviceId).ConfigureAwait(false);
        }

        public async Task<CustomerPageVm> GetCustomerPageVmAsync(Guid searchId, int pageNum = 1, int pageSize = 10)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            await _blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), Blob.Contracts.ServiceContracts.AuditLevel.View, "view", "device", searchId.ToString());
            return await _customerManager.GetCustomerPageVmAsync(searchId, pageNum, pageSize).ConfigureAwait(false);
        }
    }
}
