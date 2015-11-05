using Blob.Contracts.Models;
using Blob.Contracts.ServiceContracts;
using Blob.Services;
using System.IdentityModel.Services;
using System.Security.Permissions;
using System.ServiceModel;
using System.Threading.Tasks;

namespace Blob.WcfHost.Service
{
    [ServiceBehavior]
    [GlobalErrorBehavior(typeof(GlobalErrorHandler))]
    public class BeforeCommandWcfService : IBlobCommandManager
    {
        IBlobCommandManager _commandManager;

        public BeforeCommandWcfService(IBlobCommandManager commandManager)
        {
            _commandManager = commandManager;
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "performance", Operation = "add")]
        public async Task<BlobResultDto> AddPerformanceRecordAsync(AddPerformanceRecordDto dto)
        {
            return await _commandManager.AddPerformanceRecordAsync(dto);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "group", Operation = "edit")]
        public async Task<BlobResultDto> AddRoleToCustomerGroupAsync(AddRoleToCustomerGroupDto dto)
        {
            return await _commandManager.AddRoleToCustomerGroupAsync(dto);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "status", Operation = "add")]
        public async Task<BlobResultDto> AddStatusRecordAsync(AddStatusRecordDto dto)
        {
            return await _commandManager.AddStatusRecordAsync(dto);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "group", Operation = "edit")]
        public async Task<BlobResultDto> AddUserToCustomerGroupAsync(AddUserToCustomerGroupDto dto)
        {
            return await _commandManager.AddUserToCustomerGroupAsync(dto);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "group", Operation = "create")]
        public async Task<BlobResultDto> CreateCustomerGroupAsync(CreateCustomerGroupDto dto)
        {
            return await _commandManager.CreateCustomerGroupAsync(dto);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "user", Operation = "create")]
        public async Task<BlobResultDto> CreateUserAsync(CreateUserDto dto)
        {
            return await _commandManager.CreateUserAsync(dto);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "group", Operation = "delete")]
        public async Task<BlobResultDto> DeleteCustomerGroupAsync(DeleteCustomerGroupDto dto)
        {
            return await _commandManager.DeleteCustomerGroupAsync(dto);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "performance", Operation = "delete")]
        public async Task<BlobResultDto> DeletePerformanceRecordAsync(DeletePerformanceRecordDto dto)
        {
            return await _commandManager.DeletePerformanceRecordAsync(dto);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "status", Operation = "delete")]
        public async Task<BlobResultDto> DeleteStatusRecordAsync(DeleteStatusRecordDto dto)
        {
            return await _commandManager.DeleteStatusRecordAsync(dto);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "customer", Operation = "disable")]
        public async Task<BlobResultDto> DisableCustomerAsync(DisableCustomerDto dto)
        {
            return await _commandManager.DisableCustomerAsync(dto);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "device", Operation = "disable")]
        public async Task<BlobResultDto> DisableDeviceAsync(DisableDeviceDto dto)
        {
            return await _commandManager.DisableDeviceAsync(dto);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "user", Operation = "disable")]
        public async Task<BlobResultDto> DisableUserAsync(DisableUserDto dto)
        {
            return await _commandManager.DisableUserAsync(dto);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "customer", Operation = "enable")]
        public async Task<BlobResultDto> EnableCustomerAsync(EnableCustomerDto dto)
        {
            return await _commandManager.EnableCustomerAsync(dto);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "device", Operation = "enable")]
        public async Task<BlobResultDto> EnableDeviceAsync(EnableDeviceDto dto)
        {
            return await _commandManager.EnableDeviceAsync(dto);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "user", Operation = "enable")]
        public async Task<BlobResultDto> EnableUserAsync(EnableUserDto dto)
        {
            return await _commandManager.EnableUserAsync(dto);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "device", Operation = "issueCommand")]
        public async Task<BlobResultDto> IssueCommandAsync(IssueDeviceCommandDto dto)
        {
            return await _commandManager.IssueCommandAsync(dto);
        }

        [OperationBehavior]
        public async Task<BlobResultDto> RegisterCustomerAsync(RegisterCustomerDto dto)
        {
            return await _commandManager.RegisterCustomerAsync(dto);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "device", Operation = "register")]
        public async Task<RegisterDeviceResponseDto> RegisterDeviceAsync(RegisterDeviceDto dto)
        {
            return await _commandManager.RegisterDeviceAsync(dto);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "group", Operation = "edit")]
        public async Task<BlobResultDto> RemoveRoleFromCustomerGroupAsync(RemoveRoleFromCustomerGroupDto dto)
        {
            return await _commandManager.RemoveRoleFromCustomerGroupAsync(dto);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "group", Operation = "edit")]
        public async Task<BlobResultDto> RemoveUserFromCustomerGroupAsync(RemoveUserFromCustomerGroupDto dto)
        {
            return await _commandManager.RemoveUserFromCustomerGroupAsync(dto);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "customer", Operation = "update")]
        public async Task<BlobResultDto> UpdateCustomerAsync(UpdateCustomerDto dto)
        {
            return await _commandManager.UpdateCustomerAsync(dto);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "group", Operation = "edit")]
        public async Task<BlobResultDto> UpdateCustomerGroupAsync(UpdateCustomerGroupDto dto)
        {
            return await _commandManager.UpdateCustomerGroupAsync(dto);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "device", Operation = "update")]
        public async Task<BlobResultDto> UpdateDeviceAsync(UpdateDeviceDto dto)
        {
            return await _commandManager.UpdateDeviceAsync(dto);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "user", Operation = "update")]
        public async Task<BlobResultDto> UpdateUserAsync(UpdateUserDto dto)
        {
            return await _commandManager.UpdateUserAsync(dto);
        }
    }
}