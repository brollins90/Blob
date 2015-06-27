using System.IdentityModel.Services;
using System.Security.Claims;
using System.Security.Permissions;
using System.ServiceModel;
using System.Threading.Tasks;
using Blob.Contracts.Models;
using Blob.Contracts.ServiceContracts;
using Blob.Core.Extensions;
using AuditLevel = Blob.Contracts.ServiceContracts.AuditLevel;

namespace Blob.Services.Before
{
    [ServiceBehavior]
    [GlobalErrorBehavior(typeof(GlobalErrorHandler))]
    //[ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "service", Operation = "create")]
    public class BeforeCommandService : IBlobCommandManager
    {
        private readonly IBlobAuditor _blobAuditor;
        private readonly IBlobCommandManager _blobCommandManager;

        public BeforeCommandService(IBlobCommandManager blobCommandManager, IBlobAuditor blobAuditor)
        {
            _blobCommandManager = blobCommandManager;
            _blobAuditor = blobAuditor;
        }

        [OperationBehavior]
        //[ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "customer", Operation = "create")]
        public async Task<BlobResultDto> RegisterCustomerAsync(RegisterCustomerDto dto)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            await _blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), AuditLevel.Edit, "create", "customer", dto.CustomerId.ToString());
            return await _blobCommandManager.RegisterCustomerAsync(dto).ConfigureAwait(false);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "customer", Operation = "disable")]
        public async Task<BlobResultDto> DisableCustomerAsync(DisableCustomerDto dto)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            await _blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), AuditLevel.Edit, "disable", "customer", dto.CustomerId.ToString());
            return await _blobCommandManager.DisableCustomerAsync(dto).ConfigureAwait(false);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "customer", Operation = "enable")]
        public async Task<BlobResultDto> EnableCustomerAsync(EnableCustomerDto dto)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            await _blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), AuditLevel.Edit, "enable", "customer", dto.CustomerId.ToString());
            return await _blobCommandManager.EnableCustomerAsync(dto).ConfigureAwait(false);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "customer", Operation = "update")]
        public async Task<BlobResultDto> UpdateCustomerAsync(UpdateCustomerDto dto)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            await _blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), AuditLevel.Edit, "update", "customer", dto.CustomerId.ToString());
            return await _blobCommandManager.UpdateCustomerAsync(dto).ConfigureAwait(false);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "device", Operation = "issueCommand")]
        public async Task<BlobResultDto> IssueCommandAsync(IssueDeviceCommandDto dto)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            await _blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), AuditLevel.IssueCommand, "issueCommand", "device", dto.DeviceId.ToString());
            return await _blobCommandManager.IssueCommandAsync(dto).ConfigureAwait(false);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "device", Operation = "disable")]
        public async Task<BlobResultDto> DisableDeviceAsync(DisableDeviceDto dto)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            await _blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), AuditLevel.Edit, "disable", "device", dto.DeviceId.ToString());
            return await _blobCommandManager.DisableDeviceAsync(dto).ConfigureAwait(false);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "device", Operation = "enable")]
        public async Task<BlobResultDto> EnableDeviceAsync(EnableDeviceDto dto)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            await _blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), AuditLevel.Edit, "enable", "device", dto.DeviceId.ToString());
            return await _blobCommandManager.EnableDeviceAsync(dto).ConfigureAwait(false);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "device", Operation = "register")]
        public async Task<RegisterDeviceResponseDto> RegisterDeviceAsync(RegisterDeviceDto dto)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            await _blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), AuditLevel.Create, "register", "device", dto.DeviceId.ToString());
            return await _blobCommandManager.RegisterDeviceAsync(dto).ConfigureAwait(false);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "device", Operation = "update")]
        public async Task<BlobResultDto> UpdateDeviceAsync(UpdateDeviceDto dto)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            await _blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), AuditLevel.Edit, "update", "device", dto.DeviceId.ToString());
            return await _blobCommandManager.UpdateDeviceAsync(dto).ConfigureAwait(false);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "performance", Operation = "add")]
        public async Task<BlobResultDto> AddPerformanceRecordAsync(AddPerformanceRecordDto dto)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            await _blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), AuditLevel.Create, "add", "performance", dto.DeviceId.ToString());
            return await _blobCommandManager.AddPerformanceRecordAsync(dto).ConfigureAwait(false);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "performance", Operation = "delete")]
        public async Task<BlobResultDto> DeletePerformanceRecordAsync(DeletePerformanceRecordDto dto)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            await _blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), AuditLevel.Delete, "delete", "performance", dto.RecordId.ToString());
            return await _blobCommandManager.DeletePerformanceRecordAsync(dto).ConfigureAwait(false);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "status", Operation = "add")]
        public async Task<BlobResultDto> AddStatusRecordAsync(AddStatusRecordDto dto)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            await _blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), AuditLevel.Create, "add", "status", dto.DeviceId.ToString());
            return await _blobCommandManager.AddStatusRecordAsync(dto).ConfigureAwait(false);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "status", Operation = "delete")]
        public async Task<BlobResultDto> DeleteStatusRecordAsync(DeleteStatusRecordDto dto)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            await _blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), AuditLevel.Delete, "delete", "status", dto.RecordId.ToString());
            return await _blobCommandManager.DeleteStatusRecordAsync(dto).ConfigureAwait(false);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "user", Operation = "create")]
        public async Task<BlobResultDto> CreateUserAsync(CreateUserDto dto)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            await _blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), AuditLevel.Edit, "create", "user", dto.UserId.ToString());
            return await _blobCommandManager.CreateUserAsync(dto).ConfigureAwait(false);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "user", Operation = "disable")]
        public async Task<BlobResultDto> DisableUserAsync(DisableUserDto dto)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            await _blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), AuditLevel.Edit, "disable", "user", dto.UserId.ToString());
            return await _blobCommandManager.DisableUserAsync(dto).ConfigureAwait(false);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "user", Operation = "enable")]
        public async Task<BlobResultDto> EnableUserAsync(EnableUserDto dto)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            await _blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), AuditLevel.Edit, "enable", "user", dto.UserId.ToString());
            return await _blobCommandManager.EnableUserAsync(dto).ConfigureAwait(false);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "user", Operation = "update")]
        public async Task<BlobResultDto> UpdateUserAsync(UpdateUserDto dto)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            await _blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), AuditLevel.Edit, "update", "user", dto.UserId.ToString());
            return await _blobCommandManager.UpdateUserAsync(dto).ConfigureAwait(false);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "group", Operation = "create")]
        public async Task<BlobResultDto> CreateCustomerGroupAsync(CreateCustomerGroupDto dto)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            await _blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), AuditLevel.Edit, "create", "group", dto.GroupId.ToString());
            return await _blobCommandManager.CreateCustomerGroupAsync(dto).ConfigureAwait(false);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "group", Operation = "delete")]
        public async Task<BlobResultDto> DeleteCustomerGroupAsync(DeleteCustomerGroupDto dto)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            await _blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), AuditLevel.Edit, "delete", "group", dto.GroupId.ToString());
            return await _blobCommandManager.DeleteCustomerGroupAsync(dto).ConfigureAwait(false);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "group", Operation = "edit")]
        public async Task<BlobResultDto> UpdateCustomerGroupAsync(UpdateCustomerGroupDto dto)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            await _blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), AuditLevel.Edit, "update", "group", dto.GroupId.ToString());
            return await _blobCommandManager.UpdateCustomerGroupAsync(dto).ConfigureAwait(false);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "group", Operation = "edit")]
        public async Task<BlobResultDto> AddRoleToCustomerGroupAsync(AddRoleToCustomerGroupDto dto)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            await _blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), AuditLevel.Edit, "edit", "group", dto.GroupId.ToString());
            return await _blobCommandManager.AddRoleToCustomerGroupAsync(dto).ConfigureAwait(false);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "group", Operation = "edit")]
        public async Task<BlobResultDto> AddUserToCustomerGroupAsync(AddUserToCustomerGroupDto dto)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            await _blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), AuditLevel.Edit, "edit", "group", dto.GroupId.ToString());
            return await _blobCommandManager.AddUserToCustomerGroupAsync(dto).ConfigureAwait(false);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "group", Operation = "edit")]
        public async Task<BlobResultDto> RemoveRoleFromCustomerGroupAsync(RemoveRoleFromCustomerGroupDto dto)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            await _blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), AuditLevel.Edit, "edit", "group", dto.GroupId.ToString());
            return await _blobCommandManager.RemoveRoleFromCustomerGroupAsync(dto).ConfigureAwait(false);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "group", Operation = "edit")]
        public async Task<BlobResultDto> RemoveUserFromCustomerGroupAsync(RemoveUserFromCustomerGroupDto dto)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            await _blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), AuditLevel.Edit, "edit", "group", dto.GroupId.ToString());
            return await _blobCommandManager.RemoveUserFromCustomerGroupAsync(dto).ConfigureAwait(false);
        }
    }
}
