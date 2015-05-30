using System;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Blob.Contracts.Commands;
using Blob.Contracts.Models;
using Blob.Contracts.ServiceContracts;
using Blob.Core;
using Blob.Core.Models;
using Blob.Managers.Command;
using Blob.Managers.Extensions;
using Blob.Security.Extensions;
using Blob.Security.Identity;
using log4net;

namespace Blob.Managers.Blob
{
    public class BlobCommandManager : IBlobCommandManager
    {
        private readonly ILog _log;
        private BlobCustomerManager _customerManager;
        private BlobCustomerGroupManager _customerGroupManager;

        public BlobCommandManager(BlobDbContext context, ILog log, BlobCustomerManager customerManager, BlobCustomerGroupManager customerGroupManager)
        {
            _log = log;
            _log.Debug("Constructing BlobManager");
            Context = context;
            _customerManager = customerManager;
            _customerGroupManager = customerGroupManager;
        }
        protected BlobDbContext Context { get; private set; }

        protected ICommandConnectionManager ConnectionManager
        {
            get { return CommandConnectionManager.Instance; }
        }

        protected ICommandQueueManager QueueManager
        {
            get { return CommandQueueManager.Instance; }
        }


        // Customer
        public async Task<BlobResultDto> DisableCustomerAsync(DisableCustomerDto dto)
        {
            _log.Info(string.Format("Disabling customer {0}", dto.CustomerId));
            Customer customer = Context.Customers.Find(dto.CustomerId);
            customer.Enabled = false;
            // todo: disable all devices and users for customer ?

            Context.Entry(customer).State = EntityState.Modified;
            await Context.SaveChangesAsync().ConfigureAwait(false);
            _log.Info(string.Format("Disabled customer {0}", dto.CustomerId));
            return BlobResultDto.Success;
        }

        public async Task<BlobResultDto> EnableCustomerAsync(EnableCustomerDto dto)
        {
            _log.Info(string.Format("Enabling customer {0}", dto.CustomerId));
            Customer customer = Context.Customers.Find(dto.CustomerId);
            customer.Enabled = true;

            Context.Entry(customer).State = EntityState.Modified;
            await Context.SaveChangesAsync().ConfigureAwait(false);
            _log.Info(string.Format("Enabled customer {0}", dto.CustomerId));
            return BlobResultDto.Success;
        }

        public async Task<BlobResultDto> RegisterCustomerAsync(RegisterCustomerDto dto)
        {
            return await _customerManager.RegisterCustomerAsync(dto).ConfigureAwait(false);
        }

        public async Task<BlobResultDto> UpdateCustomerAsync(UpdateCustomerDto dto)
        {
            _log.Info(string.Format("Updating customer {0}", dto.CustomerId));
            Customer customer = Context.Customers.Find(dto.CustomerId);
            customer.Name = dto.Name;

            Context.Entry(customer).State = EntityState.Modified;
            await Context.SaveChangesAsync().ConfigureAwait(false);
            _log.Info(string.Format("Updated customer {0}", dto.CustomerId));
            return BlobResultDto.Success;
        }


        // Device Command
        public async Task<BlobResultDto> IssueCommandAsync(IssueDeviceCommandDto dto)
        {
            string assemblyName = KnownCommandsMap.GetCommandHandlerInterfaceAssembly().FullName;
            Type commandType = Type.GetType(dto.Command + ", " + assemblyName);
            var cmdInstance = Activator.CreateInstance(commandType);
            PropertyInfo[] properties = commandType.GetProperties();

            foreach (var property in properties)
            {
                if (dto.CommandParameters.ContainsKey(property.Name))
                {
                    property.SetValue(cmdInstance, dto.CommandParameters[property.Name], null);
                }
            }

            // todo: add to issue table

            Guid commandId = Guid.NewGuid();

            bool queued = await QueueManager.QueueCommandAsync(dto.DeviceId, commandId, (cmdInstance as IDeviceCommand)).ConfigureAwait(false);
            if (!queued)
            {
                // todo: remove from table, or mark as not queued
            }
            return BlobResultDto.Success;
        }

        // Device
        public async Task<BlobResultDto> DisableDeviceAsync(DisableDeviceDto dto)
        {
            Device device = await Context.Devices.FindAsync(dto.DeviceId).ConfigureAwait(false);
            device.Enabled = false;

            Context.Entry(device).State = EntityState.Modified;
            await Context.SaveChangesAsync().ConfigureAwait(false);
            return BlobResultDto.Success;
        }


        public async Task<BlobResultDto> EnableDeviceAsync(EnableDeviceDto dto)
        {
            Device device = await Context.Devices.FindAsync(dto.DeviceId).ConfigureAwait(false);
            device.Enabled = true;

            Context.Entry(device).State = EntityState.Modified;
            await Context.SaveChangesAsync().ConfigureAwait(false);
            return BlobResultDto.Success;
        }


        public async Task<RegisterDeviceResponseDto> RegisterDeviceAsync(RegisterDeviceDto message)
        {
            _log.Debug("BlobManager registering device " + message.DeviceId);

            bool succeeded = false;
            Guid deviceId = Guid.Parse(message.DeviceId);
            try
            {
                // check if device is already defined
                Device device = await Context.Devices.FirstOrDefaultAsync(x => x.Id.Equals(deviceId));
                if (device != null)
                {
                    throw new InvalidOperationException("This device has already been registered.");
                }

                DeviceType deviceType = await Context.Set<DeviceType>().FirstOrDefaultAsync(x => x.Name.Equals(message.DeviceType));

                // todo, get the customerid from the principal
                ClaimsPrincipal id = ClaimsPrincipal.Current;
                Guid customerId = Guid.Parse(id.Identity.GetCustomerId());
                //Guid customerId = Guid.Parse("79720728-171c-48a4-a866-5f905c8fdb9f");

                device = new Device
                         {
                             AlertLevel = 0, // initially set to Ok
                             CreateDateUtc = Now(),
                             CustomerId = customerId,
                             DeviceName = message.DeviceName,
                             DeviceType = deviceType,
                             Enabled = true,
                             Id = deviceId,
                             LastActivityDateUtc = OldestTime()
                         };

                Context.Devices.Add(device);
                await Context.SaveChangesAsync();
                succeeded = true;
            }
            catch (Exception e)
            {
                _log.Error("Failed to register device", e);
                succeeded = false;
            }
            return new RegisterDeviceResponseDto
                                                 {
                                                     DeviceId = deviceId,
                                                     Succeeded = succeeded,
                                                     TimeSent = Now()
                                                 };
        }


        public async Task<BlobResultDto> UpdateDeviceAsync(UpdateDeviceDto dto)
        {
            Device device = Context.Devices.Find(dto.DeviceId);
            device.DeviceName = dto.Name;
            device.DeviceTypeId = dto.DeviceTypeId;
            //device.LastActivityDateUtc = Now();

            Context.Entry(device).State = EntityState.Modified;
            await Context.SaveChangesAsync().ConfigureAwait(false);

            await UpdateDeviceActivityTimeAsync(dto.DeviceId);
            return BlobResultDto.Success;
        }


        // Performance Record

        public async Task<BlobResultDto> AddPerformanceRecordAsync(AddPerformanceRecordDto statusPerformanceData)
        {
            _log.Debug("Storing status perf data " + statusPerformanceData);
            await UpdateDeviceActivityTimeAsync(statusPerformanceData.DeviceId);
            //Device device = await Context.Devices.FirstOrDefaultAsync(x => x.Id.Equals(statusPerformanceData.DeviceId));

            //device.LastActivityDateUtc = Now();
            //Context.Entry(device).State = EntityState.Modified;

            //if (device != null)
            //{
                foreach (PerformanceRecordValue value in statusPerformanceData.Data)
                {
                    Context.DevicePerfDatas.Add(new PerformanceRecord
                    {
                        Critical = value.Critical.ToNullableDecimal(),
                        DeviceId = statusPerformanceData.DeviceId,// device.Id,
                        Label = value.Label,
                        Max = value.Max.ToNullableDecimal(),
                        Min = value.Min.ToNullableDecimal(),
                        MonitorDescription = statusPerformanceData.MonitorDescription,
                        MonitorName = statusPerformanceData.MonitorName,
                        StatusId = (statusPerformanceData.StatusRecordId.HasValue) ? statusPerformanceData.StatusRecordId.Value : 0,
                        TimeGeneratedUtc = statusPerformanceData.TimeGenerated,
                        UnitOfMeasure = value.UnitOfMeasure,
                        Value = value.Value.ToDecimal(),
                        Warning = value.Warning.ToNullableDecimal()
                    });
                    await Context.SaveChangesAsync();
                }
            //}
            return BlobResultDto.Success;
        }

        public async Task<BlobResultDto> DeletePerformanceRecordAsync(DeletePerformanceRecordDto dto)
        {
            PerformanceRecord perf = Context.DevicePerfDatas.Find(dto.RecordId);
            Context.Entry(perf).State = EntityState.Deleted;
            await Context.SaveChangesAsync().ConfigureAwait(false);

            await UpdateDeviceActivityTimeAsync(perf.DeviceId);
            return BlobResultDto.Success;
        }


        // Status Record

        public async Task<BlobResultDto> AddStatusRecordAsync(AddStatusRecordDto statusData)
        {
            _log.Debug("Storing status data " + statusData);
            Device device = Context.Devices.Find(statusData.DeviceId);
            await UpdateDeviceActivityTimeAsync(statusData.DeviceId);
            
            //var allMonPrev = (from s1 in Context.DeviceStatuses
            //    join s2 in (
            //            from s in Context.DeviceStatuses
            //            where s.DeviceId == statusData.DeviceId
            //            group s by s.MonitorId
            //            into r
            //            select new {MonitorId = r.Key, TimeGeneratedUtc = r.Max(x => x.TimeGeneratedUtc)}
            //        )
            //        on new {s1.MonitorId, s1.TimeGeneratedUtc} equals new {s2.MonitorId, s2.TimeGeneratedUtc}
            //        select s1).ToList();

            //if (device.AlertLevel == 0)
            //{
                
            //}
            // ...

            //var thisPrevStatus = allMonPrev.FirstOrDefault(x => x.MonitorId.Equals(statusData.MonitorId));
            //if (thisPrevStatus != null)
            //{
            //    if (thisPrevStatus.AlertLevel != )
            //}

            //device.LastActivityDateUtc = Now();
            //Context.Entry(device).State = EntityState.Modified;

            //if (device != null)
            //{
                StatusRecord newStatus = new StatusRecord
                {
                    AlertLevel = statusData.AlertLevel,
                    CurrentValue = statusData.CurrentValue,
                    DeviceId = statusData.DeviceId,
                    MonitorDescription = statusData.MonitorDescription,
                    MonitorId = statusData.MonitorId,
                    MonitorName = statusData.MonitorName,
                    TimeGeneratedUtc = statusData.TimeGenerated,
                    TimeSentUtc = statusData.TimeSent,
                };
                Context.DeviceStatuses.Add(newStatus);
                await Context.SaveChangesAsync();

                if (statusData.PerformanceRecordDto != null)
                {
                    statusData.PerformanceRecordDto.StatusRecordId = newStatus.Id;
                    await AddPerformanceRecordAsync(statusData.PerformanceRecordDto);
                }
            //}
            return BlobResultDto.Success;
        }

        public async Task<BlobResultDto> DeleteStatusRecordAsync(DeleteStatusRecordDto dto)
        {
            StatusRecord status = Context.DeviceStatuses.Find(dto.RecordId);
            Context.Entry(status).State = EntityState.Deleted;
            await Context.SaveChangesAsync().ConfigureAwait(false);

            await UpdateDeviceActivityTimeAsync(status.DeviceId);
            return BlobResultDto.Success;
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


        #region CustomerGroup

        public async Task<BlobResultDto> CreateCustomerGroupAsync(CreateCustomerGroupDto dto)
        {
            await _customerGroupManager.CreateGroupAsync(dto).ConfigureAwait(false);
            return BlobResultDto.Success;
        }

        public async Task<BlobResultDto> DeleteCustomerGroupAsync(DeleteCustomerGroupDto dto)
        {
            await _customerGroupManager.DeleteGroupAsync(dto.GroupId).ConfigureAwait(false);
            return BlobResultDto.Success;
        }

        public async Task<BlobResultDto> UpdateCustomerGroupAsync(UpdateCustomerGroupDto dto)
        {
            await _customerGroupManager.UpdateGroupAsync(dto).ConfigureAwait(false);
            return BlobResultDto.Success;
        }

        public async Task<BlobResultDto> AddRoleToCustomerGroupAsync(AddRoleToCustomerGroupDto dto)
        {
            await _customerGroupManager.AddRoleToCustomerGroup(dto).ConfigureAwait(false);
            return BlobResultDto.Success;
        }

        public async Task<BlobResultDto> AddUserToCustomerGroupAsync(AddUserToCustomerGroupDto dto)
        {
            await _customerGroupManager.AddUserToCustomerGroup(dto).ConfigureAwait(false);
            return BlobResultDto.Success;
        }

        public async Task<BlobResultDto> RemoveRoleFromCustomerGroupAsync(RemoveRoleFromCustomerGroupDto dto)
        {
            await _customerGroupManager.RemoveRoleFromCustomerGroupAsync(dto).ConfigureAwait(false);
            return BlobResultDto.Success;
        }

        public async Task<BlobResultDto> RemoveUserFromCustomerGroupAsync(RemoveUserFromCustomerGroupDto dto)
        {
            await _customerGroupManager.RemoveUserFromCustomerGroupAsync(dto).ConfigureAwait(false);
            return BlobResultDto.Success;
        }
        #endregion
    }
}
