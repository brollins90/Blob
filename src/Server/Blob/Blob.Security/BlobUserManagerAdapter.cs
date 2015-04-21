using Blob.Contracts.Security;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.ServiceModel;
using System.Threading.Tasks;

namespace Blob.Security
{
    [ServiceBehavior]
    public class BlobUserManagerAdapter : IUserManagerService
    {
        private readonly BlobUserManager _manager;

        public BlobUserManagerAdapter(BlobUserManager manager)
        {
            _manager = manager;
        }

        public void SetProvider(string providerName)
        {
            _manager.SetProvider(providerName);
        }

        public IPasswordHasher PasswordHasher
        {
            get { return _manager.PasswordHasher; }
        }

        public IIdentityValidator<IUser> UserValidator
        {
            get { throw new NotImplementedException(); }
        }

        public IIdentityValidator<string> PasswordValidator
        {
            get { return _manager.PasswordValidator; }
        }

        public IClaimsIdentityFactory<IUser, string> ClaimsIdentityFactory
        {
            get { throw new NotImplementedException(); }
        }

        public IIdentityMessageService EmailService
        {
            get { return  _manager.EmailService; }
        }

        public IIdentityMessageService SmsService
        {
            get { return _manager.SmsService; }
        }

        public IUserTokenProvider<IUser, string> UserTokenProvider
        {
            get { throw new NotImplementedException(); }
        }

        public bool UserLockoutEnabledByDefault
        {
            get { return _manager.UserLockoutEnabledByDefault; }
        }

        public int MaxFailedAccessAttemptsBeforeLockout
        {
            get { return _manager.MaxFailedAccessAttemptsBeforeLockout; }
        }

        public TimeSpan DefaultAccountLockoutTimeSpan
        {
            get { return _manager.DefaultAccountLockoutTimeSpan; }
        }

        public bool SupportsUserTwoFactor
        {
            get { return _manager.SupportsUserTwoFactor; }
        }

        public bool SupportsUserPassword
        {
            get { return _manager.SupportsUserPassword; }
        }

        public bool SupportsUserSecurityStamp
        {
            get { return _manager.SupportsUserSecurityStamp; }
        }

        public bool SupportsUserRole
        {
            get { return _manager.SupportsUserRole; }
        }

        public bool SupportsUserLogin
        {
            get { return _manager.SupportsUserLogin; }
        }

        public bool SupportsUserEmail
        {
            get { return _manager.SupportsUserEmail; }
        }

        public bool SupportsUserPhoneNumber
        {
            get { return _manager.SupportsUserPhoneNumber; }
        }

        public bool SupportsUserClaim
        {
            get { return _manager.SupportsUserClaim; }
        }

        public bool SupportsUserLockout
        {
            get { return _manager.SupportsUserLockout; }
        }

        public bool SupportsQueryableUsers
        {
            get { return _manager.SupportsQueryableUsers; }
        }

        public IQueryable<IUser> Users
        {
            get { throw new NotImplementedException(); }
        }

        public IDictionary<string, IUserTokenProvider<IUser, string>> TwoFactorProviders
        {
            get { throw new NotImplementedException(); }
        }

        public Task<ClaimsIdentity> CreateIdentityAsync(IUser user, string authenticationType)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResultDto> CreateAsync(IUser user)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResultDto> UpdateAsync(IUser user)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResultDto> DeleteAsync(IUser user)
        {
            throw new NotImplementedException();
        }

        public Task<IUser> FindByIdAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<IUser> FindByNameAsync(string userName)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResultDto> CreateAsync(IUser user, string password)
        {
            throw new NotImplementedException();
        }

        public Task<IUser> FindAsync(string userName, string password)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CheckPasswordAsync(IUser user, string password)
        {
            throw new NotImplementedException();
        }

        public Task<bool> HasPasswordAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResultDto> AddPasswordAsync(string userId, string password)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResultDto> ChangePasswordAsync(string userId, string currentPassword, string newPassword)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResultDto> RemovePasswordAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetSecurityStampAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResultDto> UpdateSecurityStampAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<string> GeneratePasswordResetTokenAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResultDto> ResetPasswordAsync(string userId, string token, string newPassword)
        {
            throw new NotImplementedException();
        }

        public Task<IUser> FindAsync(UserLoginInfoDto login)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResultDto> RemoveLoginAsync(string userId, UserLoginInfoDto login)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResultDto> AddLoginAsync(string userId, UserLoginInfoDto login)
        {
            throw new NotImplementedException();
        }

        public Task<IList<UserLoginInfoDto>> GetLoginsAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResultDto> AddClaimAsync(string userId, Claim claim)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResultDto> RemoveClaimAsync(string userId, Claim claim)
        {
            throw new NotImplementedException();
        }

        public Task<IList<Claim>> GetClaimsAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResultDto> AddToRoleAsync(string userId, string role)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResultDto> AddToRolesAsync(string userId, string[] roles)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResultDto> RemoveFromRoleAsync(string userId, string role)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResultDto> RemoveFromRolesAsync(string userId, string[] roles)
        {
            throw new NotImplementedException();
        }

        public Task<IList<string>> GetRolesAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsInRoleAsync(string userId, string role)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetEmailAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResultDto> SetEmailAsync(string userId, string email)
        {
            throw new NotImplementedException();
        }

        public Task<IUser> FindByEmailAsync(string email)
        {
            throw new NotImplementedException();
        }

        public Task<string> GenerateEmailConfirmationTokenAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResultDto> ConfirmEmailAsync(string userId, string token)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsEmailConfirmedAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetPhoneNumberAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResultDto> SetPhoneNumberAsync(string userId, string phoneNumber)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResultDto> ChangePhoneNumberAsync(string userId, string phoneNumber, string token)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsPhoneNumberConfirmedAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<string> GenerateChangePhoneNumberTokenAsync(string userId, string phoneNumber)
        {
            throw new NotImplementedException();
        }

        public Task<bool> VerifyChangePhoneNumberTokenAsync(string userId, string token, string phoneNumber)
        {
            throw new NotImplementedException();
        }

        public Task<bool> VerifyUserTokenAsync(string userId, string purpose, string token)
        {
            throw new NotImplementedException();
        }

        public Task<string> GenerateUserTokenAsync(string purpose, string userId)
        {
            throw new NotImplementedException();
        }

        public void RegisterTwoFactorProvider(string twoFactorProvider, IUserTokenProvider<IUser, string> provider)
        {
            throw new NotImplementedException();
        }

        public Task<IList<string>> GetValidTwoFactorProvidersAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> VerifyTwoFactorTokenAsync(string userId, string twoFactorProvider, string token)
        {
            throw new NotImplementedException();
        }

        public Task<string> GenerateTwoFactorTokenAsync(string userId, string twoFactorProvider)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResultDto> NotifyTwoFactorTokenAsync(string userId, string twoFactorProvider, string token)
        {
            throw new NotImplementedException();
        }

        public Task<bool> GetTwoFactorEnabledAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResultDto> SetTwoFactorEnabledAsync(string userId, bool enabled)
        {
            throw new NotImplementedException();
        }

        public Task SendEmailAsync(string userId, string subject, string body)
        {
            throw new NotImplementedException();
        }

        public Task SendSmsAsync(string userId, string message)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsLockedOutAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResultDto> SetLockoutEnabledAsync(string userId, bool enabled)
        {
            throw new NotImplementedException();
        }

        public Task<bool> GetLockoutEnabledAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<DateTimeOffset> GetLockoutEndDateAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResultDto> SetLockoutEndDateAsync(string userId, DateTimeOffset lockoutEnd)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResultDto> AccessFailedAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResultDto> ResetAccessFailedCountAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<int> GetAccessFailedCountAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public IAuthenticationManagerService AuthenticationManager
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public string AuthenticationType
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public IUserManagerService<IUser, string> UserManager
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public string ConvertIdFromString(string id)
        {
            throw new NotImplementedException();
        }

        public string ConvertIdToString(string id)
        {
            throw new NotImplementedException();
        }

        public Task<ClaimsIdentity> CreateUserIdentityAsync(IUser user)
        {
            throw new NotImplementedException();
        }

        public Task<SignInStatus> ExternalSignInAsync(ExternalLoginInfo loginInfo, bool isPersistent)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetVerifiedUserIdAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> HasBeenVerifiedAsync()
        {
            throw new NotImplementedException();
        }

        public Task<SignInStatus> PasswordSignInAsync(string userName, string password, bool isPersistent, bool shouldLockout)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SendTwoFactorCodeAsync(string provider)
        {
            throw new NotImplementedException();
        }

        public Task SignInAsync(IUser user, bool isPersistent, bool rememberBrowser)
        {
            throw new NotImplementedException();
        }

        public Task<SignInStatus> TwoFactorSignInAsync(string provider, string code, bool isPersistent, bool rememberBrowser)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public AuthenticationResponseChallenge AuthenticationResponseChallenge
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public AuthenticationResponseGrant AuthenticationResponseGrant
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public AuthenticationResponseRevoke AuthenticationResponseRevoke
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public ClaimsPrincipal User
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public Task<AuthenticateResult> AuthenticateAsync(string authenticationType)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<AuthenticateResult>> AuthenticateAsync(string[] authenticationTypes)
        {
            throw new NotImplementedException();
        }

        public void Challenge(params string[] authenticationTypes)
        {
            throw new NotImplementedException();
        }

        public void Challenge(AuthenticationProperties properties, params string[] authenticationTypes)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AuthenticationDescription> GetAuthenticationTypes()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AuthenticationDescription> GetAuthenticationTypes(Func<AuthenticationDescription, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public void SignIn(params ClaimsIdentity[] identities)
        {
            throw new NotImplementedException();
        }

        public void SignIn(AuthenticationProperties properties, params ClaimsIdentity[] identities)
        {
            throw new NotImplementedException();
        }

        public void SignOut(params string[] authenticationTypes)
        {
            throw new NotImplementedException();
        }

        public void SignOut(AuthenticationProperties properties, params string[] authenticationTypes)
        {
            throw new NotImplementedException();
        }

        public ClaimsIdentity CreateTwoFactorRememberBrowserIdentity(string userId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AuthenticationDescription> GetExternalAuthenticationTypes()
        {
            throw new NotImplementedException();
        }

        public Task<ClaimsIdentity> GetExternalIdentityAsync(string externalAuthenticationType)
        {
            throw new NotImplementedException();
        }

        public ExternalLoginInfo GetExternalLoginInfo()
        {
            throw new NotImplementedException();
        }

        public ExternalLoginInfo GetExternalLoginInfo(string xsrfKey, string expectedValue)
        {
            throw new NotImplementedException();
        }

        public Task<ExternalLoginInfo> GetExternalLoginInfoAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ExternalLoginInfo> GetExternalLoginInfoAsync(string xsrfKey, string expectedValue)
        {
            throw new NotImplementedException();
        }

        public Task<bool> TwoFactorBrowserRememberedAsync(string userId)
        {
            throw new NotImplementedException();
        }
    }
}
