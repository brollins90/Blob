using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Blob.Contracts.Models;
using Blob.Contracts.ServiceContracts;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace Before.Infrastructure.Identity
{
    public class BeforeSignInManager : IDisposable, ISignInManager
    {
        public IUserManagerService UserManager { get; set; }
        public IAuthenticationManager AuthenticationManager { get; set; }

        public BeforeSignInManager(IUserManagerService userManager, IAuthenticationManager authenticationManager)
        {
            UserManager = userManager;
            AuthenticationManager = authenticationManager;
        }

        public string AuthenticationType
        {
            get { return _authenticationType ?? DefaultAuthenticationTypes.ApplicationCookie; }
            set { _authenticationType = value; }
        }
        private string _authenticationType;
        

        public virtual Task<ClaimsIdentity> CreateUserIdentityAsync(UserDto user)
        {
            return UserManager.CreateIdentityAsync(user, AuthenticationType);
        }

        private async Task SignInAsync(UserDto user, bool isPersistent, bool rememberBrowser, string password = "")
        {
            ClaimsIdentity userIdentity = await CreateUserIdentityAsync(user);
            // Clear any partial cookies from external or two factor partial sign ins
            SignOut();

            //SiteGlobalConfig.UpDictionary[user.UserName] = password;
            // todo cheat the passwork until i make the passthrough tokens work
            userIdentity.AddClaim(new Claim("username", user.UserName));
            userIdentity.AddClaim(new Claim("password", password));
            
            //if (rememberBrowser)
            //{
            //    ClaimsIdentity rememberBrowserIdentity = AuthenticationManager.CreateTwoFactorRememberBrowserIdentity(user.Id);
            //    AuthenticationManager.SignIn(new AuthenticationProperties { IsPersistent = isPersistent }, userIdentity, rememberBrowserIdentity);
            //}
            //else
            //{
                AuthenticationManager.SignIn(new AuthenticationProperties { IsPersistent = isPersistent }, userIdentity);
            //}
        }

        public virtual async Task<SignInStatusDto> PasswordSignInAsync(string userName, string password, bool isPersistent, bool shouldLockout)
        {
            if (UserManager == null)
            {
                return SignInStatusDto.Failure;
            }
            var user = await UserManager.FindByNameAsync(userName);
            if (user == null)
            {
                return SignInStatusDto.Failure;
            }
            await SignInAsync(user: user, isPersistent: isPersistent, rememberBrowser: false, password: password);
            return SignInStatusDto.Success;
            //if (await UserManager.IsLockedOutAsync(user.Id))
            //{
            //    return SignInStatusDto.LockedOut;
            //}
            //if (await UserManager.CheckPasswordAsync(user, password))
            //{
            //    await UserManager.ResetAccessFailedCountAsync(user.Id);
            //    return await SignInOrTwoFactor(user, isPersistent);
            //}
            //if (shouldLockout)
            //{
            //    // If lockout is requested, increment access failed count which might lock out the user
            //    await UserManager.AccessFailedAsync(user.Id);
            //    if (await UserManager.IsLockedOutAsync(user.Id))
            //    {
            //        return SignInStatus.LockedOut;
            //    }
            //}
            //return SignInStatus.Failure;
        }

        public void SignOut()
        {
            SignOut(AuthenticationType, DefaultAuthenticationTypes.ExternalCookie, DefaultAuthenticationTypes.TwoFactorCookie);
        }

        public void SignOut(params string[] authenticationTypes)
        {
            AuthenticationManager.SignOut(authenticationTypes);
        }


        /// <summary>
        ///     Dispose
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        ///     If disposing, calls dispose on the Context.  Always nulls out the Context
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing) { }
    }
}