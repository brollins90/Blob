using System.Security.Claims;
using Blob.Proxies;

namespace Before.Infrastructure.Identity
{
    public class BeforeUserManager : IdentityManagerClient
    {
        private BeforeUserManager(string endpointName, string username, string password)
            : base(endpointName, username, password) { }

        public static BeforeUserManager Create(string endpointName)
        {
            ClaimsPrincipal principal = ClaimsPrincipal.Current;
            string username = "customerUser1";
            string password = "password";
            return new BeforeUserManager(endpointName, username, password);
        }
    }
}