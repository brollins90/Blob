using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using Blob.Contracts.Models;
using Blob.Core.Domain;
using Blob.Data;
using log4net;

namespace Blob.Managers.Blob
{
    public interface IBlobManager
    {
        Task<IList<Customer>> GetAllCustomersAsync();

        Task<RegistrationInformation> RegisterDevice(RegistrationMessage message);
    }

    public class BlobManager : IBlobManager
    {
        private readonly ILog _log;

        public BlobManager(BlobDbContext context, ILog log)
        {
            _log = log;
            _log.Debug("Constructing BlobManager");
            Context = context;
        }
        public BlobDbContext Context { get; private set; }


        // Customer
        public async Task<IList<Customer>> GetAllCustomersAsync()
        {
            return await Context.Customers.ToListAsync();
        }

        public async Task<RegistrationInformation> RegisterDevice(RegistrationMessage message)
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

            RegistrationInformation returnInfo = new RegistrationInformation
                                                 {
                                                     DeviceId = device.Id.ToString(),
                                                     TimeSent = DateTime.Now
                                                 };
            return returnInfo;
        }
    }
}
