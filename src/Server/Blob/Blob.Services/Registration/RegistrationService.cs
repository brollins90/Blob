using Blob.Contracts.Models;
using Blob.Contracts.Registration;
using Blob.Managers.Registration;
using log4net;
using System.Threading.Tasks;

namespace Blob.Services.Registration
{
    public class RegistrationService : IRegistrationService
    {
        private readonly ILog _log;
        private readonly IRegistrationManager _registrationManager;

        public RegistrationService(IRegistrationManager registrationManager, ILog log)
        {
            _log = log;
            _registrationManager = registrationManager;
        }

        public async Task<RegistrationInformation> Register(RegistrationMessage message)
        {
            _log.Debug("Server received registration message: " + message);
            return await _registrationManager.RegisterDevice(message);
        }
    }
}
