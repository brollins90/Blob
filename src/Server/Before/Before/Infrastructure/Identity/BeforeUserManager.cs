using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Before.Infrastructure.Extensions;
using Before.Models;
using Blob.Contracts.Security;
using Blob.Proxies;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

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

        //public static BeforeUserManager Create(IdentityFactoryOptions<BeforeUserManager> options, IOwinContext context)
        //{
        //    BeforeUserManager manager = new BeforeUserManager();
        //    NameValueCollection config = (NameValueCollection) ConfigurationManager.GetSection("BlobProxy");
        //    if (config == null)
        //    {
        //        throw new ConfigurationErrorsException("Cannot load config");
        //    }
        //    manager.Initialize("IdentityManagerClient", config);
        //    return manager;
        //}

        public async Task<IdentityResult> CreateAsync(BeforeUser user, string password)
        {
            IdentityResultDto res = await ((IdentityManagerClient) this).CreateAsync(user.ToDto(), password).ConfigureAwait(true);
            return res.ToResult();
        }
    }
}