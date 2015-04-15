using System;
using Blob.Contracts.Models;
using Blob.Core.Data;
using log4net;
using System.Threading.Tasks;
using Blob.Core.Domain;

namespace Blob.Managers.Registration
{
    public class RegistrationManager : IRegistrationManager
    {
        private readonly ILog _log;
        private readonly IRepositoryAsync<Device> _deviceRepository;

        public RegistrationManager(IRepositoryAsync<Device> deviceRepository, ILog log)
        {
            _log = log;
            _deviceRepository = deviceRepository;
        }

        public async Task<RegistrationInformation> RegisterDevice(RegistrationMessage message)
        {
            _log.Debug("Storing registration data " + message);

            // authenticate user
            // _authenticationService.AuthenticateUser...


            // check if device is already defined
            Device d = await _deviceRepository.GetSingleAsync(x => x.Id.ToString().Equals(message.DeviceId)).ConfigureAwait(true);

            if (d != null)
            {
                // todo: handle this case better.  We shouldnt just blow up
                throw new InvalidOperationException("This device has already been registered.");
            }


            // create device objects
            //Device newDevice = new Device()
            //                   {
            //                       Customer = message.
            //                   }

            // save device objects

            // return results


            //await _statusRepository.InsertAsync(new Core.Domain.Status()
            //{
            //    CurrentValue = statusData.CurrentValue,
            //    DeviceId = statusData.DeviceId,
            //    MonitorDescription = statusData.MonitorDescription,
            //    MonitorName = statusData.MonitorName,
            //    TimeGenerated = statusData.TimeGenerated,
            //    TimeSent = statusData.TimeSent
            //}).ConfigureAwait(false);
            return null;
        }
    }
}
