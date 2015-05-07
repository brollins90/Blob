using System;
using System.Security.Permissions;
using System.ServiceModel;
using System.Threading.Tasks;
using Blob.Contracts.Blob;

namespace Blob.Services.Before
{
    [ServiceBehavior]
    //[PrincipalPermission(SecurityAction.Demand, Authenticated = true)]
    public class BeforeQueryService : IBlobQueryManager
    {
        private readonly IBlobQueryManager _blobQueryManager;

        public BeforeQueryService(IBlobQueryManager blobQueryManager)
        {
            _blobQueryManager = blobQueryManager;
        }

        [OperationBehavior]
        [PrincipalPermission(SecurityAction.Assert, Role = "Customer")]
        public async Task<Contracts.Dto.ViewModels.CustomerDisableVm> GetCustomerDisableVmAsync(Guid customerId)
        {
            return await _blobQueryManager.GetCustomerDisableVmAsync(customerId).ConfigureAwait(false);
        }

        [OperationBehavior]
        [PrincipalPermission(SecurityAction.Assert, Role = "Customer")]
        public async Task<Contracts.Dto.ViewModels.CustomerEnableVm> GetCustomerEnableVmAsync(Guid customerId)
        {
            return await _blobQueryManager.GetCustomerEnableVmAsync(customerId).ConfigureAwait(false);
        }

        [OperationBehavior]
        [PrincipalPermission(SecurityAction.Assert, Role = "Customer")]
        public async Task<Contracts.Dto.ViewModels.CustomerSingleVm> GetCustomerSingleVmAsync(Guid customerId)
        {
            return await _blobQueryManager.GetCustomerSingleVmAsync(customerId).ConfigureAwait(false);
        }

        [OperationBehavior]
        [PrincipalPermission(SecurityAction.Assert, Role = "Customer")]
        public async Task<Contracts.Dto.ViewModels.CustomerUpdateVm> GetCustomerUpdateVmAsync(Guid customerId)
        {
            return await _blobQueryManager.GetCustomerUpdateVmAsync(customerId).ConfigureAwait(false);
        }

        [OperationBehavior]
        [PrincipalPermission(SecurityAction.Assert, Role = "Customer")]
        public Contracts.Dto.ViewModels.DeviceCommandIssueVm GetDeviceCommandIssueVm(Guid deviceId)
        {
            return _blobQueryManager.GetDeviceCommandIssueVm(deviceId);
        }

        [OperationBehavior]
        [PrincipalPermission(SecurityAction.Assert, Role = "Customer")]
        public async Task<Contracts.Dto.ViewModels.DeviceDisableVm> GetDeviceDisableVmAsync(Guid deviceId)
        {
            return await _blobQueryManager.GetDeviceDisableVmAsync(deviceId).ConfigureAwait(false);
        }

        [OperationBehavior]
        [PrincipalPermission(SecurityAction.Assert, Role = "Customer")]
        public async Task<Contracts.Dto.ViewModels.DeviceEnableVm> GetDeviceEnableVmAsync(Guid deviceId)
        {
            return await _blobQueryManager.GetDeviceEnableVmAsync(deviceId).ConfigureAwait(false);
        }

        [OperationBehavior]
        [PrincipalPermission(SecurityAction.Assert, Role = "Customer")]
        public async Task<Contracts.Dto.ViewModels.DeviceSingleVm> GetDeviceSingleVmAsync(Guid deviceId)
        {
            return await _blobQueryManager.GetDeviceSingleVmAsync(deviceId).ConfigureAwait(false);
        }

        [OperationBehavior]
        [PrincipalPermission(SecurityAction.Assert, Role = "Customer")]
        public async Task<Contracts.Dto.ViewModels.DeviceUpdateVm> GetDeviceUpdateVmAsync(Guid deviceId)
        {
            return await _blobQueryManager.GetDeviceUpdateVmAsync(deviceId).ConfigureAwait(false);
        }

        [OperationBehavior]
        [PrincipalPermission(SecurityAction.Assert, Role = "Customer")]
        public async Task<Contracts.Dto.ViewModels.PerformanceRecordDeleteVm> GetPerformanceRecordDeleteVmAsync(long recordId)
        {
            return await _blobQueryManager.GetPerformanceRecordDeleteVmAsync(recordId).ConfigureAwait(false);
        }

        [OperationBehavior]
        [PrincipalPermission(SecurityAction.Assert, Role = "Customer")]
        public async Task<Contracts.Dto.ViewModels.PerformanceRecordSingleVm> GetPerformanceRecordSingleVmAsync(long recordId)
        {
            return await _blobQueryManager.GetPerformanceRecordSingleVmAsync(recordId).ConfigureAwait(false);
        }

        [OperationBehavior]
        [PrincipalPermission(SecurityAction.Assert, Role = "Customer")]
        public async Task<Contracts.Dto.ViewModels.StatusRecordDeleteVm> GetStatusRecordDeleteVmAsync(long recordId)
        {
            return await _blobQueryManager.GetStatusRecordDeleteVmAsync(recordId).ConfigureAwait(false);
        }

        [OperationBehavior]
        [PrincipalPermission(SecurityAction.Assert, Role = "Customer")]
        public async Task<Contracts.Dto.ViewModels.StatusRecordSingleVm> GetStatusRecordSingleVmAsync(long recordId)
        {
            return await _blobQueryManager.GetStatusRecordSingleVmAsync(recordId).ConfigureAwait(false);
        }

        [OperationBehavior]
        [PrincipalPermission(SecurityAction.Assert, Role = "Customer")]
        public async Task<Contracts.Dto.ViewModels.UserDisableVm> GetUserDisableVmAsync(Guid userId)
        {
            return await _blobQueryManager.GetUserDisableVmAsync(userId).ConfigureAwait(false);
        }

        [OperationBehavior]
        [PrincipalPermission(SecurityAction.Assert, Role = "Customer")]
        public async Task<Contracts.Dto.ViewModels.UserEnableVm> GetUserEnableVmAsync(Guid userId)
        {
            return await _blobQueryManager.GetUserEnableVmAsync(userId).ConfigureAwait(false);
        }

        [OperationBehavior]
        [PrincipalPermission(SecurityAction.Assert, Role = "Customer")]
        public async Task<Contracts.Dto.ViewModels.UserSingleVm> GetUserSingleVmAsync(Guid userId)
        {
            return await _blobQueryManager.GetUserSingleVmAsync(userId).ConfigureAwait(false);
        }

        [OperationBehavior]
        [PrincipalPermission(SecurityAction.Assert, Role = "Customer")]
        public async Task<Contracts.Dto.ViewModels.UserUpdateVm> GetUserUpdateVmAsync(Guid userId)
        {
            return await _blobQueryManager.GetUserUpdateVmAsync(userId).ConfigureAwait(false);
        }

        [OperationBehavior]
        [PrincipalPermission(SecurityAction.Assert, Role = "Customer")]
        public async Task<Contracts.Dto.ViewModels.UserUpdatePasswordVm> GetUserUpdatePasswordVmAsync(Guid userId)
        {
            return await _blobQueryManager.GetUserUpdatePasswordVmAsync(userId).ConfigureAwait(false);
        }
    }
}
