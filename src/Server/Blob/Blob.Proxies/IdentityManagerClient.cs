using Blob.Contracts.Security;
using Blob.Core.Domain;
using Blob.Core.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.ServiceModel;
using System.Threading.Tasks;
using log4net;

namespace Blob.Proxies
{
    // UserManager
    public class IdentityManagerClient : 
        //UserManager<IUser, string>,
        IUserManagerService<IUser, string>//,
        //ISignInManagerService<IUser, string>,
        //IAuthenticationManagerService
    {
        private readonly ILog _log;
        private static ChannelFactory<IUserManagerService> _channelFactory;

        public IdentityManagerClient()// : base(new NullStore()) 
        {
            _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        }

        public void Initialize(string name, NameValueCollection config)
        {
            try
            {
                _log.Debug("Initializing identity manager proxy");
                if (config == null)
                    throw new ArgumentNullException("config");

                Config = config;
                
                _log.Debug(string.Format("Remote provider type is {0}", ProxyProviderName));
            }
            catch (Exception e)
            {
                _log.Error("Failed to load identity manager client.", e);
                throw;
            }
        }

        /// <summary>
        /// After the caller has completed its work with the object,
        /// it should then pass the object to DisposeProvider to close
        /// the connection.
        /// </summary>
        private IUserManagerService RemoteProvider()
        {
            if (_channelFactory == null)
            {
                _channelFactory = new ChannelFactory<IUserManagerService>("IdentityService");
            }

            IUserManagerService provider = _channelFactory.CreateChannel();
            provider.SetProvider(ProxyProviderName);

            return provider;
        }

        /// <summary>
        /// This method should be called to handle closing the 
        /// connected proxy object.
        /// </summary>
        private void DisposeRemoteProvider(IMembershipService remoteProvider)
        {
            _log.Debug("Disposing of remote provider.");

            // TODO: Add error checking for state of object.
            ((IClientChannel)remoteProvider).Dispose();
        }


        private NameValueCollection Config { get; set; }

        /// <summary>
        /// Indicates whether to log exceptions
        /// </summary>
        /// <returns>true if the membership provider is configured to log exceptions; otherwise, false. The default is false.</returns>
        public bool LogExceptions
        {
            get
            {
                if (_logExceptions.HasValue == false)
                {
                    bool bv;
                    string strv = Config["LogExceptions"];
                    if (!string.IsNullOrEmpty(strv) && bool.TryParse(strv, out bv))
                        _logExceptions = bv;
                    else
                        _logExceptions = true;
                }
                return _logExceptions.Value;
            }
        }
        private bool? _logExceptions;


        public string ProxyProviderName
        {
            get
            {
                if (string.IsNullOrEmpty(_proxyProviderName))
                {
                    _proxyProviderName = Config["ProxyProviderName"];
                    if (string.IsNullOrEmpty(_proxyProviderName))
                        _proxyProviderName = "";
                }
                return _proxyProviderName;
            }
            set { _proxyProviderName = value; }
        }
        private string _proxyProviderName;


        public void SetProvider(string providerName)
        {
            throw new NotImplementedException();
        }

        

        

        

        

        

        

        

        

        

        #region IAuthenticationManagerService - Microsoft.Owin.Security.IAuthenticationManager

        public AuthenticationResponseChallenge AuthenticationResponseChallenge
        {
            get { return _authenticationResponseChallenge; }
            set { _authenticationResponseChallenge = value; }
        }
        private AuthenticationResponseChallenge _authenticationResponseChallenge;

        public AuthenticationResponseGrant AuthenticationResponseGrant
        {
            get { return _authenticationResponseGrant; }
            set { _authenticationResponseGrant = value; }
        }
        private AuthenticationResponseGrant _authenticationResponseGrant;

        public AuthenticationResponseRevoke AuthenticationResponseRevoke
        {
            get { return _authenticationResponseRevoke; }
            set { _authenticationResponseRevoke = value; }
        }
        private AuthenticationResponseRevoke _authenticationResponseRevoke;

        public ClaimsPrincipal User
        {
            get { return _user; }
            set { _user = value; }
        }
        private ClaimsPrincipal _user;

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




        //Extensions

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

#endregion 

        #region ISignInManagerService - SignInManager

        public IAuthenticationManagerService AuthenticationManager
        {
            get { return _authenticationManager; }
            set { _authenticationManager = value; }
        }
        private IAuthenticationManagerService _authenticationManager;

        public string AuthenticationType
        {
            get { return _authenticationType; }
            set { _authenticationType = value; }
        }
        private string _authenticationType;

        public IUserManagerService<IUser, string> UserManager
        {
            get { return this; }
            set { throw new NotSupportedException(); }
        }

        public Task<ClaimsIdentity> CreateUserIdentityAsync(IUser user)
        {
            return UserManager.CreateIdentityAsync(user, AuthenticationType);
        }

        public string ConvertIdToString(string id)
        {
            return Convert.ToString(id, CultureInfo.InvariantCulture);
        }

        public string ConvertIdFromString(string id)
        {
            if (id == null)
            {
                return default(string);
            }
            return (string)Convert.ChangeType(id, typeof(string), CultureInfo.InvariantCulture);
        }

        public async Task SignInAsync(IUser user, bool isPersistent, bool rememberBrowser)
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

        public async Task<bool> SendTwoFactorCodeAsync(string provider)
        {
            var userId = await GetVerifiedUserIdAsync().WithCurrentCulture();
            if (userId == null)
            {
                return false;
            }

            var token = await UserManager.GenerateTwoFactorTokenAsync(userId, provider).WithCurrentCulture();
            // See IdentityConfig.cs to plug in Email/SMS services to actually send the code
            await UserManager.NotifyTwoFactorTokenAsync(userId, provider, token).WithCurrentCulture();
            return true;
        }

        public async Task<string> GetVerifiedUserIdAsync()
        {
            var result = await AuthenticationManager.AuthenticateAsync(DefaultAuthenticationTypes.TwoFactorCookie).WithCurrentCulture();
            if (result != null && result.Identity != null && !String.IsNullOrEmpty(result.Identity.GetUserId()))
            {
                return ConvertIdFromString(result.Identity.GetUserId());
            }
            return default(string);
        }

        public async Task<bool> HasBeenVerifiedAsync()
        {
            return await GetVerifiedUserIdAsync().WithCurrentCulture() != null;
        }

        public async Task<SignInStatus> TwoFactorSignInAsync(string provider, string code, bool isPersistent, bool rememberBrowser)
        {
            var userId = await GetVerifiedUserIdAsync().WithCurrentCulture();
            if (userId == null)
            {
                return SignInStatus.Failure;
            }
            var user = await UserManager.FindByIdAsync(userId).WithCurrentCulture();
            if (user == null)
            {
                return SignInStatus.Failure;
            }
            if (await UserManager.IsLockedOutAsync(user.Id).WithCurrentCulture())
            {
                return SignInStatus.LockedOut;
            }
            if (await UserManager.VerifyTwoFactorTokenAsync(user.Id, provider, code).WithCurrentCulture())
            {
                // When token is verified correctly, clear the access failed count used for lockout
                await UserManager.ResetAccessFailedCountAsync(user.Id).WithCurrentCulture();
                await SignInAsync(user, isPersistent, rememberBrowser).WithCurrentCulture();
                return SignInStatus.Success;
            }
            // If the token is incorrect, record the failure which also may cause the user to be locked out
            await UserManager.AccessFailedAsync(user.Id).WithCurrentCulture();
            return SignInStatus.Failure;
        }

        public async Task<SignInStatus> ExternalSignInAsync(ExternalLoginInfo loginInfo, bool isPersistent)
        {
            var user = await UserManager.FindAsync(new UserLoginInfoDto(loginInfo.Login.LoginProvider, loginInfo.Login.ProviderKey)).WithCurrentCulture();
            if (user == null)
            {
                return SignInStatus.Failure;
            }
            if (await UserManager.IsLockedOutAsync(user.Id).WithCurrentCulture())
            {
                return SignInStatus.LockedOut;
            }
            return await SignInOrTwoFactor(user, isPersistent).WithCurrentCulture();
        }

        private async Task<SignInStatus> SignInOrTwoFactor(IUser user, bool isPersistent)
        {
            var id = Convert.ToString(user.Id);
            if (await UserManager.GetTwoFactorEnabledAsync(user.Id).WithCurrentCulture()
                && (await UserManager.GetValidTwoFactorProvidersAsync(user.Id).WithCurrentCulture()).Count > 0
                && !await AuthenticationManager.TwoFactorBrowserRememberedAsync(id).WithCurrentCulture())
            {
                var identity = new ClaimsIdentity(DefaultAuthenticationTypes.TwoFactorCookie);
                identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, id));
                AuthenticationManager.SignIn(identity);
                return SignInStatus.RequiresVerification;
            }
            await SignInAsync(user, isPersistent, false).WithCurrentCulture();
            return SignInStatus.Success;
        }

        public async Task<SignInStatus> PasswordSignInAsync(string userName, string password, bool isPersistent, bool shouldLockout)
        {
            if (UserManager == null)
            {
                return SignInStatus.Failure;
            }
            var user = await UserManager.FindByNameAsync(userName).WithCurrentCulture();
            if (user == null)
            {
                return SignInStatus.Failure;
            }
            if (await UserManager.IsLockedOutAsync(user.Id).WithCurrentCulture())
            {
                return SignInStatus.LockedOut;
            }
            if (await UserManager.CheckPasswordAsync(user, password).WithCurrentCulture())
            {
                await UserManager.ResetAccessFailedCountAsync(user.Id).WithCurrentCulture();
                return await SignInOrTwoFactor(user, isPersistent).WithCurrentCulture();
            }
            if (shouldLockout)
            {
                // If lockout is requested, increment access failed count which might lock out the user
                await UserManager.AccessFailedAsync(user.Id).WithCurrentCulture();
                if (await UserManager.IsLockedOutAsync(user.Id).WithCurrentCulture())
                {
                    return SignInStatus.LockedOut;
                }
            }
            return SignInStatus.Failure;
        }

        #endregion


        #region UserManager


        private IUserManagerService<User, Guid> _userManager;
        private IPasswordHasher _passwordHasher;
        private IIdentityValidator<IUser> _userValidator;
        private IIdentityValidator<string> _passwordValidator;
        private IClaimsIdentityFactory<IUser, string> _claimsIdentityFactory;
        private IIdentityMessageService _emailService;
        private IIdentityMessageService _smsService;
        private IUserTokenProvider<IUser, string> _userTokenProvider;
        private bool _userLockoutEnabledByDefault;
        private int _maxFailedAccessAttemptsBeforeLockout;
        private TimeSpan _defaultAccountLockoutTimeSpan;
        private bool _supportsUserTwoFactor;
        private bool _supportsUserPassword;
        private bool _supportsUserSecurityStamp;
        private bool _supportsUserRole;
        private bool _supportsUserLogin;
        private bool _supportsUserEmail;
        private bool _supportsUserPhoneNumber;
        private bool _supportsUserClaim;
        private bool _supportsUserLockout;
        private bool _supportsQueryableUsers;
        private IQueryable<IUser> _users;
        private IDictionary<string, IUserTokenProvider<IUser, string>> _twoFactorProviders;

        public IPasswordHasher PasswordHasher
        {
            get { return _passwordHasher; }
        }

        public IIdentityValidator<IUser> UserValidator
        {
            get { return _userValidator; }
        }

        public IIdentityValidator<string> PasswordValidator
        {
            get { return _passwordValidator; }
        }

        public IClaimsIdentityFactory<IUser, string> ClaimsIdentityFactory
        {
            get { return _claimsIdentityFactory; }
        }

        public IIdentityMessageService EmailService
        {
            get { return _emailService; }
        }

        public IIdentityMessageService SmsService
        {
            get { return _smsService; }
        }

        public IUserTokenProvider<IUser, string> UserTokenProvider
        {
            get { return _userTokenProvider; }
        }

        public bool UserLockoutEnabledByDefault
        {
            get { return _userLockoutEnabledByDefault; }
        }

        public int MaxFailedAccessAttemptsBeforeLockout
        {
            get { return _maxFailedAccessAttemptsBeforeLockout; }
        }

        public TimeSpan DefaultAccountLockoutTimeSpan
        {
            get { return _defaultAccountLockoutTimeSpan; }
        }

        public bool SupportsUserTwoFactor
        {
            get { return _supportsUserTwoFactor; }
        }

        public bool SupportsUserPassword
        {
            get { return _supportsUserPassword; }
        }

        public bool SupportsUserSecurityStamp
        {
            get { return _supportsUserSecurityStamp; }
        }

        public bool SupportsUserRole
        {
            get { return _supportsUserRole; }
        }

        public bool SupportsUserLogin
        {
            get { return _supportsUserLogin; }
        }

        public bool SupportsUserEmail
        {
            get { return _supportsUserEmail; }
        }

        public bool SupportsUserPhoneNumber
        {
            get { return _supportsUserPhoneNumber; }
        }

        public bool SupportsUserClaim
        {
            get { return _supportsUserClaim; }
        }

        public bool SupportsUserLockout
        {
            get { return _supportsUserLockout; }
        }

        public bool SupportsQueryableUsers
        {
            get { return _supportsQueryableUsers; }
        }

        public IQueryable<IUser> Users
        {
            get { return _users; }
        }

        public IDictionary<string, IUserTokenProvider<IUser, string>> TwoFactorProviders
        {
            get { return _twoFactorProviders; }
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

        //public Task<string> GetPhoneNumberAsync(string userId)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<IdentityResultDto> SetPhoneNumberAsync(string userId, string phoneNumber)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<IdentityResultDto> ChangePhoneNumberAsync(string userId, string phoneNumber, string token)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<bool> IsPhoneNumberConfirmedAsync(string userId)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<string> GenerateChangePhoneNumberTokenAsync(string userId, string phoneNumber)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<bool> VerifyChangePhoneNumberTokenAsync(string userId, string token, string phoneNumber)
        //{
        //    throw new NotImplementedException();
        //}

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

        #endregion

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }

    internal class NullStore : IUserStore<IUser, string>
    {
        public void Dispose() { return; }
        public Task CreateAsync(IUser user) { throw new NotImplementedException(); }
        public Task UpdateAsync(IUser user) { throw new NotImplementedException(); }
        public Task DeleteAsync(IUser user) { throw new NotImplementedException(); }
        public Task<IUser> FindByIdAsync(string userId) { throw new NotImplementedException(); }
        public Task<IUser> FindByNameAsync(string userName) { throw new NotImplementedException(); }
    }
}
