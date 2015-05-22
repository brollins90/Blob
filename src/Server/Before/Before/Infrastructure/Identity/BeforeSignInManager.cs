using System;
using System.Data.Entity.Utilities;
using System.Globalization;
using System.Security.Claims;
using System.Threading.Tasks;
using Blob.Contracts.Models;
using Blob.Contracts.ServiceContracts;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;

namespace Before.Infrastructure.Identity
{
    public class BeforeSignInManager : IDisposable
    {
        public BeforeSignInManager(IUserManagerService userManager, IAuthenticationManager authenticationManager)
        {
            if (userManager == null)
            {
                throw new ArgumentNullException("userManager");
            }
            if (authenticationManager == null)
            {
                throw new ArgumentNullException("authenticationManager");
            }
            UserManager = userManager;
            AuthenticationManager = authenticationManager;
        }
        public static BeforeSignInManager Create(IdentityFactoryOptions<BeforeSignInManager> options, IOwinContext context)
        {
            return new BeforeSignInManager(context.GetUserManager<BeforeUserManager>(), context.Authentication);
        }

        /// <summary>
        /// AuthenticationType that will be used by sign in, defaults to DefaultAuthenticationTypes.ApplicationCookie
        /// </summary>
        public string AuthenticationType
        {
            get { return _authenticationType ?? DefaultAuthenticationTypes.ApplicationCookie; }
            set { _authenticationType = value; }
        }

        private string _authenticationType;

        public IUserManagerService UserManager { get; set; }
        public IAuthenticationManager AuthenticationManager { get; set; }

        public virtual Task<ClaimsIdentity> CreateUserIdentityAsync(UserDto user)
        {
            return UserManager.CreateIdentityAsync(user, AuthenticationType);
        }

        public string ConvertIdToString(Guid id)
        {
            return Convert.ToString(id, CultureInfo.InvariantCulture);
        }

        public Guid ConvertIdFromString(string id)
        {
            if (id == null)
            {
                return default(Guid);
            }
            return (Guid)Convert.ChangeType(id, typeof(Guid), CultureInfo.InvariantCulture);
        }

        public virtual async Task SignInAsync(UserDto user, bool isPersistent, bool rememberBrowser)
        {
            ClaimsIdentity userIdentity = await CreateUserIdentityAsync(user).WithCurrentCulture();
            // Clear any partial cookies from external or two factor partial sign ins
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie, DefaultAuthenticationTypes.TwoFactorCookie);
            if (rememberBrowser)
            {
                ClaimsIdentity rememberBrowserIdentity = AuthenticationManager.CreateTwoFactorRememberBrowserIdentity(user.Id);
                AuthenticationManager.SignIn(new AuthenticationProperties { IsPersistent = isPersistent }, userIdentity, rememberBrowserIdentity);
            }
            else
            {
                AuthenticationManager.SignIn(new AuthenticationProperties { IsPersistent = isPersistent }, userIdentity);
            }
        }

        public async Task<Guid> GetVerifiedUserIdAsync()
        {
            var result = await AuthenticationManager.AuthenticateAsync(DefaultAuthenticationTypes.TwoFactorCookie).WithCurrentCulture();
            if (result != null && result.Identity != null && !String.IsNullOrEmpty(result.Identity.GetUserId()))
            {
                return ConvertIdFromString(result.Identity.GetUserId());
            }
            return default(Guid);
        }

        /// <summary>
        /// Has the user been verified (ie either via password or external login)
        /// </summary>
        /// <returns></returns>
        public async Task<bool> HasBeenVerifiedAsync()
        {
            return await GetVerifiedUserIdAsync().WithCurrentCulture() != null;
        }


        /// <summary>
        /// Sign in the user in using the user name and password
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="isPersistent"></param>
        /// <param name="shouldLockout"></param>
        /// <returns></returns>
        public virtual async Task<SignInStatusDto> PasswordSignInAsync(string userName, string password, bool isPersistent, bool shouldLockout)
        {
            if (UserManager == null)
            {
                return SignInStatusDto.Failure;
            }
            var user = await UserManager.FindByNameAsync(userName).WithCurrentCulture();
            if (user == null)
            {
                return SignInStatusDto.Failure;
            }
            await SignInAsync(user, isPersistent, false);
            return SignInStatusDto.Success;
            //if (await UserManager.IsLockedOutAsync(user.Id).WithCurrentCulture())
            //{
            //    return SignInStatusDto.LockedOut;
            //}
            //if (await UserManager.CheckPasswordAsync(user, password).WithCurrentCulture())
            //{
            //    await UserManager.ResetAccessFailedCountAsync(user.Id).WithCurrentCulture();
            //    return await SignInOrTwoFactor(user, isPersistent).WithCurrentCulture();
            //}
            //if (shouldLockout)
            //{
            //    // If lockout is requested, increment access failed count which might lock out the user
            //    await UserManager.AccessFailedAsync(user.Id).WithCurrentCulture();
            //    if (await UserManager.IsLockedOutAsync(user.Id).WithCurrentCulture())
            //    {
            //        return SignInStatus.LockedOut;
            //    }
            //}
            //return SignInStatus.Failure;
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