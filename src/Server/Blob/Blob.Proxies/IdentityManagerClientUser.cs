using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Claims;
using System.ServiceModel;
using System.ServiceModel.Security;
using System.Threading.Tasks;
using Blob.Contracts.Security;
using log4net;

namespace Blob.Proxies
{
    public class IdentityManagerClient : 
        IIdentityService
    {
        private readonly ILog _log;
        private static ChannelFactory<IIdentityService> _channelFactory;
        private string Username = "customerUser1";
        private string Password = "password";

        public IdentityManagerClient()
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

        protected IIdentityService RemoteProvider()
        {
            if (_channelFactory == null)
            {
                _channelFactory = new ChannelFactory<IIdentityService>(ProxyProviderName);

                _channelFactory.Credentials.UserName.UserName = Username;
                _channelFactory.Credentials.UserName.Password = Password;
                _channelFactory.Credentials.ServiceCertificate.Authentication.CertificateValidationMode = X509CertificateValidationMode.None;
            }

            IIdentityService provider = _channelFactory.CreateChannel();

            return provider;
        }

        private void DisposeRemoteProvider(IIdentityService remoteProvider)
        {
            _log.Debug("Disposing of remote provider.");

            // TODO: Add error checking for state of object.
            ((IClientChannel)remoteProvider).Dispose();
        }


        protected internal NameValueCollection Config { get; private set; }
        protected internal bool IsDisposed { get; private set; }

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
                        _proxyProviderName = "IdentityService";
                }
                return _proxyProviderName;
            }
            set { _proxyProviderName = value; }
        }
        private string _proxyProviderName;
        
        public bool UserLockoutEnabledByDefault
        {
            get
            {
                bool output;

                try
                {
                    IIdentityService remoteProvider = RemoteProvider();
                    output = remoteProvider.UserLockoutEnabledByDefault;
                    DisposeRemoteProvider(remoteProvider);
                }
                catch (Exception e)
                {
                    if (LogExceptions)
                    {
                        _log.Error("Failed to get property.", e);
                    }
                    throw;
                }
                return output;
            }
        }

        public int MaxFailedAccessAttemptsBeforeLockout
        {
            get
            {
                int output;

                try
                {
                    IIdentityService remoteProvider = RemoteProvider();
                    output = remoteProvider.MaxFailedAccessAttemptsBeforeLockout;
                    DisposeRemoteProvider(remoteProvider);
                }
                catch (Exception e)
                {
                    if (LogExceptions)
                    {
                        _log.Error("Failed to get property.", e);
                    }
                    throw;
                }
                return output;
            }
        }

        public TimeSpan DefaultAccountLockoutTimeSpan
        {
            get
            {
                TimeSpan output;

                try
                {
                    IIdentityService remoteProvider = RemoteProvider();
                    output = remoteProvider.DefaultAccountLockoutTimeSpan;
                    DisposeRemoteProvider(remoteProvider);
                }
                catch (Exception e)
                {
                    if (LogExceptions)
                    {
                        _log.Error("Failed to get property.", e);
                    }
                    throw;
                }
                return output;
            }
        }

        public bool SupportsUserTwoFactor
        {
            get
            {
                bool output;

                try
                {
                    IIdentityService remoteProvider = RemoteProvider();
                    output = remoteProvider.SupportsUserTwoFactor;
                    DisposeRemoteProvider(remoteProvider);
                }
                catch (Exception e)
                {
                    if (LogExceptions)
                    {
                        _log.Error("Failed to get property.", e);
                    }
                    throw;
                }
                return output;
            }
        }

        public bool SupportsUserPassword
        {
            get
            {
                bool output;

                try
                {
                    IIdentityService remoteProvider = RemoteProvider();
                    output = remoteProvider.SupportsUserPassword;
                    DisposeRemoteProvider(remoteProvider);
                }
                catch (Exception e)
                {
                    if (LogExceptions)
                    {
                        _log.Error("Failed to get property.", e);
                    }
                    throw;
                }
                return output;
            }
        }

        public bool SupportsUserSecurityStamp
        {
            get
            {
                bool output;

                try
                {
                    IIdentityService remoteProvider = RemoteProvider();
                    output = remoteProvider.SupportsUserSecurityStamp;
                    DisposeRemoteProvider(remoteProvider);
                }
                catch (Exception e)
                {
                    if (LogExceptions)
                    {
                        _log.Error("Failed to get property.", e);
                    }
                    throw;
                }
                return output;
            }
        }

        public bool SupportsUserRole
        {
            get
            {
                bool output;

                try
                {
                    IIdentityService remoteProvider = RemoteProvider();
                    output = remoteProvider.SupportsUserRole;
                    DisposeRemoteProvider(remoteProvider);
                }
                catch (Exception e)
                {
                    if (LogExceptions)
                    {
                        _log.Error("Failed to get property.", e);
                    }
                    throw;
                }
                return output;
            }
        }

        public bool SupportsUserLogin
        {
            get
            {
                bool output;

                try
                {
                    IIdentityService remoteProvider = RemoteProvider();
                    output = remoteProvider.SupportsUserLogin;
                    DisposeRemoteProvider(remoteProvider);
                }
                catch (Exception e)
                {
                    if (LogExceptions)
                    {
                        _log.Error("Failed to get property.", e);
                    }
                    throw;
                }
                return output;
            }
        }

        public bool SupportsUserEmail
        {
            get
            {
                bool output;

                try
                {
                    IIdentityService remoteProvider = RemoteProvider();
                    output = remoteProvider.SupportsUserEmail;
                    DisposeRemoteProvider(remoteProvider);
                }
                catch (Exception e)
                {
                    if (LogExceptions)
                    {
                        _log.Error("Failed to get property.", e);
                    }
                    throw;
                }
                return output;
            }
        }

        public bool SupportsUserPhoneNumber
        {
            get
            {
                bool output;

                try
                {
                    IIdentityService remoteProvider = RemoteProvider();
                    output = remoteProvider.SupportsUserPhoneNumber;
                    DisposeRemoteProvider(remoteProvider);
                }
                catch (Exception e)
                {
                    if (LogExceptions)
                    {
                        _log.Error("Failed to get property.", e);
                    }
                    throw;
                }
                return output;
            }
        }

        public bool SupportsUserClaim
        {
            get
            {
                bool output;

                try
                {
                    IIdentityService remoteProvider = RemoteProvider();
                    output = remoteProvider.SupportsUserClaim;
                    DisposeRemoteProvider(remoteProvider);
                }
                catch (Exception e)
                {
                    if (LogExceptions)
                    {
                        _log.Error("Failed to get property.", e);
                    }
                    throw;
                }
                return output;
            }
        }

        public bool SupportsUserLockout
        {
            get
            {
                bool output;

                try
                {
                    IIdentityService remoteProvider = RemoteProvider();
                    output = remoteProvider.SupportsUserLockout;
                    DisposeRemoteProvider(remoteProvider);
                }
                catch (Exception e)
                {
                    if (LogExceptions)
                    {
                        _log.Error("Failed to get property.", e);
                    }
                    throw;
                }
                return output;
            }
        }

        public bool SupportsQueryableUsers
        {
            get
            {
                bool output;

                try
                {
                    IIdentityService remoteProvider = RemoteProvider();
                    output = remoteProvider.SupportsQueryableUsers;
                    DisposeRemoteProvider(remoteProvider);
                }
                catch (Exception e)
                {
                    if (LogExceptions)
                    {
                        _log.Error("Failed to get property.", e);
                    }
                    throw;
                }
                return output;
            }
        }

        public async Task AddClaimAsync(string userId, Claim claim)
        {
            //IdentityResultDto output;

            try
            {
                IIdentityService remoteProvider = RemoteProvider();
                await remoteProvider.AddClaimAsync(userId, claim);
                DisposeRemoteProvider(remoteProvider);
            }
            catch (Exception e)
            {
                if (LogExceptions)
                {
                    _log.Error("Failed to add claim.", e);
                }
                throw;
            }
            //return output;
        }

        public async Task<IList<Claim>> GetClaimsAsync(string userId)
        {
            IList<Claim> output;

            try
            {
                IIdentityService remoteProvider = RemoteProvider();
                output = await remoteProvider.GetClaimsAsync(userId);
                DisposeRemoteProvider(remoteProvider);
            }
            catch (Exception e)
            {
                if (LogExceptions)
                {
                    _log.Error("Failed to get claims.", e);
                }
                throw;
            }
            return output;
        }

        public async Task RemoveClaimAsync(string userId, Claim claim)
        {
            //IdentityResultDto output;

            try
            {
                IIdentityService remoteProvider = RemoteProvider();
                await remoteProvider.RemoveClaimAsync(userId, claim);
                DisposeRemoteProvider(remoteProvider);
            }
            catch (Exception e)
            {
                if (LogExceptions)
                {
                    _log.Error("Failed to remove claim.", e);
                }
                throw;
            }
            //return output;
        }

        public async Task<UserDto> FindByEmailAsync(string email)
        {
            UserDto output;

            try
            {
                IIdentityService remoteProvider = RemoteProvider();
                output = await remoteProvider.FindByEmailAsync(email);
                DisposeRemoteProvider(remoteProvider);
            }
            catch (Exception e)
            {
                if (LogExceptions)
                {
                    _log.Error("Failed to find by email.", e);
                }
                throw;
            }
            return output;
        }

        public async Task<string> GetEmailAsync(string userId)
        {
            string output;

            try
            {
                IIdentityService remoteProvider = RemoteProvider();
                output = await remoteProvider.GetEmailAsync(userId);
                DisposeRemoteProvider(remoteProvider);
            }
            catch (Exception e)
            {
                if (LogExceptions)
                {
                    _log.Error("Failed to get if user is in role.", e);
                }
                throw;
            }
            return output;
        }

        public async Task<bool> GetEmailConfirmedAsync(string userId)
        {
            bool output;

            try
            {
                IIdentityService remoteProvider = RemoteProvider();
                output = await remoteProvider.GetEmailConfirmedAsync(userId);
                DisposeRemoteProvider(remoteProvider);
            }
            catch (Exception e)
            {
                if (LogExceptions)
                {
                    _log.Error("Failed to get if user is in role.", e);
                }
                throw;
            }
            return output;
        }

        public async Task SetEmailAsync(string userId, string email)
        {
            //IdentityResultDto output;

            try
            {
                IIdentityService remoteProvider = RemoteProvider();
                await remoteProvider.SetEmailAsync(userId, email);
                DisposeRemoteProvider(remoteProvider);
            }
            catch (Exception e)
            {
                if (LogExceptions)
                {
                    _log.Error("Failed to set email.", e);
                }
                throw;
            }
            //return output;
        }

        public async Task SetEmailConfirmedAsync(string userId, bool confirmed)
        {
            //IdentityResultDto output;

            try
            {
                IIdentityService remoteProvider = RemoteProvider();
                await remoteProvider.SetEmailConfirmedAsync(userId, confirmed);
                DisposeRemoteProvider(remoteProvider);
            }
            catch (Exception e)
            {
                if (LogExceptions)
                {
                    _log.Error("Failed to set email.", e);
                }
                throw;
            }
            //return output;
        }

        public async Task<int> GetAccessFailedCountAsync(string userId)
        {
            int output;

            try
            {
                IIdentityService remoteProvider = RemoteProvider();
                output = await remoteProvider.GetAccessFailedCountAsync(userId);
                DisposeRemoteProvider(remoteProvider);
            }
            catch (Exception e)
            {
                if (LogExceptions)
                {
                    _log.Error("Failed to set email.", e);
                }
                throw;
            }
            return output;
        }

        public Task<bool> GetLockoutEnabledAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<DateTimeOffset> GetLockoutEndDateAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<int> IncrementAccessFailedCountAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task ResetAccessFailedCountAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task SetLockoutEnabledAsync(string userId, bool enabled)
        {
            throw new NotImplementedException();
        }

        public Task SetLockoutEndDateAsync(string userId, DateTimeOffset lockoutEnd)
        {
            throw new NotImplementedException();
        }

        public async Task AddLoginAsync(string userId, UserLoginInfoDto login)
        {
            //IdentityResultDto output;

            try
            {
                IIdentityService remoteProvider = RemoteProvider();
                await remoteProvider.AddLoginAsync(userId, login);
                DisposeRemoteProvider(remoteProvider);
            }
            catch (Exception e)
            {
                if (LogExceptions)
                {
                    _log.Error("Failed to add login.", e);
                }
                throw;
            }
            //return output;
        }

        public async Task<UserDto> FindAsync(UserLoginInfoDto login)
        {
            UserDto output;

            try
            {
                IIdentityService remoteProvider = RemoteProvider();
                output = await remoteProvider.FindAsync(login);
                DisposeRemoteProvider(remoteProvider);
            }
            catch (Exception e)
            {
                if (LogExceptions)
                {
                    _log.Error("Failed to find login.", e);
                }
                throw;
            }
            return output;
        }

        public async Task<IList<UserLoginInfoDto>> GetLoginsAsync(string userId)
        {
            IList<UserLoginInfoDto> output;

            try
            {
                IIdentityService remoteProvider = RemoteProvider();
                output = await remoteProvider.GetLoginsAsync(userId);
                DisposeRemoteProvider(remoteProvider);
            }
            catch (Exception e)
            {
                if (LogExceptions)
                {
                    _log.Error("Failed to get logins.", e);
                }
                throw;
            }
            return output;
        }

        public async Task RemoveLoginAsync(string userId, UserLoginInfoDto login)
        {
            //IdentityResultDto output;

            try
            {
                IIdentityService remoteProvider = RemoteProvider();
                await remoteProvider.RemoveLoginAsync(userId, login);
                DisposeRemoteProvider(remoteProvider);
            }
            catch (Exception e)
            {
                if (LogExceptions)
                {
                    _log.Error("Failed to remove login.", e);
                }
                throw;
            }
            //return output;
        }

        public Task<string> GetPasswordHashAsync(string userId)
        {
            throw new NotImplementedException();
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
            //IdentityResultDto output;

            try
            {
                IIdentityService remoteProvider = RemoteProvider();
                await remoteProvider.AddToRoleAsync(userId, role);
                DisposeRemoteProvider(remoteProvider);
            }
            catch (Exception e)
            {
                if (LogExceptions)
                {
                    _log.Error("Failed to add user to role.", e);
                }
                throw;
            }
            //return output;
        }

        public async Task<IList<string>> GetRolesAsync(string userId)
        {
            IList<string> output;

            try
            {
                IIdentityService remoteProvider = RemoteProvider();
                output = await remoteProvider.GetRolesAsync(userId);
                DisposeRemoteProvider(remoteProvider);
            }
            catch (Exception e)
            {
                if (LogExceptions)
                {
                    _log.Error("Failed to get roles.", e);
                }
                throw;
            }
            return output;
        }

        public async Task<bool> IsInRoleAsync(string userId, string role)
        {
            bool output;

            try
            {
                IIdentityService remoteProvider = RemoteProvider();
                output = await remoteProvider.IsInRoleAsync(userId, role);
                DisposeRemoteProvider(remoteProvider);
            }
            catch (Exception e)
            {
                if (LogExceptions)
                {
                    _log.Error("Failed to get if user is in role.", e);
                }
                throw;
            }
            return output;
        }

        public async Task RemoveFromRoleAsync(string userId, string role)
        {
            //IdentityResultDto output;

            try
            {
                IIdentityService remoteProvider = RemoteProvider();
                await remoteProvider.RemoveFromRoleAsync(userId, role);
                DisposeRemoteProvider(remoteProvider);
            }
            catch (Exception e)
            {
                if (LogExceptions)
                {
                    _log.Error("Failed to remove user from role.", e);
                }
                throw;
            }
            //return output;
        }

        public async Task<string> GetSecurityStampAsync(string userId)
        {
            string output;

            try
            {
                IIdentityService remoteProvider = RemoteProvider();
                output = await remoteProvider.GetSecurityStampAsync(userId);
                DisposeRemoteProvider(remoteProvider);
            }
            catch (Exception e)
            {
                if (LogExceptions)
                {
                    _log.Error("Failed to get security stamp.", e);
                }
                throw;
            }
            return output;
        }

        public async Task SetSecurityStampAsync(string userId, string stamp)
        {
            //string output;

            try
            {
                IIdentityService remoteProvider = RemoteProvider();
                await remoteProvider.SetSecurityStampAsync(userId, stamp);
                DisposeRemoteProvider(remoteProvider);
            }
            catch (Exception e)
            {
                if (LogExceptions)
                {
                    _log.Error("Failed to get security stamp.", e);
                }
                throw;
            }
            //return output;
        }

        public async Task CreateAsync(UserDto user)
        {
            //IdentityResultDto output;

            try
            {
                IIdentityService remoteProvider = RemoteProvider();
                await remoteProvider.CreateAsync(user);
                DisposeRemoteProvider(remoteProvider);
            }
            catch (Exception e)
            {
                if (LogExceptions)
                {
                    _log.Error("Failed to create user.", e);
                }
                throw;
            }
            //return output;
        }

        public async Task DeleteAsync(string userId)
        {
            //IdentityResultDto output;

            try
            {
                IIdentityService remoteProvider = RemoteProvider();
                await remoteProvider.DeleteAsync(userId);
                DisposeRemoteProvider(remoteProvider);
            }
            catch (Exception e)
            {
                if (LogExceptions)
                {
                    _log.Error("Failed to delete user.", e);
                }
                throw;
            }
            //return output;
        }

        public async Task<UserDto> FindByIdAsync(string userId)
        {
            UserDto output;

            try
            {
                IIdentityService remoteProvider = RemoteProvider();
                output = await remoteProvider.FindByIdAsync(userId);
                DisposeRemoteProvider(remoteProvider);
            }
            catch (Exception e)
            {
                if (LogExceptions)
                {
                    _log.Error("Failed to find user.", e);
                }
                throw;
            }
            return output;
        }

        public async Task<UserDto> FindByNameAsync(string userName)
        {
            UserDto output;

            try
            {
                IIdentityService remoteProvider = RemoteProvider();
                output = await remoteProvider.FindByNameAsync(userName);
                DisposeRemoteProvider(remoteProvider);
            }
            catch (Exception e)
            {
                if (LogExceptions)
                {
                    _log.Error("Failed to find user.", e);
                }
                throw;
            }
            return output;
        }

        public async Task UpdateAsync(UserDto user)
        {
            //IdentityResultDto output;

            try
            {
                IIdentityService remoteProvider = RemoteProvider();
                await remoteProvider.UpdateAsync(user);
                DisposeRemoteProvider(remoteProvider);
            }
            catch (Exception e)
            {
                if (LogExceptions)
                {
                    _log.Error("Failed to update user.", e);
                }
                throw;
            }
            //return output;
        }


        public async Task<ClaimsIdentity> CreateIdentityAsync(UserDto user, string authenticationType)
        {
            ClaimsIdentity output;

            try
            {
                IIdentityService remoteProvider = RemoteProvider();
                output = await remoteProvider.CreateIdentityAsync(user, authenticationType);
                DisposeRemoteProvider(remoteProvider);
            }
            catch (Exception e)
            {
                if (LogExceptions)
                {
                    _log.Error("Failed to create identity.", e);
                }
                throw;
            }
            return output;
        }

        public async Task<IdentityResultDto> CreateAsync(UserDto user, string password)
        {
            IdentityResultDto output;

            try
            {
                IIdentityService remoteProvider = RemoteProvider();
                output = await remoteProvider.CreateAsync(user, password);
                DisposeRemoteProvider(remoteProvider);
            }
            catch (Exception e)
            {
                if (LogExceptions)
                {
                    _log.Error("Failed to create user.", e);
                }
                throw;
            }
            return output;
        }

        public async Task<IdentityResultDto> ChangePasswordAsync(Guid userId, string currentPassword, string newPassword)
        {
            IdentityResultDto output;

            try
            {
                IIdentityService remoteProvider = RemoteProvider();
                output = await remoteProvider.ChangePasswordAsync(userId, currentPassword, newPassword);
                DisposeRemoteProvider(remoteProvider);
            }
            catch (Exception e)
            {
                if (LogExceptions)
                {
                    _log.Error("Failed to update user password.", e);
                }
                throw;
            }
            return output;
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
    }

}
