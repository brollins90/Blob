using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Blob.Contracts.Models;
using Blob.Core.Data;
using Blob.Core.Domain;
using log4net;

namespace Blob.Managers.Blob
{
    public class BlobManager : IBlobManager
    {
        private readonly ILog _log;
        private readonly IAccountRepository _accountRepository;
        private readonly IStatusRepository _statusRepository;

        public BlobManager(IAccountRepository accountRepository, IStatusRepository statusRepository, ILog log)
        {
            _log = log;
            _log.Debug("Constructing BlobManager");
            _accountRepository = accountRepository;
            _statusRepository = statusRepository;
        }


        // Customer
        public async Task<IList<Customer>> GetAllCustomersAsync()
        {
            return await _accountRepository.GetAllCustomersAsync().ConfigureAwait(false);
        }

        public async Task<RegistrationInformation> RegisterDevice(RegistrationMessage message)
        {
            _log.Debug("BlobManager registering device " + message.DeviceId);
            // Authenticate user is done, it is required in the service

            Guid deviceId = Guid.Parse(message.DeviceId);
            // check if device is already defined
            Device d = await GetDeviceByIdAsync(deviceId).ConfigureAwait(true);

            if (d != null)
            {
                throw new InvalidOperationException("This device has already been registered.");
            }

            DateTime createDate = DateTime.Now;

            DeviceType deviceType = await _statusRepository.FindDeviceTypeByValueAsync(message.DeviceType).ConfigureAwait(true);

            // todo, get the customerid from the principal
            Guid customerId = Guid.Parse("79720728-171c-48a4-a866-5f905c8fdb9f");
            //create device objects
            Device device = new Device
                            {
                                CustomerId = customerId,
                                Id = Guid.Parse(message.DeviceId),
                                DeviceName = message.DeviceName,
                                DeviceType = deviceType,
                                LastActivityDate = createDate
                            };

            // save device objects
            await _statusRepository.CreateDeviceAsync(device).ConfigureAwait(true);

            // return results

            RegistrationInformation returnInfo = new RegistrationInformation
                                                 {
                                                     DeviceId = device.Id.ToString(),
                                                     TimeSent = DateTime.Now
                                                 };
            return returnInfo;
        }

        public async Task DeleteDeviceAsync(Guid deviceId)
        {
            _log.Debug("DeleteDeviceAsync " + deviceId);
            // Authenticate user is done, it is required in the service

            Device d = await GetDeviceByIdAsync(deviceId).ConfigureAwait(true);
            await _statusRepository.DeleteDeviceAsync(d).ConfigureAwait(false);
        }

        public async Task<IList<Device>> GetAllDevicesAsync()
        {
            return await _accountRepository.GetAllDevicesAsync().ConfigureAwait(false);
        }

        public async Task<Device> GetDeviceByIdAsync(Guid deviceId)
        {
            return await _statusRepository.FindDeviceByIdAsync(deviceId).ConfigureAwait(false);
        }

        public async Task<IList<Device>> FindDevicesForCustomer(Guid customerId)
        {
            return await _statusRepository.FindDevicesByForCustomerAsync(customerId).ConfigureAwait(false);
        }

        public async Task UpdateDeviceAsync(Device device)
        {
            _log.Debug("DeleteDeviceAsync " + device.Id);

            await _statusRepository.DeleteDeviceAsync(device).ConfigureAwait(false);
        }

        public async Task<IList<Core.Domain.Status>> GetStatusForDeviceAsync(Guid deviceId)
        {
            return await _statusRepository.FindStatusesForDeviceAsync(deviceId).ConfigureAwait(false);
        }

        public async Task<IList<StatusPerf>> GetPerformanceForDeviceAsync(Guid deviceId)
        {
            return await _statusRepository.FindPerformanceForDeviceAsync(deviceId).ConfigureAwait(false);
        }
    }
}
