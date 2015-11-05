using Blob.Contracts.Models;
using Blob.Contracts.ServiceContracts;
using Blob.Contracts.Services;
using Blob.Core.Extensions;
using System.Security.Claims;
using System.Threading.Tasks;
using AuditLevel = Blob.Contracts.ServiceContracts.AuditLevel;

namespace Blob.Services.Before
{
    public class BeforeCommandService : IBlobCommandManager
    {
        private readonly IBlobAuditor _blobAuditor;
        private readonly ICustomerService _customerManager;
        private readonly ICustomerGroupService _customerGroupManager;
        private readonly IDeviceService _deviceManager;
        private readonly IDeviceCommandService _deviceCommandManager;
        private readonly IPerformanceRecordService _performanceRecordManager;
        private readonly IStatusRecordService _statusRecordManager;
        private readonly IUserService _userManager;

        public BeforeCommandService(
            IBlobAuditor blobAuditor,
            ICustomerService customerManager,
            ICustomerGroupService customerGroupManager,
            IDeviceService deviceManager,
            IDeviceCommandService deviceCommandManager,
            IPerformanceRecordService performanceRecordManager,
            IStatusRecordService statusRecordManager,
            IUserService userManager)
        {
            _blobAuditor = blobAuditor;

            _customerManager = customerManager;
            _customerGroupManager = customerGroupManager;
            _deviceManager = deviceManager;
            _deviceCommandManager = deviceCommandManager;
            _performanceRecordManager = performanceRecordManager;
            _statusRecordManager = statusRecordManager;
            _userManager = userManager;
        }

        public async Task<BlobResultDto> RegisterCustomerAsync(RegisterCustomerDto dto)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            await _blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), AuditLevel.Edit, "create", "customer", dto.CustomerId.ToString());
            return await _customerManager.RegisterCustomerAsync(dto).ConfigureAwait(false);
        }

        public async Task<BlobResultDto> DisableCustomerAsync(DisableCustomerDto dto)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            await _blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), AuditLevel.Edit, "disable", "customer", dto.CustomerId.ToString());
            return await _customerManager.DisableCustomerAsync(dto).ConfigureAwait(false);
        }

        public async Task<BlobResultDto> EnableCustomerAsync(EnableCustomerDto dto)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            await _blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), AuditLevel.Edit, "enable", "customer", dto.CustomerId.ToString());
            return await _customerManager.EnableCustomerAsync(dto).ConfigureAwait(false);
        }

        public async Task<BlobResultDto> UpdateCustomerAsync(UpdateCustomerDto dto)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            await _blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), AuditLevel.Edit, "update", "customer", dto.CustomerId.ToString());
            return await _customerManager.UpdateCustomerAsync(dto).ConfigureAwait(false);
        }

        public async Task<BlobResultDto> IssueCommandAsync(IssueDeviceCommandDto dto)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            await _blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), AuditLevel.IssueCommand, "issueCommand", "device", dto.DeviceId.ToString());
            return await _deviceCommandManager.IssueCommandAsync(dto).ConfigureAwait(false);
        }

        public async Task<BlobResultDto> DisableDeviceAsync(DisableDeviceDto dto)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            await _blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), AuditLevel.Edit, "disable", "device", dto.DeviceId.ToString());
            return await _deviceManager.DisableDeviceAsync(dto).ConfigureAwait(false);
        }

        public async Task<BlobResultDto> EnableDeviceAsync(EnableDeviceDto dto)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            await _blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), AuditLevel.Edit, "enable", "device", dto.DeviceId.ToString());
            return await _deviceManager.EnableDeviceAsync(dto).ConfigureAwait(false);
        }

        public async Task<RegisterDeviceResponseDto> RegisterDeviceAsync(RegisterDeviceDto dto)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            await _blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), AuditLevel.Create, "register", "device", dto.DeviceId.ToString());
            return await _deviceManager.RegisterDeviceAsync(dto).ConfigureAwait(false);
        }

        public async Task<BlobResultDto> UpdateDeviceAsync(UpdateDeviceDto dto)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            await _blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), AuditLevel.Edit, "update", "device", dto.DeviceId.ToString());
            return await _deviceManager.UpdateDeviceAsync(dto).ConfigureAwait(false);
        }

        public async Task<BlobResultDto> AddPerformanceRecordAsync(AddPerformanceRecordDto dto)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            await _blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), AuditLevel.Create, "add", "performance", dto.DeviceId.ToString());
            return await _performanceRecordManager.AddPerformanceRecordAsync(dto).ConfigureAwait(false);
        }

        public async Task<BlobResultDto> DeletePerformanceRecordAsync(DeletePerformanceRecordDto dto)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            await _blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), AuditLevel.Delete, "delete", "performance", dto.RecordId.ToString());
            return await _performanceRecordManager.DeletePerformanceRecordAsync(dto).ConfigureAwait(false);
        }

        public async Task<BlobResultDto> AddStatusRecordAsync(AddStatusRecordDto dto)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            await _blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), AuditLevel.Create, "add", "status", dto.DeviceId.ToString());
            return await _statusRecordManager.AddStatusRecordAsync(dto).ConfigureAwait(false);
        }

        public async Task<BlobResultDto> DeleteStatusRecordAsync(DeleteStatusRecordDto dto)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            await _blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), AuditLevel.Delete, "delete", "status", dto.RecordId.ToString());
            return await _statusRecordManager.DeleteStatusRecordAsync(dto).ConfigureAwait(false);
        }

        public async Task<BlobResultDto> CreateUserAsync(CreateUserDto dto)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            await _blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), AuditLevel.Edit, "create", "user", dto.UserId.ToString());
            return await _userManager.CreateUserAsync(dto).ConfigureAwait(false);
        }

        public async Task<BlobResultDto> DisableUserAsync(DisableUserDto dto)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            await _blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), AuditLevel.Edit, "disable", "user", dto.UserId.ToString());
            return await _userManager.DisableUserAsync(dto).ConfigureAwait(false);
        }

        public async Task<BlobResultDto> EnableUserAsync(EnableUserDto dto)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            await _blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), AuditLevel.Edit, "enable", "user", dto.UserId.ToString());
            return await _userManager.EnableUserAsync(dto).ConfigureAwait(false);
        }

        public async Task<BlobResultDto> UpdateUserAsync(UpdateUserDto dto)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            await _blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), AuditLevel.Edit, "update", "user", dto.UserId.ToString());
            return await _userManager.UpdateUserAsync(dto).ConfigureAwait(false);
        }

        public async Task<BlobResultDto> CreateCustomerGroupAsync(CreateCustomerGroupDto dto)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            await _blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), AuditLevel.Edit, "create", "group", dto.GroupId.ToString());
            return await _customerGroupManager.CreateCustomerGroupAsync(dto).ConfigureAwait(false);
        }

        public async Task<BlobResultDto> DeleteCustomerGroupAsync(DeleteCustomerGroupDto dto)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            await _blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), AuditLevel.Edit, "delete", "group", dto.GroupId.ToString());
            return await _customerGroupManager.DeleteCustomerGroupAsync(dto).ConfigureAwait(false);
        }

        public async Task<BlobResultDto> UpdateCustomerGroupAsync(UpdateCustomerGroupDto dto)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            await _blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), AuditLevel.Edit, "update", "group", dto.GroupId.ToString());
            return await _customerGroupManager.UpdateCustomerGroupAsync(dto).ConfigureAwait(false);
        }

        public async Task<BlobResultDto> AddRoleToCustomerGroupAsync(AddRoleToCustomerGroupDto dto)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            await _blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), AuditLevel.Edit, "edit", "group", dto.GroupId.ToString());
            return await _customerGroupManager.AddRoleToCustomerGroupAsync(dto).ConfigureAwait(false);
        }

        public async Task<BlobResultDto> AddUserToCustomerGroupAsync(AddUserToCustomerGroupDto dto)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            await _blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), AuditLevel.Edit, "edit", "group", dto.GroupId.ToString());
            return await _customerGroupManager.AddUserToCustomerGroupAsync(dto).ConfigureAwait(false);
        }

        public async Task<BlobResultDto> RemoveRoleFromCustomerGroupAsync(RemoveRoleFromCustomerGroupDto dto)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            await _blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), AuditLevel.Edit, "edit", "group", dto.GroupId.ToString());
            return await _customerGroupManager.RemoveRoleFromCustomerGroupAsync(dto).ConfigureAwait(false);
        }

        public async Task<BlobResultDto> RemoveUserFromCustomerGroupAsync(RemoveUserFromCustomerGroupDto dto)
        {
            var identity = ClaimsPrincipal.Current.Identity;
            await _blobAuditor.AddAuditEntryAsync(identity.GetBlobId(), AuditLevel.Edit, "edit", "group", dto.GroupId.ToString());
            return await _customerGroupManager.RemoveUserFromCustomerGroupAsync(dto).ConfigureAwait(false);
        }
    }
}
