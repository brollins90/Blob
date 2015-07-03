namespace Blob.Services.Before
{
    using System.IdentityModel.Services;
    using System.Security.Claims;
    using System.Security.Permissions;
    using System.ServiceModel;
    using System.Threading.Tasks;
    using Contracts.Request;
    using Contracts.Response;
    using Contracts.ServiceContracts;
    using Core.Extensions;
    using AuditLevel = Contracts.ServiceContracts.AuditLevel;

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
        public async Task<BlobResult> RegisterCustomerAsync(RegisterCustomerRequest dto)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            await _blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), AuditLevel.Edit, "create", "customer", dto.CustomerId.ToString());
            return await _blobCommandManager.RegisterCustomerAsync(dto).ConfigureAwait(false);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "customer", Operation = "disable")]
        public async Task<BlobResult> DisableCustomerAsync(DisableCustomerRequest dto)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            await _blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), AuditLevel.Edit, "disable", "customer", dto.CustomerId.ToString());
            return await _blobCommandManager.DisableCustomerAsync(dto).ConfigureAwait(false);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "customer", Operation = "enable")]
        public async Task<BlobResult> EnableCustomerAsync(EnableCustomerRequest dto)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            await _blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), AuditLevel.Edit, "enable", "customer", dto.CustomerId.ToString());
            return await _blobCommandManager.EnableCustomerAsync(dto).ConfigureAwait(false);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "customer", Operation = "update")]
        public async Task<BlobResult> UpdateCustomerAsync(UpdateCustomerRequest dto)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            await _blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), AuditLevel.Edit, "update", "customer", dto.CustomerId.ToString());
            return await _blobCommandManager.UpdateCustomerAsync(dto).ConfigureAwait(false);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "device", Operation = "issueCommand")]
        public async Task<BlobResult> IssueCommandAsync(IssueDeviceCommandRequest dto)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            await _blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), AuditLevel.IssueCommand, "issueCommand", "device", dto.DeviceId.ToString());
            return await _blobCommandManager.IssueCommandAsync(dto).ConfigureAwait(false);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "device", Operation = "disable")]
        public async Task<BlobResult> DisableDeviceAsync(DisableDeviceRequest dto)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            await _blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), AuditLevel.Edit, "disable", "device", dto.DeviceId.ToString());
            return await _blobCommandManager.DisableDeviceAsync(dto).ConfigureAwait(false);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "device", Operation = "enable")]
        public async Task<BlobResult> EnableDeviceAsync(EnableDeviceRequest dto)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            await _blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), AuditLevel.Edit, "enable", "device", dto.DeviceId.ToString());
            return await _blobCommandManager.EnableDeviceAsync(dto).ConfigureAwait(false);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "device", Operation = "register")]
        public async Task<RegisterDeviceResponse> RegisterDeviceAsync(RegisterDeviceRequest dto)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            await _blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), AuditLevel.Create, "register", "device", dto.DeviceId.ToString());
            return await _blobCommandManager.RegisterDeviceAsync(dto).ConfigureAwait(false);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "device", Operation = "update")]
        public async Task<BlobResult> UpdateDeviceAsync(UpdateDeviceRequest dto)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            await _blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), AuditLevel.Edit, "update", "device", dto.DeviceId.ToString());
            return await _blobCommandManager.UpdateDeviceAsync(dto).ConfigureAwait(false);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "performance", Operation = "add")]
        public async Task<BlobResult> AddPerformanceRecordAsync(AddPerformanceRecordRequest dto)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            await _blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), AuditLevel.Create, "add", "performance", dto.DeviceId.ToString());
            return await _blobCommandManager.AddPerformanceRecordAsync(dto).ConfigureAwait(false);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "performance", Operation = "delete")]
        public async Task<BlobResult> DeletePerformanceRecordAsync(DeletePerformanceRecordRequest dto)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            await _blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), AuditLevel.Delete, "delete", "performance", dto.RecordId.ToString());
            return await _blobCommandManager.DeletePerformanceRecordAsync(dto).ConfigureAwait(false);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "status", Operation = "add")]
        public async Task<BlobResult> AddStatusRecordAsync(AddStatusRecordRequest dto)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            await _blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), AuditLevel.Create, "add", "status", dto.DeviceId.ToString());
            return await _blobCommandManager.AddStatusRecordAsync(dto).ConfigureAwait(false);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "status", Operation = "delete")]
        public async Task<BlobResult> DeleteStatusRecordAsync(DeleteStatusRecordRequest dto)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            await _blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), AuditLevel.Delete, "delete", "status", dto.RecordId.ToString());
            return await _blobCommandManager.DeleteStatusRecordAsync(dto).ConfigureAwait(false);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "user", Operation = "create")]
        public async Task<BlobResult> CreateUserAsync(CreateUserRequest dto)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            await _blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), AuditLevel.Edit, "create", "user", dto.UserId.ToString());
            return await _blobCommandManager.CreateUserAsync(dto).ConfigureAwait(false);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "user", Operation = "disable")]
        public async Task<BlobResult> DisableUserAsync(DisableUserRequest dto)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            await _blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), AuditLevel.Edit, "disable", "user", dto.UserId.ToString());
            return await _blobCommandManager.DisableUserAsync(dto).ConfigureAwait(false);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "user", Operation = "enable")]
        public async Task<BlobResult> EnableUserAsync(EnableUserRequest dto)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            await _blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), AuditLevel.Edit, "enable", "user", dto.UserId.ToString());
            return await _blobCommandManager.EnableUserAsync(dto).ConfigureAwait(false);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "user", Operation = "update")]
        public async Task<BlobResult> UpdateUserAsync(UpdateUserRequest dto)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            await _blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), AuditLevel.Edit, "update", "user", dto.UserId.ToString());
            return await _blobCommandManager.UpdateUserAsync(dto).ConfigureAwait(false);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "group", Operation = "create")]
        public async Task<BlobResult> CreateCustomerGroupAsync(CreateCustomerGroupRequest dto)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            await _blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), AuditLevel.Edit, "create", "group", dto.GroupId.ToString());
            return await _blobCommandManager.CreateCustomerGroupAsync(dto).ConfigureAwait(false);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "group", Operation = "delete")]
        public async Task<BlobResult> DeleteCustomerGroupAsync(DeleteCustomerGroupRequest dto)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            await _blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), AuditLevel.Edit, "delete", "group", dto.GroupId.ToString());
            return await _blobCommandManager.DeleteCustomerGroupAsync(dto).ConfigureAwait(false);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "group", Operation = "edit")]
        public async Task<BlobResult> UpdateCustomerGroupAsync(UpdateCustomerGroupRequest dto)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            await _blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), AuditLevel.Edit, "update", "group", dto.GroupId.ToString());
            return await _blobCommandManager.UpdateCustomerGroupAsync(dto).ConfigureAwait(false);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "group", Operation = "edit")]
        public async Task<BlobResult> AddRoleToCustomerGroupAsync(AddRoleToCustomerGroupRequest dto)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            await _blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), AuditLevel.Edit, "edit", "group", dto.GroupId.ToString());
            return await _blobCommandManager.AddRoleToCustomerGroupAsync(dto).ConfigureAwait(false);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "group", Operation = "edit")]
        public async Task<BlobResult> AddUserToCustomerGroupAsync(AddUserToCustomerGroupRequest dto)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            await _blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), AuditLevel.Edit, "edit", "group", dto.GroupId.ToString());
            return await _blobCommandManager.AddUserToCustomerGroupAsync(dto).ConfigureAwait(false);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "group", Operation = "edit")]
        public async Task<BlobResult> RemoveRoleFromCustomerGroupAsync(RemoveRoleFromCustomerGroupRequest dto)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            await _blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), AuditLevel.Edit, "edit", "group", dto.GroupId.ToString());
            return await _blobCommandManager.RemoveRoleFromCustomerGroupAsync(dto).ConfigureAwait(false);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "group", Operation = "edit")]
        public async Task<BlobResult> RemoveUserFromCustomerGroupAsync(RemoveUserFromCustomerGroupRequest dto)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            await _blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), AuditLevel.Edit, "edit", "group", dto.GroupId.ToString());
            return await _blobCommandManager.RemoveUserFromCustomerGroupAsync(dto).ConfigureAwait(false);
        }
    }
}