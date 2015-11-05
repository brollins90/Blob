using Blob.Contracts.Models;
using Blob.Contracts.ServiceContracts;
using Blob.Services;
using System.ServiceModel;
using System.Threading.Tasks;

namespace Blob.WcfHost.Service
{
    [ServiceBehavior]
    [GlobalErrorBehavior(typeof(GlobalErrorHandler))]
    public class BeforeAuthorizationWcfService : IAuthorizationManagerService
    {
        IAuthorizationManagerService _authorizationManagerService;

        public BeforeAuthorizationWcfService(IAuthorizationManagerService authorizationManagerService)
        {
            _authorizationManagerService = authorizationManagerService;
        }

        [OperationBehavior]
        public async Task<bool> CheckAccessAsync(AuthorizationContextDto context)
        {
            return await _authorizationManagerService.CheckAccessAsync(context);
        }
    }
}