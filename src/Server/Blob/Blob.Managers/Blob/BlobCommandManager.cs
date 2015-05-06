using System;
using System.Data.Entity;
using System.Reflection;
using System.Threading.Tasks;
using Blob.Contracts.Blob;
using Blob.Contracts.Command;
using Blob.Contracts.Dto;
using Blob.Core.Domain;
using Blob.Data;
using Blob.Managers.Command;
using log4net;

namespace Blob.Managers.Blob
{
    public class BlobCommandManager : IBlobCommandManager
    {
        private readonly ILog _log;

        public BlobCommandManager(BlobDbContext context, ILog log)
        {
            _log = log;
            _log.Debug("Constructing BlobManager");
            Context = context;
        }
        public BlobDbContext Context { get; private set; }

        protected CommandConnectionManager CommandConnectionManager
        {
            get { return CommandConnectionManager.Instance; }
        }


        // Customer
        public async Task DisableCustomerAsync(DisableCustomerDto dto)
        {
            Customer customer = Context.Customers.Find(dto.CustomerId);
            customer.Enabled = false;

            Context.Entry(customer).State = EntityState.Modified;
            await Context.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task EnableCustomerAsync(EnableCustomerDto dto)
        {
            Customer customer = Context.Customers.Find(dto.CustomerId);
            customer.Enabled = true;

            Context.Entry(customer).State = EntityState.Modified;
            await Context.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task UpdateCustomerAsync(UpdateCustomerDto dto)
        {
            Customer customer = Context.Customers.Find(dto.CustomerId);
            customer.Name = dto.Name;

            Context.Entry(customer).State = EntityState.Modified;
            await Context.SaveChangesAsync().ConfigureAwait(false);
        }


        // Device Command
        public async Task IssueCommandAsync(IssueDeviceCommandDto dto)
        {
            Type commandType = Type.GetType(dto.Command,true);
            var cmdInstance = Activator.CreateInstance(commandType);
            PropertyInfo[] properties = commandType.GetProperties();

            foreach (var property in properties)
            {
                if (dto.CommandParameters.ContainsKey(property.Name))
                {
                    property.SetValue(cmdInstance, dto.CommandParameters[property.Name], null);
                }
            }
            await CommandConnectionManager.QueueCommandAsync(dto.DeviceId, (cmdInstance as ICommand));
        }

        // Device
        public async Task DisableDeviceAsync(DisableDeviceDto dto)
        {
            Device device = await Context.Devices.FindAsync(dto.DeviceId).ConfigureAwait(false);
            device.Enabled = false;

            Context.Entry(device).State = EntityState.Modified;
            await Context.SaveChangesAsync().ConfigureAwait(false);
        }


        public async Task EnableDeviceAsync(EnableDeviceDto dto)
        {
            Device device = await Context.Devices.FindAsync(dto.DeviceId).ConfigureAwait(false);
            device.Enabled = true;

            Context.Entry(device).State = EntityState.Modified;
            await Context.SaveChangesAsync().ConfigureAwait(false);
        }


        public async Task<RegisterDeviceResponseDto> RegisterDeviceAsync(RegisterDeviceDto message)
        {
            _log.Debug("BlobManager registering device " + message.DeviceId);
            // Authenticate user is done, it is required in the service

            Guid deviceId = Guid.Parse(message.DeviceId);
            // check if device is already defined
            Device device = await Context.Devices.FirstOrDefaultAsync(x => x.Id.Equals(deviceId));
            if (device != null)
            {
                throw new InvalidOperationException("This device has already been registered.");
            }

            DateTime createDate = DateTime.Now;
            DeviceType deviceType = await Context.Set<DeviceType>().FirstOrDefaultAsync(x => x.Value.Equals(message.DeviceType));

            // todo, get the customerid from the principal
            Guid customerId = Guid.Parse("79720728-171c-48a4-a866-5f905c8fdb9f");
            
            device = new Device
                            {
                                AlertLevel = 0, // initially set to Ok
                                CreateDate = createDate,
                                CustomerId = customerId,
                                DeviceName = message.DeviceName,
                                DeviceType = deviceType,
                                Enabled = true,
                                Id = Guid.Parse(message.DeviceId),
                                LastActivityDate = createDate
                            };

            Context.Devices.Add(device);
            await Context.SaveChangesAsync();

            return new RegisterDeviceResponseDto
                                                 {
                                                     DeviceId = device.Id.ToString(),
                                                     TimeSent = DateTime.Now
                                                 };
        }


        public async Task UpdateDeviceAsync(UpdateDeviceDto dto)
        {
            Device device = Context.Devices.Find(dto.DeviceId);
            device.DeviceName = dto.Name;
            device.DeviceTypeId = dto.DeviceTypeId;

            Context.Entry(device).State = EntityState.Modified;
            await Context.SaveChangesAsync().ConfigureAwait(false);
        }


        // Performance Record
        public async Task DeletePerformanceRecordAsync(DeletePerformanceRecordDto dto)
        {
            StatusPerf perf = Context.DevicePerfDatas.Find(dto.RecordId);
            Context.Entry(perf).State = EntityState.Deleted;
            await Context.SaveChangesAsync().ConfigureAwait(false);
        }


        // Status Record
        public async Task DeleteStatusRecordAsync(DeleteStatusRecordDto dto)
        {
            Core.Domain.Status status = Context.DeviceStatuses.Find(dto.RecordId);
            Context.Entry(status).State = EntityState.Deleted;
            await Context.SaveChangesAsync().ConfigureAwait(false);
        }


        // User
        public async Task DisableUserAsync(DisableUserDto dto)
        {
            User user = Context.Users.Find(dto.UserId);
            user.Enabled = false;

            Context.Entry(user).State = EntityState.Modified;
            await Context.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task EnableUserAsync(EnableUserDto dto)
        {
            User user = Context.Users.Find(dto.UserId);
            user.Enabled = true;

            Context.Entry(user).State = EntityState.Modified;
            await Context.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task UpdateUserAsync(UpdateUserDto dto)
        {
            User user = Context.Users.Find(dto.UserId);
            //user.UserName = dto.UserName;
            if (!string.IsNullOrEmpty(dto.Email) && !user.Email.Equals(dto.Email))
            {
                user.Email = dto.Email;
                user.EmailConfirmed = false;
            }

            Context.Entry(user).State = EntityState.Modified;
            await Context.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}
