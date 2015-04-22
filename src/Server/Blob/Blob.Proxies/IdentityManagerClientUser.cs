using Blob.Contracts.Security;
using log4net;
using System;
using System.Collections.Specialized;
using System.Security.Claims;
using System.ServiceModel;
using System.ServiceModel.Security;
using System.Threading.Tasks;

namespace Blob.Proxies
{
    public partial class IdentityManagerClient : 
        //UserManager<IUser, string>,
        IIdentityService<string>//,
        //ISignInManagerService<IUser, string>,
        //IAuthenticationManagerService
    {
        private readonly ILog _log;
        private static ChannelFactory<IIdentityStoreService> _channelFactory;
        private string Username = "customerUser1";
        private string Password = "password";

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
        protected IIdentityStoreService RemoteProvider()
        {
            if (_channelFactory == null)
            {
                _channelFactory = new ChannelFactory<IIdentityStoreService>(ProxyProviderName);

                _channelFactory.Credentials.UserName.UserName = Username;
                _channelFactory.Credentials.UserName.Password = Password;
                _channelFactory.Credentials.ServiceCertificate.Authentication.CertificateValidationMode = X509CertificateValidationMode.None;
            }

            IIdentityStoreService provider = _channelFactory.CreateChannel();
            provider.SetProvider(ProxyProviderName);

            return provider;
        }

        /// <summary>
        /// This method should be called to handle closing the 
        /// connected proxy object.
        /// </summary>
        private void DisposeRemoteProvider(IIdentityStoreService remoteProvider)
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
                        _proxyProviderName = "IdentityService";
                }
                return _proxyProviderName;
            }
            set { _proxyProviderName = value; }
        }
        private string _proxyProviderName;


        public void SetProvider(string providerName)
        {
            // This isnt really part of the client side, just needed on the server side
            throw new NotImplementedException();
        }
        

        //#region UserManager

        
        //public IPasswordHasher PasswordHasher
        //{
        //    get
        //    {
        //        IPasswordHasher output;

        //        try
        //        {
        //            var remoteProvider = RemoteProvider();
        //            output = remoteProvider.PasswordHasher;
        //            DisposeRemoteProvider(remoteProvider);
        //        }
        //        catch (Exception e)
        //        {
        //            if (LogExceptions)
        //            {
        //                _log.Error("Failed to get property.", e);
        //            }
        //            throw;
        //        }
        //        return output;
        //    }
        //}

        //public IIdentityValidator<UserDto> UserValidator
        //{
        //    get
        //    {
        //        IIdentityValidator<UserDto> output;

        //        try
        //        {
        //            var remoteProvider = RemoteProvider();
        //            output = remoteProvider.UserValidator;
        //            DisposeRemoteProvider(remoteProvider);
        //        }
        //        catch (Exception e)
        //        {
        //            if (LogExceptions)
        //            {
        //                _log.Error("Failed to get property.", e);
        //            }
        //            throw;
        //        }
        //        return output;
        //    }
        //}

        //public IIdentityValidator<string> PasswordValidator
        //{
        //    get
        //    {
        //        IIdentityValidator<string> output;

        //        try
        //        {
        //            var remoteProvider = RemoteProvider();
        //            output = remoteProvider.PasswordValidator;
        //            DisposeRemoteProvider(remoteProvider);
        //        }
        //        catch (Exception e)
        //        {
        //            if (LogExceptions)
        //            {
        //                _log.Error("Failed to get property.", e);
        //            }
        //            throw;
        //        }
        //        return output;
        //    }
        //}

        //public IClaimsIdentityFactory<UserDto, string> ClaimsIdentityFactory
        //{
        //    get
        //    {
        //        IClaimsIdentityFactory<UserDto,string> output;

        //        try
        //        {
        //            var remoteProvider = RemoteProvider();
        //            output = remoteProvider.ClaimsIdentityFactory;
        //            DisposeRemoteProvider(remoteProvider);
        //        }
        //        catch (Exception e)
        //        {
        //            if (LogExceptions)
        //            {
        //                _log.Error("Failed to get property.", e);
        //            }
        //            throw;
        //        }
        //        return output;
        //    }
        //}

        //public IIdentityMessageService EmailService
        //{
        //    get
        //    {
        //        IIdentityMessageService output;

        //        try
        //        {
        //            var remoteProvider = RemoteProvider();
        //            output = remoteProvider.EmailService;
        //            DisposeRemoteProvider(remoteProvider);
        //        }
        //        catch (Exception e)
        //        {
        //            if (LogExceptions)
        //            {
        //                _log.Error("Failed to get property.", e);
        //            }
        //            throw;
        //        }
        //        return output;
        //    }
        //}

        //public IIdentityMessageService SmsService
        //{
        //    get
        //    {
        //        IIdentityMessageService output;

        //        try
        //        {
        //            var remoteProvider = RemoteProvider();
        //            output = remoteProvider.SmsService;
        //            DisposeRemoteProvider(remoteProvider);
        //        }
        //        catch (Exception e)
        //        {
        //            if (LogExceptions)
        //            {
        //                _log.Error("Failed to get property.", e);
        //            }
        //            throw;
        //        }
        //        return output;
        //    }
        //}

        //public IUserTokenProvider<UserDto, string> UserTokenProvider
        //{
        //    get
        //    {
        //        IUserTokenProvider<UserDto,string> output;

        //        try
        //        {
        //            var remoteProvider = RemoteProvider();
        //            output = remoteProvider.UserTokenProvider;
        //            DisposeRemoteProvider(remoteProvider);
        //        }
        //        catch (Exception e)
        //        {
        //            if (LogExceptions)
        //            {
        //                _log.Error("Failed to get property.", e);
        //            }
        //            throw;
        //        }
        //        return output;
        //    }
        //}

        public bool UserLockoutEnabledByDefault
        {
            get
            {
                bool output;

                try
                {
                    var remoteProvider = RemoteProvider();
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
                    var remoteProvider = RemoteProvider();
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
                    var remoteProvider = RemoteProvider();
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
                    var remoteProvider = RemoteProvider();
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
                    var remoteProvider = RemoteProvider();
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
                    var remoteProvider = RemoteProvider();
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
                    var remoteProvider = RemoteProvider();
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
                    var remoteProvider = RemoteProvider();
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
                    var remoteProvider = RemoteProvider();
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
                    var remoteProvider = RemoteProvider();
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
                    var remoteProvider = RemoteProvider();
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
                    var remoteProvider = RemoteProvider();
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
                    var remoteProvider = RemoteProvider();
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

        //public IQueryable<UserDto> Users
        //{
        //    get
        //    {
        //        IQueryable<UserDto> output;

        //        try
        //        {
        //            var remoteProvider = RemoteProvider();
        //            output = remoteProvider.Users;
        //            DisposeRemoteProvider(remoteProvider);
        //        }
        //        catch (Exception e)
        //        {
        //            if (LogExceptions)
        //            {
        //                _log.Error("Failed to get property.", e);
        //            }
        //            throw;
        //        }
        //        return output;
        //    }
        //}

        //public IDictionary<string, IUserTokenProvider<UserDto, string>> TwoFactorProviders
        //{
        //    get
        //    {
        //        IDictionary<string, IUserTokenProvider<UserDto, string>> output;

        //        try
        //        {
        //            var remoteProvider = RemoteProvider();
        //            output = remoteProvider.TwoFactorProviders;
        //            DisposeRemoteProvider(remoteProvider);
        //        }
        //        catch (Exception e)
        //        {
        //            if (LogExceptions)
        //            {
        //                _log.Error("Failed to get property.", e);
        //            }
        //            throw;
        //        }
        //        return output;
        //    }
        //}

        public async Task<ClaimsIdentity> CreateIdentityAsync(UserDto user, string authenticationType)
        {
            ClaimsIdentity output;

            try
            {
                IIdentityStoreService remoteProvider = RemoteProvider();
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

        public async Task<IdentityResultDto> CreateAsync(UserDto user)
        {
            IdentityResultDto output;

            try
            {
                IIdentityStoreService remoteProvider = RemoteProvider();
                output = await remoteProvider.CreateAsync(user);
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

        public async Task<IdentityResultDto> UpdateAsync(UserDto user)
        {
            IdentityResultDto output;

            try
            {
                IIdentityStoreService remoteProvider = RemoteProvider();
                output = await remoteProvider.UpdateAsync(user);
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
            return output;
        }

        public async Task<IdentityResultDto> DeleteAsync(UserDto user)
        {
            IdentityResultDto output;

            try
            {
                IIdentityStoreService remoteProvider = RemoteProvider();
                output = await remoteProvider.DeleteAsync(user);
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
            return output;
        }

        public async Task<UserDto> FindByIdAsync(string userId)
        {
            UserDto output;

            try
            {
                IIdentityStoreService remoteProvider = RemoteProvider();
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
                IIdentityStoreService remoteProvider = RemoteProvider();
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

        public async Task<IdentityResultDto> CreateAsync(UserDto user, string password)
        {
            IdentityResultDto output;

            try
            {
                IIdentityStoreService remoteProvider = RemoteProvider();
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

        public async Task<UserDto> FindAsync(string userName, string password)
        {
            UserDto output;

            try
            {
                IIdentityStoreService remoteProvider = RemoteProvider();
                output = await remoteProvider.FindAsync(userName, password);
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

        public async Task<bool> CheckPasswordAsync(UserDto user, string password)
        {
            bool output;

            try
            {
                IIdentityStoreService remoteProvider = RemoteProvider();
                output = await remoteProvider.CheckPasswordAsync(user, password);
                DisposeRemoteProvider(remoteProvider);
            }
            catch (Exception e)
            {
                if (LogExceptions)
                {
                    _log.Error("Failed to check password.", e);
                }
                throw;
            }
            return output;
        }

        public async Task<bool> HasPasswordAsync(string userId)
        {
            bool output;

            try
            {
                IIdentityStoreService remoteProvider = RemoteProvider();
                output = await remoteProvider.HasPasswordAsync(userId);
                DisposeRemoteProvider(remoteProvider);
            }
            catch (Exception e)
            {
                if (LogExceptions)
                {
                    _log.Error("Failed to check for password.", e);
                }
                throw;
            }
            return output;
        }

        public async Task<IdentityResultDto> AddPasswordAsync(string userId, string password)
        {
            IdentityResultDto output;

            try
            {
                IIdentityStoreService remoteProvider = RemoteProvider();
                output = await remoteProvider.AddPasswordAsync(userId, password);
                DisposeRemoteProvider(remoteProvider);
            }
            catch (Exception e)
            {
                if (LogExceptions)
                {
                    _log.Error("Failed to add password.", e);
                }
                throw;
            }
            return output;
        }

        public async Task<IdentityResultDto> ChangePasswordAsync(string userId, string currentPassword, string newPassword)
        {
            IdentityResultDto output;

            try
            {
                IIdentityStoreService remoteProvider = RemoteProvider();
                output = await remoteProvider.ChangePasswordAsync(userId, currentPassword, newPassword);
                DisposeRemoteProvider(remoteProvider);
            }
            catch (Exception e)
            {
                if (LogExceptions)
                {
                    _log.Error("Failed to change password.", e);
                }
                throw;
            }
            return output;
        }

        public async Task<IdentityResultDto> RemovePasswordAsync(string userId)
        {
            IdentityResultDto output;

            try
            {
                IIdentityStoreService remoteProvider = RemoteProvider();
                output = await remoteProvider.RemovePasswordAsync(userId);
                DisposeRemoteProvider(remoteProvider);
            }
            catch (Exception e)
            {
                if (LogExceptions)
                {
                    _log.Error("Failed to remove password.", e);
                }
                throw;
            }
            return output;
        }

        //public async Task<string> GetSecurityStampAsync(string userId)
        //{
        //    string output;

        //    try
        //    {
        //        IUserManagerService remoteProvider = RemoteProvider();
        //        output = await remoteProvider.GetSecurityStampAsync(userId);
        //        DisposeRemoteProvider(remoteProvider);
        //    }
        //    catch (Exception e)
        //    {
        //        if (LogExceptions)
        //        {
        //            _log.Error("Failed to get security stamp.", e);
        //        }
        //        throw;
        //    }
        //    return output;
        //}

        //public async Task<IdentityResultDto> UpdateSecurityStampAsync(string userId)
        //{
        //    IdentityResultDto output;

        //    try
        //    {
        //        IUserManagerService remoteProvider = RemoteProvider();
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
        //        IUserManagerService remoteProvider = RemoteProvider();
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

        //public async Task<IdentityResultDto> ResetPasswordAsync(string userId, string token, string newPassword)
        //{
        //    IdentityResultDto output;

        //    try
        //    {
        //        IUserManagerService remoteProvider = RemoteProvider();
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

        //public async Task<UserDto> FindAsync(UserLoginInfoDto login)
        //{
        //    UserDto output;

        //    try
        //    {
        //        IUserManagerService remoteProvider = RemoteProvider();
        //        output = await remoteProvider.FindAsync(login);
        //        DisposeRemoteProvider(remoteProvider);
        //    }
        //    catch (Exception e)
        //    {
        //        if (LogExceptions)
        //        {
        //            _log.Error("Failed to find login.", e);
        //        }
        //        throw;
        //    }
        //    return output;
        //}

        //public async Task<IdentityResultDto> RemoveLoginAsync(string userId, UserLoginInfoDto login)
        //{
        //    IdentityResultDto output;

        //    try
        //    {
        //        IUserManagerService remoteProvider = RemoteProvider();
        //        output = await remoteProvider.RemoveLoginAsync(userId, login);
        //        DisposeRemoteProvider(remoteProvider);
        //    }
        //    catch (Exception e)
        //    {
        //        if (LogExceptions)
        //        {
        //            _log.Error("Failed to remove login.", e);
        //        }
        //        throw;
        //    }
        //    return output;
        //}

        //public async Task<IdentityResultDto> AddLoginAsync(string userId, UserLoginInfoDto login)
        //{
        //    IdentityResultDto output;

        //    try
        //    {
        //        IUserManagerService remoteProvider = RemoteProvider();
        //        output = await remoteProvider.AddLoginAsync(userId, login);
        //        DisposeRemoteProvider(remoteProvider);
        //    }
        //    catch (Exception e)
        //    {
        //        if (LogExceptions)
        //        {
        //            _log.Error("Failed to add login.", e);
        //        }
        //        throw;
        //    }
        //    return output;
        //}

        //public async Task<IList<UserLoginInfoDto>> GetLoginsAsync(string userId)
        //{
        //    IList<UserLoginInfoDto> output;

        //    try
        //    {
        //        IUserManagerService remoteProvider = RemoteProvider();
        //        output = await remoteProvider.GetLoginsAsync(userId);
        //        DisposeRemoteProvider(remoteProvider);
        //    }
        //    catch (Exception e)
        //    {
        //        if (LogExceptions)
        //        {
        //            _log.Error("Failed to get logins.", e);
        //        }
        //        throw;
        //    }
        //    return output;
        //}

        //public async Task<IdentityResultDto> AddClaimAsync(string userId, Claim claim)
        //{
        //    IdentityResultDto output;

        //    try
        //    {
        //        IUserManagerService remoteProvider = RemoteProvider();
        //        output = await remoteProvider.AddClaimAsync(userId, claim);
        //        DisposeRemoteProvider(remoteProvider);
        //    }
        //    catch (Exception e)
        //    {
        //        if (LogExceptions)
        //        {
        //            _log.Error("Failed to add claim.", e);
        //        }
        //        throw;
        //    }
        //    return output;
        //}

        //public async Task<IdentityResultDto> RemoveClaimAsync(string userId, Claim claim)
        //{
        //    IdentityResultDto output;

        //    try
        //    {
        //        IUserManagerService remoteProvider = RemoteProvider();
        //        output = await remoteProvider.RemoveClaimAsync(userId, claim);
        //        DisposeRemoteProvider(remoteProvider);
        //    }
        //    catch (Exception e)
        //    {
        //        if (LogExceptions)
        //        {
        //            _log.Error("Failed to remove claim.", e);
        //        }
        //        throw;
        //    }
        //    return output;
        //}

        //public async Task<IList<Claim>> GetClaimsAsync(string userId)
        //{
        //    IList<Claim> output;

        //    try
        //    {
        //        IUserManagerService remoteProvider = RemoteProvider();
        //        output = await remoteProvider.GetClaimsAsync(userId);
        //        DisposeRemoteProvider(remoteProvider);
        //    }
        //    catch (Exception e)
        //    {
        //        if (LogExceptions)
        //        {
        //            _log.Error("Failed to get claims.", e);
        //        }
        //        throw;
        //    }
        //    return output;
        //}

        //public async Task<IdentityResultDto> AddToRoleAsync(string userId, string role)
        //{
        //    IdentityResultDto output;

        //    try
        //    {
        //        IUserManagerService remoteProvider = RemoteProvider();
        //        output = await remoteProvider.AddToRoleAsync(userId, role);
        //        DisposeRemoteProvider(remoteProvider);
        //    }
        //    catch (Exception e)
        //    {
        //        if (LogExceptions)
        //        {
        //            _log.Error("Failed to add user to role.", e);
        //        }
        //        throw;
        //    }
        //    return output;
        //}

        //public async Task<IdentityResultDto> AddToRolesAsync(string userId, string[] roles)
        //{
        //    IdentityResultDto output;

        //    try
        //    {
        //        IUserManagerService remoteProvider = RemoteProvider();
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

        //public async Task<IdentityResultDto> RemoveFromRoleAsync(string userId, string role)
        //{
        //    IdentityResultDto output;

        //    try
        //    {
        //        IUserManagerService remoteProvider = RemoteProvider();
        //        output = await remoteProvider.RemoveFromRoleAsync(userId, role);
        //        DisposeRemoteProvider(remoteProvider);
        //    }
        //    catch (Exception e)
        //    {
        //        if (LogExceptions)
        //        {
        //            _log.Error("Failed to remove user from role.", e);
        //        }
        //        throw;
        //    }
        //    return output;
        //}

        //public async Task<IdentityResultDto> RemoveFromRolesAsync(string userId, string[] roles)
        //{
        //    IdentityResultDto output;

        //    try
        //    {
        //        IUserManagerService remoteProvider = RemoteProvider();
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

        //public async Task<IList<string>> GetRolesAsync(string userId)
        //{
        //    IList<string> output;

        //    try
        //    {
        //        IUserManagerService remoteProvider = RemoteProvider();
        //        output = await remoteProvider.GetRolesAsync(userId);
        //        DisposeRemoteProvider(remoteProvider);
        //    }
        //    catch (Exception e)
        //    {
        //        if (LogExceptions)
        //        {
        //            _log.Error("Failed to get roles.", e);
        //        }
        //        throw;
        //    }
        //    return output;
        //}

        //public async Task<bool> IsInRoleAsync(string userId, string role)
        //{
        //    bool output;

        //    try
        //    {
        //        IUserManagerService remoteProvider = RemoteProvider();
        //        output = await remoteProvider.IsInRoleAsync(userId, role);
        //        DisposeRemoteProvider(remoteProvider);
        //    }
        //    catch (Exception e)
        //    {
        //        if (LogExceptions)
        //        {
        //            _log.Error("Failed to get if user is in role.", e);
        //        }
        //        throw;
        //    }
        //    return output;
        //}

        //public async Task<string> GetEmailAsync(string userId)
        //{
        //    string output;

        //    try
        //    {
        //        IUserManagerService remoteProvider = RemoteProvider();
        //        output = await remoteProvider.GetEmailAsync(userId);
        //        DisposeRemoteProvider(remoteProvider);
        //    }
        //    catch (Exception e)
        //    {
        //        if (LogExceptions)
        //        {
        //            _log.Error("Failed to get if user is in role.", e);
        //        }
        //        throw;
        //    }
        //    return output;
        //}

        //public async Task<IdentityResultDto> SetEmailAsync(string userId, string email)
        //{
        //    IdentityResultDto output;

        //    try
        //    {
        //        IUserManagerService remoteProvider = RemoteProvider();
        //        output = await remoteProvider.SetEmailAsync(userId, email);
        //        DisposeRemoteProvider(remoteProvider);
        //    }
        //    catch (Exception e)
        //    {
        //        if (LogExceptions)
        //        {
        //            _log.Error("Failed to set email.", e);
        //        }
        //        throw;
        //    }
        //    return output;
        //}

        //public async Task<UserDto> FindByEmailAsync(string email)
        //{
        //    UserDto output;

        //    try
        //    {
        //        IUserManagerService remoteProvider = RemoteProvider();
        //        output = await remoteProvider.FindByEmailAsync(email);
        //        DisposeRemoteProvider(remoteProvider);
        //    }
        //    catch (Exception e)
        //    {
        //        if (LogExceptions)
        //        {
        //            _log.Error("Failed to find by email.", e);
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
        //        IUserManagerService remoteProvider = RemoteProvider();
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

        //public async Task<IdentityResultDto> ConfirmEmailAsync(string userId, string token)
        //{
        //    IdentityResultDto output;

        //    try
        //    {
        //        IUserManagerService remoteProvider = RemoteProvider();
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
        //        IUserManagerService remoteProvider = RemoteProvider();
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

        ////public Task<IdentityResultDto> SetPhoneNumberAsync(string userId, string phoneNumber)
        ////{
        ////    throw new NotImplementedException();
        ////}

        ////public Task<IdentityResultDto> ChangePhoneNumberAsync(string userId, string phoneNumber, string token)
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
        //        IUserManagerService remoteProvider = RemoteProvider();
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
        //        IUserManagerService remoteProvider = RemoteProvider();
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
        //        IUserManagerService remoteProvider = RemoteProvider();
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
        //        IUserManagerService remoteProvider = RemoteProvider();
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

        //public Task<IdentityResultDto> NotifyTwoFactorTokenAsync(string userId, string twoFactorProvider, string token)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<bool> GetTwoFactorEnabledAsync(string userId)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<IdentityResultDto> SetTwoFactorEnabledAsync(string userId, bool enabled)
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

        //public Task<bool> IsLockedOutAsync(string userId)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<IdentityResultDto> SetLockoutEnabledAsync(string userId, bool enabled)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<bool> GetLockoutEnabledAsync(string userId)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<DateTimeOffset> GetLockoutEndDateAsync(string userId)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<IdentityResultDto> SetLockoutEndDateAsync(string userId, DateTimeOffset lockoutEnd)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<IdentityResultDto> AccessFailedAsync(string userId)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<IdentityResultDto> ResetAccessFailedCountAsync(string userId)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<int> GetAccessFailedCountAsync(string userId)
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
            //todo, actually do...
            //throw new NotImplementedException();
        }
    }

}
