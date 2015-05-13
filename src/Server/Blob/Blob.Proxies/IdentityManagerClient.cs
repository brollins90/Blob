using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Blob.Contracts.Security;

namespace Blob.Proxies
{
    public class IdentityManagerClient : BaseClient<IUserManagerService>, IUserManagerService
    {
        public IdentityManagerClient(string endpointName, string username, string password)
            : base(endpointName, username, password) { }

        protected internal bool IsDisposed { get; private set; }

        public bool UserLockoutEnabledByDefault
        {
            get
            {
                try
                {
                    return Channel.UserLockoutEnabledByDefault;
                }
                catch (Exception ex)
                {
                    HandleError(ex);
                }
                return default(bool);
            }
        }

        public int MaxFailedAccessAttemptsBeforeLockout
        {
            get
            {
                try
                {
                    return Channel.MaxFailedAccessAttemptsBeforeLockout;
                }
                catch (Exception ex)
                {
                    HandleError(ex);
                }
                return default(int);
            }
        }

        public TimeSpan DefaultAccountLockoutTimeSpan
        {
            get
            {
                try
                {
                    return Channel.DefaultAccountLockoutTimeSpan;
                }
                catch (Exception ex)
                {
                    HandleError(ex);
                }
                return default(TimeSpan);
            }
        }

        public bool SupportsUserTwoFactor
        {
            get
            {
                try
                {
                    return Channel.SupportsUserTwoFactor;
                }
                catch (Exception ex)
                {
                    HandleError(ex);
                }
                return default(bool);
            }
        }

        public bool SupportsUserPassword
        {
            get
            {
                try
                {
                    return Channel.SupportsUserPassword;
                }
                catch (Exception ex)
                {
                    HandleError(ex);
                }
                return default(bool);
            }
        }

        public bool SupportsUserSecurityStamp
        {
            get
            {
                try
                {
                    return Channel.SupportsUserSecurityStamp;
                }
                catch (Exception ex)
                {
                    HandleError(ex);
                }
                return default(bool);
            }
        }

        public bool SupportsUserRole
        {
            get
            {
                try
                {
                    return Channel.SupportsUserRole;
                }
                catch (Exception ex)
                {
                    HandleError(ex);
                }
                return default(bool);
            }
        }

        public bool SupportsUserLogin
        {
            get
            {
                try
                {
                    return Channel.SupportsUserLogin;
                }
                catch (Exception ex)
                {
                    HandleError(ex);
                }
                return default(bool);
            }
        }

        public bool SupportsUserEmail
        {
            get
            {
                try
                {
                    return Channel.SupportsUserEmail;
                }
                catch (Exception ex)
                {
                    HandleError(ex);
                }
                return default(bool);
            }
        }

        public bool SupportsUserPhoneNumber
        {
            get
            {
                try
                {
                    return Channel.SupportsUserPhoneNumber;
                }
                catch (Exception ex)
                {
                    HandleError(ex);
                }
                return default(bool);
            }
        }

        public bool SupportsUserClaim
        {
            get
            {
                try
                {
                    return Channel.SupportsUserClaim;
                }
                catch (Exception ex)
                {
                    HandleError(ex);
                }
                return default(bool);
            }
        }

        public bool SupportsUserLockout
        {
            get
            {
                try
                {
                    return Channel.SupportsUserLockout;
                }
                catch (Exception ex)
                {
                    HandleError(ex);
                }
                return default(bool);
            }
        }

        public bool SupportsQueryableUsers
        {
            get
            {
                try
                {
                    return Channel.SupportsQueryableUsers;
                }
                catch (Exception ex)
                {
                    HandleError(ex);
                }
                return default(bool);
            }
        }

        public async Task AddClaimAsync(string userId, Claim claim)
        {
            try
            {
                await Channel.AddClaimAsync(userId, claim).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
        }

        public async Task<IList<Claim>> GetClaimsAsync(string userId)
        {
            try
            {
                return await Channel.GetClaimsAsync(userId).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
            return null;
        }

        public async Task RemoveClaimAsync(string userId, Claim claim)
        {
            try
            {
                await Channel.RemoveClaimAsync(userId, claim).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
        }

        public async Task<UserDto> FindByEmailAsync(string email)
        {
            try
            {
                return await Channel.FindByEmailAsync(email).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
            return null;
        }

        public async Task<string> GetEmailAsync(string userId)
        {
            try
            {
                return await Channel.GetEmailAsync(userId).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
            return null;
        }

        public async Task<bool> GetEmailConfirmedAsync(string userId)
        {
            try
            {
                return await Channel.GetEmailConfirmedAsync(userId).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
            return default(bool);
        }

        public async Task SetEmailAsync(string userId, string email)
        {
            try
            {
                await Channel.SetEmailAsync(userId, email).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
        }

        public async Task SetEmailConfirmedAsync(string userId, bool confirmed)
        {
            try
            {
                await Channel.SetEmailConfirmedAsync(userId, confirmed).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
        }

        public async Task<int> GetAccessFailedCountAsync(string userId)
        {
            try
            {
                return await Channel.GetAccessFailedCountAsync(userId).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
            return default(int);
        }

        public async Task<bool> GetLockoutEnabledAsync(string userId)
        {
            try
            {
                return await Channel.GetLockoutEnabledAsync(userId).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
            return default(bool);
        }

        public async Task<DateTimeOffset> GetLockoutEndDateAsync(string userId)
        {
            try
            {
                return await Channel.GetLockoutEndDateAsync(userId).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
            return default(DateTimeOffset);
        }

        public async Task<int> IncrementAccessFailedCountAsync(string userId)
        {
            try
            {
                return await Channel.IncrementAccessFailedCountAsync(userId).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
            return default(int);
        }

        public async Task ResetAccessFailedCountAsync(string userId)
        {
            try
            {
                await Channel.ResetAccessFailedCountAsync(userId).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
        }

        public async Task SetLockoutEnabledAsync(string userId, bool enabled)
        {
            try
            {
                await Channel.SetLockoutEnabledAsync(userId, enabled).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
        }

        public async Task SetLockoutEndDateAsync(string userId, DateTimeOffset lockoutEnd)
        {
            try
            {
                await Channel.SetLockoutEndDateAsync(userId, lockoutEnd).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
        }

        public async Task AddLoginAsync(string userId, UserLoginInfoDto login)
        {
            try
            {
                await Channel.AddLoginAsync(userId, login).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
        }

        public async Task<UserDto> FindAsync(UserLoginInfoDto login)
        {
            try
            {
                return await Channel.FindAsync(login).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
            return null;
        }

        public async Task<IList<UserLoginInfoDto>> GetLoginsAsync(string userId)
        {
            try
            {
                return await Channel.GetLoginsAsync(userId).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
            return null;
        }

        public async Task RemoveLoginAsync(string userId, UserLoginInfoDto login)
        {
            try
            {
                await Channel.RemoveLoginAsync(userId, login).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
        }

        public async Task<string> GetPasswordHashAsync(string userId)
        {
            try
            {
                return await Channel.GetPasswordHashAsync(userId).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
            return null;
        }

        public Task<bool> HasPasswordAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task SetPasswordHashAsync(string userId, string passwordHash)
        {
            throw new NotImplementedException();
        }

        public async Task AddToRoleAsync(string userId, string role)
        {
            throw new NotImplementedException();
        }

        public async Task<IList<string>> GetRolesAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> IsInRoleAsync(string userId, string role)
        {
            throw new NotImplementedException();
        }

        public async Task RemoveFromRoleAsync(string userId, string role)
        {
            throw new NotImplementedException();
        }

        public async Task<string> GetSecurityStampAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public async Task SetSecurityStampAsync(string userId, string stamp)
        {
            throw new NotImplementedException();
        }

        public async Task CreateAsync(UserDto user)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public async Task<UserDto> FindByIdAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public async Task<UserDto> FindByNameAsync(string userName)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(UserDto user)
        {
            throw new NotImplementedException();
        }


        public async Task<ClaimsIdentity> CreateIdentityAsync(UserDto user, string authenticationType)
        {
            throw new NotImplementedException();
        }

        public async Task<IdentityResultDto> CreateAsync(UserDto user, string password)
        {
            throw new NotImplementedException();
        }

        public async Task<IdentityResultDto> ChangePasswordAsync(Guid userId, string currentPassword, string newPassword)
        {
            throw new NotImplementedException();
        }

        //public async Task<UserDto> FindAsync(string userName, string password)
        //{
        //    UserDto output;

        //    try
        //    {
        //        IIdentityStoreService remoteProvider = RemoteProvider();
        //        output = await remoteProvider.FindAsync(userName, password);
        //        DisposeRemoteProvider(remoteProvider);
        //    }
        //    catch (Exception e)
        //    {
        //        if (LogExceptions)
        //        {
        //            _log.Error("Failed to find user.", e);
        //        }
        //        throw;
        //    }
        //    return output;
        //}

        //public async Task<bool> CheckPasswordAsync(UserDto user, string password)
        //{
        //    bool output;

        //    try
        //    {
        //        IIdentityStoreService remoteProvider = RemoteProvider();
        //        output = await remoteProvider.CheckPasswordAsync(user, password);
        //        DisposeRemoteProvider(remoteProvider);
        //    }
        //    catch (Exception e)
        //    {
        //        if (LogExceptions)
        //        {
        //            _log.Error("Failed to check password.", e);
        //        }
        //        throw;
        //    }
        //    return output;
        //}

        //public async Task<bool> HasPasswordAsync(string userId)
        //{
        //    bool output;

        //    try
        //    {
        //        IIdentityStoreService remoteProvider = RemoteProvider();
        //        output = await remoteProvider.HasPasswordAsync(userId);
        //        DisposeRemoteProvider(remoteProvider);
        //    }
        //    catch (Exception e)
        //    {
        //        if (LogExceptions)
        //        {
        //            _log.Error("Failed to check for password.", e);
        //        }
        //        throw;
        //    }
        //    return output;
        //}

        //public async Task AddPasswordAsync(string userId, string password)
        //{
        //    IdentityResultDto output;

        //    try
        //    {
        //        IIdentityStoreService remoteProvider = RemoteProvider();
        //        output = await remoteProvider.AddPasswordAsync(userId, password);
        //        DisposeRemoteProvider(remoteProvider);
        //    }
        //    catch (Exception e)
        //    {
        //        if (LogExceptions)
        //        {
        //            _log.Error("Failed to add password.", e);
        //        }
        //        throw;
        //    }
        //    return output;
        //}

        //public async Task ChangePasswordAsync(string userId, string currentPassword, string newPassword)
        //{
        //    IdentityResultDto output;

        //    try
        //    {
        //        IIdentityStoreService remoteProvider = RemoteProvider();
        //        output = await remoteProvider.ChangePasswordAsync(userId, currentPassword, newPassword);
        //        DisposeRemoteProvider(remoteProvider);
        //    }
        //    catch (Exception e)
        //    {
        //        if (LogExceptions)
        //        {
        //            _log.Error("Failed to change password.", e);
        //        }
        //        throw;
        //    }
        //    return output;
        //}

        //public async Task RemovePasswordAsync(string userId)
        //{
        //    IdentityResultDto output;

        //    try
        //    {
        //        IIdentityStoreService remoteProvider = RemoteProvider();
        //        output = await remoteProvider.RemovePasswordAsync(userId);
        //        DisposeRemoteProvider(remoteProvider);
        //    }
        //    catch (Exception e)
        //    {
        //        if (LogExceptions)
        //        {
        //            _log.Error("Failed to remove password.", e);
        //        }
        //        throw;
        //    }
        //    return output;
        //}

        //public async Task UpdateSecurityStampAsync(string userId)
        //{
        //    IdentityResultDto output;

        //    try
        //    {
        //        IIdentityService remoteProvider = RemoteProvider();
        //        output = await remoteProvider.UpdateSecurityStampAsync(userId);
        //        DisposeRemoteProvider(remoteProvider);
        //    }
        //    catch (Exception e)
        //    {
        //        if (LogExceptions)
        //        {
        //            _log.Error("Failed to update security stamp.", e);
        //        }
        //        throw;
        //    }
        //    return output;
        //}

        //public async Task<string> GeneratePasswordResetTokenAsync(string userId)
        //{
        //    string output;

        //    try
        //    {
        //        IIdentityService remoteProvider = RemoteProvider();
        //        output = await remoteProvider.GeneratePasswordResetTokenAsync(userId);
        //        DisposeRemoteProvider(remoteProvider);
        //    }
        //    catch (Exception e)
        //    {
        //        if (LogExceptions)
        //        {
        //            _log.Error("Failed to generate password reset token.", e);
        //        }
        //        throw;
        //    }
        //    return output;
        //}

        //public async Task ResetPasswordAsync(string userId, string token, string newPassword)
        //{
        //    IdentityResultDto output;

        //    try
        //    {
        //        IIdentityService remoteProvider = RemoteProvider();
        //        output = await remoteProvider.ResetPasswordAsync(userId, token, newPassword);
        //        DisposeRemoteProvider(remoteProvider);
        //    }
        //    catch (Exception e)
        //    {
        //        if (LogExceptions)
        //        {
        //            _log.Error("Failed to reset password.", e);
        //        }
        //        throw;
        //    }
        //    return output;
        //}

        //public async Task AddToRolesAsync(string userId, string[] roles)
        //{
        //    IdentityResultDto output;

        //    try
        //    {
        //        IIdentityService remoteProvider = RemoteProvider();
        //        output = await remoteProvider.AddToRolesAsync(userId, roles);
        //        DisposeRemoteProvider(remoteProvider);
        //    }
        //    catch (Exception e)
        //    {
        //        if (LogExceptions)
        //        {
        //            _log.Error("Failed to add user to roles.", e);
        //        }
        //        throw;
        //    }
        //    return output;
        //}

        //public async Task RemoveFromRolesAsync(string userId, string[] roles)
        //{
        //    IdentityResultDto output;

        //    try
        //    {
        //        IIdentityService remoteProvider = RemoteProvider();
        //        output = await remoteProvider.RemoveFromRolesAsync(userId, roles);
        //        DisposeRemoteProvider(remoteProvider);
        //    }
        //    catch (Exception e)
        //    {
        //        if (LogExceptions)
        //        {
        //            _log.Error("Failed to remove user from roles.", e);
        //        }
        //        throw;
        //    }
        //    return output;
        //}

        //public async Task<string> GenerateEmailConfirmationTokenAsync(string userId)
        //{
        //    string output;

        //    try
        //    {
        //        IIdentityService remoteProvider = RemoteProvider();
        //        output = await remoteProvider.GenerateEmailConfirmationTokenAsync(userId);
        //        DisposeRemoteProvider(remoteProvider);
        //    }
        //    catch (Exception e)
        //    {
        //        if (LogExceptions)
        //        {
        //            _log.Error("Failed to generate email token.", e);
        //        }
        //        throw;
        //    }
        //    return output;
        //}

        //public async Task ConfirmEmailAsync(string userId, string token)
        //{
        //    IdentityResultDto output;

        //    try
        //    {
        //        IIdentityService remoteProvider = RemoteProvider();
        //        output = await remoteProvider.ConfirmEmailAsync(userId, token);
        //        DisposeRemoteProvider(remoteProvider);
        //    }
        //    catch (Exception e)
        //    {
        //        if (LogExceptions)
        //        {
        //            _log.Error("Failed to confirm email.", e);
        //        }
        //        throw;
        //    }
        //    return output;
        //}

        //public async Task<bool> IsEmailConfirmedAsync(string userId)
        //{
        //    bool output;

        //    try
        //    {
        //        IIdentityService remoteProvider = RemoteProvider();
        //        output = await remoteProvider.IsEmailConfirmedAsync(userId);
        //        DisposeRemoteProvider(remoteProvider);
        //    }
        //    catch (Exception e)
        //    {
        //        if (LogExceptions)
        //        {
        //            _log.Error("Failed to find if email is confirmed.", e);
        //        }
        //        throw;
        //    }
        //    return output;
        //}

        ////public Task<string> GetPhoneNumberAsync(string userId)
        ////{
        ////    throw new NotImplementedException();
        ////}

        ////public Task SetPhoneNumberAsync(string userId, string phoneNumber)
        ////{
        ////    throw new NotImplementedException();
        ////}

        ////public Task ChangePhoneNumberAsync(string userId, string phoneNumber, string token)
        ////{
        ////    throw new NotImplementedException();
        ////}

        ////public Task<bool> IsPhoneNumberConfirmedAsync(string userId)
        ////{
        ////    throw new NotImplementedException();
        ////}

        ////public Task<string> GenerateChangePhoneNumberTokenAsync(string userId, string phoneNumber)
        ////{
        ////    throw new NotImplementedException();
        ////}

        ////public Task<bool> VerifyChangePhoneNumberTokenAsync(string userId, string token, string phoneNumber)
        ////{
        ////    throw new NotImplementedException();
        ////}

        //public async Task<bool> VerifyUserTokenAsync(string userId, string purpose, string token)
        //{
        //    bool output;

        //    try
        //    {
        //        IIdentityService remoteProvider = RemoteProvider();
        //        output = await remoteProvider.VerifyUserTokenAsync(userId,purpose,token);
        //        DisposeRemoteProvider(remoteProvider);
        //    }
        //    catch (Exception e)
        //    {
        //        if (LogExceptions)
        //        {
        //            _log.Error("Failed to verify user token.", e);
        //        }
        //        throw;
        //    }
        //    return output;
        //}

        //public async Task<string> GenerateUserTokenAsync(string purpose, string userId)
        //{
        //    string output;

        //    try
        //    {
        //        IIdentityService remoteProvider = RemoteProvider();
        //        output = await remoteProvider.GenerateUserTokenAsync(purpose, userId);
        //        DisposeRemoteProvider(remoteProvider);
        //    }
        //    catch (Exception e)
        //    {
        //        if (LogExceptions)
        //        {
        //            _log.Error("Failed to generate user token.", e);
        //        }
        //        throw;
        //    }
        //    return output;
        //}

        //public void RegisterTwoFactorProvider(string twoFactorProvider, IUserTokenProvider<UserDto, string> provider)
        //{

        //    try
        //    {
        //        IIdentityService remoteProvider = RemoteProvider();
        //        remoteProvider.RegisterTwoFactorProvider(twoFactorProvider, provider);
        //        DisposeRemoteProvider(remoteProvider);
        //    }
        //    catch (Exception e)
        //    {
        //        if (LogExceptions)
        //        {
        //            _log.Error("Failed to register two factor provider.", e);
        //        }
        //        throw;
        //    }
        //}

        //public async Task<IList<string>> GetValidTwoFactorProvidersAsync(string userId)
        //{
        //    IList<string> output;

        //    try
        //    {
        //        IIdentityService remoteProvider = RemoteProvider();
        //        output = await remoteProvider.GetValidTwoFactorProvidersAsync(userId);
        //        DisposeRemoteProvider(remoteProvider);
        //    }
        //    catch (Exception e)
        //    {
        //        if (LogExceptions)
        //        {
        //            _log.Error("Failed to get valid two factor providers.", e);
        //        }
        //        throw;
        //    }
        //    return output;
        //}

        //public Task<bool> VerifyTwoFactorTokenAsync(string userId, string twoFactorProvider, string token)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<string> GenerateTwoFactorTokenAsync(string userId, string twoFactorProvider)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task NotifyTwoFactorTokenAsync(string userId, string twoFactorProvider, string token)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<bool> GetTwoFactorEnabledAsync(string userId)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task SetTwoFactorEnabledAsync(string userId, bool enabled)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task SendEmailAsync(string userId, string subject, string body)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task SendSmsAsync(string userId, string message)
        //{
        //    throw new NotImplementedException();
        //}

        //#endregion


        //#region IAuthenticationManagerService - Microsoft.Owin.Security.IAuthenticationManager

        //public AuthenticationResponseChallenge AuthenticationResponseChallenge
        //{
        //    get { return _authenticationResponseChallenge; }
        //    set { _authenticationResponseChallenge = value; }
        //}
        //private AuthenticationResponseChallenge _authenticationResponseChallenge;

        //public AuthenticationResponseGrant AuthenticationResponseGrant
        //{
        //    get { return _authenticationResponseGrant; }
        //    set { _authenticationResponseGrant = value; }
        //}
        //private AuthenticationResponseGrant _authenticationResponseGrant;

        //public AuthenticationResponseRevoke AuthenticationResponseRevoke
        //{
        //    get { return _authenticationResponseRevoke; }
        //    set { _authenticationResponseRevoke = value; }
        //}
        //private AuthenticationResponseRevoke _authenticationResponseRevoke;

        //public ClaimsPrincipal User
        //{
        //    get { return _user; }
        //    set { _user = value; }
        //}
        //private ClaimsPrincipal _user;

        //public Task<AuthenticateResult> AuthenticateAsync(string authenticationType)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<IEnumerable<AuthenticateResult>> AuthenticateAsync(string[] authenticationTypes)
        //{
        //    throw new NotImplementedException();
        //}

        //public void Challenge(params string[] authenticationTypes)
        //{
        //    throw new NotImplementedException();
        //}

        //public void Challenge(AuthenticationProperties properties, params string[] authenticationTypes)
        //{
        //    throw new NotImplementedException();
        //}

        //public IEnumerable<AuthenticationDescription> GetAuthenticationTypes()
        //{
        //    throw new NotImplementedException();
        //}

        //public IEnumerable<AuthenticationDescription> GetAuthenticationTypes(Func<AuthenticationDescription, bool> predicate)
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




        ////Extensions

        //public ClaimsIdentity CreateTwoFactorRememberBrowserIdentity(string userId)
        //{
        //    throw new NotImplementedException();
        //}

        //public IEnumerable<AuthenticationDescription> GetExternalAuthenticationTypes()
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<ClaimsIdentity> GetExternalIdentityAsync(string externalAuthenticationType)
        //{
        //    throw new NotImplementedException();
        //}

        //public ExternalLoginInfo GetExternalLoginInfo()
        //{
        //    throw new NotImplementedException();
        //}

        //public ExternalLoginInfo GetExternalLoginInfo(string xsrfKey, string expectedValue)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<ExternalLoginInfo> GetExternalLoginInfoAsync()
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<ExternalLoginInfo> GetExternalLoginInfoAsync(string xsrfKey, string expectedValue)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<bool> TwoFactorBrowserRememberedAsync(string userId)
        //{
        //    throw new NotImplementedException();
        //}

        //#endregion

        //#region ISignInManagerService - SignInManager


        


        //#endregion

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void ThrowIfDisposed()
        {
            if (IsDisposed)
            {
                throw new ObjectDisposedException(GetType().Name);
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing && !IsDisposed)
            {
                //Store.Dispose();
                IsDisposed = true;
            }
        }

        public AuthenticationResponseChallengeDto AuthenticationResponseChallenge
        {
            get
            {
                try
                {
                    return Channel.AuthenticationResponseChallenge;
                }
                catch (Exception ex)
                {
                    HandleError(ex);
                }
                return null;
            }
            set
            {
                throw new NotImplementedException("Cannot change the AuthenticationResponseChallenge through the service.");
                //ThrowIfDisposed();
                //_manager.AuthenticationResponseChallenge = value;
            }
        }

        public AuthenticationResponseGrantDto AuthenticationResponseGrant
        {
            get
            {
                try
                {
                    return Channel.AuthenticationResponseGrant;
                }
                catch (Exception ex)
                {
                    HandleError(ex);
                }
                return null;
            }
            set
            {
                throw new NotImplementedException("Cannot change the AuthenticationResponseGrant through the service.");
                //ThrowIfDisposed();
                //_manager.AuthenticationResponseGrant = value;
            }
        }

        public AuthenticationResponseRevokeDto AuthenticationResponseRevoke
        {
            get
            {
                try
                {
                    return Channel.AuthenticationResponseRevoke;
                }
                catch (Exception ex)
                {
                    HandleError(ex);
                }
                return null;
            }
            set
            {
                throw new NotImplementedException("Cannot change the AuthenticationResponseRevoke through the service.");
                //ThrowIfDisposed();
                //_manager.AuthenticationResponseRevoke = value;
            }
        }

        public ClaimsPrincipal User
        {
            get
            {
                try
                {
                    return Channel.User;
                }
                catch (Exception ex)
                {
                    HandleError(ex);
                }
                return null;
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public async Task<AuthenticateResultDto> AuthenticateAsync(string authenticationType)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<AuthenticateResultDto>> AuthenticateAsync(string[] authenticationTypes)
        {
            throw new NotImplementedException();
        }

        public async Task ChallengeAsync(params string[] authenticationTypes)
        {
            throw new NotImplementedException();
        }

        public async Task ChallengeAsync(AuthenticationPropertiesDto properties, params string[] authenticationTypes)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<AuthenticationDescriptionDto>> GetAuthenticationTypesAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<AuthenticationDescriptionDto>> GetAuthenticationTypesAsync(Func<AuthenticationDescriptionDto, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public async Task SignInAsync(params ClaimsIdentity[] identities)
        {
            throw new NotImplementedException();
        }

        public async Task SignInAsync(AuthenticationPropertiesDto properties, params ClaimsIdentity[] identities)
        {
            throw new NotImplementedException();
        }

        public async Task SignOutAsync(params string[] authenticationTypes)
        {
            throw new NotImplementedException();
        }

        public async Task SignOutAsync(AuthenticationPropertiesDto properties, params string[] authenticationTypes)
        {
            throw new NotImplementedException();
        }
    }

}
