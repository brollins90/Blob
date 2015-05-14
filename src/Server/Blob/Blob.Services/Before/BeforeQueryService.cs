using System;
using System.Collections.Generic;
using System.IdentityModel.Services;
using System.Security.Permissions;
using System.ServiceModel;
using System.Threading.Tasks;
using Blob.Contracts.Models.ViewModels;
using Blob.Contracts.ServiceContracts;

namespace Blob.Services.Before
{
    [ServiceBehavior]
    //[ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "service", Operation = "create")]
    public class BeforeQueryService : IBlobQueryManager
    {
        private readonly IBlobQueryManager _blobQueryManager;

        public BeforeQueryService(IBlobQueryManager blobQueryManager)
        {
            _blobQueryManager = blobQueryManager;
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "customer", Operation = "disable")]
        public async Task<CustomerDisableVm> GetCustomerDisableVmAsync(Guid customerId)
        {
            return await _blobQueryManager.GetCustomerDisableVmAsync(customerId).ConfigureAwait(false);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "customer", Operation = "enable")]
        public async Task<CustomerEnableVm> GetCustomerEnableVmAsync(Guid customerId)
        {
            return await _blobQueryManager.GetCustomerEnableVmAsync(customerId).ConfigureAwait(false);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "customer", Operation = "view")]
        public async Task<CustomerSingleVm> GetCustomerSingleVmAsync(Guid customerId)
        {
            return await _blobQueryManager.GetCustomerSingleVmAsync(customerId).ConfigureAwait(false);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "customer", Operation = "update")]
        public async Task<CustomerUpdateVm> GetCustomerUpdateVmAsync(Guid customerId)
        {
            return await _blobQueryManager.GetCustomerUpdateVmAsync(customerId).ConfigureAwait(false);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "device", Operation = "issueCommand")]
        public IEnumerable<DeviceCommandVm> GetDeviceCommandVmList()
        {
            return _blobQueryManager.GetDeviceCommandVmList();
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "device", Operation = "issueCommand")]
        public DeviceCommandIssueVm GetDeviceCommandIssueVm(Guid deviceId, string commandType)
        {
            return _blobQueryManager.GetDeviceCommandIssueVm(deviceId, commandType);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "device", Operation = "disable")]
        public async Task<DeviceDisableVm> GetDeviceDisableVmAsync(Guid deviceId)
        {
            return await _blobQueryManager.GetDeviceDisableVmAsync(deviceId).ConfigureAwait(false);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "device", Operation = "enable")]
        public async Task<DeviceEnableVm> GetDeviceEnableVmAsync(Guid deviceId)
        {
            return await _blobQueryManager.GetDeviceEnableVmAsync(deviceId).ConfigureAwait(false);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "device", Operation = "view")]
        public async Task<DeviceSingleVm> GetDeviceSingleVmAsync(Guid deviceId)
        {
            return await _blobQueryManager.GetDeviceSingleVmAsync(deviceId).ConfigureAwait(false);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "device", Operation = "update")]
        public async Task<DeviceUpdateVm> GetDeviceUpdateVmAsync(Guid deviceId)
        {
            return await _blobQueryManager.GetDeviceUpdateVmAsync(deviceId).ConfigureAwait(false);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "performance", Operation = "delete")]
        public async Task<PerformanceRecordDeleteVm> GetPerformanceRecordDeleteVmAsync(long recordId)
        {
            return await _blobQueryManager.GetPerformanceRecordDeleteVmAsync(recordId).ConfigureAwait(false);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "performance", Operation = "view")]
        public async Task<PerformanceRecordSingleVm> GetPerformanceRecordSingleVmAsync(long recordId)
        {
            return await _blobQueryManager.GetPerformanceRecordSingleVmAsync(recordId).ConfigureAwait(false);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "status", Operation = "delete")]
        public async Task<StatusRecordDeleteVm> GetStatusRecordDeleteVmAsync(long recordId)
        {
            return await _blobQueryManager.GetStatusRecordDeleteVmAsync(recordId).ConfigureAwait(false);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "status", Operation = "view")]
        public async Task<StatusRecordSingleVm> GetStatusRecordSingleVmAsync(long recordId)
        {
            return await _blobQueryManager.GetStatusRecordSingleVmAsync(recordId).ConfigureAwait(false);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "user", Operation = "disable")]
        public async Task<UserDisableVm> GetUserDisableVmAsync(Guid userId)
        {
            return await _blobQueryManager.GetUserDisableVmAsync(userId).ConfigureAwait(false);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "user", Operation = "enable")]
        public async Task<UserEnableVm> GetUserEnableVmAsync(Guid userId)
        {
            return await _blobQueryManager.GetUserEnableVmAsync(userId).ConfigureAwait(false);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "user", Operation = "view")]
        public async Task<UserSingleVm> GetUserSingleVmAsync(Guid userId)
        {
            return await _blobQueryManager.GetUserSingleVmAsync(userId).ConfigureAwait(false);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "user", Operation = "update")]
        public async Task<UserUpdateVm> GetUserUpdateVmAsync(Guid userId)
        {
            return await _blobQueryManager.GetUserUpdateVmAsync(userId).ConfigureAwait(false);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "user", Operation = "update")]
        public async Task<UserUpdatePasswordVm> GetUserUpdatePasswordVmAsync(Guid userId)
        {
            return await _blobQueryManager.GetUserUpdatePasswordVmAsync(userId).ConfigureAwait(false);
        }
    }
}
