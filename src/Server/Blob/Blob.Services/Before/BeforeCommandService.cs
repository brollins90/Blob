using System.IdentityModel.Services;
using System.Security.Claims;
using System.Security.Permissions;
using System.ServiceModel;
using System.Threading.Tasks;
using Blob.Contracts.Models;
using Blob.Contracts.ServiceContracts;
using Blob.Security.Extensions;
using AuditLevel = Blob.Contracts.ServiceContracts.AuditLevel;

namespace Blob.Services.Before
{
    [ServiceBehavior]
    //[GlobalErrorBehaviorAttribute(typeof(GlobalErrorHandler))]
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
        public async Task RegisterCustomerAsync(RegisterCustomerDto dto)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            await _blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), AuditLevel.Edit, "create", "customer", dto.CustomerId.ToString());
            await _blobCommandManager.RegisterCustomerAsync(dto).ConfigureAwait(false);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "customer", Operation = "disable")]
        public async Task DisableCustomerAsync(DisableCustomerDto dto)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            await _blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), AuditLevel.Edit, "disable", "customer", dto.CustomerId.ToString());
            await _blobCommandManager.DisableCustomerAsync(dto).ConfigureAwait(false);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "customer", Operation = "enable")]
        public async Task EnableCustomerAsync(EnableCustomerDto dto)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            await _blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), AuditLevel.Edit, "enable", "customer", dto.CustomerId.ToString());
            await _blobCommandManager.EnableCustomerAsync(dto).ConfigureAwait(false);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "customer", Operation = "update")]
        public async Task UpdateCustomerAsync(UpdateCustomerDto dto)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            await _blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), AuditLevel.Edit, "update", "customer", dto.CustomerId.ToString());
            await _blobCommandManager.UpdateCustomerAsync(dto).ConfigureAwait(false);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "device", Operation = "issueCommand")]
        public async Task IssueCommandAsync(IssueDeviceCommandDto dto)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            await _blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), AuditLevel.IssueCommand, "issueCommand", "device", dto.DeviceId.ToString());
            await _blobCommandManager.IssueCommandAsync(dto).ConfigureAwait(false);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "device", Operation = "disable")]
        public async Task DisableDeviceAsync(DisableDeviceDto dto)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            await _blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), AuditLevel.Edit, "disable", "device", dto.DeviceId.ToString());
            await _blobCommandManager.DisableDeviceAsync(dto).ConfigureAwait(false);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "device", Operation = "enable")]
        public async Task EnableDeviceAsync(EnableDeviceDto dto)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            await _blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), AuditLevel.Edit, "enable", "device", dto.DeviceId.ToString());
            await _blobCommandManager.EnableDeviceAsync(dto).ConfigureAwait(false);
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
        public async Task UpdateDeviceAsync(UpdateDeviceDto dto)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            await _blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), AuditLevel.Edit, "update", "device", dto.DeviceId.ToString());
            await _blobCommandManager.UpdateDeviceAsync(dto).ConfigureAwait(false);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "performance", Operation = "add")]
        public async Task AddPerformanceRecordAsync(AddPerformanceRecordDto dto)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            await _blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), AuditLevel.Create, "add", "performance", dto.DeviceId.ToString());
            await _blobCommandManager.AddPerformanceRecordAsync(dto).ConfigureAwait(false);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "performance", Operation = "delete")]
        public async Task DeletePerformanceRecordAsync(DeletePerformanceRecordDto dto)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            await _blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), AuditLevel.Delete, "delete", "performance", dto.RecordId.ToString());
            await _blobCommandManager.DeletePerformanceRecordAsync(dto).ConfigureAwait(false);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "status", Operation = "add")]
        public async Task AddStatusRecordAsync(AddStatusRecordDto dto)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            await _blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), AuditLevel.Create, "add", "status", dto.DeviceId.ToString());
            await _blobCommandManager.AddStatusRecordAsync(dto).ConfigureAwait(false);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "status", Operation = "delete")]
        public async Task DeleteStatusRecordAsync(DeleteStatusRecordDto dto)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            await _blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), AuditLevel.Delete, "delete", "status", dto.RecordId.ToString());
            await _blobCommandManager.DeleteStatusRecordAsync(dto).ConfigureAwait(false);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "user", Operation = "create")]
        public async Task CreateUserAsync(CreateUserDto dto)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            await _blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), AuditLevel.Edit, "create", "user", dto.UserId.ToString());
            await _blobCommandManager.CreateUserAsync(dto).ConfigureAwait(false);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "user", Operation = "disable")]
        public async Task DisableUserAsync(DisableUserDto dto)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            await _blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), AuditLevel.Edit, "disable", "user", dto.UserId.ToString());
            await _blobCommandManager.DisableUserAsync(dto).ConfigureAwait(false);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "user", Operation = "enable")]
        public async Task EnableUserAsync(EnableUserDto dto)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            await _blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), AuditLevel.Edit, "enable", "user", dto.UserId.ToString());
            await _blobCommandManager.EnableUserAsync(dto).ConfigureAwait(false);
        }

        [OperationBehavior]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Resource = "user", Operation = "update")]
        public async Task UpdateUserAsync(UpdateUserDto dto)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            await _blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), AuditLevel.Edit, "update", "user", dto.UserId.ToString());
            await _blobCommandManager.UpdateUserAsync(dto).ConfigureAwait(false);
        }
    }
}
