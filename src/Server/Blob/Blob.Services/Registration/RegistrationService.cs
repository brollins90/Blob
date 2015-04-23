using System.Security.Permissions;
using System.ServiceModel;
using System.Threading.Tasks;
using Blob.Contracts.Models;
using Blob.Contracts.Registration;
using Blob.Managers.Blob;
using log4net;

namespace Blob.Services.Registration
{
    [ServiceBehavior]
    //[PrincipalPermission(SecurityAction.Demand, Authenticated = true)]
    public class RegistrationService : IRegistrationService
    {
        private readonly ILog _log;
        private readonly IBlobManager _blobManager;

        public RegistrationService(IBlobManager blobManager, ILog log)
        {
            _log = log;
            _blobManager = blobManager;
        }

        [OperationBehavior]
        [PrincipalPermission(SecurityAction.Demand, Role = "Customer")]
        public async Task<RegistrationInformation> Register(RegistrationMessage message)
        {
            _log.Debug("RegistrationService received registration message: " + message);
            return await _blobManager.RegisterDevice(message).ConfigureAwait(false);
        }
    }
}
