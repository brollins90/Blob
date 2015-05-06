using System.Security.Claims;
using System.Security.Permissions;
using System.ServiceModel;
using System.Threading;
using System.Threading.Tasks;
using Blob.Contracts.Blob;
using Blob.Contracts.Dto;
using Blob.Contracts.Registration;
using log4net;

namespace Blob.Services.Registration
{
    [ServiceBehavior]
    //[PrincipalPermission(SecurityAction.Demand, Authenticated = true)]
    public class RegistrationService : IRegistrationService
    {
        private readonly ILog _log;
        private readonly IBlobCommandManager _blobCommandManager;

        public RegistrationService(IBlobCommandManager blobCommandManager, ILog log)
        {
            _log = log;
            _blobCommandManager = blobCommandManager;
        }

        [OperationBehavior]
        [PrincipalPermission(SecurityAction.Demand, Role = "Customer")]
        public async Task<RegisterDeviceResponseDto> Register(RegisterDeviceDto message)
        {
            _log.Debug("RegistrationService received registration message: " + message);
            
            return await _blobCommandManager.RegisterDeviceAsync(message).ConfigureAwait(false);
        }
    }
}
