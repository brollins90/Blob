﻿using Blob.Contracts.Models;
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
        private readonly IRepositoryAsync<Device> _deviceRepository;
        private readonly IRepositoryAsync<DeviceSecurity> _deviceSecurityRepository;
        private readonly IRepositoryAsync<DeviceType> _deviceTypeRepository;

        public RegistrationManager(
            IRepositoryAsync<Device> deviceRepository,
            IRepositoryAsync<DeviceSecurity> deviceSecurityRepository,
            IRepositoryAsync<DeviceType> deviceTypeRepository,
            ILog log)
        {
            _log = log;
            _deviceRepository = deviceRepository;
            _deviceSecurityRepository = deviceSecurityRepository;
            _deviceTypeRepository = deviceTypeRepository;
        }

        public async Task<RegistrationInformation> RegisterDevice(RegistrationMessage message)
        {
            _log.Debug("RegistrationManager registering device " + message.DeviceId);
            // Authenticate user is done, it is required in the service

            // check if device is already defined
            Device d = await _deviceRepository.GetSingleAsync(x => x.Id.ToString().Equals(message.DeviceId)).ConfigureAwait(true);

            if (d != null)
            {
                throw new InvalidOperationException("This device has already been registered.");
            }
            
            DateTime createDate = DateTime.Now;

            DeviceType deviceType = await _deviceTypeRepository.GetSingleAsync(x => x.Value.Equals(message.DeviceType)).ConfigureAwait(true);
             //create device objects
            Device device = new Device
                            {
                                //Customer,
                                CustomerId = Guid.Parse("79720728-171c-48a4-a866-5f905c8fdb9f"),
                                Id = Guid.Parse(message.DeviceId),
                                DeviceName = message.DeviceName,
                                DeviceTypeId = deviceType.Id,
                                LastActivityDate = createDate
                            };
            DeviceSecurity deviceSecurity = new DeviceSecurity
                                    {
                                        Comment = null,
                                        CreateDate = createDate,
                                        Device = device,
                                        DeviceId = device.Id,
                                        IsApproved = true, // todo: ???
                                        IsLockedOut = false,
                                        Key1 = message.DeviceKey1,
                                        Key1Format = (int) DeviceSecurityKeyFormat.CLEAR,
                                        Key1Salt = "",
                                        Key2 = message.DeviceKey2,
                                        Key2Format = (int) DeviceSecurityKeyFormat.CLEAR,
                                        Key2Salt = ""
                                    };

            // save device objects
            await _deviceRepository.InsertAsync(device).ConfigureAwait(true);
            await _deviceSecurityRepository.InsertAsync(deviceSecurity).ConfigureAwait(true);

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
