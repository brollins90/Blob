using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Claims;
using System.ServiceModel;
using System.Threading.Tasks;

namespace Blob.Contracts.Security
{
    [ServiceContract]
    public interface IUserManagerService : IUserManagerService<IUser, string> { }

    [ServiceContract]
    public interface IUserManagerService<TUser, TKey> : ISignInManagerService<TUser, TKey>, IAuthenticationManagerService
        where TUser : class, IUser<TKey>
        where TKey : IEquatable<TKey>
    {
        [OperationContract]
        void SetProvider(string providerName);

        IPasswordHasher PasswordHasher { [OperationContract] get; }
        IIdentityValidator<TUser> UserValidator { [OperationContract] get; }
        IIdentityValidator<string> PasswordValidator { [OperationContract] get; }
        IClaimsIdentityFactory<TUser, TKey> ClaimsIdentityFactory { [OperationContract] get; }
        IIdentityMessageService EmailService { [OperationContract] get; }
        IIdentityMessageService SmsService { [OperationContract] get; }
        IUserTokenProvider<TUser, TKey> UserTokenProvider { [OperationContract] get; }
        bool UserLockoutEnabledByDefault { [OperationContract] get; }
        int MaxFailedAccessAttemptsBeforeLockout { [OperationContract] get; }
        TimeSpan DefaultAccountLockoutTimeSpan { [OperationContract] get; }
        bool SupportsUserTwoFactor { [OperationContract] get; }
        bool SupportsUserPassword { [OperationContract] get; }
        bool SupportsUserSecurityStamp { [OperationContract] get; }
        bool SupportsUserRole { [OperationContract] get; }
        bool SupportsUserLogin { [OperationContract] get; }
        bool SupportsUserEmail { [OperationContract] get; }
        bool SupportsUserPhoneNumber { [OperationContract] get; }
        bool SupportsUserClaim { [OperationContract] get; }
        bool SupportsUserLockout { [OperationContract] get; }
        bool SupportsQueryableUsers { [OperationContract] get; }
        IQueryable<TUser> Users { [OperationContract] get; }
        IDictionary<string, IUserTokenProvider<TUser, TKey>> TwoFactorProviders { [OperationContract] get; }

        [DebuggerStepThrough]
        [OperationContract]
        Task<ClaimsIdentity> CreateIdentityAsync(TUser user, string authenticationType);

        [DebuggerStepThrough]
        [OperationContract(Name = "CreateUserAsync")]
        Task<IdentityResultDto> CreateAsync(TUser user);

        [DebuggerStepThrough]
        [OperationContract]
        Task<IdentityResultDto> UpdateAsync(TUser user);

        [DebuggerStepThrough]
        [OperationContract]
        Task<IdentityResultDto> DeleteAsync(TUser user);

        [DebuggerStepThrough]
        [OperationContract]
        Task<TUser> FindByIdAsync(TKey userId);

        [DebuggerStepThrough]
        [OperationContract]
        Task<TUser> FindByNameAsync(string userName);

        [DebuggerStepThrough]
        [OperationContract(Name = "CreateUserAsyncWithPassword")]
        Task<IdentityResultDto> CreateAsync(TUser user, string password);

        [DebuggerStepThrough]
        [OperationContract(Name = "FindUserAsync")]
        Task<TUser> FindAsync(string userName, string password);

        [DebuggerStepThrough]
        [OperationContract]
        Task<bool> CheckPasswordAsync(TUser user, string password);

        [DebuggerStepThrough]
        [OperationContract]
        Task<bool> HasPasswordAsync(TKey userId);

        [DebuggerStepThrough]
        [OperationContract]
        Task<IdentityResultDto> AddPasswordAsync(TKey userId, string password);

        [DebuggerStepThrough]
        [OperationContract]
        Task<IdentityResultDto> ChangePasswordAsync(TKey userId, string currentPassword, string newPassword);

        [DebuggerStepThrough]
        [OperationContract]
        Task<IdentityResultDto> RemovePasswordAsync(TKey userId);

        [DebuggerStepThrough]
        [OperationContract]
        Task<string> GetSecurityStampAsync(TKey userId);

        [DebuggerStepThrough]
        [OperationContract]
        Task<IdentityResultDto> UpdateSecurityStampAsync(TKey userId);

        [OperationContract]
        Task<string> GeneratePasswordResetTokenAsync(TKey userId);

        [DebuggerStepThrough]
        [OperationContract]
        Task<IdentityResultDto> ResetPasswordAsync(TKey userId, string token, string newPassword);

        [OperationContract(Name = "FindLoginAsync")]
        Task<TUser> FindAsync(UserLoginInfoDto login);

        [DebuggerStepThrough]
        [OperationContract]
        Task<IdentityResultDto> RemoveLoginAsync(TKey userId, UserLoginInfoDto login);

        [DebuggerStepThrough]
        [OperationContract]
        Task<IdentityResultDto> AddLoginAsync(TKey userId, UserLoginInfoDto login);

        [DebuggerStepThrough]
        [OperationContract]
        Task<IList<UserLoginInfoDto>> GetLoginsAsync(TKey userId);

        [DebuggerStepThrough]
        [OperationContract]
        Task<IdentityResultDto> AddClaimAsync(TKey userId, Claim claim);

        [DebuggerStepThrough]
        [OperationContract]
        Task<IdentityResultDto> RemoveClaimAsync(TKey userId, Claim claim);

        [DebuggerStepThrough]
        [OperationContract]
        Task<IList<Claim>> GetClaimsAsync(TKey userId);

        [DebuggerStepThrough]
        [OperationContract]
        Task<IdentityResultDto> AddToRoleAsync(TKey userId, string role);

        [DebuggerStepThrough]
        [OperationContract]
        Task<IdentityResultDto> AddToRolesAsync(TKey userId, string[] roles);

        [DebuggerStepThrough]
        [OperationContract]
        Task<IdentityResultDto> RemoveFromRoleAsync(TKey userId, string role);

        [DebuggerStepThrough]
        [OperationContract]
        Task<IdentityResultDto> RemoveFromRolesAsync(TKey userId, string[] roles);

        [DebuggerStepThrough]
        [OperationContract]
        Task<IList<string>> GetRolesAsync(TKey userId);

        [DebuggerStepThrough]
        [OperationContract]
        Task<bool> IsInRoleAsync(TKey userId, string role);

        [DebuggerStepThrough]
        [OperationContract]
        Task<string> GetEmailAsync(TKey userId);

        [DebuggerStepThrough]
        [OperationContract]
        Task<IdentityResultDto> SetEmailAsync(TKey userId, string email);

        [OperationContract]
        Task<TUser> FindByEmailAsync(string email);

        [OperationContract]
        Task<string> GenerateEmailConfirmationTokenAsync(TKey userId);

        [DebuggerStepThrough]
        [OperationContract]
        Task<IdentityResultDto> ConfirmEmailAsync(TKey userId, string token);

        [DebuggerStepThrough]
        [OperationContract]
        Task<bool> IsEmailConfirmedAsync(TKey userId);

        //[DebuggerStepThrough]
        //[OperationContract]
        //Task<string> GetPhoneNumberAsync(TKey userId);

        //[DebuggerStepThrough]
        //[OperationContract]
        //Task<IdentityResultDto> SetPhoneNumberAsync(TKey userId, string phoneNumber);

        //[DebuggerStepThrough]
        //[OperationContract]
        //Task<IdentityResultDto> ChangePhoneNumberAsync(TKey userId, string phoneNumber, string token);

        //[DebuggerStepThrough]
        //[OperationContract]
        //Task<bool> IsPhoneNumberConfirmedAsync(TKey userId);

        //[DebuggerStepThrough]
        //[OperationContract]
        //Task<string> GenerateChangePhoneNumberTokenAsync(TKey userId, string phoneNumber);

        //[DebuggerStepThrough]
        //[OperationContract]
        //Task<bool> VerifyChangePhoneNumberTokenAsync(TKey userId, string token, string phoneNumber);

        [DebuggerStepThrough]
        [OperationContract]
        Task<bool> VerifyUserTokenAsync(TKey userId, string purpose, string token);

        [DebuggerStepThrough]
        [OperationContract]
        Task<string> GenerateUserTokenAsync(string purpose, TKey userId);

        [OperationContract]
        void RegisterTwoFactorProvider(string twoFactorProvider, IUserTokenProvider<TUser, TKey> provider);

        [DebuggerStepThrough]
        [OperationContract]
        Task<IList<string>> GetValidTwoFactorProvidersAsync(TKey userId);

        [DebuggerStepThrough]
        [OperationContract]
        Task<bool> VerifyTwoFactorTokenAsync(TKey userId, string twoFactorProvider, string token);

        [DebuggerStepThrough]
        [OperationContract]
        Task<string> GenerateTwoFactorTokenAsync(TKey userId, string twoFactorProvider);

        [DebuggerStepThrough]
        [OperationContract]
        Task<IdentityResultDto> NotifyTwoFactorTokenAsync(TKey userId, string twoFactorProvider, string token);

        [DebuggerStepThrough]
        [OperationContract]
        Task<bool> GetTwoFactorEnabledAsync(TKey userId);

        [DebuggerStepThrough]
        [OperationContract]
        Task<IdentityResultDto> SetTwoFactorEnabledAsync(TKey userId, bool enabled);

        [DebuggerStepThrough]
        [OperationContract]
        Task SendEmailAsync(TKey userId, string subject, string body);

        [DebuggerStepThrough]
        [OperationContract]
        Task SendSmsAsync(TKey userId, string message);

        [DebuggerStepThrough]
        [OperationContract]
        Task<bool> IsLockedOutAsync(TKey userId);

        [DebuggerStepThrough]
        [OperationContract]
        Task<IdentityResultDto> SetLockoutEnabledAsync(TKey userId, bool enabled);

        [DebuggerStepThrough]
        [OperationContract]
        Task<bool> GetLockoutEnabledAsync(TKey userId);

        [DebuggerStepThrough]
        [OperationContract]
        Task<DateTimeOffset> GetLockoutEndDateAsync(TKey userId);

        [DebuggerStepThrough]
        [OperationContract]
        Task<IdentityResultDto> SetLockoutEndDateAsync(TKey userId, DateTimeOffset lockoutEnd);

        [DebuggerStepThrough]
        [OperationContract]
        Task<IdentityResultDto> AccessFailedAsync(TKey userId);

        [DebuggerStepThrough]
        [OperationContract]
        Task<IdentityResultDto> ResetAccessFailedCountAsync(TKey userId);

        [DebuggerStepThrough]
        [OperationContract]
        Task<int> GetAccessFailedCountAsync(TKey userId);
    }

    [DataContract]
    public class IdentityResultDto// : IdentityResult
    {
        public IdentityResultDto(params string[] errors) : this((IEnumerable<string>)errors) { }
        public IdentityResultDto(IEnumerable<string> errors)
        {
            if (errors == null)
            {
                errors = new[] {"error"};
            }
            _succeeded = false;
            _errors = errors;
        }
        public IdentityResultDto(bool success)
        {
            _succeeded = success;
            _errors = new string[0];
        }
        public IdentityResultDto(IdentityResult res)
        {
            _succeeded = res.Succeeded;
            _errors = res.Errors.ToList();
        }
        public new bool Succeeded { get { return _succeeded; } }
        protected bool _succeeded;
        public new IEnumerable<string> Errors { get { return _errors; } }
        protected IEnumerable<string> _errors;
    }

    [DataContract]
    public class UserLoginInfoDto
    {

        public UserLoginInfoDto(string loginProvider, string providerKey)
        {
            LoginProvider = loginProvider;
            ProviderKey = providerKey;
        }

        // Summary:
        //     Provider for the linked login, i.e. Facebook, Google, etc.
        public string LoginProvider { get; set; }
        //
        // Summary:
        //     User specific key for the login provider
        public string ProviderKey { get; set; }
    }

    public static class IdentityUtil
    {
        public static IdentityResultDto ToDto(this IdentityResult res)
        {
            return new IdentityResultDto(res);
        }

        public static UserLoginInfo ToLoginInfo(this UserLoginInfoDto res)
        {
            return new UserLoginInfo(res.LoginProvider, res.ProviderKey);
        }
    }
}
