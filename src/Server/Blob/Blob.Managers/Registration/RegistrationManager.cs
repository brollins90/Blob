using Blob.Contracts.Models;
using Blob.Core.Data;
using Blob.Core.Domain;
using log4net;
using System;
using System.Threading.Tasks;

namespace Blob.Managers.Registration
{
    public class RegistrationManager : IRegistrationManager
    {
        private readonly ILog _log;
        private readonly IAccountRepository _accountRepository;
        private readonly IStatusRepository _statusRepository;

        public RegistrationManager(IAccountRepository accountRepository, IStatusRepository statusRepository, ILog log)
        {
            _log = log;
            _log.Debug("Constructing RegistrationManager");
            _accountRepository = accountRepository;
            _statusRepository = statusRepository;
        }

        public async Task<RegistrationInformation> RegisterDevice(RegistrationMessage message)
        {
            _log.Debug("RegistrationManager registering device " + message.DeviceId);
            // Authenticate user is done, it is required in the service

            Guid deviceId = Guid.Parse(message.DeviceId);
            // check if device is already defined
            Device d = await _statusRepository.FindDeviceByIdAsync(deviceId).ConfigureAwait(true);

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
    }
}
