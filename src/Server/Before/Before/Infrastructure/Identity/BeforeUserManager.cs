using System.Collections.Specialized;
using System.Configuration;
using System.Threading.Tasks;
using Before.Infrastructure.Extensions;
using Blob.Contracts.Security;
using Blob.Proxies;
using Microsoft.AspNet.Identity;

namespace Before.Infrastructure.Identity
{
    public class BeforeUserManager : IdentityManagerClient
    {
        public BeforeUserManager()
        {
            NameValueCollection config = (NameValueCollection)ConfigurationManager.GetSection("BlobProxy");
            if (config == null)
            {
                throw new ConfigurationErrorsException("Cannot load config");
            }
            Initialize("IdentityManagerClient", config);
        }

        public BeforeUser FindById(string userId)
        {
            return new BeforeUser(FindByIdAsync(userId).Result);
        }

        public async Task<IdentityResult> CreateAsync(BeforeUser user, string password)
        {
            IdentityResultDto res = await ((IdentityManagerClient) this).CreateAsync(user.ToDto(), password).ConfigureAwait(true);
            return res.ToResult();
        }
    }
}