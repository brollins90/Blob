//using Blob.Contracts.Security;
//using System;
//using System.Globalization;
//using System.Security.Claims;
//using System.Threading.Tasks;

//namespace Blob.Proxies
//{
//    public partial class IdentityManagerClient :
//        IAuthenticationManagerService
//    {
//        public Microsoft.Owin.Security.AuthenticationResponseChallenge AuthenticationResponseChallenge
//        {
//            get
//            {
//                throw new NotImplementedException();
//            }
//            set
//            {
//                throw new NotImplementedException();
//            }
//        }

//        public Microsoft.Owin.Security.AuthenticationResponseGrant AuthenticationResponseGrant
//        {
//            get
//            {
//                throw new NotImplementedException();
//            }
//            set
//            {
//                throw new NotImplementedException();
//            }
//        }

//        public Microsoft.Owin.Security.AuthenticationResponseRevoke AuthenticationResponseRevoke
//        {
//            get
//            {
//                throw new NotImplementedException();
//            }
//            set
//            {
//                throw new NotImplementedException();
//            }
//        }

//        public ClaimsPrincipal User
//        {
//            get
//            {
//                throw new NotImplementedException();
//            }
//            set
//            {
//                throw new NotImplementedException();
//            }
//        }

//        public Task<Microsoft.Owin.Security.AuthenticateResult> AuthenticateAsync(string authenticationType)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<System.Collections.Generic.IEnumerable<Microsoft.Owin.Security.AuthenticateResult>> AuthenticateAsync(string[] authenticationTypes)
//        {
//            throw new NotImplementedException();
//        }

//        public void Challenge(params string[] authenticationTypes)
//        {
//            throw new NotImplementedException();
//        }

//        public void Challenge(Microsoft.Owin.Security.AuthenticationProperties properties, params string[] authenticationTypes)
//        {
//            throw new NotImplementedException();
//        }

//        public System.Collections.Generic.IEnumerable<Microsoft.Owin.Security.AuthenticationDescription> GetAuthenticationTypes()
//        {
//            throw new NotImplementedException();
//        }

//        public System.Collections.Generic.IEnumerable<Microsoft.Owin.Security.AuthenticationDescription> GetAuthenticationTypes(Func<Microsoft.Owin.Security.AuthenticationDescription, bool> predicate)
//        {
//            throw new NotImplementedException();
//        }

//        public void SignIn(params ClaimsIdentity[] identities)
//        {
//            throw new NotImplementedException();
//        }

//        public void SignIn(Microsoft.Owin.Security.AuthenticationProperties properties, params ClaimsIdentity[] identities)
//        {
//            throw new NotImplementedException();
//        }

//        public void SignOut(params string[] authenticationTypes)
//        {
//            throw new NotImplementedException();
//        }

//        public void SignOut(Microsoft.Owin.Security.AuthenticationProperties properties, params string[] authenticationTypes)
//        {
//            throw new NotImplementedException();
//        }

//        public ClaimsIdentity CreateTwoFactorRememberBrowserIdentity(string userId)
//        {
//            throw new NotImplementedException();
//        }

//        public System.Collections.Generic.IEnumerable<Microsoft.Owin.Security.AuthenticationDescription> GetExternalAuthenticationTypes()
//        {
//            throw new NotImplementedException();
//        }

//        public Task<ClaimsIdentity> GetExternalIdentityAsync(string externalAuthenticationType)
//        {
//            throw new NotImplementedException();
//        }

//        public Microsoft.AspNet.Identity.Owin.ExternalLoginInfo GetExternalLoginInfo()
//        {
//            throw new NotImplementedException();
//        }

//        public Microsoft.AspNet.Identity.Owin.ExternalLoginInfo GetExternalLoginInfo(string xsrfKey, string expectedValue)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<Microsoft.AspNet.Identity.Owin.ExternalLoginInfo> GetExternalLoginInfoAsync()
//        {
//            throw new NotImplementedException();
//        }

//        public Task<Microsoft.AspNet.Identity.Owin.ExternalLoginInfo> GetExternalLoginInfoAsync(string xsrfKey, string expectedValue)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<bool> TwoFactorBrowserRememberedAsync(string userId)
//        {
//            throw new NotImplementedException();
//        }
//    }
//}
