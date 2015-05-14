using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Before.Infrastructure.Extensions;
using Blob.Contracts.Models;
using Blob.Proxies;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

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

        //public BeforeUser FindById(string userId)
        //{
        //    return new BeforeUser(FindByIdAsync(userId).Result);
        //}

        //public async Task<IdentityResult> CreateAsync(BeforeUser user, string password)
        //{
        //    IdentityResultDto res = await ((IdentityManagerClient)this).CreateAsync(user.ToDto(), password).ConfigureAwait(true);
        //    return res.ToResult();
        //}


    }
}