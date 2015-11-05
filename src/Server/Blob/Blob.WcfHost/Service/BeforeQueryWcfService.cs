using Blob.Contracts.Models.ViewModels;
using Blob.Contracts.ServiceContracts;
using System;
using System.Collections.Generic;
using System.IdentityModel.Services;
using System.Security.Permissions;
using System.ServiceModel;
using System.Threading.Tasks;

namespace Blob.WcfHost.Service
{
    [ServiceBehavior]
    [GlobalErrorBehavior(typeof(GlobalErrorHandler))]
    public class BeforeQueryWcfService : IBlobQueryManager
    {
        IBlobQueryManager _queryManager;

        public BeforeQueryWcfService(IBlobQueryManager queryManager)
        {
            _queryManager = queryManager;
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "customer", Operation = "disable")]
        public async Task<CustomerDisableVm> GetCustomerDisableVmAsync(Guid customerId)
        {
            return await _queryManager.GetCustomerDisableVmAsync(customerId);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "customer", Operation = "enable")]
        public async Task<CustomerEnableVm> GetCustomerEnableVmAsync(Guid customerId)
        {
            return await _queryManager.GetCustomerEnableVmAsync(customerId);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "group", Operation = "create")]
        public async Task<CustomerGroupCreateVm> GetCustomerGroupCreateVmAsync(Guid customerId)
        {
            return await _queryManager.GetCustomerGroupCreateVmAsync(customerId);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "group", Operation = "delete")]
        public async Task<CustomerGroupDeleteVm> GetCustomerGroupDeleteVmAsync(Guid groupId)
        {
            return await _queryManager.GetCustomerGroupDeleteVmAsync(groupId);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "group", Operation = "view")]
        public async Task<CustomerGroupPageVm> GetCustomerGroupPageVmAsync(Guid customerId, int pageNum = 1, int pageSize = 10)
        {
            return await _queryManager.GetCustomerGroupPageVmAsync(customerId, pageNum, pageSize);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "group", Operation = "view")]
        public async Task<IEnumerable<CustomerGroupRoleListItem>> GetCustomerGroupRolesAsync(Guid groupId)
        {
            return await _queryManager.GetCustomerGroupRolesAsync(groupId);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "group", Operation = "view")]
        public async Task<CustomerGroupSingleVm> GetCustomerGroupSingleVmAsync(Guid groupId)
        {
            return await _queryManager.GetCustomerGroupSingleVmAsync(groupId);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "group", Operation = "update")]
        public async Task<CustomerGroupUpdateVm> GetCustomerGroupUpdateVmAsync(Guid groupId)
        {
            return await _queryManager.GetCustomerGroupUpdateVmAsync(groupId);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "group", Operation = "view")]
        public async Task<IEnumerable<CustomerGroupUserListItem>> GetCustomerGroupUsersAsync(Guid groupId)
        {
            return await _queryManager.GetCustomerGroupUsersAsync(groupId);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "customer", Operation = "view")]
        public async Task<CustomerPageVm> GetCustomerPageVmAsync(Guid searchId, int pageNum = 1, int pageSize = 10)
        {
            return await _queryManager.GetCustomerPageVmAsync(searchId, pageNum, pageSize);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "role", Operation = "view")]
        public async Task<IEnumerable<CustomerGroupRoleListItem>> GetCustomerRolesAsync(Guid customerId)
        {
            return await _queryManager.GetCustomerRolesAsync(customerId);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "customer", Operation = "view")]
        public async Task<CustomerSingleVm> GetCustomerSingleVmAsync(Guid customerId)
        {
            return await _queryManager.GetCustomerSingleVmAsync(customerId);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "customer", Operation = "update")]
        public async Task<CustomerUpdateVm> GetCustomerUpdateVmAsync(Guid customerId)
        {
            return await _queryManager.GetCustomerUpdateVmAsync(customerId);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "customer", Operation = "view")]
        public async Task<DashCurrentConnectionsLargeVm> GetDashCurrentConnectionsLargeVmAsync(Guid searchId, int pageNum = 1, int pageSize = 10)
        {
            return await _queryManager.GetDashCurrentConnectionsLargeVmAsync(searchId, pageNum, pageSize);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "customer", Operation = "view")]
        public async Task<DashDevicesLargeVm> GetDashDevicesLargeVmAsync(Guid searchId, int pageNum = 1, int pageSize = 10)
        {
            return await _queryManager.GetDashDevicesLargeVmAsync(searchId, pageNum, pageSize);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "device", Operation = "issueCommand")]
        public DeviceCommandIssueVm GetDeviceCommandIssueVm(Guid deviceId, string commandType)
        {
            return _queryManager.GetDeviceCommandIssueVm(deviceId, commandType);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "device", Operation = "issueCommand")]
        public IEnumerable<DeviceCommandVm> GetDeviceCommandVmList()
        {
            return _queryManager.GetDeviceCommandVmList();
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "device", Operation = "disable")]
        public async Task<DeviceDisableVm> GetDeviceDisableVmAsync(Guid deviceId)
        {
            return await _queryManager.GetDeviceDisableVmAsync(deviceId);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "device", Operation = "enable")]
        public async Task<DeviceEnableVm> GetDeviceEnableVmAsync(Guid deviceId)
        {
            return await _queryManager.GetDeviceEnableVmAsync(deviceId);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "device", Operation = "view")]
        public async Task<DevicePageVm> GetDevicePageVmAsync(Guid customerId, int pageNum = 1, int pageSize = 10)
        {
            return await _queryManager.GetDevicePageVmAsync(customerId, pageNum, pageSize);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "device", Operation = "view")]
        public async Task<DeviceSingleVm> GetDeviceSingleVmAsync(Guid deviceId)
        {
            return await _queryManager.GetDeviceSingleVmAsync(deviceId);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "device", Operation = "update")]
        public async Task<DeviceUpdateVm> GetDeviceUpdateVmAsync(Guid deviceId)
        {
            return await _queryManager.GetDeviceUpdateVmAsync(deviceId);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "device", Operation = "view")]
        public async Task<MonitorListVm> GetMonitorListVmAsync(Guid deviceId)
        {
            return await _queryManager.GetMonitorListVmAsync(deviceId);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "performance", Operation = "delete")]
        public async Task<PerformanceRecordDeleteVm> GetPerformanceRecordDeleteVmAsync(long recordId)
        {
            return await _queryManager.GetPerformanceRecordDeleteVmAsync(recordId);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "performance", Operation = "view")]
        public async Task<PerformanceRecordPageVm> GetPerformanceRecordPageVmAsync(Guid deviceId, int pageNum = 1, int pageSize = 10)
        {
            return await _queryManager.GetPerformanceRecordPageVmAsync(deviceId, pageNum, pageSize);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "performance", Operation = "view")]
        public async Task<PerformanceRecordPageVm> GetPerformanceRecordPageVmForStatusAsync(long recordId, int pageNum = 1, int pageSize = 10)
        {
            return await _queryManager.GetPerformanceRecordPageVmForStatusAsync(recordId, pageNum, pageSize);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "performance", Operation = "view")]
        public async Task<PerformanceRecordSingleVm> GetPerformanceRecordSingleVmAsync(long recordId)
        {
            return await _queryManager.GetPerformanceRecordSingleVmAsync(recordId);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "status", Operation = "delete")]
        public async Task<StatusRecordDeleteVm> GetStatusRecordDeleteVmAsync(long recordId)
        {
            return await _queryManager.GetStatusRecordDeleteVmAsync(recordId);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "status", Operation = "view")]
        public async Task<StatusRecordPageVm> GetStatusRecordPageVmAsync(Guid deviceId, int pageNum = 1, int pageSize = 10)
        {
            return await _queryManager.GetStatusRecordPageVmAsync(deviceId, pageNum, pageSize);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "status", Operation = "view")]
        public async Task<StatusRecordSingleVm> GetStatusRecordSingleVmAsync(long recordId)
        {
            return await _queryManager.GetStatusRecordSingleVmAsync(recordId);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "user", Operation = "disable")]
        public async Task<UserDisableVm> GetUserDisableVmAsync(Guid userId)
        {
            return await _queryManager.GetUserDisableVmAsync(userId);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "user", Operation = "enable")]
        public async Task<UserEnableVm> GetUserEnableVmAsync(Guid userId)
        {
            return await _queryManager.GetUserEnableVmAsync(userId);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "user", Operation = "view")]
        public async Task<UserPageVm> GetUserPageVmAsync(Guid customerId, int pageNum = 1, int pageSize = 10)
        {
            return await _queryManager.GetUserPageVmAsync(customerId, pageNum, pageSize);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "user", Operation = "view")]
        public async Task<UserSingleVm> GetUserSingleVmAsync(Guid userId)
        {
            return await _queryManager.GetUserSingleVmAsync(userId);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "user", Operation = "update")]
        public async Task<UserUpdatePasswordVm> GetUserUpdatePasswordVmAsync(Guid userId)
        {
            return await _queryManager.GetUserUpdatePasswordVmAsync(userId);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "user", Operation = "update")]
        public async Task<UserUpdateVm> GetUserUpdateVmAsync(Guid userId)
        {
            return await _queryManager.GetUserUpdateVmAsync(userId);
        }
    }
}