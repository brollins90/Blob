//using Blob.Contracts.Security;
//using Blob.Core.Domain;
//using log4net;
//using Microsoft.AspNet.Identity;
//using Microsoft.AspNet.Identity.Owin;
//using Microsoft.Owin;
//using Microsoft.Owin.Security;
//using System;
//using System.Collections.Generic;
//using System.Collections.Specialized;
//using System.Configuration;
//using System.Configuration.Provider;
//using System.Data.Entity.Utilities;
//using System.Globalization;
//using System.Linq;
//using System.Security.Claims;
//using System.Threading.Tasks;
//using System.Web.Configuration;

//namespace Blob.Security
//{
//    public class BlobUserManager :
//        UserManager<User, Guid>,
//        IUserManagerService<User, Guid>
//    {
//        private readonly ILog _log;
//        private readonly string _dbConnectionString;

//        /// <summary>
//        ///     Constructor
//        /// </summary>
//        /// <param name="store">The IUserStore is responsible for commiting changes via the UpdateAsync/CreateAsync methods</param>
//        public BlobUserManager(BlobUserStore store)
//            : base(store)
//        {
//            _log = LogManager.GetLogger("MembershipLogger");
//            _dbConnectionString = ConfigurationManager.ConnectionStrings["BlobDbContext"].ConnectionString;

//            if (store == null)
//            {
//                throw new ArgumentNullException("store");
//            }
//            Store = store;
//            UserValidator = new BlobUserValidator(this);
//            PasswordValidator = new PasswordValidator
//                                {
//                                    RequireDigit = false,
//                                    RequiredLength = 0,
//                                    RequireLowercase = false,
//                                    RequireNonLetterOrDigit = false,
//                                    RequireUppercase = false
//                                };
//            PasswordHasher = new BlobPasswordHasher();
//            ClaimsIdentityFactory = new BlobClaimsIdentityFactory();
//        }

//        public void Initialize(string name, NameValueCollection config)
//        {
//            try
//            {
//                _log.Debug("Initializing BlobUserManager");

//                if (config == null)
//                    throw new ArgumentNullException("config");

//                Config = config;

//                try
//                {
//                    if (MachineKey.ValidationKey.Contains("AutoGenerate")/* && PasswordFormat != MembershipPasswordFormat.Clear */)
//                    {
//                        throw new ProviderException("Hashed or Encrypted passwords are not supported with auto-generated keys.");
//                    }
//                }
//                catch (Exception e)
//                {
//                    if (LogExceptions)
//                    {
//                        _log.Error("Failed to load the machine key.", e);
//                    }
//                    throw; // throw this because we want it to break if this fails
//                }
//            }
//            catch (Exception e)
//            {
//                _log.Error("Failed to load blob user manager.", e);
//                throw;
//            }
//        }
//        protected internal new BlobUserStore Store { get; set; }
//        protected internal NameValueCollection Config { get; set; }
//        protected internal bool IsDisposed { get; private set; }
//        protected internal MachineKeySection MachineKey
//        {
//            get
//            {
//                if (_machineKey == null)
//                {
//                    Configuration cfg = WebConfigurationManager.OpenWebConfiguration(System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath);
//                    _machineKey = (MachineKeySection)cfg.GetSection("system.web/machineKey");
//                }
//                return _machineKey;
//            }
//        }
//        private MachineKeySection _machineKey;


//        /// <summary>
//        /// Indicates whether to log exceptions
//        /// </summary>
//        /// <returns>true if the membership provider is configured to log exceptions; otherwise, false. The default is false.</returns>
//        public bool LogExceptions
//        {
//            get
//            {
//                if (_logExceptions.HasValue == false)
//                {
//                    bool bv;
//                    string strv = Config["LogExceptions"];
//                    if (!string.IsNullOrEmpty(strv) && bool.TryParse(strv, out bv))
//                        _logExceptions = bv;
//                    else
//                        _logExceptions = true;
//                }
//                return _logExceptions.Value;
//            }
//        }
//        private bool? _logExceptions;

//        //
//        //


//        public void SetProvider(string providerName)
//        {
//            // i guess this was used in the older code.  we dont really need to init anything because this is already the class...
//        }
//        //    _internalProvider = Membership.Provider;
//        //    //try
//        //    //{
//        //    //    _internalProvider = (String.IsNullOrWhiteSpace(providerName))
//        //    //        ? Membership.Provider :
//        //    //        Membership.Providers[providerName];
//        //    //}
//        //    //catch
//        //    //{
//        //    //    throw;
//        //    //    _internalProvider = Membership.Provider;
//        //    //}
//        //}
//        //private MembershipProvider _internalProvider;


//        //    private readonly Dictionary<string, IUserTokenProvider<User, Guid>> _factors =
//        //       new Dictionary<string, IUserTokenProvider<User, Guid>>();

//        //    //private IClaimsIdentityFactory<User, Guid> _claimsFactory;
//        //    //private TimeSpan _defaultLockout = TimeSpan.Zero;
//        //    //private bool _disposed;
//        //    //private IPasswordHasher _passwordHasher;
//        //    //private IIdentityValidator<string> _passwordValidator;
//        //    //private IIdentityValidator<User> _userValidator;



//        //    #region IAuthenticationManagerService - Microsoft.Owin.Security.IAuthenticationManager

//        //    public AuthenticationResponseChallenge AuthenticationResponseChallenge
//        //    {
//        //        get { return _authenticationResponseChallenge; }
//        //        set { _authenticationResponseChallenge = value; }
//        //    }
//        //    private AuthenticationResponseChallenge _authenticationResponseChallenge;

//        //    public AuthenticationResponseGrant AuthenticationResponseGrant
//        //    {
//        //        get { return _authenticationResponseGrant; }
//        //        set { _authenticationResponseGrant = value; }
//        //    }
//        //    private AuthenticationResponseGrant _authenticationResponseGrant;

//        //    public AuthenticationResponseRevoke AuthenticationResponseRevoke
//        //    {
//        //        get { return _authenticationResponseRevoke; }
//        //        set { _authenticationResponseRevoke = value; }
//        //    }
//        //    private AuthenticationResponseRevoke _authenticationResponseRevoke;

//        //    public ClaimsPrincipal User
//        //    {
//        //        get { return _user; }
//        //        set { _user = value; }
//        //    }
//        //    private ClaimsPrincipal _user;

//        //    public Task<AuthenticateResult> AuthenticateAsync(string authenticationType)
//        //    {
//        //        throw new NotImplementedException();
//        //    }

//        //    public Task<IEnumerable<AuthenticateResult>> AuthenticateAsync(string[] authenticationTypes)
//        //    {
//        //        throw new NotImplementedException();
//        //    }

//        //    public void Challenge(params string[] authenticationTypes)
//        //    {
//        //        throw new NotImplementedException();
//        //    }

//        //    public void Challenge(AuthenticationProperties properties, params string[] authenticationTypes)
//        //    {
//        //        throw new NotImplementedException();
//        //    }

//        //    public IEnumerable<AuthenticationDescription> GetAuthenticationTypes()
//        //    {
//        //        throw new NotImplementedException();
//        //    }

//        //    public IEnumerable<AuthenticationDescription> GetAuthenticationTypes(Func<AuthenticationDescription, bool> predicate)
//        //    {
//        //        throw new NotImplementedException();
//        //    }

//        //    public void SignIn(params ClaimsIdentity[] identities)
//        //    {
//        //        throw new NotImplementedException();
//        //    }

//        //    public void SignIn(AuthenticationProperties properties, params ClaimsIdentity[] identities)
//        //    {
//        //        throw new NotImplementedException();
//        //    }

//        //    public void SignOut(params string[] authenticationTypes)
//        //    {
//        //        throw new NotImplementedException();
//        //    }

//        //    public void SignOut(AuthenticationProperties properties, params string[] authenticationTypes)
//        //    {
//        //        throw new NotImplementedException();
//        //    }




//        //    //Extensions

//        //    public ClaimsIdentity CreateTwoFactorRememberBrowserIdentity(string userId)
//        //    {
//        //        throw new NotImplementedException();
//        //    }

//        //    public IEnumerable<AuthenticationDescription> GetExternalAuthenticationTypes()
//        //    {
//        //        throw new NotImplementedException();
//        //    }

//        //    public Task<ClaimsIdentity> GetExternalIdentityAsync(string externalAuthenticationType)
//        //    {
//        //        throw new NotImplementedException();
//        //    }

//        //    public ExternalLoginInfo GetExternalLoginInfo()
//        //    {
//        //        throw new NotImplementedException();
//        //    }

//        //    public ExternalLoginInfo GetExternalLoginInfo(string xsrfKey, string expectedValue)
//        //    {
//        //        throw new NotImplementedException();
//        //    }

//        //    public Task<ExternalLoginInfo> GetExternalLoginInfoAsync()
//        //    {
//        //        throw new NotImplementedException();
//        //    }

//        //    public Task<ExternalLoginInfo> GetExternalLoginInfoAsync(string xsrfKey, string expectedValue)
//        //    {
//        //        throw new NotImplementedException();
//        //    }

//        //    public Task<bool> TwoFactorBrowserRememberedAsync(string userId)
//        //    {
//        //        throw new NotImplementedException();
//        //    }

//        //    #endregion



//        //    #region UserManager



//        public new IPasswordHasher PasswordHasher
//        {
//            get
//            {
//                ThrowIfDisposed();
//                return _passwordHasher;
//            }
//            set
//            {
//                ThrowIfDisposed();
//                if (value == null)
//                {
//                    throw new ArgumentNullException("value");
//                }
//                _passwordHasher = value;
//            }
//        }
//        private IPasswordHasher _passwordHasher;

//        public new IIdentityValidator<User> UserValidator
//        {
//            get
//            {
//                ThrowIfDisposed();
//                return _userValidator;
//            }
//            set
//            {
//                ThrowIfDisposed();
//                if (value == null)
//                {
//                    throw new ArgumentNullException("value");
//                }
//                _userValidator = value;
//            }
//        }
//        private IIdentityValidator<User> _userValidator;

//        public new IIdentityValidator<string> PasswordValidator
//        {
//            get
//            {
//                ThrowIfDisposed();
//                return _passwordValidator;
//            }
//            set
//            {
//                ThrowIfDisposed();
//                if (value == null)
//                {
//                    throw new ArgumentNullException("value");
//                }
//                _passwordValidator = value;
//            }
//        }
//        private IIdentityValidator<string> _passwordValidator;

//        public new IClaimsIdentityFactory<User, Guid> ClaimsIdentityFactory
//        {
//            get
//            {
//                ThrowIfDisposed();
//                return _claimsIdentityFactory;
//            }
//            set
//            {
//                ThrowIfDisposed();
//                if (value == null)
//                {
//                    throw new ArgumentNullException("value");
//                }
//                _claimsIdentityFactory = value;
//            }
//        }
//        private IClaimsIdentityFactory<User, Guid> _claimsIdentityFactory;

//        public new IIdentityMessageService EmailService
//        {
//            get { return _emailService; }
//            set { _emailService = value; }
//        }
//        private IIdentityMessageService _emailService;

//        public new IIdentityMessageService SmsService
//        {
//            get { return _smsService; }
//            set { _smsService = value; }
//        }
//        private IIdentityMessageService _smsService;

//        public new IUserTokenProvider<User, Guid> UserTokenProvider
//        {
//            get { return _userTokenProvider; }
//            set { _userTokenProvider = value; }
//        }
//        private IUserTokenProvider<User, Guid> _userTokenProvider;

//        public new bool UserLockoutEnabledByDefault
//        {
//            get { return _userLockoutEnabledByDefault; }
//            set { _userLockoutEnabledByDefault = value; }
//        }
//        private bool _userLockoutEnabledByDefault;

//        public new int MaxFailedAccessAttemptsBeforeLockout
//        {
//            get { return _maxFailedAccessAttemptsBeforeLockout; }
//            set { _maxFailedAccessAttemptsBeforeLockout = value; }
//        }
//        private int _maxFailedAccessAttemptsBeforeLockout;

//        public new TimeSpan DefaultAccountLockoutTimeSpan
//        {
//            get { return _defaultLockout; }
//            set { _defaultLockout = value; }
//        }
//        private TimeSpan _defaultLockout = TimeSpan.Zero;

//        public new virtual bool SupportsUserTwoFactor
//        {
//            get
//            {
//                ThrowIfDisposed();
//                return _supportsUserTwoFactor;
//            }
//            protected set { _supportsUserTwoFactor = value; }
//        }
//        private bool _supportsUserTwoFactor;

//        public new virtual bool SupportsUserPassword
//        {
//            get
//            {
//                ThrowIfDisposed();
//                return _supportsUserPassword;
//            }
//            protected set { _supportsUserPassword = value; }
//        }
//        private bool _supportsUserPassword;

//        public new virtual bool SupportsUserSecurityStamp
//        {
//            get
//            {
//                ThrowIfDisposed();
//                return _supportsUserSecurityStamp;
//            }
//            protected set { _supportsUserSecurityStamp = value; }
//        }
//        private bool _supportsUserSecurityStamp;

//        public new virtual bool SupportsUserRole
//        {
//            get
//            {
//                ThrowIfDisposed();
//                return _supportsUserRole;
//            }
//            protected set { _supportsUserRole = value; }
//        }
//        private bool _supportsUserRole;

//        public new virtual bool SupportsUserLogin
//        {
//            get
//            {
//                ThrowIfDisposed();
//                return _supportsUserLogin;
//            }
//            protected set { _supportsUserLogin = value; }
//        }
//        private bool _supportsUserLogin;

//        public new virtual bool SupportsUserEmail
//        {
//            get
//            {
//                ThrowIfDisposed();
//                return _supportsUserEmail;
//            }
//            protected set { _supportsUserEmail = value; }
//        }
//        private bool _supportsUserEmail;

//        public new virtual bool SupportsUserPhoneNumber
//        {
//            get
//            {
//                ThrowIfDisposed();
//                return _supportsUserPhoneNumber;
//            }
//            protected set { _supportsUserPhoneNumber = value; }
//        }
//        private bool _supportsUserPhoneNumber;

//        public new virtual bool SupportsUserClaim
//        {
//            get
//            {
//                ThrowIfDisposed();
//                return _supportsUserClaim;
//            }
//            protected set { _supportsUserClaim = value; }
//        }
//        private bool _supportsUserClaim;

//        public new virtual bool SupportsUserLockout
//        {
//            get
//            {
//                ThrowIfDisposed();
//                return _supportsUserLockout;
//            }
//            protected set { _supportsUserLockout = value; }
//        }
//        private bool _supportsUserLockout;

//        public new virtual bool SupportsQueryableUsers
//        {
//            get
//            {
//                ThrowIfDisposed();
//                return _supportsQueryableUsers;
//            }
//            protected set { _supportsQueryableUsers = value; }
//        }
//        private bool _supportsQueryableUsers;

//        public new virtual IQueryable<User> Users
//        {
//            get
//            {
//                if (!SupportsQueryableUsers)
//                {
//                    throw new NotSupportedException();
//                }
//                return _users;
//            }
//            protected set { _users = value; }
//        }
//        private IQueryable<User> _users;

//        public new IDictionary<string, IUserTokenProvider<User, Guid>> TwoFactorProviders
//        {
//            get { return _factors; }
//        }
//        private readonly Dictionary<string, IUserTokenProvider<User, Guid>> _factors =
//            new Dictionary<string, IUserTokenProvider<User, Guid>>();


//        public new void Dispose()
//        {
//            Dispose(true);
//            GC.SuppressFinalize(this);
//        }

//        /// <summary>
//        ///     Creates a ClaimsIdentity representing the user
//        /// </summary>
//        /// <param name="user"></param>
//        /// <param name="authenticationType"></param>
//        /// <returns></returns>
//        public new virtual Task<ClaimsIdentity> CreateIdentityAsync(User user, string authenticationType)
//        {
//            ThrowIfDisposed();
//            if (user == null)
//            {
//                throw new ArgumentNullException("user");
//            }
//            return ClaimsIdentityFactory.CreateAsync(this, user, authenticationType);
//        }

//        /// <summary>
//        ///     Create a user with no password
//        /// </summary>
//        /// <param name="user"></param>
//        /// <returns></returns>
//        public new virtual async Task<IdentityResultDto> CreateAsync(User user)
//        {
//            ThrowIfDisposed();
//            await UpdateSecurityStampInternal(user).WithCurrentCulture();
//            var result = await UserValidator.ValidateAsync(user).WithCurrentCulture();
//            if (!result.Succeeded)
//            {
//                return result.ToDto();
//            }
//            if (UserLockoutEnabledByDefault && SupportsUserLockout)
//            {
//                await GetUserLockoutStore().SetLockoutEnabledAsync(user, true).WithCurrentCulture();
//            }
//            await Store.CreateAsync(user).WithCurrentCulture();
//            return IdentityResult.Success.ToDto();
//        }

//        /// <summary>
//        ///     Update a user
//        /// </summary>
//        /// <param name="user"></param>
//        /// <returns></returns>
//        public new virtual async Task<IdentityResultDto> UpdateAsync(User user)
//        {
//            ThrowIfDisposed();
//            if (user == null)
//            {
//                throw new ArgumentNullException("user");
//            }

//            var result = await UserValidator.ValidateAsync(user).WithCurrentCulture();
//            if (!result.Succeeded)
//            {
//                return result.ToDto();
//            }
//            await Store.UpdateAsync(user).WithCurrentCulture();
//            return IdentityResult.Success.ToDto();
//        }

//        /// <summary>
//        ///     Delete a user
//        /// </summary>
//        /// <param name="user"></param>
//        /// <returns></returns>
//        public new virtual async Task<IdentityResultDto> DeleteAsync(User user)
//        {
//            ThrowIfDisposed();
//            await Store.DeleteAsync(user).WithCurrentCulture();
//            return IdentityResult.Success.ToDto();
//        }

//        /// <summary>
//        ///     Find a user by id
//        /// </summary>
//        /// <param name="userId"></param>
//        /// <returns></returns>
//        public new virtual Task<User> FindByIdAsync(Guid userId)
//        {
//            ThrowIfDisposed();
//            return Store.FindByIdAsync(userId);
//        }

//        /// <summary>
//        ///     Find a user by user name
//        /// </summary>
//        /// <param name="userName"></param>
//        /// <returns></returns>
//        public new virtual Task<User> FindByNameAsync(string userName)
//        {
//            ThrowIfDisposed();
//            if (userName == null)
//            {
//                throw new ArgumentNullException("userName");
//            }
//            return Store.FindByNameAsync(userName);
//        }

//        // IUserPasswordStore methods
//        private IUserPasswordStore<User, Guid> GetPasswordStore()
//        {
//            // todo:
//            var cast = Store as IUserPasswordStore<User, Guid>;
//            if (cast == null)
//            {
//                throw new NotSupportedException(Resources.StoreNotIUserPasswordStore);
//            }
//            return cast;
//        }

//        /// <summary>
//        ///     Create a user with the given password
//        /// </summary>
//        /// <param name="user"></param>
//        /// <param name="password"></param>
//        /// <returns></returns>
//        public new virtual async Task<IdentityResultDto> CreateAsync(User user, string password)
//        {
//            ThrowIfDisposed();
//            var passwordStore = GetPasswordStore();
//            if (user == null)
//            {
//                throw new ArgumentNullException("user");
//            }
//            if (password == null)
//            {
//                throw new ArgumentNullException("password");
//            }
//            var result = await UpdatePassword(passwordStore, user, password).WithCurrentCulture();
//            if (!result.Succeeded)
//            {
//                return result;
//            }
//            return await CreateAsync(user).WithCurrentCulture();
//        }

//        /// <summary>
//        ///     Return a user with the specified username and password or null if there is no match.
//        /// </summary>
//        /// <param name="userName"></param>
//        /// <param name="password"></param>
//        /// <returns></returns>
//        public new virtual async Task<User> FindAsync(string userName, string password)
//        {
//            ThrowIfDisposed();
//            var user = await FindByNameAsync(userName).WithCurrentCulture();
//            if (user == null)
//            {
//                return null;
//            }
//            return await CheckPasswordAsync(user, password).WithCurrentCulture() ? user : null;
//        }

//        /// <summary>
//        ///     Returns true if the password is valid for the user
//        /// </summary>
//        /// <param name="user"></param>
//        /// <param name="password"></param>
//        /// <returns></returns>
//        public new virtual async Task<bool> CheckPasswordAsync(User user, string password)
//        {
//            ThrowIfDisposed();
//            var passwordStore = GetPasswordStore();
//            if (user == null)
//            {
//                return false;
//            }
//            return await VerifyPasswordAsync(passwordStore, user, password).WithCurrentCulture();
//        }

//        /// <summary>
//        ///     Returns true if the user has a password
//        /// </summary>
//        /// <param name="userId"></param>
//        /// <returns></returns>
//        public new virtual async Task<bool> HasPasswordAsync(Guid userId)
//        {
//            ThrowIfDisposed();
//            var passwordStore = GetPasswordStore();
//            var user = await FindByIdAsync(userId).WithCurrentCulture();
//            if (user == null)
//            {
//                throw new InvalidOperationException(String.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound,
//                    userId));
//            }
//            return await passwordStore.HasPasswordAsync(user).WithCurrentCulture();
//        }

//        /// <summary>
//        ///     Add a user password only if one does not already exist
//        /// </summary>
//        /// <param name="userId"></param>
//        /// <param name="password"></param>
//        /// <returns></returns>
//        public new virtual async Task<IdentityResultDto> AddPasswordAsync(Guid userId, string password)
//        {
//            ThrowIfDisposed();
//            var passwordStore = GetPasswordStore();
//            var user = await FindByIdAsync(userId).WithCurrentCulture();
//            if (user == null)
//            {
//                throw new InvalidOperationException(String.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound,
//                    userId));
//            }
//            var hash = await passwordStore.GetPasswordHashAsync(user).WithCurrentCulture();
//            if (hash != null)
//            {
//                return new IdentityResult(Resources.UserAlreadyHasPassword).ToDto();
//            }
//            var result = await UpdatePassword(passwordStore, user, password).WithCurrentCulture();
//            if (!result.Succeeded)
//            {
//                return result;
//            }
//            return await UpdateAsync(user).WithCurrentCulture();
//        }

//        /// <summary>
//        ///     Change a user password
//        /// </summary>
//        /// <param name="userId"></param>
//        /// <param name="currentPassword"></param>
//        /// <param name="newPassword"></param>
//        /// <returns></returns>
//        public new virtual async Task<IdentityResultDto> ChangePasswordAsync(Guid userId, string currentPassword,
//            string newPassword)
//        {
//            ThrowIfDisposed();
//            var passwordStore = GetPasswordStore();
//            var user = await FindByIdAsync(userId).WithCurrentCulture();
//            if (user == null)
//            {
//                throw new InvalidOperationException(String.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound,
//                    userId));
//            }
//            if (await VerifyPasswordAsync(passwordStore, user, currentPassword).WithCurrentCulture())
//            {
//                var result = await UpdatePassword(passwordStore, user, newPassword).WithCurrentCulture();
//                if (!result.Succeeded)
//                {
//                    return result;
//                }
//                return await UpdateAsync(user).WithCurrentCulture();
//            }
//            return IdentityResult.Failed(Resources.PasswordMismatch).ToDto();
//        }

//        /// <summary>
//        ///     Remove a user's password
//        /// </summary>
//        /// <param name="userId"></param>
//        /// <returns></returns>
//        public new virtual async Task<IdentityResultDto> RemovePasswordAsync(Guid userId)
//        {
//            ThrowIfDisposed();
//            var passwordStore = GetPasswordStore();
//            var user = await FindByIdAsync(userId).WithCurrentCulture();
//            if (user == null)
//            {
//                throw new InvalidOperationException(String.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound,
//                    userId));
//            }
//            await passwordStore.SetPasswordHashAsync(user, null).WithCurrentCulture();
//            await UpdateSecurityStampInternal(user).WithCurrentCulture();
//            return await UpdateAsync(user).WithCurrentCulture();
//        }

//        protected new virtual async Task<IdentityResultDto> UpdatePassword(IUserPasswordStore<User, Guid> passwordStore,
//            User user, string newPassword)
//        {
//            var result = await PasswordValidator.ValidateAsync(newPassword).WithCurrentCulture();
//            if (!result.Succeeded)
//            {
//                return result.ToDto();
//            }
//            await
//                passwordStore.SetPasswordHashAsync(user, PasswordHasher.HashPassword(newPassword)).WithCurrentCulture();
//            await UpdateSecurityStampInternal(user).WithCurrentCulture();
//            return IdentityResult.Success.ToDto();
//        }

//        /// <summary>
//        ///     By default, retrieves the hashed password from the user store and calls PasswordHasher.VerifyHashPassword
//        /// </summary>
//        /// <param name="store"></param>
//        /// <param name="user"></param>
//        /// <param name="password"></param>
//        /// <returns></returns>
//        protected new virtual async Task<bool> VerifyPasswordAsync(IUserPasswordStore<User, Guid> store, User user,
//            string password)
//        {
//            var hash = await store.GetPasswordHashAsync(user).WithCurrentCulture();
//            return PasswordHasher.VerifyHashedPassword(hash, password) != PasswordVerificationResult.Failed;
//        }

//        // IUserSecurityStampStore methods
//        private IUserSecurityStampStore<User, Guid> GetSecurityStore()
//        {
//            //todo:
//            var cast = Store as IUserSecurityStampStore<User, Guid>;
//            if (cast == null)
//            {
//                throw new NotSupportedException(Resources.StoreNotIUserSecurityStampStore);
//            }
//            return cast;
//        }

//        /// <summary>
//        ///     Returns the current security stamp for a user
//        /// </summary>
//        /// <param name="userId"></param>
//        /// <returns></returns>
//        public new virtual async Task<string> GetSecurityStampAsync(Guid userId)
//        {
//            ThrowIfDisposed();
//            var securityStore = GetSecurityStore();
//            var user = await FindByIdAsync(userId).WithCurrentCulture();
//            if (user == null)
//            {
//                throw new InvalidOperationException(String.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound,
//                    userId));
//            }
//            return await securityStore.GetSecurityStampAsync(user).WithCurrentCulture();
//        }

//        /// <summary>
//        ///     Generate a new security stamp for a user, used for SignOutEverywhere functionality
//        /// </summary>
//        /// <param name="userId"></param>
//        /// <returns></returns>
//        public new virtual async Task<IdentityResultDto> UpdateSecurityStampAsync(Guid userId)
//        {
//            ThrowIfDisposed();
//            var securityStore = GetSecurityStore();
//            var user = await FindByIdAsync(userId).WithCurrentCulture();
//            if (user == null)
//            {
//                throw new InvalidOperationException(String.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound,
//                    userId));
//            }
//            await securityStore.SetSecurityStampAsync(user, NewSecurityStamp()).WithCurrentCulture();
//            return await UpdateAsync(user).WithCurrentCulture();
//        }

//        /// <summary>
//        ///     Generate a password reset token for the user using the UserTokenProvider
//        /// </summary>
//        /// <param name="userId"></param>
//        /// <returns></returns>
//        public new virtual Task<string> GeneratePasswordResetTokenAsync(Guid userId)
//        {
//            ThrowIfDisposed();
//            return GenerateUserTokenAsync("ResetPassword", userId);
//        }

//        /// <summary>
//        ///     Reset a user's password using a reset password token
//        /// </summary>
//        /// <param name="userId"></param>
//        /// <param name="token"></param>
//        /// <param name="newPassword"></param>
//        /// <returns></returns>
//        public new virtual async Task<IdentityResultDto> ResetPasswordAsync(Guid userId, string token, string newPassword)
//        {
//            ThrowIfDisposed();
//            var user = await FindByIdAsync(userId).WithCurrentCulture();
//            if (user == null)
//            {
//                throw new InvalidOperationException(String.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound,
//                    userId));
//            }
//            // Make sure the token is valid and the stamp matches
//            if (!await VerifyUserTokenAsync(userId, "ResetPassword", token).WithCurrentCulture())
//            {
//                return IdentityResult.Failed(Resources.InvalidToken).ToDto();
//            }
//            var passwordStore = GetPasswordStore();
//            var result = await UpdatePassword(passwordStore, user, newPassword).WithCurrentCulture();
//            if (!result.Succeeded)
//            {
//                return result;
//            }
//            return await UpdateAsync(user).WithCurrentCulture();
//        }

//        // Update the security stamp if the store supports it
//        internal async Task UpdateSecurityStampInternal(User user)
//        {
//            if (SupportsUserSecurityStamp)
//            {
//                await GetSecurityStore().SetSecurityStampAsync(user, NewSecurityStamp()).WithCurrentCulture();
//            }
//        }

//        private static string NewSecurityStamp()
//        {
//            return Guid.NewGuid().ToString();
//        }

//        // IUserLoginStore methods
//        private IUserLoginStore<User, Guid> GetLoginStore()
//        {
//            // todo:
//            var cast = Store as IUserLoginStore<User, Guid>;
//            if (cast == null)
//            {
//                throw new NotSupportedException(Resources.StoreNotIUserLoginStore);
//            }
//            return cast;
//        }

//        /// <summary>
//        ///     Returns the user associated with this login
//        /// </summary>
//        /// <returns></returns>
//        public virtual Task<User> FindAsync(UserLoginInfoDto login)
//        {
//            ThrowIfDisposed();
//            return GetLoginStore().FindAsync(login.ToLoginInfo());
//        }

//        /// <summary>
//        ///     Remove a user login
//        /// </summary>
//        /// <param name="userId"></param>
//        /// <param name="login"></param>
//        /// <returns></returns>
//        public virtual async Task<IdentityResultDto> RemoveLoginAsync(Guid userId, UserLoginInfoDto login)
//        {
//            ThrowIfDisposed();
//            var loginStore = GetLoginStore();
//            if (login == null)
//            {
//                throw new ArgumentNullException("login");
//            }
//            var user = await FindByIdAsync(userId).WithCurrentCulture();
//            if (user == null)
//            {
//                throw new InvalidOperationException(String.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound,
//                    userId));
//            }
//            await loginStore.RemoveLoginAsync(user, login.ToLoginInfo()).WithCurrentCulture();
//            await UpdateSecurityStampInternal(user).WithCurrentCulture();
//            return await UpdateAsync(user).WithCurrentCulture();
//        }

//        /// <summary>
//        ///     Associate a login with a user
//        /// </summary>
//        /// <param name="userId"></param>
//        /// <param name="login"></param>
//        /// <returns></returns>
//        public virtual async Task<IdentityResultDto> AddLoginAsync(Guid userId, UserLoginInfoDto login)
//        {
//            ThrowIfDisposed();
//            var loginStore = GetLoginStore();
//            if (login == null)
//            {
//                throw new ArgumentNullException("login");
//            }
//            var user = await FindByIdAsync(userId).WithCurrentCulture();
//            if (user == null)
//            {
//                throw new InvalidOperationException(String.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound,
//                    userId));
//            }
//            var existingUser = await FindAsync(login).WithCurrentCulture();
//            if (existingUser != null)
//            {
//                return IdentityResult.Failed(Resources.ExternalLoginExists).ToDto();
//            }
//            await loginStore.AddLoginAsync(user, login.ToLoginInfo()).WithCurrentCulture();
//            return await UpdateAsync(user).WithCurrentCulture();
//        }

//        /// <summary>
//        ///     Gets the logins for a user.
//        /// </summary>
//        /// <param name="userId"></param>
//        /// <returns></returns>
//        public new virtual async Task<IList<UserLoginInfoDto>> GetLoginsAsync(Guid userId)
//        {
//            ThrowIfDisposed();
//            var loginStore = GetLoginStore();
//            var user = await FindByIdAsync(userId).WithCurrentCulture();
//            if (user == null)
//            {
//                throw new InvalidOperationException(String.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound,
//                    userId));
//            }
//            var logins = await loginStore.GetLoginsAsync(user).WithCurrentCulture();
//            throw new NotImplementedException();
//            //return null;
//        }

//        // IUserClaimStore methods
//        private IUserClaimStore<User, Guid> GetClaimStore()
//        {
//            var cast = Store as IUserClaimStore<User, Guid>;
//            if (cast == null)
//            {
//                throw new NotSupportedException(Resources.StoreNotIUserClaimStore);
//            }
//            return cast;
//        }

//        /// <summary>
//        ///     Add a user claim
//        /// </summary>
//        /// <param name="userId"></param>
//        /// <param name="claim"></param>
//        /// <returns></returns>
//        public new virtual async Task<IdentityResultDto> AddClaimAsync(Guid userId, Claim claim)
//        {
//            ThrowIfDisposed();
//            var claimStore = GetClaimStore();
//            if (claim == null)
//            {
//                throw new ArgumentNullException("claim");
//            }
//            var user = await FindByIdAsync(userId).WithCurrentCulture();
//            if (user == null)
//            {
//                throw new InvalidOperationException(String.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound,
//                    userId));
//            }
//            await claimStore.AddClaimAsync(user, claim).WithCurrentCulture();
//            return await UpdateAsync(user).WithCurrentCulture();
//        }

//        /// <summary>
//        ///     Remove a user claim
//        /// </summary>
//        /// <param name="userId"></param>
//        /// <param name="claim"></param>
//        /// <returns></returns>
//        public new virtual async Task<IdentityResultDto> RemoveClaimAsync(Guid userId, Claim claim)
//        {
//            ThrowIfDisposed();
//            var claimStore = GetClaimStore();
//            var user = await FindByIdAsync(userId).WithCurrentCulture();
//            if (user == null)
//            {
//                throw new InvalidOperationException(String.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound,
//                    userId));
//            }
//            await claimStore.RemoveClaimAsync(user, claim).WithCurrentCulture();
//            return await UpdateAsync(user).WithCurrentCulture();
//        }

//        /// <summary>
//        ///     Get a users's claims
//        /// </summary>
//        /// <param name="userId"></param>
//        /// <returns></returns>
//        public new virtual async Task<IList<Claim>> GetClaimsAsync(Guid userId)
//        {
//            ThrowIfDisposed();
//            var claimStore = GetClaimStore();
//            var user = await FindByIdAsync(userId).WithCurrentCulture();
//            if (user == null)
//            {
//                throw new InvalidOperationException(String.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound,
//                    userId));
//            }
//            return await claimStore.GetClaimsAsync(user).WithCurrentCulture();
//        }

//        private IUserRoleStore<User, Guid> GetUserRoleStore()
//        {
//            var cast = Store as IUserRoleStore<User, Guid>;
//            if (cast == null)
//            {
//                throw new NotSupportedException(Resources.StoreNotIUserRoleStore);
//            }
//            return cast;
//        }

//        /// <summary>
//        ///     Add a user to a role
//        /// </summary>
//        /// <param name="userId"></param>
//        /// <param name="role"></param>
//        /// <returns></returns>
//        public new virtual async Task<IdentityResultDto> AddToRoleAsync(Guid userId, string role)
//        {
//            ThrowIfDisposed();
//            var userRoleStore = GetUserRoleStore();
//            var user = await FindByIdAsync(userId).WithCurrentCulture();
//            if (user == null)
//            {
//                throw new InvalidOperationException(String.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound,
//                    userId));
//            }
//            var userRoles = await userRoleStore.GetRolesAsync(user).WithCurrentCulture();
//            if (userRoles.Contains(role))
//            {
//                return new IdentityResult(Resources.UserAlreadyInRole).ToDto();
//            }
//            await userRoleStore.AddToRoleAsync(user, role).WithCurrentCulture();
//            return await UpdateAsync(user).WithCurrentCulture();
//        }

//        /// <summary>
//        /// Method to add user to multiple roles
//        /// </summary>
//        /// <param name="userId">user id</param>
//        /// <param name="roles">list of role names</param>
//        /// <returns></returns>
//        public new virtual async Task<IdentityResultDto> AddToRolesAsync(Guid userId, params string[] roles)
//        {
//            ThrowIfDisposed();
//            var userRoleStore = GetUserRoleStore();
//            if (roles == null)
//            {
//                throw new ArgumentNullException("roles");
//            }
//            var user = await FindByIdAsync(userId).WithCurrentCulture();
//            if (user == null)
//            {
//                throw new InvalidOperationException(String.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound,
//                    userId));
//            }
//            var userRoles = await userRoleStore.GetRolesAsync(user).WithCurrentCulture();
//            foreach (var r in roles)
//            {
//                if (userRoles.Contains(r))
//                {
//                    return new IdentityResult(Resources.UserAlreadyInRole).ToDto();
//                }
//                await userRoleStore.AddToRoleAsync(user, r).WithCurrentCulture();
//            }
//            return await UpdateAsync(user).WithCurrentCulture();
//        }

//        /// <summary>
//        /// Remove user from multiple roles
//        /// </summary>
//        /// <param name="userId">user id</param>
//        /// <param name="roles">list of role names</param>
//        /// <returns></returns>
//        public new virtual async Task<IdentityResultDto> RemoveFromRolesAsync(Guid userId, params string[] roles)
//        {
//            ThrowIfDisposed();
//            var userRoleStore = GetUserRoleStore();
//            if (roles == null)
//            {
//                throw new ArgumentNullException("roles");
//            }
//            var user = await FindByIdAsync(userId).WithCurrentCulture();
//            if (user == null)
//            {
//                throw new InvalidOperationException(String.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound,
//                    userId));
//            }

//            // Remove user to each role using UserRoleStore
//            var userRoles = await userRoleStore.GetRolesAsync(user).WithCurrentCulture();
//            foreach (var role in roles)
//            {
//                if (!userRoles.Contains(role))
//                {
//                    return new IdentityResult(Resources.UserNotInRole).ToDto();
//                }
//                await userRoleStore.RemoveFromRoleAsync(user, role).WithCurrentCulture();
//            }

//            // Call update once when all roles are removed
//            return await UpdateAsync(user).WithCurrentCulture();
//        }

//        /// <summary>
//        ///     Remove a user from a role.
//        /// </summary>
//        /// <param name="userId"></param>
//        /// <param name="role"></param>
//        /// <returns></returns>
//        public new virtual async Task<IdentityResultDto> RemoveFromRoleAsync(Guid userId, string role)
//        {
//            ThrowIfDisposed();
//            var userRoleStore = GetUserRoleStore();
//            var user = await FindByIdAsync(userId).WithCurrentCulture();
//            if (user == null)
//            {
//                throw new InvalidOperationException(String.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound,
//                    userId));
//            }
//            if (!await userRoleStore.IsInRoleAsync(user, role).WithCurrentCulture())
//            {
//                return new IdentityResult(Resources.UserNotInRole).ToDto();
//            }
//            await userRoleStore.RemoveFromRoleAsync(user, role).WithCurrentCulture();
//            return await UpdateAsync(user).WithCurrentCulture();
//        }

//        /// <summary>
//        ///     Returns the roles for the user
//        /// </summary>
//        /// <param name="userId"></param>
//        /// <returns></returns>
//        public new virtual async Task<IList<string>> GetRolesAsync(Guid userId)
//        {
//            ThrowIfDisposed();
//            var userRoleStore = GetUserRoleStore();
//            var user = await FindByIdAsync(userId).WithCurrentCulture();
//            if (user == null)
//            {
//                throw new InvalidOperationException(String.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound,
//                    userId));
//            }
//            return await userRoleStore.GetRolesAsync(user).WithCurrentCulture();
//        }

//        /// <summary>
//        ///     Returns true if the user is in the specified role
//        /// </summary>
//        /// <param name="userId"></param>
//        /// <param name="role"></param>
//        /// <returns></returns>
//        public new virtual async Task<bool> IsInRoleAsync(Guid userId, string role)
//        {
//            ThrowIfDisposed();
//            var userRoleStore = GetUserRoleStore();
//            var user = await FindByIdAsync(userId).WithCurrentCulture();
//            if (user == null)
//            {
//                throw new InvalidOperationException(String.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound,
//                    userId));
//            }
//            return await userRoleStore.IsInRoleAsync(user, role).WithCurrentCulture();
//        }

//        // IUserEmailStore methods
//        internal IUserEmailStore<User, Guid> GetEmailStore()
//        {
//            var cast = Store as IUserEmailStore<User, Guid>;
//            if (cast == null)
//            {
//                throw new NotSupportedException(Resources.StoreNotIUserEmailStore);
//            }
//            return cast;
//        }

//        /// <summary>
//        ///     Get a user's email
//        /// </summary>
//        /// <param name="userId"></param>
//        /// <returns></returns>
//        public new virtual async Task<string> GetEmailAsync(Guid userId)
//        {
//            ThrowIfDisposed();
//            var store = GetEmailStore();
//            var user = await FindByIdAsync(userId).WithCurrentCulture();
//            if (user == null)
//            {
//                throw new InvalidOperationException(String.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound,
//                    userId));
//            }
//            return await store.GetEmailAsync(user).WithCurrentCulture();
//        }

//        /// <summary>
//        ///     Set a user's email
//        /// </summary>
//        /// <param name="userId"></param>
//        /// <param name="email"></param>
//        /// <returns></returns>
//        public new virtual async Task<IdentityResultDto> SetEmailAsync(Guid userId, string email)
//        {
//            ThrowIfDisposed();
//            var store = GetEmailStore();
//            var user = await FindByIdAsync(userId).WithCurrentCulture();
//            if (user == null)
//            {
//                throw new InvalidOperationException(String.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound,
//                    userId));
//            }
//            await store.SetEmailAsync(user, email).WithCurrentCulture();
//            await store.SetEmailConfirmedAsync(user, false).WithCurrentCulture();
//            await UpdateSecurityStampInternal(user).WithCurrentCulture();
//            return await UpdateAsync(user).WithCurrentCulture();
//        }

//        /// <summary>
//        ///     Find a user by his email
//        /// </summary>
//        /// <param name="email"></param>
//        /// <returns></returns>
//        public new virtual Task<User> FindByEmailAsync(string email)
//        {
//            ThrowIfDisposed();
//            var store = GetEmailStore();
//            if (email == null)
//            {
//                throw new ArgumentNullException("email");
//            }
//            return store.FindByEmailAsync(email);
//        }

//        /// <summary>
//        ///     Get the email confirmation token for the user
//        /// </summary>
//        /// <param name="userId"></param>
//        /// <returns></returns>
//        public new virtual Task<string> GenerateEmailConfirmationTokenAsync(Guid userId)
//        {
//            ThrowIfDisposed();
//            return GenerateUserTokenAsync("Confirmation", userId);
//        }

//        /// <summary>
//        ///     Confirm the user's email with confirmation token
//        /// </summary>
//        /// <param name="userId"></param>
//        /// <param name="token"></param>
//        /// <returns></returns>
//        public new virtual async Task<IdentityResultDto> ConfirmEmailAsync(Guid userId, string token)
//        {
//            ThrowIfDisposed();
//            var store = GetEmailStore();
//            var user = await FindByIdAsync(userId).WithCurrentCulture();
//            if (user == null)
//            {
//                throw new InvalidOperationException(String.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound,
//                    userId));
//            }
//            if (!await VerifyUserTokenAsync(userId, "Confirmation", token).WithCurrentCulture())
//            {
//                return IdentityResult.Failed(Resources.InvalidToken).ToDto();
//            }
//            await store.SetEmailConfirmedAsync(user, true).WithCurrentCulture();
//            return await UpdateAsync(user).WithCurrentCulture();
//        }

//        /// <summary>
//        ///     Returns true if the user's email has been confirmed
//        /// </summary>
//        /// <param name="userId"></param>
//        /// <returns></returns>
//        public new virtual async Task<bool> IsEmailConfirmedAsync(Guid userId)
//        {
//            ThrowIfDisposed();
//            var store = GetEmailStore();
//            var user = await FindByIdAsync(userId).WithCurrentCulture();
//            if (user == null)
//            {
//                throw new InvalidOperationException(String.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound,
//                    userId));
//            }
//            return await store.GetEmailConfirmedAsync(user).WithCurrentCulture();
//        }

//        // IUserPhoneNumberStore methods
//        internal IUserPhoneNumberStore<User, Guid> GetPhoneNumberStore()
//        {
//            var cast = Store as IUserPhoneNumberStore<User, Guid>;
//            if (cast == null)
//            {
//                throw new NotSupportedException(Resources.StoreNotIUserPhoneNumberStore);
//            }
//            return cast;
//        }

//        ///// <summary>
//        /////     Get a user's phoneNumber
//        ///// </summary>
//        ///// <param name="userId"></param>
//        ///// <returns></returns>
//        //public virtual async Task<string> GetPhoneNumberAsync(Guid userId)
//        //{
//        //    ThrowIfDisposed();
//        //    var store = GetPhoneNumberStore();
//        //    var user = await FindByIdAsync(userId).WithCurrentCulture();
//        //    if (user == null)
//        //    {
//        //        throw new InvalidOperationException(String.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound,
//        //            userId));
//        //    }
//        //    return await store.GetPhoneNumberAsync(user).WithCurrentCulture();
//        //}

//        ///// <summary>
//        /////     Set a user's phoneNumber
//        ///// </summary>
//        ///// <param name="userId"></param>
//        ///// <param name="phoneNumber"></param>
//        ///// <returns></returns>
//        //public virtual async Task<IdentityResult> SetPhoneNumberAsync(Guid userId, string phoneNumber)
//        //{
//        //    ThrowIfDisposed();
//        //    var store = GetPhoneNumberStore();
//        //    var user = await FindByIdAsync(userId).WithCurrentCulture();
//        //    if (user == null)
//        //    {
//        //        throw new InvalidOperationException(String.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound,
//        //            userId));
//        //    }
//        //    await store.SetPhoneNumberAsync(user, phoneNumber).WithCurrentCulture();
//        //    await store.SetPhoneNumberConfirmedAsync(user, false).WithCurrentCulture();
//        //    await UpdateSecurityStampInternal(user).WithCurrentCulture();
//        //    return await UpdateAsync(user).WithCurrentCulture();
//        //}

//        ///// <summary>
//        /////     Set a user's phoneNumber with the verification token
//        ///// </summary>
//        ///// <param name="userId"></param>
//        ///// <param name="phoneNumber"></param>
//        ///// <param name="token"></param>
//        ///// <returns></returns>
//        //public virtual async Task<IdentityResult> ChangePhoneNumberAsync(Guid userId, string phoneNumber, string token)
//        //{
//        //    ThrowIfDisposed();
//        //    var store = GetPhoneNumberStore();
//        //    var user = await FindByIdAsync(userId).WithCurrentCulture();
//        //    if (user == null)
//        //    {
//        //        throw new InvalidOperationException(String.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound,
//        //            userId));
//        //    }
//        //    if (await VerifyChangePhoneNumberTokenAsync(userId, token, phoneNumber).WithCurrentCulture())
//        //    {
//        //        await store.SetPhoneNumberAsync(user, phoneNumber).WithCurrentCulture();
//        //        await store.SetPhoneNumberConfirmedAsync(user, true).WithCurrentCulture();
//        //        await UpdateSecurityStampInternal(user).WithCurrentCulture();
//        //        return await UpdateAsync(user).WithCurrentCulture();
//        //    }
//        //    return IdentityResult.Failed(Resources.InvalidToken);
//        //}

//        ///// <summary>
//        /////     Returns true if the user's phone number has been confirmed
//        ///// </summary>
//        ///// <param name="userId"></param>
//        ///// <returns></returns>
//        //public virtual async Task<bool> IsPhoneNumberConfirmedAsync(Guid userId)
//        //{
//        //    ThrowIfDisposed();
//        //    var store = GetPhoneNumberStore();
//        //    var user = await FindByIdAsync(userId).WithCurrentCulture();
//        //    if (user == null)
//        //    {
//        //        throw new InvalidOperationException(String.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound,
//        //            userId));
//        //    }
//        //    return await store.GetPhoneNumberConfirmedAsync(user).WithCurrentCulture();
//        //}

//        // Two factor APIS

//        //internal async Task<SecurityToken> CreateSecurityTokenAsync(Guid userId)
//        //{
//        //    return
//        //        new SecurityToken(Encoding.Unicode.GetBytes(await GetSecurityStampAsync(userId).WithCurrentCulture()));
//        //}

//        ///// <summary>
//        /////     Generate a code that the user can use to change their phone number to a specific number
//        ///// </summary>
//        ///// <param name="userId"></param>
//        ///// <param name="phoneNumber"></param>
//        ///// <returns></returns>
//        //public virtual async Task<string> GenerateChangePhoneNumberTokenAsync(Guid userId, string phoneNumber)
//        //{
//        //    ThrowIfDisposed();
//        //    return
//        //        Rfc6238AuthenticationService.GenerateCode(await CreateSecurityTokenAsync(userId).WithCurrentCulture(), phoneNumber)
//        //            .ToString("D6", CultureInfo.InvariantCulture);
//        //}

//        ///// <summary>
//        /////     Verify the code is valid for a specific user and for a specific phone number
//        ///// </summary>
//        ///// <param name="userId"></param>
//        ///// <param name="token"></param>
//        ///// <param name="phoneNumber"></param>
//        ///// <returns></returns>
//        //public virtual async Task<bool> VerifyChangePhoneNumberTokenAsync(Guid userId, string token, string phoneNumber)
//        //{
//        //    ThrowIfDisposed();
//        //    var securityToken = await CreateSecurityTokenAsync(userId).WithCurrentCulture();
//        //    int code;
//        //    if (securityToken != null && Int32.TryParse(token, out code))
//        //    {
//        //        return Rfc6238AuthenticationService.ValidateCode(securityToken, code, phoneNumber);
//        //    }
//        //    return false;
//        //}

//        /// <summary>
//        ///     Verify a user token with the specified purpose
//        /// </summary>
//        /// <param name="userId"></param>
//        /// <param name="purpose"></param>
//        /// <param name="token"></param>
//        /// <returns></returns>
//        public new virtual async Task<bool> VerifyUserTokenAsync(Guid userId, string purpose, string token)
//        {
//            ThrowIfDisposed();
//            if (UserTokenProvider == null)
//            {
//                throw new NotSupportedException(Resources.NoTokenProvider);
//            }
//            var user = await FindByIdAsync(userId).WithCurrentCulture();
//            if (user == null)
//            {
//                throw new InvalidOperationException(String.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound,
//                    userId));
//            }
//            // Make sure the token is valid
//            return await UserTokenProvider.ValidateAsync(purpose, token, this, user).WithCurrentCulture();
//        }

//        /// <summary>
//        ///     Get a user token for a specific purpose
//        /// </summary>
//        /// <param name="purpose"></param>
//        /// <param name="userId"></param>
//        /// <returns></returns>
//        public new virtual async Task<string> GenerateUserTokenAsync(string purpose, Guid userId)
//        {
//            ThrowIfDisposed();
//            if (UserTokenProvider == null)
//            {
//                throw new NotSupportedException(Resources.NoTokenProvider);
//            }
//            var user = await FindByIdAsync(userId).WithCurrentCulture();
//            if (user == null)
//            {
//                throw new InvalidOperationException(String.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound,
//                    userId));
//            }
//            return await UserTokenProvider.GenerateAsync(purpose, this, user).WithCurrentCulture();
//        }

//        /// <summary>
//        ///     Register a two factor authentication provider with the TwoFactorProviders mapping
//        /// </summary>
//        /// <param name="twoFactorProvider"></param>
//        /// <param name="provider"></param>
//        public new virtual void RegisterTwoFactorProvider(string twoFactorProvider, IUserTokenProvider<User, Guid> provider)
//        {
//            ThrowIfDisposed();
//            if (twoFactorProvider == null)
//            {
//                throw new ArgumentNullException("twoFactorProvider");
//            }
//            if (provider == null)
//            {
//                throw new ArgumentNullException("provider");
//            }
//            TwoFactorProviders[twoFactorProvider] = provider;
//        }

//        /// <summary>
//        ///     Returns a list of valid two factor providers for a user
//        /// </summary>
//        /// <param name="userId"></param>
//        /// <returns></returns>
//        public new virtual async Task<IList<string>> GetValidTwoFactorProvidersAsync(Guid userId)
//        {
//            ThrowIfDisposed();
//            var user = await FindByIdAsync(userId).WithCurrentCulture();
//            if (user == null)
//            {
//                throw new InvalidOperationException(String.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound,
//                    userId));
//            }
//            var results = new List<string>();
//            foreach (var f in TwoFactorProviders)
//            {
//                if (await f.Value.IsValidProviderForUserAsync(this, user).WithCurrentCulture())
//                {
//                    results.Add(f.Key);
//                }
//            }
//            return results;
//        }

//        /// <summary>
//        ///     Verify a two factor token with the specified provider
//        /// </summary>
//        /// <param name="userId"></param>
//        /// <param name="twoFactorProvider"></param>
//        /// <param name="token"></param>
//        /// <returns></returns>
//        public new virtual async Task<bool> VerifyTwoFactorTokenAsync(Guid userId, string twoFactorProvider, string token)
//        {
//            ThrowIfDisposed();
//            var user = await FindByIdAsync(userId).WithCurrentCulture();
//            if (user == null)
//            {
//                throw new InvalidOperationException(String.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound,
//                    userId));
//            }
//            if (!_factors.ContainsKey(twoFactorProvider))
//            {
//                throw new NotSupportedException(String.Format(CultureInfo.CurrentCulture, Resources.NoTwoFactorProvider,
//                    twoFactorProvider));
//            }
//            // Make sure the token is valid
//            var provider = _factors[twoFactorProvider];
//            return await provider.ValidateAsync(twoFactorProvider, token, this, user).WithCurrentCulture();
//        }

//        /// <summary>
//        ///     Get a token for a specific two factor provider
//        /// </summary>
//        /// <param name="userId"></param>
//        /// <param name="twoFactorProvider"></param>
//        /// <returns></returns>
//        public new virtual async Task<string> GenerateTwoFactorTokenAsync(Guid userId, string twoFactorProvider)
//        {
//            ThrowIfDisposed();
//            var user = await FindByIdAsync(userId).WithCurrentCulture();
//            if (user == null)
//            {
//                throw new InvalidOperationException(String.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound,
//                    userId));
//            }
//            if (!_factors.ContainsKey(twoFactorProvider))
//            {
//                throw new NotSupportedException(String.Format(CultureInfo.CurrentCulture, Resources.NoTwoFactorProvider,
//                    twoFactorProvider));
//            }
//            return await _factors[twoFactorProvider].GenerateAsync(twoFactorProvider, this, user).WithCurrentCulture();
//        }

//        /// <summary>
//        ///     Notify a user with a token using a specific two-factor authentication provider's Notify method
//        /// </summary>
//        /// <param name="userId"></param>
//        /// <param name="twoFactorProvider"></param>
//        /// <param name="token"></param>
//        /// <returns></returns>
//        public new virtual async Task<IdentityResultDto> NotifyTwoFactorTokenAsync(Guid userId, string twoFactorProvider,
//            string token)
//        {
//            ThrowIfDisposed();
//            var user = await FindByIdAsync(userId).WithCurrentCulture();
//            if (user == null)
//            {
//                throw new InvalidOperationException(String.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound,
//                    userId));
//            }
//            if (!_factors.ContainsKey(twoFactorProvider))
//            {
//                throw new NotSupportedException(String.Format(CultureInfo.CurrentCulture, Resources.NoTwoFactorProvider,
//                    twoFactorProvider));
//            }
//            await _factors[twoFactorProvider].NotifyAsync(token, this, user).WithCurrentCulture();
//            return IdentityResult.Success.ToDto();
//        }

//        // IUserFactorStore methods
//        internal IUserTwoFactorStore<User, Guid> GetUserTwoFactorStore()
//        {
//            var cast = Store as IUserTwoFactorStore<User, Guid>;
//            if (cast == null)
//            {
//                throw new NotSupportedException(Resources.StoreNotIUserTwoFactorStore);
//            }
//            return cast;
//        }

//        /// <summary>
//        ///     Get whether two factor authentication is enabled for a user
//        /// </summary>
//        /// <param name="userId"></param>
//        /// <returns></returns>
//        public new virtual async Task<bool> GetTwoFactorEnabledAsync(Guid userId)
//        {
//            ThrowIfDisposed();
//            var store = GetUserTwoFactorStore();
//            var user = await FindByIdAsync(userId).WithCurrentCulture();
//            if (user == null)
//            {
//                throw new InvalidOperationException(String.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound,
//                    userId));
//            }
//            return await store.GetTwoFactorEnabledAsync(user).WithCurrentCulture();
//        }

//        /// <summary>
//        ///     Set whether a user has two factor authentication enabled
//        /// </summary>
//        /// <param name="userId"></param>
//        /// <param name="enabled"></param>
//        /// <returns></returns>
//        public new virtual async Task<IdentityResultDto> SetTwoFactorEnabledAsync(Guid userId, bool enabled)
//        {
//            ThrowIfDisposed();
//            var store = GetUserTwoFactorStore();
//            var user = await FindByIdAsync(userId).WithCurrentCulture();
//            if (user == null)
//            {
//                throw new InvalidOperationException(String.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound,
//                    userId));
//            }
//            await store.SetTwoFactorEnabledAsync(user, enabled).WithCurrentCulture();
//            await UpdateSecurityStampInternal(user).WithCurrentCulture();
//            return await UpdateAsync(user).WithCurrentCulture();
//        }

//        // SMS/Email methods

//        /// <summary>
//        ///     Send an email to the user
//        /// </summary>
//        /// <param name="userId"></param>
//        /// <param name="subject"></param>
//        /// <param name="body"></param>
//        /// <returns></returns>
//        public new virtual async Task SendEmailAsync(Guid userId, string subject, string body)
//        {
//            ThrowIfDisposed();
//            if (EmailService != null)
//            {
//                var msg = new IdentityMessage
//                {
//                    Destination = await GetEmailAsync(userId).WithCurrentCulture(),
//                    Subject = subject,
//                    Body = body,
//                };
//                await EmailService.SendAsync(msg).WithCurrentCulture();
//            }
//        }

//        ///// <summary>
//        /////     Send a user a sms message
//        ///// </summary>
//        ///// <param name="userId"></param>
//        ///// <param name="message"></param>
//        ///// <returns></returns>
//        //public virtual async Task SendSmsAsync(Guid userId, string message)
//        //{
//        //    ThrowIfDisposed();
//        //    if (SmsService != null)
//        //    {
//        //        var msg = new IdentityMessage
//        //        {
//        //            Destination = await GetPhoneNumberAsync(userId).WithCurrentCulture(),
//        //            Body = message
//        //        };
//        //        await SmsService.SendAsync(msg).WithCurrentCulture();
//        //    }
//        //}

//        // IUserLockoutStore methods
//        internal IUserLockoutStore<User, Guid> GetUserLockoutStore()
//        {
//            var cast = Store as IUserLockoutStore<User, Guid>;
//            if (cast == null)
//            {
//                throw new NotSupportedException(Resources.StoreNotIUserLockoutStore);
//            }
//            return cast;
//        }

//        /// <summary>
//        ///     Returns true if the user is locked out
//        /// </summary>
//        /// <param name="userId"></param>
//        /// <returns></returns>
//        public new virtual async Task<bool> IsLockedOutAsync(Guid userId)
//        {
//            ThrowIfDisposed();
//            var store = GetUserLockoutStore();
//            var user = await FindByIdAsync(userId).WithCurrentCulture();
//            if (user == null)
//            {
//                throw new InvalidOperationException(String.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound,
//                    userId));
//            }
//            if (!await store.GetLockoutEnabledAsync(user).WithCurrentCulture())
//            {
//                return false;
//            }
//            var lockoutTime = await store.GetLockoutEndDateAsync(user).WithCurrentCulture();
//            return lockoutTime >= DateTimeOffset.UtcNow;
//        }

//        /// <summary>
//        ///     Sets whether lockout is enabled for this user
//        /// </summary>
//        /// <param name="userId"></param>
//        /// <param name="enabled"></param>
//        /// <returns></returns>
//        public new virtual async Task<IdentityResultDto> SetLockoutEnabledAsync(Guid userId, bool enabled)
//        {
//            ThrowIfDisposed();
//            var store = GetUserLockoutStore();
//            var user = await FindByIdAsync(userId).WithCurrentCulture();
//            if (user == null)
//            {
//                throw new InvalidOperationException(String.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound,
//                    userId));
//            }
//            await store.SetLockoutEnabledAsync(user, enabled).WithCurrentCulture();
//            return await UpdateAsync(user).WithCurrentCulture();
//        }

//        /// <summary>
//        ///     Returns whether lockout is enabled for the user
//        /// </summary>
//        /// <param name="userId"></param>
//        /// <returns></returns>
//        public new virtual async Task<bool> GetLockoutEnabledAsync(Guid userId)
//        {
//            ThrowIfDisposed();
//            var store = GetUserLockoutStore();
//            var user = await FindByIdAsync(userId).WithCurrentCulture();
//            if (user == null)
//            {
//                throw new InvalidOperationException(String.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound,
//                    userId));
//            }
//            return await store.GetLockoutEnabledAsync(user).WithCurrentCulture();
//        }

//        /// <summary>
//        ///     Returns when the user is no longer locked out, dates in the past are considered as not being locked out
//        /// </summary>
//        /// <param name="userId"></param>
//        /// <returns></returns>
//        public new virtual async Task<DateTimeOffset> GetLockoutEndDateAsync(Guid userId)
//        {
//            ThrowIfDisposed();
//            var store = GetUserLockoutStore();
//            var user = await FindByIdAsync(userId).WithCurrentCulture();
//            if (user == null)
//            {
//                throw new InvalidOperationException(String.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound,
//                    userId));
//            }
//            return await store.GetLockoutEndDateAsync(user).WithCurrentCulture();
//        }

//        /// <summary>
//        ///     Sets the when a user lockout ends
//        /// </summary>
//        /// <param name="userId"></param>
//        /// <param name="lockoutEnd"></param>
//        /// <returns></returns>
//        public new virtual async Task<IdentityResultDto> SetLockoutEndDateAsync(Guid userId, DateTimeOffset lockoutEnd)
//        {
//            ThrowIfDisposed();
//            var store = GetUserLockoutStore();
//            var user = await FindByIdAsync(userId).WithCurrentCulture();
//            if (user == null)
//            {
//                throw new InvalidOperationException(String.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound,
//                    userId));
//            }
//            if (!await store.GetLockoutEnabledAsync(user).WithCurrentCulture())
//            {
//                return IdentityResult.Failed(Resources.LockoutNotEnabled).ToDto();
//            }
//            await store.SetLockoutEndDateAsync(user, lockoutEnd).WithCurrentCulture();
//            return await UpdateAsync(user).WithCurrentCulture();
//        }

//        /// <summary>
//        /// Increments the access failed count for the user and if the failed access account is greater than or equal
//        /// to the MaxFailedAccessAttempsBeforeLockout, the user will be locked out for the next DefaultAccountLockoutTimeSpan
//        /// and the AccessFailedCount will be reset to 0. This is used for locking out the user account.
//        /// </summary>
//        /// <param name="userId"></param>
//        /// <returns></returns>
//        public new virtual async Task<IdentityResultDto> AccessFailedAsync(Guid userId)
//        {
//            ThrowIfDisposed();
//            var store = GetUserLockoutStore();
//            var user = await FindByIdAsync(userId).WithCurrentCulture();
//            if (user == null)
//            {
//                throw new InvalidOperationException(String.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound,
//                    userId));
//            }
//            // If this puts the user over the threshold for lockout, lock them out and reset the access failed count
//            var count = await store.IncrementAccessFailedCountAsync(user).WithCurrentCulture();
//            if (count >= MaxFailedAccessAttemptsBeforeLockout)
//            {
//                await
//                    store.SetLockoutEndDateAsync(user, DateTimeOffset.UtcNow.Add(DefaultAccountLockoutTimeSpan))
//                        .WithCurrentCulture();
//                await store.ResetAccessFailedCountAsync(user).WithCurrentCulture();
//            }
//            return await UpdateAsync(user).WithCurrentCulture();
//        }

//        /// <summary>
//        ///     Resets the access failed count for the user to 0
//        /// </summary>
//        /// <param name="userId"></param>
//        /// <returns></returns>
//        public new virtual async Task<IdentityResultDto> ResetAccessFailedCountAsync(Guid userId)
//        {
//            ThrowIfDisposed();
//            var store = GetUserLockoutStore();
//            var user = await FindByIdAsync(userId).WithCurrentCulture();
//            if (user == null)
//            {
//                throw new InvalidOperationException(String.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound,
//                    userId));
//            }

//            if (await GetAccessFailedCountAsync(user.Id).WithCurrentCulture() == 0)
//            {
//                return IdentityResult.Success.ToDto();
//            }

//            await store.ResetAccessFailedCountAsync(user).WithCurrentCulture();
//            return await UpdateAsync(user).WithCurrentCulture();
//        }

//        /// <summary>
//        ///     Returns the number of failed access attempts for the user
//        /// </summary>
//        /// <param name="userId"></param>
//        /// <returns></returns>
//        public new virtual async Task<int> GetAccessFailedCountAsync(Guid userId)
//        {
//            ThrowIfDisposed();
//            var store = GetUserLockoutStore();
//            var user = await FindByIdAsync(userId).WithCurrentCulture();
//            if (user == null)
//            {
//                throw new InvalidOperationException(String.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound,
//                    userId));
//            }
//            return await store.GetAccessFailedCountAsync(user).WithCurrentCulture();
//        }



//        #region ISignInManagerService - SignInManager


//        public AuthenticationResponseChallenge AuthenticationResponseChallenge
//        {
//            get { return _authenticationResponseChallenge; }
//            set { _authenticationResponseChallenge = value; }
//        }

//        private AuthenticationResponseChallenge _authenticationResponseChallenge;

//        public AuthenticationResponseGrant AuthenticationResponseGrant
//        {
//            get { return _authenticationResponseGrant; }
//            set { _authenticationResponseGrant = value; }
//        }
//        private AuthenticationResponseGrant _authenticationResponseGrant;

//        public AuthenticationResponseRevoke AuthenticationResponseRevoke
//        {
//            get { return _authenticationResponseRevoke; }
//            set { _authenticationResponseRevoke = value; }
//        }
//        private AuthenticationResponseRevoke _authenticationResponseRevoke;

//        public ClaimsPrincipal User
//        {
//            get { return _user; }
//            set { _user = value; }
//        }
//        private ClaimsPrincipal _user;

//        public IAuthenticationManagerService AuthenticationManager
//        {
//            get { return _authenticationManager; }
//            set { _authenticationManager = value; }
//        }
//        private IAuthenticationManagerService _authenticationManager;

//        public string AuthenticationType
//        {
//            get { return _authenticationType; }
//            set { _authenticationType = value; }
//        }
//        private string _authenticationType;

//        public IUserManagerService<User, Guid> UserManager
//        {
//            get { return _userManager; }
//            set { _userManager = value; }
//        }
//        private IUserManagerService<User, Guid> _userManager;


//        public Task<ClaimsIdentity> CreateUserIdentityAsync(User user)
//        {
//            return UserManager.CreateIdentityAsync(user, AuthenticationType);
//        }

//        public Guid ConvertIdFromString(string id)
//        {
//            if (id == null)
//            {
//                return default(Guid);
//            }
//            return (Guid)Convert.ChangeType(id, typeof(string), CultureInfo.InvariantCulture);
//        }

//        public string ConvertIdToString(Guid id)
//        {
//            return Convert.ToString(id, CultureInfo.InvariantCulture);
//        }

//        public async Task SignInAsync(User user, bool isPersistent, bool rememberBrowser)
//        {
//            var userIdentity = await CreateUserIdentityAsync(user).WithCurrentCulture();
//            // Clear any partial cookies from external or two factor partial sign ins
//            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie, DefaultAuthenticationTypes.TwoFactorCookie);
//            if (rememberBrowser)
//            {
//                var rememberBrowserIdentity = AuthenticationManager.CreateTwoFactorRememberBrowserIdentity(ConvertIdToString(user.Id));
//                AuthenticationManager.SignIn(new AuthenticationProperties { IsPersistent = isPersistent }, userIdentity, rememberBrowserIdentity);
//            }
//            else
//            {
//                AuthenticationManager.SignIn(new AuthenticationProperties { IsPersistent = isPersistent }, userIdentity);
//            }
//        }

//        public async Task<bool> SendTwoFactorCodeAsync(string provider)
//        {
//            var userId = await GetVerifiedUserIdAsync().WithCurrentCulture();
//            //if (userId == null)
//            //{
//            //    return false;
//            //}

//            var token = await UserManager.GenerateTwoFactorTokenAsync(userId, provider).WithCurrentCulture();
//            // See IdentityConfig.cs to plug in Email/SMS services to actually send the code
//            await UserManager.NotifyTwoFactorTokenAsync(userId, provider, token).WithCurrentCulture();
//            return true;
//        }

//        public async Task<Guid> GetVerifiedUserIdAsync()
//        {
//            var result = await AuthenticationManager.AuthenticateAsync(DefaultAuthenticationTypes.TwoFactorCookie).WithCurrentCulture();
//            if (result != null && result.Identity != null && !String.IsNullOrEmpty(result.Identity.GetUserId()))
//            {
//                return ConvertIdFromString(result.Identity.GetUserId());
//            }
//            return default(Guid);
//        }

//        public async Task<bool> HasBeenVerifiedAsync()
//        {
//            return await GetVerifiedUserIdAsync().WithCurrentCulture() != null;
//        }

//        public async Task<SignInStatus> TwoFactorSignInAsync(string provider, string code, bool isPersistent, bool rememberBrowser)
//        {
//            var userId = await GetVerifiedUserIdAsync().WithCurrentCulture();
//            if (userId == null)
//            {
//                return SignInStatus.Failure;
//            }
//            var user = await UserManager.FindByIdAsync(userId).WithCurrentCulture();
//            if (user == null)
//            {
//                return SignInStatus.Failure;
//            }
//            if (await UserManager.IsLockedOutAsync(user.Id).WithCurrentCulture())
//            {
//                return SignInStatus.LockedOut;
//            }
//            if (await UserManager.VerifyTwoFactorTokenAsync(user.Id, provider, code).WithCurrentCulture())
//            {
//                // When token is verified correctly, clear the access failed count used for lockout
//                await UserManager.ResetAccessFailedCountAsync(user.Id).WithCurrentCulture();
//                await SignInAsync(user, isPersistent, rememberBrowser).WithCurrentCulture();
//                return SignInStatus.Success;
//            }
//            // If the token is incorrect, record the failure which also may cause the user to be locked out
//            await UserManager.AccessFailedAsync(user.Id).WithCurrentCulture();
//            return SignInStatus.Failure;
//        }

//        public async Task<SignInStatus> ExternalSignInAsync(ExternalLoginInfo loginInfo, bool isPersistent)
//        {
//            var user = await UserManager.FindAsync(new UserLoginInfoDto(loginInfo.Login.LoginProvider, loginInfo.Login.ProviderKey)).WithCurrentCulture();
//            if (user == null)
//            {
//                return SignInStatus.Failure;
//            }
//            if (await UserManager.IsLockedOutAsync(user.Id).WithCurrentCulture())
//            {
//                return SignInStatus.LockedOut;
//            }
//            return await SignInOrTwoFactor(user, isPersistent).WithCurrentCulture();
//        }

//        private async Task<SignInStatus> SignInOrTwoFactor(User user, bool isPersistent)
//        {
//            var id = Convert.ToString(user.Id);
//            if (await UserManager.GetTwoFactorEnabledAsync(user.Id).WithCurrentCulture()
//                && (await UserManager.GetValidTwoFactorProvidersAsync(user.Id).WithCurrentCulture()).Count > 0
//                && !await AuthenticationManager.TwoFactorBrowserRememberedAsync(id).WithCurrentCulture())
//            {
//                var identity = new ClaimsIdentity(DefaultAuthenticationTypes.TwoFactorCookie);
//                identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, id));
//                AuthenticationManager.SignIn(identity);
//                return SignInStatus.RequiresVerification;
//            }
//            await SignInAsync(user, isPersistent, false).WithCurrentCulture();
//            return SignInStatus.Success;
//        }

//        public async Task<SignInStatus> PasswordSignInAsync(string userName, string password, bool isPersistent, bool shouldLockout)
//        {
//            if (UserManager == null)
//            {
//                return SignInStatus.Failure;
//            }
//            var user = await UserManager.FindByNameAsync(userName).WithCurrentCulture();
//            if (user == null)
//            {
//                return SignInStatus.Failure;
//            }
//            if (await UserManager.IsLockedOutAsync(user.Id).WithCurrentCulture())
//            {
//                return SignInStatus.LockedOut;
//            }
//            if (await UserManager.CheckPasswordAsync(user, password).WithCurrentCulture())
//            {
//                await UserManager.ResetAccessFailedCountAsync(user.Id).WithCurrentCulture();
//                return await SignInOrTwoFactor(user, isPersistent).WithCurrentCulture();
//            }
//            if (shouldLockout)
//            {
//                // If lockout is requested, increment access failed count which might lock out the user
//                await UserManager.AccessFailedAsync(user.Id).WithCurrentCulture();
//                if (await UserManager.IsLockedOutAsync(user.Id).WithCurrentCulture())
//                {
//                    return SignInStatus.LockedOut;
//                }
//            }
//            return SignInStatus.Failure;
//        }

//        #endregion




//        private void ThrowIfDisposed()
//        {
//            if (IsDisposed)
//            {
//                throw new ObjectDisposedException(GetType().Name);
//            }
//        }

//        /// <summary>
//        ///     When disposing, actually dipose the store
//        /// </summary>
//        /// <param name="disposing"></param>
//        protected new virtual void Dispose(bool disposing)
//        {
//            if (disposing && !IsDisposed)
//            {
//                Store.Dispose();
//                IsDisposed = true;
//            }
//        }
















//        public Task<AuthenticateResult> AuthenticateAsync(string authenticationType)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<IEnumerable<AuthenticateResult>> AuthenticateAsync(string[] authenticationTypes)
//        {
//            throw new NotImplementedException();
//        }

//        public void Challenge(params string[] authenticationTypes)
//        {
//            throw new NotImplementedException();
//        }

//        public void Challenge(AuthenticationProperties properties, params string[] authenticationTypes)
//        {
//            throw new NotImplementedException();
//        }

//        public IEnumerable<AuthenticationDescription> GetAuthenticationTypes()
//        {
//            throw new NotImplementedException();
//        }

//        public IEnumerable<AuthenticationDescription> GetAuthenticationTypes(Func<AuthenticationDescription, bool> predicate)
//        {
//            throw new NotImplementedException();
//        }

//        public void SignIn(params ClaimsIdentity[] identities)
//        {
//            throw new NotImplementedException();
//        }

//        public void SignIn(AuthenticationProperties properties, params ClaimsIdentity[] identities)
//        {
//            throw new NotImplementedException();
//        }

//        public void SignOut(params string[] authenticationTypes)
//        {
//            throw new NotImplementedException();
//        }

//        public void SignOut(AuthenticationProperties properties, params string[] authenticationTypes)
//        {
//            throw new NotImplementedException();
//        }

//        public ClaimsIdentity CreateTwoFactorRememberBrowserIdentity(string userId)
//        {
//            throw new NotImplementedException();
//        }

//        public IEnumerable<AuthenticationDescription> GetExternalAuthenticationTypes()
//        {
//            throw new NotImplementedException();
//        }

//        public Task<ClaimsIdentity> GetExternalIdentityAsync(string externalAuthenticationType)
//        {
//            throw new NotImplementedException();
//        }

//        public ExternalLoginInfo GetExternalLoginInfo()
//        {
//            throw new NotImplementedException();
//        }

//        public ExternalLoginInfo GetExternalLoginInfo(string xsrfKey, string expectedValue)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<ExternalLoginInfo> GetExternalLoginInfoAsync()
//        {
//            throw new NotImplementedException();
//        }

//        public Task<ExternalLoginInfo> GetExternalLoginInfoAsync(string xsrfKey, string expectedValue)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<bool> TwoFactorBrowserRememberedAsync(string userId)
//        {
//            throw new NotImplementedException();
//        }
//    }
//}
