namespace Blob.Core.Blob
{
    using System.Threading.Tasks;
    using Contracts.Request;
    using Contracts.Response;
    using Contracts.ServiceContracts;
    using Services;
    using log4net;

    public class BlobCommandManager : IBlobCommandManager
    {
        private readonly ILog _log;
        private readonly BlobCustomerManager _customerManager;
        private readonly BlobCustomerGroupManager _customerGroupManager;
        private readonly BlobDeviceManager _deviceManager;
        private readonly BlobDeviceCommandManager _deviceCommandManager;
        private readonly BlobPerformanceRecordManager _performanceRecordManager;
        private readonly BlobStatusRecordManager _statusRecordManager;
        private readonly BlobUserManager2 _userManager2;

        public BlobCommandManager(
            ILog log,
            BlobCustomerManager customerManager,
            BlobCustomerGroupManager customerGroupManager,
            BlobDeviceManager deviceManager,
            BlobDeviceCommandManager deviceCommandManager,
            BlobPerformanceRecordManager performanceRecordManager,
            BlobStatusRecordManager statusRecordManager,
            BlobUserManager2 userManager2)
        {
            _log = log;
            _log.Debug("Constructing BlobManager");
            _customerManager = customerManager;
            _customerGroupManager = customerGroupManager;
            _deviceManager = deviceManager;
            _deviceCommandManager = deviceCommandManager;
            _performanceRecordManager = performanceRecordManager;
            _statusRecordManager = statusRecordManager;
            _userManager2 = userManager2;
        }


        // Device Command
        public async Task<BlobResult> IssueCommandAsync(IssueDeviceCommandRequest dto)
        {
            return await _deviceCommandManager.IssueCommandAsync(dto).ConfigureAwait(false);
        }

        // Device
        public async Task<BlobResult> DisableDeviceAsync(DisableDeviceRequest dto)
        {
            return await _deviceManager.DisableDeviceAsync(dto).ConfigureAwait(false);
        }


        public async Task<BlobResult> EnableDeviceAsync(EnableDeviceRequest dto)
        {
            return await _deviceManager.EnableDeviceAsync(dto).ConfigureAwait(false);
        }


        public async Task<RegisterDeviceResponse> RegisterDeviceAsync(RegisterDeviceRequest dto)
        {
            return await _deviceManager.RegisterDeviceAsync(dto).ConfigureAwait(false);
        }


        public async Task<BlobResult> UpdateDeviceAsync(UpdateDeviceRequest dto)
        {
            return await _deviceManager.UpdateDeviceAsync(dto).ConfigureAwait(false);
        }

        public async Task<BlobResult> AuthenticateDeviceAsync(AuthenticateDeviceRequest dto)
        {
            return await _deviceManager.AuthenticateDeviceAsync(dto);
        }

        // Performance Record
        public async Task<BlobResult> AddPerformanceRecordAsync(AddPerformanceRecordRequest dto)
        {
            return await _performanceRecordManager.AddPerformanceRecordAsync(dto).ConfigureAwait(false);
        }

        public async Task<BlobResult> DeletePerformanceRecordAsync(DeletePerformanceRecordRequest dto)
        {
            return await _performanceRecordManager.DeletePerformanceRecordAsync(dto).ConfigureAwait(false);
        }


        // Status Record
        public async Task<BlobResult> AddStatusRecordAsync(AddStatusRecordRequest dto)
        {
            return await _statusRecordManager.AddStatusRecordAsync(dto).ConfigureAwait(false);
        }

        public async Task<BlobResult> DeleteStatusRecordAsync(DeleteStatusRecordRequest dto)
        {
            return await _statusRecordManager.DeleteStatusRecordAsync(dto).ConfigureAwait(false);
        }


        // User
        public async Task<BlobResult> CreateUserAsync(CreateUserRequest dto)
        {
            return await _userManager2.CreateUserAsync(dto).ConfigureAwait(false);
        }

        public async Task<BlobResult> DisableUserAsync(DisableUserRequest dto)
        {
            return await _userManager2.DisableUserAsync(dto).ConfigureAwait(false);
        }

        public async Task<BlobResult> EnableUserAsync(EnableUserRequest dto)
        {
            return await _userManager2.EnableUserAsync(dto).ConfigureAwait(false);
        }

        public async Task<BlobResult> UpdateUserAsync(UpdateUserRequest dto)
        {
            return await _userManager2.UpdateUserAsync(dto).ConfigureAwait(false);
        }

        #region Customer

        // Customer
        public async Task<BlobResult> DisableCustomerAsync(DisableCustomerRequest dto)
        {
            return await _customerManager.DisableCustomerAsync(dto).ConfigureAwait(false);
        }

        public async Task<BlobResult> EnableCustomerAsync(EnableCustomerRequest dto)
        {
            return await _customerManager.EnableCustomerAsync(dto).ConfigureAwait(false);
        }

        public async Task<BlobResult> RegisterCustomerAsync(RegisterCustomerRequest dto)
        {
            return await _customerManager.RegisterCustomerAsync(dto).ConfigureAwait(false);
        }

        public async Task<BlobResult> UpdateCustomerAsync(UpdateCustomerRequest dto)
        {
            return await _customerManager.UpdateCustomerAsync(dto).ConfigureAwait(false);
        }
        #endregion

        #region CustomerGroup

        public async Task<BlobResult> CreateCustomerGroupAsync(CreateCustomerGroupRequest dto)
        {
            return await _customerGroupManager.CreateCustomerGroupAsync(dto).ConfigureAwait(false);
        }

        public async Task<BlobResult> DeleteCustomerGroupAsync(DeleteCustomerGroupRequest dto)
        {
            return await _customerGroupManager.DeleteCustomerGroupAsync(dto).ConfigureAwait(false);
        }

        public async Task<BlobResult> UpdateCustomerGroupAsync(UpdateCustomerGroupRequest dto)
        {
            return await _customerGroupManager.UpdateCustomerGroupAsync(dto).ConfigureAwait(false);
        }

        public async Task<BlobResult> AddRoleToCustomerGroupAsync(AddRoleToCustomerGroupRequest dto)
        {
            return await _customerGroupManager.AddRoleToCustomerGroupAsync(dto).ConfigureAwait(false);
        }

        public async Task<BlobResult> AddUserToCustomerGroupAsync(AddUserToCustomerGroupRequest dto)
        {
            return await _customerGroupManager.AddUserToCustomerGroupAsync(dto).ConfigureAwait(false);
        }

        public async Task<BlobResult> RemoveRoleFromCustomerGroupAsync(RemoveRoleFromCustomerGroupRequest dto)
        {
            return await _customerGroupManager.RemoveRoleFromCustomerGroupAsync(dto).ConfigureAwait(false);
        }

        public async Task<BlobResult> RemoveUserFromCustomerGroupAsync(RemoveUserFromCustomerGroupRequest dto)
        {
            return await _customerGroupManager.RemoveUserFromCustomerGroupAsync(dto).ConfigureAwait(false);
        }
        #endregion
    }
}