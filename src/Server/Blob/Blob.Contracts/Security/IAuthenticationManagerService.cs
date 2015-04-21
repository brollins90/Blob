using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Blob.Contracts.Security
{
    public interface IAuthenticationManagerService
    {
        AuthenticationResponseChallenge AuthenticationResponseChallenge { get; set; }
        AuthenticationResponseGrant AuthenticationResponseGrant { get; set; }
        AuthenticationResponseRevoke AuthenticationResponseRevoke { get; set; }
        ClaimsPrincipal User { get; set; }
        Task<AuthenticateResult> AuthenticateAsync(string authenticationType);
        Task<IEnumerable<AuthenticateResult>> AuthenticateAsync(string[] authenticationTypes);
        void Challenge(params string[] authenticationTypes);
        void Challenge(AuthenticationProperties properties, params string[] authenticationTypes);
        IEnumerable<AuthenticationDescription> GetAuthenticationTypes();
        IEnumerable<AuthenticationDescription> GetAuthenticationTypes(Func<AuthenticationDescription, bool> predicate);
        void SignIn(params ClaimsIdentity[] identities);
        void SignIn(AuthenticationProperties properties, params ClaimsIdentity[] identities);
        void SignOut(params string[] authenticationTypes);
        void SignOut(AuthenticationProperties properties, params string[] authenticationTypes);

        //Ext
        ClaimsIdentity CreateTwoFactorRememberBrowserIdentity(string userId);
        IEnumerable<AuthenticationDescription> GetExternalAuthenticationTypes();
        Task<ClaimsIdentity> GetExternalIdentityAsync(string externalAuthenticationType);
        ExternalLoginInfo GetExternalLoginInfo();
        ExternalLoginInfo GetExternalLoginInfo(string xsrfKey, string expectedValue);
        Task<ExternalLoginInfo> GetExternalLoginInfoAsync();
        Task<ExternalLoginInfo> GetExternalLoginInfoAsync(string xsrfKey, string expectedValue);
        Task<bool> TwoFactorBrowserRememberedAsync(string userId);
    }
}
