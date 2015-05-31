using System;
using System.Data.Entity;
using System.Threading.Tasks;
using Blob.Contracts.Models;
using Blob.Contracts.ServiceContracts;
using Blob.Core.Models;
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
        private readonly BlobStatusRecordManager _statusRecordManager;
        private readonly BlobPerformanceRecordManager _performanceRecordManager;

        public BlobCommandManager(
            ILog log, BlobDbContext context, BlobCustomerManager customerManager, BlobCustomerGroupManager customerGroupManager,
            BlobDeviceManager deviceManager, BlobDeviceCommandManager deviceCommandManager, BlobStatusRecordManager statusRecordManager, 
            BlobPerformanceRecordManager performanceRecordManager)
        {
            _log = log;
            _log.Debug("Constructing BlobManager");
            Context = context;
            _customerManager = customerManager;
            _customerGroupManager = customerGroupManager;
            _deviceManager = deviceManager;
            _deviceCommandManager = deviceCommandManager;
            _statusRecordManager = statusRecordManager;
            _performanceRecordManager = performanceRecordManager;
        }

        protected BlobDbContext Context { get; private set; }


        // Device Command
        public async Task<BlobResultDto> IssueCommandAsync(IssueDeviceCommandDto dto)
        {
            return await _deviceCommandManager.IssueCommandAsync(dto).ConfigureAwait(false);
        }

        // Device
        public async Task<BlobResultDto> DisableDeviceAsync(DisableDeviceDto dto)
        {
            return await _deviceManager.DisableDeviceAsync(dto).ConfigureAwait(false);
        }


        public async Task<BlobResultDto> EnableDeviceAsync(EnableDeviceDto dto)
        {
            return await _deviceManager.EnableDeviceAsync(dto).ConfigureAwait(false);
        }


        public async Task<RegisterDeviceResponseDto> RegisterDeviceAsync(RegisterDeviceDto dto)
        {
            return await _deviceManager.RegisterDeviceAsync(dto).ConfigureAwait(false);
        }


        public async Task<BlobResultDto> UpdateDeviceAsync(UpdateDeviceDto dto)
        {
            return await _deviceManager.UpdateDeviceAsync(dto).ConfigureAwait(false);
        }


        // Performance Record

        public async Task<BlobResultDto> AddPerformanceRecordAsync(AddPerformanceRecordDto dto)
        {
            return await _performanceRecordManager.AddPerformanceRecordAsync(dto).ConfigureAwait(false);
        }

        public async Task<BlobResultDto> DeletePerformanceRecordAsync(DeletePerformanceRecordDto dto)
        {
            return await _performanceRecordManager.DeletePerformanceRecordAsync(dto).ConfigureAwait(false);
        }


        // Status Record

        public async Task<BlobResultDto> AddStatusRecordAsync(AddStatusRecordDto dto)
        {
            return await _statusRecordManager.AddStatusRecordAsync(dto).ConfigureAwait(false);
        }

        public async Task<BlobResultDto> DeleteStatusRecordAsync(DeleteStatusRecordDto dto)
        {
            return await _statusRecordManager.DeleteStatusRecordAsync(dto).ConfigureAwait(false);
        }


        // User
        public async Task<BlobResultDto> CreateUserAsync(CreateUserDto dto)
        {
            User newUser = new User
            {
                AccessFailedCount = 0,
                CreateDateUtc = Now(),
                CustomerId = dto.CustomerId,
                Email = dto.Email,
                EmailConfirmed = false,
                Enabled = true,
                Id = dto.UserId,
                LastActivityDate = OldestTime(),
                LockoutEnabled = false,
                LockoutEndDateUtc = OldestTime(),
                PasswordHash = dto.Password,
                UserName = dto.UserName
            };
            Context.Users.Add(newUser);
            await Context.SaveChangesAsync().ConfigureAwait(false);
            return BlobResultDto.Success;
        }

        public async Task<BlobResultDto> DisableUserAsync(DisableUserDto dto)
        {
            User user = Context.Users.Find(dto.UserId);
            user.Enabled = false;

            Context.Entry(user).State = EntityState.Modified;
            await Context.SaveChangesAsync().ConfigureAwait(false);
            return BlobResultDto.Success;
        }

        public async Task<BlobResultDto> EnableUserAsync(EnableUserDto dto)
        {
            User user = Context.Users.Find(dto.UserId);
            user.Enabled = true;

            Context.Entry(user).State = EntityState.Modified;
            await Context.SaveChangesAsync().ConfigureAwait(false);
            return BlobResultDto.Success;
        }

        public async Task<BlobResultDto> UpdateUserAsync(UpdateUserDto dto)
        {
            User user = Context.Users.Find(dto.UserId);
            // todo: can username change?
            //user.UserName = dto.UserName;
            if (!string.IsNullOrEmpty(dto.Email) && !user.Email.Equals(dto.Email))
            {
                user.Email = dto.Email;
                user.EmailConfirmed = false;
                user.LastActivityDate = Now();
            }

            Context.Entry(user).State = EntityState.Modified;
            await Context.SaveChangesAsync().ConfigureAwait(false);
            return BlobResultDto.Success;
        }

        private async Task<BlobResultDto> UpdateDeviceActivityTimeAsync(Guid deviceId)
        {
            Device device = Context.Devices.Find(deviceId);
            device.LastActivityDateUtc = Now();

            Context.Entry(device).State = EntityState.Modified;
            await Context.SaveChangesAsync().ConfigureAwait(false);
            return BlobResultDto.Success;
        }

        private async Task<BlobResultDto> UpdateUserActivityTimeAsync(Guid userId)
        {
            User user = Context.Users.Find(userId);
            user.LastActivityDate = Now();

            Context.Entry(user).State = EntityState.Modified;
            await Context.SaveChangesAsync().ConfigureAwait(false);
            return BlobResultDto.Success;
        }

        public DateTime Now()
        {
            return DateTime.UtcNow;
        }

        public DateTime OldestTime()
        {
            return _oldestDateTime;
        }
        private readonly DateTime _oldestDateTime = DateTime.Parse("2010-01-01").ToUniversalTime();


        #region Customer

        // Customer
        public async Task<BlobResultDto> DisableCustomerAsync(DisableCustomerDto dto)
        {
            return await _customerManager.DisableCustomerAsync(dto).ConfigureAwait(false);
        }

        public async Task<BlobResultDto> EnableCustomerAsync(EnableCustomerDto dto)
        {
            return await _customerManager.EnableCustomerAsync(dto).ConfigureAwait(false);
        }

        public async Task<BlobResultDto> RegisterCustomerAsync(RegisterCustomerDto dto)
        {
            return await _customerManager.RegisterCustomerAsync(dto).ConfigureAwait(false);
        }

        public async Task<BlobResultDto> UpdateCustomerAsync(UpdateCustomerDto dto)
        {
            return await _customerManager.UpdateCustomerAsync(dto).ConfigureAwait(false);
        }
        #endregion

        #region CustomerGroup

        public async Task<BlobResultDto> CreateCustomerGroupAsync(CreateCustomerGroupDto dto)
        {
            return await _customerGroupManager.CreateCustomerGroupAsync(dto).ConfigureAwait(false);
        }

        public async Task<BlobResultDto> DeleteCustomerGroupAsync(DeleteCustomerGroupDto dto)
        {
            return await _customerGroupManager.DeleteCustomerGroupAsync(dto).ConfigureAwait(false);
        }

        public async Task<BlobResultDto> UpdateCustomerGroupAsync(UpdateCustomerGroupDto dto)
        {
            return await _customerGroupManager.UpdateCustomerGroupAsync(dto).ConfigureAwait(false);
        }

        public async Task<BlobResultDto> AddRoleToCustomerGroupAsync(AddRoleToCustomerGroupDto dto)
        {
            return await _customerGroupManager.AddRoleToCustomerGroupAsync(dto).ConfigureAwait(false);
        }

        public async Task<BlobResultDto> AddUserToCustomerGroupAsync(AddUserToCustomerGroupDto dto)
        {
            return await _customerGroupManager.AddUserToCustomerGroupAsync(dto).ConfigureAwait(false);
        }

        public async Task<BlobResultDto> RemoveRoleFromCustomerGroupAsync(RemoveRoleFromCustomerGroupDto dto)
        {
            return await _customerGroupManager.RemoveRoleFromCustomerGroupAsync(dto).ConfigureAwait(false);
        }

        public async Task<BlobResultDto> RemoveUserFromCustomerGroupAsync(RemoveUserFromCustomerGroupDto dto)
        {
            return await _customerGroupManager.RemoveUserFromCustomerGroupAsync(dto).ConfigureAwait(false);
        }
        #endregion
    }
}
