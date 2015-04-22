using Blob.Contracts.Security;
using System;
using System.Globalization;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace Blob.Proxies
{
    public partial class IdentityManagerClient :
        ISignInManagerService<string>
    {

        public IAuthenticationManagerService AuthenticationManager
        {
            get { throw new NotSupportedException();
                //return this as IAuthenticationManagerService; 
            }
            set { throw new NotSupportedException(); }
        }

        public string AuthenticationType
        {
            get { return _authenticationType; }
            set { _authenticationType = value; }
        }
        private string _authenticationType;

        public IUserManagerService<string> UserManager
        {
            get { return this as IUserManagerService<string>; }
            set { throw new NotSupportedException(); }
        }

        public string ConvertIdFromString(string id)
        {
            if (id == null)
            {
                return default(string);
            }
            return (string)Convert.ChangeType(id, typeof(string), CultureInfo.InvariantCulture);
        }

        public string ConvertIdToString(string id)
        {
            return Convert.ToString(id, CultureInfo.InvariantCulture);
        }
        public Task<ClaimsIdentity> CreateUserIdentityAsync(UserDto user)
        {
            return UserManager.CreateIdentityAsync(user, AuthenticationType);
        }

        public async Task<SignInStatusDto> ExternalSignInAsync(ExternalLoginInfoDto loginInfo, bool isPersistent)
        {
            throw new NotImplementedException("Requireds IUserLockout");
            //var user = await UserManager.FindAsync(new UserLoginInfoDto(loginInfo.Login.LoginProvider, loginInfo.Login.ProviderKey)).WithCurrentCulture();
            //if (user == null)
            //{
            //    return SignInStatus.Failure;
            //}
            //if (await UserManager.IsLockedOutAsync(user.Id).WithCurrentCulture())
            //{
            //    return SignInStatus.LockedOut;
            //}
            //return await SignInOrTwoFactor(user, isPersistent).WithCurrentCulture();
        }

        public async Task<string> GetVerifiedUserIdAsync()
        {
            //var result = await AuthenticationManager.AuthenticateAsync(DefaultAuthenticationTypes.TwoFactorCookie).WithCurrentCulture();
            //if (result != null && result.Identity != null && !String.IsNullOrEmpty(result.Identity.GetUserId()))
            //{
            //    return ConvertIdFromString(result.Identity.GetUserId());
            //}
            return default(string);
        }

        public async Task<bool> HasBeenVerifiedAsync()
        {
            return await GetVerifiedUserIdAsync().WithCurrentCulture() != null;
        }

        public async Task<SignInStatusDto> PasswordSignInAsync(string userName, string password, bool isPersistent, bool shouldLockout)
        {
            throw new NotImplementedException("Requireds IUserLockout");
            //if (UserManager == null)
            //{
            //    return SignInStatus.Failure;
            //}
            //var user = await UserManager.FindByNameAsync(userName).WithCurrentCulture();
            //if (user == null)
            //{
            //    return SignInStatus.Failure;
            //}
            //if (await UserManager.IsLockedOutAsync(user.Id).WithCurrentCulture())
            //{
            //    return SignInStatus.LockedOut;
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

        public async Task<bool> SendTwoFactorCodeAsync(string provider)
        {
            throw new NotImplementedException("Requireds two factor");
            //var userId = await GetVerifiedUserIdAsync().WithCurrentCulture();
            //if (userId == null)
            //{
            //    return false;
            //}

            //var token = await UserManager.GenerateTwoFactorTokenAsync(userId, provider).WithCurrentCulture();
            //// See IdentityConfig.cs to plug in Email/SMS services to actually send the code
            //await UserManager.NotifyTwoFactorTokenAsync(userId, provider, token).WithCurrentCulture();
            //return true;
        }

        public async Task SignInAsync(UserDto user, bool isPersistent, bool rememberBrowser)
        {
            var userIdentity = await CreateUserIdentityAsync(user).WithCurrentCulture();
            // Clear any partial cookies from external or two factor partial sign ins
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie, DefaultAuthenticationTypes.TwoFactorCookie);
            if (rememberBrowser)
            {
                var rememberBrowserIdentity = AuthenticationManager.CreateTwoFactorRememberBrowserIdentity(ConvertIdToString(user.Id));
                AuthenticationManager.SignIn(new AuthenticationProperties { IsPersistent = isPersistent }, userIdentity, rememberBrowserIdentity);
            }
            else
            {
                AuthenticationManager.SignIn(new AuthenticationProperties { IsPersistent = isPersistent }, userIdentity);
            }
        }

        private Task<SignInStatusDto> SignInOrTwoFactor(UserDto user, bool isPersistent)
        {
            throw new NotImplementedException("Requireds IUtwo factorserLockout");
            //var id = Convert.ToString(user.Id);
            //if (await UserManager.GetTwoFactorEnabledAsync(user.Id).WithCurrentCulture()
            //    && (await UserManager.GetValidTwoFactorProvidersAsync(user.Id).WithCurrentCulture()).Count > 0
            //    && !await AuthenticationManager.TwoFactorBrowserRememberedAsync(id).WithCurrentCulture())
            //{
            //    var identity = new ClaimsIdentity(DefaultAuthenticationTypes.TwoFactorCookie);
            //    identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, id));
            //    AuthenticationManager.SignIn(identity);
            //    return SignInStatus.RequiresVerification;
            //}
            //await SignInAsync(user, isPersistent, false).WithCurrentCulture();
            //return SignInStatus.Success;
        }

        public async Task<SignInStatusDto> TwoFactorSignInAsync(string provider, string code, bool isPersistent, bool rememberBrowser)
        {
            throw new NotImplementedException("Requireds IUserLockout and twofactor");
            //var userId = await GetVerifiedUserIdAsync().WithCurrentCulture();
            //if (userId == null)
            //{
            //    return SignInStatus.Failure;
            //}
            //var user = await UserManager.FindByIdAsync(userId).WithCurrentCulture();
            //if (user == null)
            //{
            //    return SignInStatus.Failure;
            //}
            //if (await UserManager.IsLockedOutAsync(user.Id).WithCurrentCulture())
            //{
            //    return SignInStatus.LockedOut;
            //}
            //if (await UserManager.VerifyTwoFactorTokenAsync(user.Id, provider, code).WithCurrentCulture())
            //{
            //    // When token is verified correctly, clear the access failed count used for lockout
            //    await UserManager.ResetAccessFailedCountAsync(user.Id).WithCurrentCulture();
            //    await SignInAsync(user, isPersistent, rememberBrowser).WithCurrentCulture();
            //    return SignInStatus.Success;
            //}
            //// If the token is incorrect, record the failure which also may cause the user to be locked out
            //await UserManager.AccessFailedAsync(user.Id).WithCurrentCulture();
            //return SignInStatus.Failure;
        }
    }
}
