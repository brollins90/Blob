//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Security.Claims;
//using System.ServiceModel;
//using System.Threading.Tasks;
//using Blob.Contracts.Security;
//using Blob.Core.Domain;
//using Microsoft.AspNet.Identity;
//using Microsoft.AspNet.Identity.Owin;
//using Microsoft.Owin.Security;

//namespace Blob.Security
//{
//    public class StringUser : IUser
//    {
//        public string Id { get; set; }
//        public string UserName { get; set; }
//    }

//    public static class UserConverter
//    {
        
//        public static User UserFromIUser(IUser user)
//        {
//            return (user == null)
//                ? null
//                : new User { Id = Guid.Parse(user.Id), UserName = user.UserName };
//        }

//        // ReSharper disable once InconsistentNaming
//        public static IUser IUserFromUser(User user)
//        {
//            return (user == null)
//                ? null
//                : new StringUser { Id = user.Id.ToString(), UserName = user.UserName };
//        }

//        public static Guid ToGuid(this string s)
//        {
//            return Guid.Parse(s);
//        }

//        //public static StringUser ToStringUser(this User user)
//        //{
//        //    return new StringUser { Id = user.Id.ToString(), UserName = user.UserName };
//        //}
//    }


//    [ServiceBehavior]
//    public class BlobUserManagerAdapter : IUserManagerService
//    {
//        private readonly BlobUserManager _manager;
//        private bool _disposed;

//        public BlobUserManagerAdapter(BlobUserManager manager)
//        {
//            _manager = manager;
//        }

//        public void SetProvider(string providerName)
//        {
//            ThrowIfDisposed();
//            _manager.SetProvider(providerName);
//        }

//        public IPasswordHasher PasswordHasher
//        {
//            get
//            {
//                ThrowIfDisposed(); 
//                return _manager.PasswordHasher;
//            }
//        }

//        public IIdentityValidator<IUser> UserValidator
//        {
//            get
//            {
//                ThrowIfDisposed(); 
//                throw new NotImplementedException();
//            }
//        }

//        public IIdentityValidator<string> PasswordValidator
//        {
//            get
//            {
//                ThrowIfDisposed();
//                return _manager.PasswordValidator;
//            }
//        }

//        public IClaimsIdentityFactory<IUser, string> ClaimsIdentityFactory
//        {
//            get
//            {
//                ThrowIfDisposed();
//                throw new NotImplementedException();
//            }
//        }

//        public IIdentityMessageService EmailService
//        {
//            get
//            {
//                ThrowIfDisposed();
//                return _manager.EmailService;
//            }
//        }

//        public IIdentityMessageService SmsService
//        {
//            get
//            {
//                ThrowIfDisposed();
//                return _manager.SmsService;
//            }
//        }

//        public IUserTokenProvider<IUser, string> UserTokenProvider
//        {
//            get
//            {
//                ThrowIfDisposed();
//                throw new NotImplementedException();
//            }
//        }

//        public bool UserLockoutEnabledByDefault
//        {
//            get
//            {
//                ThrowIfDisposed();
//                return _manager.UserLockoutEnabledByDefault;
//            }
//        }

//        public int MaxFailedAccessAttemptsBeforeLockout
//        {
//            get
//            {
//                ThrowIfDisposed();
//                return _manager.MaxFailedAccessAttemptsBeforeLockout;
//            }
//        }

//        public TimeSpan DefaultAccountLockoutTimeSpan
//        {
//            get
//            {
//                ThrowIfDisposed();
//                return _manager.DefaultAccountLockoutTimeSpan;
//            }
//        }

//        public bool SupportsUserTwoFactor
//        {
//            get
//            {
//                ThrowIfDisposed();
//                return _manager.SupportsUserTwoFactor;
//            }
//        }

//        public bool SupportsUserPassword
//        {
//            get
//            {
//                ThrowIfDisposed();
//                return _manager.SupportsUserPassword;
//            }
//        }

//        public bool SupportsUserSecurityStamp
//        {
//            get
//            {
//                ThrowIfDisposed();
//                return _manager.SupportsUserSecurityStamp;
//            }
//        }

//        public bool SupportsUserRole
//        {
//            get
//            {
//                ThrowIfDisposed();
//                return _manager.SupportsUserRole;
//            }
//        }

//        public bool SupportsUserLogin
//        {
//            get
//            {
//                ThrowIfDisposed();
//                return _manager.SupportsUserLogin;
//            }
//        }

//        public bool SupportsUserEmail
//        {
//            get
//            {
//                ThrowIfDisposed();
//                return _manager.SupportsUserEmail;
//            }
//        }

//        public bool SupportsUserPhoneNumber
//        {
//            get
//            {
//                ThrowIfDisposed();
//                return _manager.SupportsUserPhoneNumber;
//            }
//        }

//        public bool SupportsUserClaim
//        {
//            get
//            {
//                ThrowIfDisposed();
//                return _manager.SupportsUserClaim;
//            }
//        }

//        public bool SupportsUserLockout
//        {
//            get
//            {
//                ThrowIfDisposed();
//                return _manager.SupportsUserLockout;
//            }
//        }

//        public bool SupportsQueryableUsers
//        {
//            get
//            {
//                ThrowIfDisposed();
//                return _manager.SupportsQueryableUsers;
//            }
//        }

//        public IQueryable<IUser> Users
//        {
//            get
//            {
//                ThrowIfDisposed();
//                throw new NotImplementedException();
//            }
//        }

//        public IDictionary<string, IUserTokenProvider<IUser, string>> TwoFactorProviders
//        {
//            get
//            {
//                ThrowIfDisposed();
//                throw new NotImplementedException();
//            }
//        }

//        public Task<ClaimsIdentity> CreateIdentityAsync(IUser user, string authenticationType)
//        {
//            ThrowIfDisposed(); 
//            return _manager.CreateIdentityAsync(UserConverter.UserFromIUser(user), authenticationType);
//        }

//        public Task<IdentityResultDto> CreateAsync(IUser user)
//        {
//            ThrowIfDisposed(); 
//            return _manager.CreateAsync(UserConverter.UserFromIUser(user));
//        }

//        public Task<IdentityResultDto> UpdateAsync(IUser user)
//        {
//            ThrowIfDisposed(); 
//            return _manager.UpdateAsync(UserConverter.UserFromIUser(user));
//        }

//        public Task<IdentityResultDto> DeleteAsync(IUser user)
//        {
//            ThrowIfDisposed(); 
//            return _manager.DeleteAsync(UserConverter.UserFromIUser(user));
//        }

//        public Task<IUser> FindByIdAsync(string userId)
//        {
//            ThrowIfDisposed(); 
//            User u = _manager.FindByIdAsync(userId.ToGuid()).Result;
//            return Task.FromResult(UserConverter.IUserFromUser(u));
//        }

//        public Task<IUser> FindByNameAsync(string userName)
//        {
//            ThrowIfDisposed(); 
//            User u = _manager.FindByNameAsync(userName).Result;
//            return Task.FromResult(UserConverter.IUserFromUser(u));
//        }

//        public Task<IdentityResultDto> CreateAsync(IUser user, string password)
//        {
//            ThrowIfDisposed(); 
//            return _manager.CreateAsync(UserConverter.UserFromIUser(user), password);
//        }

//        public Task<IUser> FindAsync(string userName, string password)
//        {
//            ThrowIfDisposed(); 
//            User u = _manager.FindAsync(userName, password).Result;
//            return Task.FromResult(UserConverter.IUserFromUser(u));
//        }

//        public Task<bool> CheckPasswordAsync(IUser user, string password)
//        {
//            ThrowIfDisposed(); 
//            return _manager.CheckPasswordAsync(UserConverter.UserFromIUser(user), password);
//        }

//        public Task<bool> HasPasswordAsync(string userId)
//        {
//            ThrowIfDisposed(); 
//            return _manager.HasPasswordAsync(userId.ToGuid());
//        }

//        public Task<IdentityResultDto> AddPasswordAsync(string userId, string password)
//        {
//            ThrowIfDisposed(); 
//            return _manager.AddPasswordAsync(userId.ToGuid(), password);
//        }

//        public Task<IdentityResultDto> ChangePasswordAsync(string userId, string currentPassword, string newPassword)
//        {
//            ThrowIfDisposed(); 
//            return _manager.ChangePasswordAsync(userId.ToGuid(), currentPassword, newPassword);
//        }

//        public Task<IdentityResultDto> RemovePasswordAsync(string userId)
//        {
//            ThrowIfDisposed(); 
//            return _manager.RemovePasswordAsync(userId.ToGuid());
//        }

//        public Task<string> GetSecurityStampAsync(string userId)
//        {
//            ThrowIfDisposed();
//            return _manager.GetSecurityStampAsync(userId.ToGuid());
//        }

//        public Task<IdentityResultDto> UpdateSecurityStampAsync(string userId)
//        {
//            ThrowIfDisposed();
//            return _manager.UpdateSecurityStampAsync(userId.ToGuid());
//        }

//        public Task<string> GeneratePasswordResetTokenAsync(string userId)
//        {
//            ThrowIfDisposed();
//            return _manager.GeneratePasswordResetTokenAsync(userId.ToGuid());
//        }

//        public Task<IdentityResultDto> ResetPasswordAsync(string userId, string token, string newPassword)
//        {
//            ThrowIfDisposed();
//            return _manager.ResetPasswordAsync(userId.ToGuid(), token, newPassword);
//        }

//        public Task<IUser> FindAsync(UserLoginInfoDto login)
//        {
//            ThrowIfDisposed();
//            User u = _manager.FindAsync(login).Result;
//            return Task.FromResult(UserConverter.IUserFromUser(u));
//        }

//        public Task<IdentityResultDto> RemoveLoginAsync(string userId, UserLoginInfoDto login)
//        {
//            ThrowIfDisposed();
//            return _manager.RemoveLoginAsync(userId.ToGuid(), login);
//        }

//        public Task<IdentityResultDto> AddLoginAsync(string userId, UserLoginInfoDto login)
//        {
//            ThrowIfDisposed();
//            return _manager.AddLoginAsync(userId.ToGuid(), login);
//        }

//        public Task<IList<UserLoginInfoDto>> GetLoginsAsync(string userId)
//        {
//            ThrowIfDisposed();
//            return _manager.GetLoginsAsync(userId.ToGuid());
//        }

//        public Task<IdentityResultDto> AddClaimAsync(string userId, Claim claim)
//        {
//            ThrowIfDisposed();
//            return _manager.AddClaimAsync(userId.ToGuid(), claim);
//        }

//        public Task<IdentityResultDto> RemoveClaimAsync(string userId, Claim claim)
//        {
//            ThrowIfDisposed();
//            return _manager.RemoveClaimAsync(userId.ToGuid(), claim);
//        }

//        public Task<IList<Claim>> GetClaimsAsync(string userId)
//        {
//            ThrowIfDisposed();
//            return _manager.GetClaimsAsync(userId.ToGuid());
//        }

//        public Task<IdentityResultDto> AddToRoleAsync(string userId, string role)
//        {
//            ThrowIfDisposed();
//            return _manager.AddToRoleAsync(userId.ToGuid(), role);
//        }

//        public Task<IdentityResultDto> AddToRolesAsync(string userId, string[] roles)
//        {
//            ThrowIfDisposed();
//            return _manager.AddToRolesAsync(userId.ToGuid(), roles);
//        }

//        public Task<IdentityResultDto> RemoveFromRoleAsync(string userId, string role)
//        {
//            ThrowIfDisposed();
//            return _manager.RemoveFromRoleAsync(userId.ToGuid(), role);
//        }

//        public Task<IdentityResultDto> RemoveFromRolesAsync(string userId, string[] roles)
//        {
//            ThrowIfDisposed();
//            return _manager.RemoveFromRolesAsync(userId.ToGuid(), roles);
//        }

//        public Task<IList<string>> GetRolesAsync(string userId)
//        {
//            ThrowIfDisposed();
//            return _manager.GetRolesAsync(userId.ToGuid());
//        }

//        public Task<bool> IsInRoleAsync(string userId, string role)
//        {
//            ThrowIfDisposed();
//            return _manager.IsInRoleAsync(userId.ToGuid(), role);
//        }

//        public Task<string> GetEmailAsync(string userId)
//        {
//            ThrowIfDisposed();
//            return _manager.GetEmailAsync(userId.ToGuid());
//        }

//        public Task<IdentityResultDto> SetEmailAsync(string userId, string email)
//        {
//            ThrowIfDisposed();
//            return _manager.SetEmailAsync(userId.ToGuid(), email);
//        }

//        public Task<IUser> FindByEmailAsync(string email)
//        {
//            ThrowIfDisposed();
//            User u = _manager.FindByEmailAsync(email).Result;
//            return Task.FromResult(UserConverter.IUserFromUser(u));
//        }

//        public Task<string> GenerateEmailConfirmationTokenAsync(string userId)
//        {
//            ThrowIfDisposed();
//            return _manager.GenerateEmailConfirmationTokenAsync(userId.ToGuid());
//        }

//        public Task<IdentityResultDto> ConfirmEmailAsync(string userId, string token)
//        {
//            ThrowIfDisposed();
//            return _manager.ConfirmEmailAsync(userId.ToGuid(), token);
//        }

//        public Task<bool> IsEmailConfirmedAsync(string userId)
//        {
//            ThrowIfDisposed();
//            return _manager.IsEmailConfirmedAsync(userId.ToGuid());
//        }

//        public Task<string> GetPhoneNumberAsync(string userId)
//        {
//            ThrowIfDisposed();
//            return _manager.GetPhoneNumberAsync(userId.ToGuid());
//        }

//        //public Task<IdentityResultDto> SetPhoneNumberAsync(string userId, string phoneNumber)
//        //{
//        //    ThrowIfDisposed();
//        //    return _manager.SetPhoneNumberAsync(userId.ToGuid(), phoneNumber);
//        //}

//        //public Task<IdentityResultDto> ChangePhoneNumberAsync(string userId, string phoneNumber, string token)
//        //{
//        //    ThrowIfDisposed();
//        //    return _manager.ChangePhoneNumberAsync(userId.ToGuid(), phoneNumber, token);
//        //}

//        public Task<bool> IsPhoneNumberConfirmedAsync(string userId)
//        {
//            ThrowIfDisposed();
//            return _manager.IsPhoneNumberConfirmedAsync(userId.ToGuid());
//        }

//        public Task<string> GenerateChangePhoneNumberTokenAsync(string userId, string phoneNumber)
//        {
//            ThrowIfDisposed();
//            return _manager.GenerateChangePhoneNumberTokenAsync(userId.ToGuid(), phoneNumber);
//        }

//        public Task<bool> VerifyChangePhoneNumberTokenAsync(string userId, string token, string phoneNumber)
//        {
//            ThrowIfDisposed();
//            return _manager.VerifyChangePhoneNumberTokenAsync(userId.ToGuid(), token, phoneNumber);
//        }

//        public Task<bool> VerifyUserTokenAsync(string userId, string purpose, string token)
//        {
//            ThrowIfDisposed();
//            return _manager.VerifyUserTokenAsync(userId.ToGuid(), purpose, token);
//        }

//        public Task<string> GenerateUserTokenAsync(string purpose, string userId)
//        {
//            ThrowIfDisposed();
//            return _manager.GenerateUserTokenAsync(purpose, userId.ToGuid());
//        }

//        public void RegisterTwoFactorProvider(string twoFactorProvider, IUserTokenProvider<IUser, string> provider)
//        {
//            ThrowIfDisposed();
//            throw new NotImplementedException();
//        }

//        public Task<IList<string>> GetValidTwoFactorProvidersAsync(string userId)
//        {
//            ThrowIfDisposed();
//            return _manager.GetValidTwoFactorProvidersAsync(userId.ToGuid());
//        }

//        public Task<bool> VerifyTwoFactorTokenAsync(string userId, string twoFactorProvider, string token)
//        {
//            ThrowIfDisposed();
//            return _manager.VerifyTwoFactorTokenAsync(userId.ToGuid(), twoFactorProvider, token);
//        }

//        public Task<string> GenerateTwoFactorTokenAsync(string userId, string twoFactorProvider)
//        {
//            ThrowIfDisposed();
//            return _manager.GenerateTwoFactorTokenAsync(userId.ToGuid(), twoFactorProvider);
//        }

//        public Task<IdentityResultDto> NotifyTwoFactorTokenAsync(string userId, string twoFactorProvider, string token)
//        {
//            ThrowIfDisposed();
//            return _manager.NotifyTwoFactorTokenAsync(userId.ToGuid(), twoFactorProvider, token);
//        }

//        public Task<bool> GetTwoFactorEnabledAsync(string userId)
//        {
//            ThrowIfDisposed();
//            return _manager.GetTwoFactorEnabledAsync(userId.ToGuid());
//        }

//        public Task<IdentityResultDto> SetTwoFactorEnabledAsync(string userId, bool enabled)
//        {
//            ThrowIfDisposed();
//            return _manager.SetTwoFactorEnabledAsync(userId.ToGuid(), enabled);
//        }

//        public Task SendEmailAsync(string userId, string subject, string body)
//        {
//            ThrowIfDisposed();
//            return _manager.SendEmailAsync(userId.ToGuid(), subject, body);
//        }

//        public Task SendSmsAsync(string userId, string message)
//        {
//            ThrowIfDisposed();
//            return _manager.SendSmsAsync(userId.ToGuid(), message);
//        }

//        public Task<bool> IsLockedOutAsync(string userId)
//        {
//            ThrowIfDisposed();
//            return _manager.IsLockedOutAsync(userId.ToGuid());
//        }

//        public Task<IdentityResultDto> SetLockoutEnabledAsync(string userId, bool enabled)
//        {
//            ThrowIfDisposed();
//            return _manager.SetLockoutEnabledAsync(userId.ToGuid(), enabled);
//        }

//        public Task<bool> GetLockoutEnabledAsync(string userId)
//        {
//            ThrowIfDisposed();
//            return _manager.GetLockoutEnabledAsync(userId.ToGuid());
//        }

//        public Task<DateTimeOffset> GetLockoutEndDateAsync(string userId)
//        {
//            ThrowIfDisposed();
//            return _manager.GetLockoutEndDateAsync(userId.ToGuid());
//        }

//        public Task<IdentityResultDto> SetLockoutEndDateAsync(string userId, DateTimeOffset lockoutEnd)
//        {
//            ThrowIfDisposed();
//            return _manager.SetLockoutEndDateAsync(userId.ToGuid(), lockoutEnd);
//        }

//        public Task<IdentityResultDto> AccessFailedAsync(string userId)
//        {
//            ThrowIfDisposed();
//            return _manager.AccessFailedAsync(userId.ToGuid());
//        }

//        public Task<IdentityResultDto> ResetAccessFailedCountAsync(string userId)
//        {
//            ThrowIfDisposed();
//            return _manager.ResetAccessFailedCountAsync(userId.ToGuid());
//        }

//        public Task<int> GetAccessFailedCountAsync(string userId)
//        {
//            ThrowIfDisposed();
//            return _manager.GetAccessFailedCountAsync(userId.ToGuid());
//        }

//        public IAuthenticationManagerService AuthenticationManager
//        {
//            get
//            {
//                ThrowIfDisposed();
//                return _manager.AuthenticationManager;
//            }
//            set
//            {
//                throw new NotImplementedException("Cannot change the AuthenticationManager through the service.");
//                //ThrowIfDisposed();
//                //_manager.AuthenticationManager = value;
//            }
//        }

//        public string AuthenticationType
//        {
//            get
//            {
//                ThrowIfDisposed();
//                return _manager.AuthenticationType;
//            }
//            set
//            {
//                throw new NotImplementedException("Cannot change the AuthenticationType through the service.");
//                //ThrowIfDisposed();
//                //_manager.AuthenticationType = value;
//            }
//        }

//        public IUserManagerService<IUser, string> UserManager
//        {
//            get
//            {
//                throw new NotImplementedException("Cannot access the UserManager directly through the service.");
//            }
//            set
//            {
//                throw new NotImplementedException("Cannot change the UserManager through the service.");
//                //ThrowIfDisposed();
//                //_manager.UserManager = value;
//            }
//        }

//        public string ConvertIdFromString(string id)
//        {
//            ThrowIfDisposed();
//            return id; // no passthrough
//        }

//        public string ConvertIdToString(string id)
//        {
//            ThrowIfDisposed();
//            return id; // no passthrough
//        }

//        public Task<ClaimsIdentity> CreateUserIdentityAsync(IUser user)
//        {
//            ThrowIfDisposed();
//            return _manager.CreateUserIdentityAsync(UserConverter.UserFromIUser(user));
//        }

//        public Task<SignInStatus> ExternalSignInAsync(ExternalLoginInfoDto loginInfo, bool isPersistent)
//        {
//            ThrowIfDisposed();
//            return _manager.ExternalSignInAsync(loginInfo, isPersistent);
//        }

//        public Task<string> GetVerifiedUserIdAsync()
//        {
//            ThrowIfDisposed();
//            Guid t = _manager.GetVerifiedUserIdAsync().Result;
//            return Task.FromResult(t.ToString());
//        }

//        public Task<bool> HasBeenVerifiedAsync()
//        {
//            ThrowIfDisposed();
//            return _manager.HasBeenVerifiedAsync();
//        }

//        public Task<SignInStatus> PasswordSignInAsync(string userName, string password, bool isPersistent, bool shouldLockout)
//        {
//            ThrowIfDisposed();
//            return _manager.PasswordSignInAsync(userName, password, isPersistent, shouldLockout);
//        }

//        public Task<bool> SendTwoFactorCodeAsync(string provider)
//        {
//            ThrowIfDisposed();
//            return _manager.SendTwoFactorCodeAsync(provider);
//        }

//        public async Task SignInAsync(IUser user, bool isPersistent, bool rememberBrowser)
//        {
//            ThrowIfDisposed();
//            await _manager.SignInAsync(UserConverter.UserFromIUser(user), isPersistent, rememberBrowser);
//        }

//        public Task<SignInStatus> TwoFactorSignInAsync(string provider, string code, bool isPersistent, bool rememberBrowser)
//        {
//            ThrowIfDisposed();
//            return _manager.TwoFactorSignInAsync(provider, code, isPersistent, rememberBrowser);
//        }

//        public AuthenticationResponseChallenge AuthenticationResponseChallenge
//        {
//            get
//            {
//                ThrowIfDisposed();
//                return _manager.AuthenticationResponseChallenge;
//            }
//            set
//            {
//                throw new NotImplementedException("Cannot change the AuthenticationResponseChallenge through the service.");
//                //ThrowIfDisposed();
//                //_manager.AuthenticationResponseChallenge = value;
//            }
//        }

//        public AuthenticationResponseGrant AuthenticationResponseGrant
//        {
//            get
//            {
//                ThrowIfDisposed();
//                return _manager.AuthenticationResponseGrant;
//            }
//            set
//            {
//                throw new NotImplementedException("Cannot change the AuthenticationResponseGrant through the service.");
//                //ThrowIfDisposed();
//                //_manager.AuthenticationResponseGrant = value;
//            }
//        }

//        public AuthenticationResponseRevoke AuthenticationResponseRevoke
//        {
//            get
//            {
//                ThrowIfDisposed();
//                return _manager.AuthenticationResponseRevoke;
//            }
//            set
//            {
//                throw new NotImplementedException("Cannot change the AuthenticationResponseRevoke through the service.");
//                //ThrowIfDisposed();
//                //_manager.AuthenticationResponseRevoke = value;
//            }
//        }

//        public ClaimsPrincipal User
//        {
//            get
//            {
//                ThrowIfDisposed();
//                return _manager.User;
//            }
//            set
//            {
//                throw new NotImplementedException("Cannot change the User through the service.");
//                //ThrowIfDisposed();
//                //_manager.User = value;
//            }
//        }

//        public Task<AuthenticateResult> AuthenticateAsync(string authenticationType)
//        {
//            ThrowIfDisposed();
//            return _manager.AuthenticateAsync(authenticationType);
//        }

//        public Task<IEnumerable<AuthenticateResult>> AuthenticateAsync(string[] authenticationTypes)
//        {
//            ThrowIfDisposed();
//            return _manager.AuthenticateAsync(authenticationTypes);
//        }

//        public void Challenge(params string[] authenticationTypes)
//        {
//            ThrowIfDisposed();
//            _manager.Challenge(authenticationTypes);
//        }

//        public void Challenge(AuthenticationProperties properties, params string[] authenticationTypes)
//        {
//            ThrowIfDisposed();
//            _manager.Challenge(properties, authenticationTypes);
//        }

//        public IEnumerable<AuthenticationDescription> GetAuthenticationTypes()
//        {
//            ThrowIfDisposed();
//            return _manager.GetAuthenticationTypes();
//        }

//        public IEnumerable<AuthenticationDescription> GetAuthenticationTypes(Func<AuthenticationDescription, bool> predicate)
//        {
//            ThrowIfDisposed();
//            return _manager.GetAuthenticationTypes(predicate);
//        }

//        public void SignIn(params ClaimsIdentity[] identities)
//        {
//            ThrowIfDisposed();
//            _manager.SignIn(identities);
//        }

//        public void SignIn(AuthenticationProperties properties, params ClaimsIdentity[] identities)
//        {
//            ThrowIfDisposed();
//            _manager.SignIn(properties, identities);
//        }

//        public void SignOut(params string[] authenticationTypes)
//        {
//            ThrowIfDisposed();
//            _manager.SignOut(authenticationTypes);
//        }

//        public void SignOut(AuthenticationProperties properties, params string[] authenticationTypes)
//        {
//            ThrowIfDisposed();
//            _manager.SignOut(properties, authenticationTypes);
//        }

//        public ClaimsIdentity CreateTwoFactorRememberBrowserIdentity(string userId)
//        {
//            ThrowIfDisposed();
//            return _manager.CreateTwoFactorRememberBrowserIdentity(userId);
//        }

//        public IEnumerable<AuthenticationDescription> GetExternalAuthenticationTypes()
//        {
//            ThrowIfDisposed();
//            return _manager.GetExternalAuthenticationTypes();
//        }

//        public Task<ClaimsIdentity> GetExternalIdentityAsync(string externalAuthenticationType)
//        {
//            ThrowIfDisposed();
//            return _manager.GetExternalIdentityAsync(externalAuthenticationType);
//        }

//        public ExternalLoginInfo GetExternalLoginInfo()
//        {
//            ThrowIfDisposed();
//            return _manager.GetExternalLoginInfo();
//        }

//        public ExternalLoginInfo GetExternalLoginInfo(string xsrfKey, string expectedValue)
//        {
//            ThrowIfDisposed();
//            return _manager.GetExternalLoginInfo(xsrfKey, expectedValue);
//        }

//        public Task<ExternalLoginInfo> GetExternalLoginInfoAsync()
//        {
//            ThrowIfDisposed();
//            return _manager.GetExternalLoginInfoAsync();
//        }

//        public Task<ExternalLoginInfo> GetExternalLoginInfoAsync(string xsrfKey, string expectedValue)
//        {
//            ThrowIfDisposed();
//            return _manager.GetExternalLoginInfoAsync(xsrfKey, expectedValue);
//        }

//        public Task<bool> TwoFactorBrowserRememberedAsync(string userId)
//        {
//            ThrowIfDisposed();
//            return _manager.TwoFactorBrowserRememberedAsync(userId);
//        }

//        private void ThrowIfDisposed()
//        {
//            if (_disposed)
//            {
//                throw new ObjectDisposedException(GetType().Name);
//            }
//        }

//        /// <summary>
//        ///     When disposing, actually dipose the store
//        /// </summary>
//        /// <param name="disposing"></param>
//        protected virtual void Dispose(bool disposing)
//        {
//            if (disposing && !_disposed)
//            {
//                if (!_manager.IsDisposed)
//                    _manager.Dispose();
//                _disposed = true;
//            }
//        }

//        public void Dispose()
//        {
//            ThrowIfDisposed();
//            Dispose(true);
//            GC.SuppressFinalize(this);
//        }
//    }
//}
