using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Before.Infrastructure.Extensions;
using Blob.Contracts.Security;
using Blob.Proxies;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace Before.Infrastructure.Identity
{
    public class BeforeUserManager : IdentityManagerClient//, IAuthenticationManager
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


        //public new Task<IEnumerable<AuthenticateResult>> AuthenticateAsync(string[] authenticationTypes)
        //{
        //    throw new NotImplementedException();
        //}

        //Task<AuthenticateResult> IAuthenticationManager.AuthenticateAsync(string authenticationType)
        //{
        //    throw new NotImplementedException();
        //}

        //public new AuthenticationResponseChallenge AuthenticationResponseChallenge
        //{
        //    get
        //    {
        //        throw new NotImplementedException();
        //    }
        //    set
        //    {
        //        throw new NotImplementedException();
        //    }
        //}

        //public new AuthenticationResponseGrant AuthenticationResponseGrant
        //{
        //    get
        //    {
        //        throw new NotImplementedException();
        //    }
        //    set
        //    {
        //        throw new NotImplementedException();
        //    }
        //}

        //public new AuthenticationResponseRevoke AuthenticationResponseRevoke
        //{
        //    get
        //    {
        //        throw new NotImplementedException();
        //    }
        //    set
        //    {
        //        throw new NotImplementedException();
        //    }
        //}

        //public void Challenge(params string[] authenticationTypes)
        //{
        //    throw new NotImplementedException();
        //}

        //public void Challenge(AuthenticationProperties properties, params string[] authenticationTypes)
        //{
        //    throw new NotImplementedException();
        //}

        //public System.Collections.Generic.IEnumerable<AuthenticationDescription> GetAuthenticationTypes(Func<AuthenticationDescription, bool> predicate)
        //{
        //    throw new NotImplementedException();
        //}

        //public System.Collections.Generic.IEnumerable<AuthenticationDescription> GetAuthenticationTypes()
        //{
        //    throw new NotImplementedException();
        //}

        //public void SignIn(params ClaimsIdentity[] identities)
        //{
        //    throw new NotImplementedException();
        //}

        //public void SignIn(AuthenticationProperties properties, params ClaimsIdentity[] identities)
        //{
        //    throw new NotImplementedException();
        //}

        //public void SignOut(params string[] authenticationTypes)
        //{
        //    throw new NotImplementedException();
        //}

        //public void SignOut(AuthenticationProperties properties, params string[] authenticationTypes)
        //{
        //    throw new NotImplementedException();
        //}
    }
}