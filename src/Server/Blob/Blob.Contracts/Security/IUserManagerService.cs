using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.ServiceModel;
using System.Threading.Tasks;

namespace Blob.Contracts.Security
{
    public interface IUserManagerService<TUser, TKey>
        where TUser : class, IUser<TKey>
        where TKey : IEquatable<TKey>
    {
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
        [OperationContract]
        Task<IdentityResult> CreateAsync(TUser user);

        [DebuggerStepThrough]
        [OperationContract]
        Task<IdentityResult> UpdateAsync(TUser user);

        [DebuggerStepThrough]
        [OperationContract]
        Task<IdentityResult> DeleteAsync(TUser user);

        [DebuggerStepThrough]
        [OperationContract]
        Task<TUser> FindByIdAsync(TKey userId);

        [DebuggerStepThrough]
        [OperationContract]
        Task<TUser> FindByNameAsync(string userName);

        [DebuggerStepThrough]
        [OperationContract]
        Task<IdentityResult> CreateAsync(TUser user, string password);

        [DebuggerStepThrough]
        [OperationContract]
        Task<TUser> FindAsync(string userName, string password);

        [DebuggerStepThrough]
        [OperationContract]
        Task<bool> CheckPasswordAsync(TUser user, string password);

        [DebuggerStepThrough]
        [OperationContract]
        Task<bool> HasPasswordAsync(TKey userId);

        [DebuggerStepThrough]
        [OperationContract]
        Task<IdentityResult> AddPasswordAsync(TKey userId, string password);

        [DebuggerStepThrough]
        [OperationContract]
        Task<IdentityResult> ChangePasswordAsync(TKey userId, string currentPassword, string newPassword);

        [DebuggerStepThrough]
        [OperationContract]
        Task<IdentityResult> RemovePasswordAsync(TKey userId);

        [DebuggerStepThrough]
        [OperationContract]
        Task<string> GetSecurityStampAsync(TKey userId);

        [DebuggerStepThrough]
        [OperationContract]
        Task<IdentityResult> UpdateSecurityStampAsync(TKey userId);

        [OperationContract]
        Task<string> GeneratePasswordResetTokenAsync(TKey userId);

        [DebuggerStepThrough]
        [OperationContract]
        Task<IdentityResult> ResetPasswordAsync(TKey userId, string token, string newPassword);

        [OperationContract]
        Task<TUser> FindAsync(UserLoginInfo login);

        [DebuggerStepThrough]
        [OperationContract]
        Task<IdentityResult> RemoveLoginAsync(TKey userId, UserLoginInfo login);

        [DebuggerStepThrough]
        [OperationContract]
        Task<IdentityResult> AddLoginAsync(TKey userId, UserLoginInfo login);

        [DebuggerStepThrough]
        [OperationContract]
        Task<IList<UserLoginInfo>> GetLoginsAsync(TKey userId);

        [DebuggerStepThrough]
        [OperationContract]
        Task<IdentityResult> AddClaimAsync(TKey userId, Claim claim);

        [DebuggerStepThrough]
        [OperationContract]
        Task<IdentityResult> RemoveClaimAsync(TKey userId, Claim claim);

        [DebuggerStepThrough]
        [OperationContract]
        Task<IList<Claim>> GetClaimsAsync(TKey userId);

        [DebuggerStepThrough]
        [OperationContract]
        Task<IdentityResult> AddToRoleAsync(TKey userId, string role);

        [DebuggerStepThrough]
        [OperationContract]
        Task<IdentityResult> AddToRolesAsync(TKey userId, string[] roles);

        [DebuggerStepThrough]
        [OperationContract]
        Task<IdentityResult> RemoveFromRoleAsync(TKey userId, string role);

        [DebuggerStepThrough]
        [OperationContract]
        Task<IdentityResult> RemoveFromRolesAsync(TKey userId, string[] roles);

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
        Task<IdentityResult> SetEmailAsync(TKey userId, string email);

        [OperationContract]
        Task<TUser> FindByEmailAsync(string email);

        [OperationContract]
        Task<string> GenerateEmailConfirmationTokenAsync(TKey userId);

        [DebuggerStepThrough]
        [OperationContract]
        Task<IdentityResult> ConfirmEmailAsync(TKey userId, string token);

        [DebuggerStepThrough]
        [OperationContract]
        Task<bool> IsEmailConfirmedAsync(TKey userId);

        [DebuggerStepThrough]
        [OperationContract]
        Task<string> GetPhoneNumberAsync(TKey userId);

        [DebuggerStepThrough]
        [OperationContract]
        Task<IdentityResult> SetPhoneNumberAsync(TKey userId, string phoneNumber);

        [DebuggerStepThrough]
        [OperationContract]
        Task<IdentityResult> ChangePhoneNumberAsync(TKey userId, string phoneNumber, string token);

        [DebuggerStepThrough]
        [OperationContract]
        Task<bool> IsPhoneNumberConfirmedAsync(TKey userId);

        [DebuggerStepThrough]
        [OperationContract]
        Task<string> GenerateChangePhoneNumberTokenAsync(TKey userId, string phoneNumber);

        [DebuggerStepThrough]
        [OperationContract]
        Task<bool> VerifyChangePhoneNumberTokenAsync(TKey userId, string token, string phoneNumber);

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
        Task<IdentityResult> NotifyTwoFactorTokenAsync(TKey userId, string twoFactorProvider, string token);

        [DebuggerStepThrough]
        [OperationContract]
        Task<bool> GetTwoFactorEnabledAsync(TKey userId);

        [DebuggerStepThrough]
        [OperationContract]
        Task<IdentityResult> SetTwoFactorEnabledAsync(TKey userId, bool enabled);

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
        Task<IdentityResult> SetLockoutEnabledAsync(TKey userId, bool enabled);

        [DebuggerStepThrough]
        [OperationContract]
        Task<bool> GetLockoutEnabledAsync(TKey userId);

        [DebuggerStepThrough]
        [OperationContract]
        Task<DateTimeOffset> GetLockoutEndDateAsync(TKey userId);

        [DebuggerStepThrough]
        [OperationContract]
        Task<IdentityResult> SetLockoutEndDateAsync(TKey userId, DateTimeOffset lockoutEnd);

        [DebuggerStepThrough]
        [OperationContract]
        Task<IdentityResult> AccessFailedAsync(TKey userId);

        [DebuggerStepThrough]
        [OperationContract]
        Task<IdentityResult> ResetAccessFailedCountAsync(TKey userId);

        [DebuggerStepThrough]
        [OperationContract]
        Task<int> GetAccessFailedCountAsync(TKey userId);
    }

    //[DataContract]
    //public class IdentityResult
    //{
    //    public bool Success { get; set; }
    //    public IEnumerable<string> Errors
    //    {
    //        get { return _errors ?? (_errors = new List<string>()); }
    //        set { _errors = value; }
    //    }
    //    private IEnumerable<string> _errors;
    //}
}
