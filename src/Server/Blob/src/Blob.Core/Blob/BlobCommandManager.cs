using System.Threading.Tasks;
using Blob.Contracts.Models;
using Blob.Contracts.ServiceContracts;
using Blob.Core.Services;
using log4net;

namespace Blob.Core.Blob
{
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
        public async Task<BlobResult> IssueCommandAsync(IssueDeviceCommandDto dto)
        {
            return await _deviceCommandManager.IssueCommandAsync(dto).ConfigureAwait(false);
        }

        // Device
        public async Task<BlobResult> DisableDeviceAsync(DisableDeviceDto dto)
        {
            return await _deviceManager.DisableDeviceAsync(dto).ConfigureAwait(false);
        }


        public async Task<BlobResult> EnableDeviceAsync(EnableDeviceDto dto)
        {
            return await _deviceManager.EnableDeviceAsync(dto).ConfigureAwait(false);
        }


        public async Task<RegisterDeviceResponse> RegisterDeviceAsync(RegisterDeviceRequest dto)
        {
            return await _deviceManager.RegisterDeviceAsync(dto).ConfigureAwait(false);
        }


        public async Task<BlobResult> UpdateDeviceAsync(UpdateDeviceDto dto)
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

        public async Task<BlobResult> DeletePerformanceRecordAsync(DeletePerformanceRecordDto dto)
        {
            return await _performanceRecordManager.DeletePerformanceRecordAsync(dto).ConfigureAwait(false);
        }


        // Status Record
        public async Task<BlobResult> AddStatusRecordAsync(AddStatusRecordRequest dto)
        {
            return await _statusRecordManager.AddStatusRecordAsync(dto).ConfigureAwait(false);
        }

        public async Task<BlobResult> DeleteStatusRecordAsync(DeleteStatusRecordDto dto)
        {
            return await _statusRecordManager.DeleteStatusRecordAsync(dto).ConfigureAwait(false);
        }


        // User
        public async Task<BlobResult> CreateUserAsync(CreateUserDto dto)
        {
            return await _userManager2.CreateUserAsync(dto).ConfigureAwait(false);
        }

        public async Task<BlobResult> DisableUserAsync(DisableUserDto dto)
        {
            return await _userManager2.DisableUserAsync(dto).ConfigureAwait(false);
        }

        public async Task<BlobResult> EnableUserAsync(EnableUserDto dto)
        {
            return await _userManager2.EnableUserAsync(dto).ConfigureAwait(false);
        }

        public async Task<BlobResult> UpdateUserAsync(UpdateUserDto dto)
        {
            return await _userManager2.UpdateUserAsync(dto).ConfigureAwait(false);
        }

        #region Customer

        // Customer
        public async Task<BlobResult> DisableCustomerAsync(DisableCustomerDto dto)
        {
            return await _customerManager.DisableCustomerAsync(dto).ConfigureAwait(false);
        }

        public async Task<BlobResult> EnableCustomerAsync(EnableCustomerDto dto)
        {
            return await _customerManager.EnableCustomerAsync(dto).ConfigureAwait(false);
        }

        public async Task<BlobResult> RegisterCustomerAsync(RegisterCustomerDto dto)
        {
            return await _customerManager.RegisterCustomerAsync(dto).ConfigureAwait(false);
        }

        public async Task<BlobResult> UpdateCustomerAsync(UpdateCustomerDto dto)
        {
            return await _customerManager.UpdateCustomerAsync(dto).ConfigureAwait(false);
        }
        #endregion

        #region CustomerGroup

        public async Task<BlobResult> CreateCustomerGroupAsync(CreateCustomerGroupDto dto)
        {
            return await _customerGroupManager.CreateCustomerGroupAsync(dto).ConfigureAwait(false);
        }

        public async Task<BlobResult> DeleteCustomerGroupAsync(DeleteCustomerGroupDto dto)
        {
            return await _customerGroupManager.DeleteCustomerGroupAsync(dto).ConfigureAwait(false);
        }

        public async Task<BlobResult> UpdateCustomerGroupAsync(UpdateCustomerGroupDto dto)
        {
            return await _customerGroupManager.UpdateCustomerGroupAsync(dto).ConfigureAwait(false);
        }

        public async Task<BlobResult> AddRoleToCustomerGroupAsync(AddRoleToCustomerGroupDto dto)
        {
            return await _customerGroupManager.AddRoleToCustomerGroupAsync(dto).ConfigureAwait(false);
        }

        public async Task<BlobResult> AddUserToCustomerGroupAsync(AddUserToCustomerGroupDto dto)
        {
            return await _customerGroupManager.AddUserToCustomerGroupAsync(dto).ConfigureAwait(false);
        }

        public async Task<BlobResult> RemoveRoleFromCustomerGroupAsync(RemoveRoleFromCustomerGroupDto dto)
        {
            return await _customerGroupManager.RemoveRoleFromCustomerGroupAsync(dto).ConfigureAwait(false);
        }

        public async Task<BlobResult> RemoveUserFromCustomerGroupAsync(RemoveUserFromCustomerGroupDto dto)
        {
            return await _customerGroupManager.RemoveUserFromCustomerGroupAsync(dto).ConfigureAwait(false);
        }
        #endregion
    }
}
