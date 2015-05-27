using System;
using System.Collections.Generic;
using System.IdentityModel.Services;
using System.Security.Claims;
using System.Security.Permissions;
using System.ServiceModel;
using System.Threading.Tasks;
using Blob.Contracts.Models.ViewModels;
using Blob.Contracts.ServiceContracts;
using Blob.Security.Extensions;

namespace Blob.Services.Before
{
    [ServiceBehavior]
    //[GlobalErrorBehaviorAttribute(typeof(GlobalErrorHandler))]
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
        public async Task<DashDevicesLargeVm> GetDashDevicesLargeVmAsync(Guid customerId, int pageNum, int pageSize)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            await _blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), Blob.Contracts.ServiceContracts.AuditLevel.View, "view", "customer", customerId.ToString());
            return await _blobQueryManager.GetDashDevicesLargeVmAsync(customerId, pageNum, pageSize).ConfigureAwait(false);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "customer", Operation = "disable")]
        public async Task<CustomerDisableVm> GetCustomerDisableVmAsync(Guid customerId)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            await _blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), Blob.Contracts.ServiceContracts.AuditLevel.View, "disableview", "customer", customerId.ToString());
            return await _blobQueryManager.GetCustomerDisableVmAsync(customerId).ConfigureAwait(false);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "customer", Operation = "enable")]
        public async Task<CustomerEnableVm> GetCustomerEnableVmAsync(Guid customerId)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            await _blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), Blob.Contracts.ServiceContracts.AuditLevel.View, "enableview", "customer", customerId.ToString());
            return await _blobQueryManager.GetCustomerEnableVmAsync(customerId).ConfigureAwait(false);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "customer", Operation = "view")]
        public async Task<CustomerSingleVm> GetCustomerSingleVmAsync(Guid customerId)//, int devicePageSize, int devicePageNum, int userPageSize, int userPageNum)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            await _blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), Blob.Contracts.ServiceContracts.AuditLevel.View, "view", "customer", customerId.ToString());
            return await _blobQueryManager.GetCustomerSingleVmAsync(customerId).ConfigureAwait(false);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "customer", Operation = "update")]
        public async Task<CustomerUpdateVm> GetCustomerUpdateVmAsync(Guid customerId)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            await _blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), Blob.Contracts.ServiceContracts.AuditLevel.View, "editview", "customer", customerId.ToString());
            return await _blobQueryManager.GetCustomerUpdateVmAsync(customerId).ConfigureAwait(false);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "device", Operation = "issueCommand")]
        public IEnumerable<DeviceCommandVm> GetDeviceCommandVmList()
        {
            var identity = ClaimsPrincipal.Current.Identity;
            Task.WaitAll(_blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), Blob.Contracts.ServiceContracts.AuditLevel.View, "list", "deviceCommand", "all"));
            return _blobQueryManager.GetDeviceCommandVmList();
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "device", Operation = "issueCommand")]
        public DeviceCommandIssueVm GetDeviceCommandIssueVm(Guid deviceId, string commandType)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            Task.WaitAll(_blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), Blob.Contracts.ServiceContracts.AuditLevel.View, "view", "deviceCommand", commandType));
            return _blobQueryManager.GetDeviceCommandIssueVm(deviceId, commandType);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "device", Operation = "disable")]
        public async Task<DeviceDisableVm> GetDeviceDisableVmAsync(Guid deviceId)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            await _blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), Blob.Contracts.ServiceContracts.AuditLevel.View, "disableview", "device", deviceId.ToString());
            return await _blobQueryManager.GetDeviceDisableVmAsync(deviceId).ConfigureAwait(false);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "device", Operation = "enable")]
        public async Task<DeviceEnableVm> GetDeviceEnableVmAsync(Guid deviceId)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            await _blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), Blob.Contracts.ServiceContracts.AuditLevel.View, "enableview", "device", deviceId.ToString());
            return await _blobQueryManager.GetDeviceEnableVmAsync(deviceId).ConfigureAwait(false);
        }
        
        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "device", Operation = "view")]
        public async Task<DevicePageVm> GetDevicePageVmAsync(Guid customerId, int pageNum, int pageSize)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            await _blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), Blob.Contracts.ServiceContracts.AuditLevel.View, "view", "device", customerId.ToString());
            return await _blobQueryManager.GetDevicePageVmAsync(customerId, pageNum, pageSize).ConfigureAwait(false);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "device", Operation = "view")]
        public async Task<DeviceSingleVm> GetDeviceSingleVmAsync(Guid deviceId)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            await _blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), Blob.Contracts.ServiceContracts.AuditLevel.View, "view", "device", deviceId.ToString());
            return await _blobQueryManager.GetDeviceSingleVmAsync(deviceId).ConfigureAwait(false);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "device", Operation = "update")]
        public async Task<DeviceUpdateVm> GetDeviceUpdateVmAsync(Guid deviceId)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            await _blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), Blob.Contracts.ServiceContracts.AuditLevel.View, "updateview", "device", deviceId.ToString());
            return await _blobQueryManager.GetDeviceUpdateVmAsync(deviceId).ConfigureAwait(false);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "performance", Operation = "delete")]
        public async Task<PerformanceRecordDeleteVm> GetPerformanceRecordDeleteVmAsync(long recordId)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            await _blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), Blob.Contracts.ServiceContracts.AuditLevel.View, "deleteview", "performance", recordId.ToString());
            return await _blobQueryManager.GetPerformanceRecordDeleteVmAsync(recordId).ConfigureAwait(false);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "performance", Operation = "view")]
        public async Task<PerformanceRecordPageVm> GetPerformanceRecordPageVmAsync(Guid deviceId, int pageNum, int pageSize)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            await _blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), Blob.Contracts.ServiceContracts.AuditLevel.View, "view", "performance", deviceId.ToString());
            return await _blobQueryManager.GetPerformanceRecordPageVmAsync(deviceId, pageNum, pageSize).ConfigureAwait(false);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "performance", Operation = "view")]
        public async Task<PerformanceRecordPageVm> GetPerformanceRecordPageVmForStatusAsync(long recordId, int pageNum, int pageSize)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            await _blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), Blob.Contracts.ServiceContracts.AuditLevel.View, "view", "performance", recordId.ToString());
            return await _blobQueryManager.GetPerformanceRecordPageVmForStatusAsync(recordId, pageNum, pageSize).ConfigureAwait(false);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "performance", Operation = "view")]
        public async Task<PerformanceRecordSingleVm> GetPerformanceRecordSingleVmAsync(long recordId)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            await _blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), Blob.Contracts.ServiceContracts.AuditLevel.View, "view", "performance", recordId.ToString());
            return await _blobQueryManager.GetPerformanceRecordSingleVmAsync(recordId).ConfigureAwait(false);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "status", Operation = "delete")]
        public async Task<StatusRecordDeleteVm> GetStatusRecordDeleteVmAsync(long recordId)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            await _blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), Blob.Contracts.ServiceContracts.AuditLevel.View, "deleteview", "status", recordId.ToString());
            return await _blobQueryManager.GetStatusRecordDeleteVmAsync(recordId).ConfigureAwait(false);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "status", Operation = "view")]
        public async Task<StatusRecordPageVm> GetStatusRecordPageVmAsync(Guid deviceId, int pageNum, int pageSize)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            await _blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), Blob.Contracts.ServiceContracts.AuditLevel.View, "view", "status", deviceId.ToString());
            return await _blobQueryManager.GetStatusRecordPageVmAsync(deviceId, pageNum, pageSize).ConfigureAwait(false);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "status", Operation = "view")]
        public async Task<StatusRecordSingleVm> GetStatusRecordSingleVmAsync(long recordId)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            await _blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), Blob.Contracts.ServiceContracts.AuditLevel.View, "view", "status", recordId.ToString());
            return await _blobQueryManager.GetStatusRecordSingleVmAsync(recordId).ConfigureAwait(false);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "user", Operation = "disable")]
        public async Task<UserDisableVm> GetUserDisableVmAsync(Guid userId)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            await _blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), Blob.Contracts.ServiceContracts.AuditLevel.View, "disableview", "user", userId.ToString());
            return await _blobQueryManager.GetUserDisableVmAsync(userId).ConfigureAwait(false);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "user", Operation = "enable")]
        public async Task<UserEnableVm> GetUserEnableVmAsync(Guid userId)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            await _blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), Blob.Contracts.ServiceContracts.AuditLevel.View, "enableview", "user", userId.ToString());
            return await _blobQueryManager.GetUserEnableVmAsync(userId).ConfigureAwait(false);
        }
        
        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "user", Operation = "view")]
        public async Task<UserPageVm> GetUserPageVmAsync(Guid customerId, int pageNum = 1, int pageSize = 10)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            await _blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), Blob.Contracts.ServiceContracts.AuditLevel.View, "view", "user", customerId.ToString());
            return await _blobQueryManager.GetUserPageVmAsync(customerId, pageNum, pageSize).ConfigureAwait(false);
        }
        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "user", Operation = "view")]
        public async Task<UserSingleVm> GetUserSingleVmAsync(Guid userId)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            await _blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), Blob.Contracts.ServiceContracts.AuditLevel.View, "view", "user", userId.ToString());
            return await _blobQueryManager.GetUserSingleVmAsync(userId).ConfigureAwait(false);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "user", Operation = "update")]
        public async Task<UserUpdateVm> GetUserUpdateVmAsync(Guid userId)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            await _blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), Blob.Contracts.ServiceContracts.AuditLevel.View, "updateview", "user", userId.ToString());
            return await _blobQueryManager.GetUserUpdateVmAsync(userId).ConfigureAwait(false);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "user", Operation = "update")]
        public async Task<UserUpdatePasswordVm> GetUserUpdatePasswordVmAsync(Guid userId)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            await _blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), Blob.Contracts.ServiceContracts.AuditLevel.View, "updateview", "user", userId.ToString());
            return await _blobQueryManager.GetUserUpdatePasswordVmAsync(userId).ConfigureAwait(false);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "group", Operation = "delete")]
        public async Task<CustomerGroupDeleteVm> GetCustomerGroupDeleteVmAsync(Guid groupId)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            await _blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), Blob.Contracts.ServiceContracts.AuditLevel.View, "delete", "group", groupId.ToString());
            return await _blobQueryManager.GetCustomerGroupDeleteVmAsync(groupId).ConfigureAwait(false);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "group", Operation = "view")]
        public async Task<CustomerGroupSingleVm> GetCustomerGroupSingleVmAsync(Guid groupId)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            await _blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), Blob.Contracts.ServiceContracts.AuditLevel.View, "view", "group", groupId.ToString());
            return await _blobQueryManager.GetCustomerGroupSingleVmAsync(groupId).ConfigureAwait(false);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "group", Operation = "update")]
        public async Task<CustomerGroupUpdateVm> GetCustomerGroupUpdateVmAsync(Guid groupId)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            await _blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), Blob.Contracts.ServiceContracts.AuditLevel.View, "update", "group", groupId.ToString());
            return await _blobQueryManager.GetCustomerGroupUpdateVmAsync(groupId).ConfigureAwait(false);
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
        public async Task<CustomerGroupPageVm> GetCustomerGroupPageVmAsync(Guid customerId, int pageNum = 1, int pageSize = 10)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            await _blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), Blob.Contracts.ServiceContracts.AuditLevel.View, "view", "group", customerId.ToString());
            return await _blobQueryManager.GetCustomerGroupPageVmAsync(customerId, pageNum, pageSize).ConfigureAwait(false);
        }
    }
}
