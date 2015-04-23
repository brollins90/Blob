using System;
using System.Security.Claims;
using System.ServiceModel;
using System.Threading.Tasks;

namespace Blob.Contracts.Security
{
    [ServiceContract]
    public interface IIdentityService : IIdentityService<string> { }

    [ServiceContract]
    public interface IIdentityService<in TUserIdType> :
        IIdentityStore<TUserIdType>
        //ISignInService<TUserIdType>
        //IAuthenticationManagerService
        where TUserIdType : IEquatable<TUserIdType>
    {
        [OperationContract]
        void SetProvider(string providerName);

        //IPasswordHasher PasswordHasher { [OperationContract] get; }
        //IIdentityValidator<TUser> UserValidator { [OperationContract] get; }
        //IIdentityValidator<string> PasswordValidator { [OperationContract] get; }
        //IClaimsIdentityFactory<TUser, TKey> ClaimsIdentityFactory { [OperationContract] get; }
        //IIdentityMessageService EmailService { [OperationContract] get; }
        //IIdentityMessageService SmsService { [OperationContract] get; }
        //IUserTokenProvider<TUser, TKey> UserTokenProvider { [OperationContract] get; }
        bool UserLockoutEnabledByDefault { [OperationContract] get; }
        int MaxFailedAccessAttemptsBeforeLockout { [OperationContract] get; }
        TimeSpan DefaultAccountLockoutTimeSpan { [OperationContract] get; }
        //IQueryable<TUser> Users { [OperationContract] get; }
        //IDictionary<string, IUserTokenProvider<TUser, TKey>> TwoFactorProviders { [OperationContract] get; }

        [OperationContract]
        Task<ClaimsIdentity> CreateIdentityAsync(UserDto user, string authenticationType);

        [OperationContract(Name = "CreateUserAsyncWithPassword")]
        Task<IdentityResultDto> CreateAsync(UserDto user, string password);

        //[DebuggerStepThrough]
        //[OperationContract(Name = "FindUserAsync")]
        //Task<UserDto> FindAsync(string userName, string password);

        //[DebuggerStepThrough]
        //[OperationContract]
        //Task<bool> CheckPasswordAsync(UserDto user, string password);

        //[DebuggerStepThrough]
        //[OperationContract]
        //Task<bool> HasPasswordAsync(TUserIdType userId);

        //[DebuggerStepThrough]
        //[OperationContract]
        //Task<IdentityResultDto> AddPasswordAsync(TUserIdType userId, string password);

        //[DebuggerStepThrough]
        //[OperationContract]
        //Task<IdentityResultDto> ChangePasswordAsync(TUserIdType userId, string currentPassword, string newPassword);

        //[DebuggerStepThrough]
        //[OperationContract]
        //Task<IdentityResultDto> RemovePasswordAsync(TUserIdType userId);

        //[OperationContract]
        //Task<string> GeneratePasswordResetTokenAsync(TUserIdType userId);

        //[DebuggerStepThrough]
        //[OperationContract]
        //Task<IdentityResultDto> ResetPasswordAsync(TUserIdType userId, string token, string newPassword);

        //[DebuggerStepThrough]
        //[OperationContract]
        //Task<string> GenerateChangePhoneNumberTokenAsync(TKey userId, string phoneNumber);

        //[DebuggerStepThrough]
        //[OperationContract]
        //Task<bool> VerifyChangePhoneNumberTokenAsync(TKey userId, string token, string phoneNumber);

        //[DebuggerStepThrough]
        //[OperationContract]
        //Task<bool> VerifyUserTokenAsync(TUserIdType userId, string purpose, string token);

        //[DebuggerStepThrough]
        //[OperationContract]
        //Task<string> GenerateUserTokenAsync(string purpose, TUserIdType userId);

        //[OperationContract]
        //void RegisterTwoFactorProvider(string twoFactorProvider, IUserTokenProvider<TUser, TUserIdType> provider);

        //[DebuggerStepThrough]
        //[OperationContract]
        //Task<IList<string>> GetValidTwoFactorProvidersAsync(TUserIdType userId);

        //[DebuggerStepThrough]
        //[OperationContract]
        //Task<bool> VerifyTwoFactorTokenAsync(TUserIdType userId, string twoFactorProvider, string token);

        //[DebuggerStepThrough]
        //[OperationContract]
        //Task<string> GenerateTwoFactorTokenAsync(TUserIdType userId, string twoFactorProvider);

        //[DebuggerStepThrough]
        //[OperationContract]
        //Task<IdentityResultDto> NotifyTwoFactorTokenAsync(TUserIdType userId, string twoFactorProvider, string token);

        //[DebuggerStepThrough]
        //[OperationContract]
        //Task<bool> GetTwoFactorEnabledAsync(TUserIdType userId);

        //[DebuggerStepThrough]
        //[OperationContract]
        //Task<IdentityResultDto> SetTwoFactorEnabledAsync(TUserIdType userId, bool enabled);

        //[DebuggerStepThrough]
        //[OperationContract]
        //Task SendEmailAsync(TUserIdType userId, string subject, string body);

        //[DebuggerStepThrough]
        //[OperationContract]
        //Task SendSmsAsync(TUserIdType userId, string message);

    }

    //public interface ISignInManager<TUserIdType> : IDisposable
    //       where TUserIdType : IEquatable<TUserIdType>
    //{
    //    Task<ClaimsIdentity> CreateUserIdentityAsync(UserDto user);
    //    //Task<SignInStatusDto> ExternalSignInAsync(ExternalLoginInfoDto loginInfo, bool isPersistent);
    //    //Task<TUserIdType> GetVerifiedUserIdAsync();
    //    //Task<bool> HasBeenVerifiedAsync();
    //    Task<SignInStatusDto> PasswordSignInAsync(string userName, string password, bool isPersistent, bool shouldLockout);
    //    //Task<bool> SendTwoFactorCodeAsync(string provider);
    //    Task SignInAsync(UserDto user, bool isPersistent, bool rememberBrowser);
    //    //Task<SignInStatusDto> TwoFactorSignInAsync(string provider, string code, bool isPersistent, bool rememberBrowser);
    //    //IAuthenticationManagerService AuthenticationManager { get; set; }
    //    //string AuthenticationType { get; set; }
    //    //IUserManagerService<TUserIdType> UserManager { get; set; }
    //}

    //public interface ISignInService<TUserIdType> : IDisposable
    //       where TUserIdType : IEquatable<TUserIdType>
    //{
    //    TUserIdType ConvertIdFromString(string id);
    //    string ConvertIdToString(TUserIdType id);
    //    Task<ClaimsIdentity> CreateUserIdentityAsync(UserDto user);
    //    //Task<SignInStatusDto> ExternalSignInAsync(ExternalLoginInfoDto loginInfo, bool isPersistent);
    //    //Task<TUserIdType> GetVerifiedUserIdAsync();
    //    //Task<bool> HasBeenVerifiedAsync();
    //    Task<SignInStatusDto> PasswordSignInAsync(string userName, string password, bool isPersistent, bool shouldLockout);
    //    //Task<bool> SendTwoFactorCodeAsync(string provider);
    //    Task SignInAsync(UserDto user, bool isPersistent, bool rememberBrowser);
    //    //Task<SignInStatusDto> TwoFactorSignInAsync(string provider, string code, bool isPersistent, bool rememberBrowser);
    //    //IAuthenticationManagerService AuthenticationManager { get; set; }
    //    //string AuthenticationType { get; set; }
    //    //IUserManagerService<TUserIdType> UserManager { get; set; }
    //}


    
}
