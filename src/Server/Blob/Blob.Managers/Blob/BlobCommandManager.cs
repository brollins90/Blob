using System;
using System.Data.Entity;
using System.Threading.Tasks;
using Blob.Contracts.Blob;
using Blob.Contracts.Dto;
using Blob.Core.Domain;
using Blob.Data;
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


        // Customer
        public async Task UpdateCustomerAsync(UpdateCustomerDto dto)
        {
            Customer customer = Context.Customers.Find(dto.CustomerId);
            customer.Name = dto.Name;

            Context.Entry(customer).State = EntityState.Modified;
            await Context.SaveChangesAsync().ConfigureAwait(false);
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
                                CustomerId = customerId,
                                Id = Guid.Parse(message.DeviceId),
                                DeviceName = message.DeviceName,
                                DeviceType = deviceType,
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
    }
}
