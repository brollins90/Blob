using System;
using System.Threading.Tasks;
using Blob.Contracts.Security;

namespace Blob.Proxies
{
    public class BeforeAuthorizationClient : BaseClient<IAuthorizationManagerService>, IAuthorizationManagerService
    {
        public BeforeAuthorizationClient(string endpointName, string username, string password) : base(endpointName, username, password) { }

        public async Task<bool> CheckAccessAsync(AuthorizationContextDto context)
        {
            try
            {
                return await Channel.CheckAccessAsync(context).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
            return false;
        }
    }
}
