using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Claims;
using System.ServiceModel;
using System.Threading.Tasks;

namespace Blob.Contracts.Security
{
    [ServiceContract]
    public interface IIdentityStoreService : IIdentityStoreService<string> { }

    [ServiceContract]
    public interface IIdentityStoreService<in TUserIdType> :
        IUserClaimStoreService<TUserIdType>,
        IUserEmailStoreService<TUserIdType>,
        IUserLockoutStoreService<TUserIdType>,
        IUserLoginStoreService<TUserIdType>,
        IUserPasswordStoreService<TUserIdType>,
        IUserPhoneNumberStoreService<TUserIdType>,
        IUserRoleStoreService<TUserIdType>,
        IUserSecurityStampStoreService<TUserIdType>,
        IUserStoreService<TUserIdType>,
        IUserTokenProviderService<TUserIdType>,
        IUserTwoFactorStoreService<TUserIdType>
        where TUserIdType : IEquatable<TUserIdType>
    {
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
        //IQueryable<TUser> Users { [OperationContract] get; }
        //IDictionary<string, IUserTokenProvider<TUser, TKey>> TwoFactorProviders { [OperationContract] get; }
    }

    public interface IUserClaimStoreService<in TUserIdType> : IDisposable
        where TUserIdType : IEquatable<TUserIdType>
    {
        [DebuggerStepThrough]
        [OperationContract]
        Task<IList<Claim>> GetClaimsAsync(TUserIdType userId);

        [DebuggerStepThrough]
        [OperationContract]
        Task<IdentityResultDto> AddClaimAsync(TUserIdType userId, Claim claim);

        [DebuggerStepThrough]
        [OperationContract]
        Task<IdentityResultDto> RemoveClaimAsync(TUserIdType userId, Claim claim);
    }

    public interface IUserEmailStoreService<in TUserIdType> : IDisposable
        where TUserIdType : IEquatable<TUserIdType>
    {

        [DebuggerStepThrough]
        [OperationContract]
        Task<IdentityResultDto> SetEmailAsync(TUserIdType userId, string email);

        [DebuggerStepThrough]
        [OperationContract]
        Task<string> GetEmailAsync(TUserIdType userId);

        [DebuggerStepThrough]
        [OperationContract]
        Task<bool> GetEmailConfirmedAsync(TUserIdType userId);

        [DebuggerStepThrough]
        [OperationContract]
        Task<IdentityResultDto> SetEmailConfirmedAsync(TUserIdType userId, bool confirmed);

        [DebuggerStepThrough]
        [OperationContract]
        Task<UserDto> FindByEmailAsync(string email);
    }

    public interface IUserLockoutStoreService<in TUserIdType> : IDisposable
        where TUserIdType : IEquatable<TUserIdType>
    {

        [DebuggerStepThrough]
        [OperationContract]
        Task<DateTimeOffset> GetLockoutEndDateAsync(TUserIdType userId);

        [DebuggerStepThrough]
        [OperationContract]
        Task<IdentityResultDto> SetLockoutEndDateAsync(TUserIdType userId, DateTimeOffset lockoutEnd);

        [DebuggerStepThrough]
        [OperationContract]
        Task<int> IncrementAccessFailedCountAsync(TUserIdType userId);

        [DebuggerStepThrough]
        [OperationContract]
        Task<IdentityResultDto> ResetAccessFailedCountAsync(TUserIdType userId);

        [DebuggerStepThrough]
        [OperationContract]
        Task<int> GetAccessFailedCountAsync(TUserIdType userId);

        [DebuggerStepThrough]
        [OperationContract]
        Task<bool> GetLockoutEnabledAsync(TUserIdType userId);

        [DebuggerStepThrough]
        [OperationContract]
        Task<IdentityResultDto> SetLockoutEnabledAsync(TUserIdType userId, bool enabled);
    }

    public interface IUserLoginStoreService<in TUserIdType> : IDisposable
        where TUserIdType : IEquatable<TUserIdType>
    {
        [DebuggerStepThrough]
        [OperationContract(Name = "FindLoginAsync")]
        Task<UserDto> FindAsync(UserLoginInfoDto login);

        [DebuggerStepThrough]
        [OperationContract]
        Task<IdentityResultDto> RemoveLoginAsync(TUserIdType userId, UserLoginInfoDto login);

        [DebuggerStepThrough]
        [OperationContract]
        Task<IdentityResultDto> AddLoginAsync(TUserIdType userId, UserLoginInfoDto login);

        [DebuggerStepThrough]
        [OperationContract]
        Task<IList<UserLoginInfoDto>> GetLoginsAsync(TUserIdType userId);
    }

    public interface IUserPasswordStoreService<in TUserIdType> : IDisposable
        where TUserIdType : IEquatable<TUserIdType>
    {
        [DebuggerStepThrough]
        [OperationContract]
        Task<IdentityResultDto> SetPasswordHashAsync(TUserIdType userId, string passwordHash);

        [DebuggerStepThrough]
        [OperationContract]
        Task<string> GetPasswordHashAsync(TUserIdType userId);

        [DebuggerStepThrough]
        [OperationContract]
        Task<bool> HasPasswordAsync(TUserIdType userId);
    }

    public interface IUserPhoneNumberStoreService<in TUserIdType> : IDisposable
        where TUserIdType : IEquatable<TUserIdType>
    {

        [DebuggerStepThrough]
        [OperationContract]
        Task<string> GetPhoneNumberAsync(TUserIdType userId);

        [DebuggerStepThrough]
        [OperationContract]
        Task<IdentityResultDto> SetPhoneNumberAsync(TUserIdType userId, string phoneNumber);

        [DebuggerStepThrough]
        [OperationContract]
        Task<IdentityResultDto> SetPhoneNumberConfirmedAsync(TUserIdType userId, bool confirmed);

        [DebuggerStepThrough]
        [OperationContract]
        Task<bool> GetPhoneNumberConfirmedAsync(TUserIdType userId);
    }

    public interface IUserRoleStoreService<in TUserIdType> : IDisposable
        where TUserIdType : IEquatable<TUserIdType>
    {
        [DebuggerStepThrough]
        [OperationContract]
        Task<IdentityResultDto> AddToRoleAsync(TUserIdType userId, string roleName);

        [DebuggerStepThrough]
        [OperationContract]
        Task<IdentityResultDto> RemoveFromRoleAsync(TUserIdType userId, string roleName);

        [DebuggerStepThrough]
        [OperationContract]
        Task<IList<string>> GetRolesAsync(TUserIdType userId);

        [DebuggerStepThrough]
        [OperationContract]
        Task<bool> IsInRoleAsync(TUserIdType userId, string roleName);
    }

    public interface IUserSecurityStampStoreService<in TUserIdType> : IDisposable
        where TUserIdType : IEquatable<TUserIdType>
    {
        [DebuggerStepThrough]
        [OperationContract]
        Task<IdentityResultDto> SetSecurityStampAsync(TUserIdType userId, string stamp);

        [DebuggerStepThrough]
        [OperationContract]
        Task<string> GetSecurityStampAsync(TUserIdType userId);
    }

    public interface IUserStoreService<in TUserIdType> : IDisposable
        where TUserIdType : IEquatable<TUserIdType>
    {
        [DebuggerStepThrough]
        [OperationContract(Name = "CreateUserAsync")]
        Task<IdentityResultDto> CreateAsync(UserDto user);

        [DebuggerStepThrough]
        [OperationContract]
        Task<IdentityResultDto> UpdateAsync(UserDto user);

        [DebuggerStepThrough]
        [OperationContract]
        Task<IdentityResultDto> DeleteAsync(TUserIdType userId);

        [DebuggerStepThrough]
        [OperationContract]
        Task<UserDto> FindByIdAsync(TUserIdType userId);

        [DebuggerStepThrough]
        [OperationContract]
        Task<UserDto> FindByNameAsync(string userName);
    }

    public interface IUserTokenProviderService<in TUserIdType> :
        IDisposable
        where TUserIdType : IEquatable<TUserIdType>
    {
        [DebuggerStepThrough]
        [OperationContract]
        Task<string> GenerateAsync(string purpose, TUserIdType userId);

        [DebuggerStepThrough]
        [OperationContract]
        Task<bool> ValidateAsync(string purpose, string token, TUserIdType userId);

        [DebuggerStepThrough]
        [OperationContract]
        Task NotifyAsync(string token, TUserIdType userId);

        [DebuggerStepThrough]
        [OperationContract]
        Task<bool> IsValidProviderForUserAsync(TUserIdType userId);
    }

    public interface IUserTwoFactorStoreService<in TUserIdType> : IDisposable
        where TUserIdType : IEquatable<TUserIdType>
    {
        [DebuggerStepThrough]
        [OperationContract]
        Task<IdentityResultDto> SetTwoFactorEnabledAsync(TUserIdType userId, bool enabled);

        [DebuggerStepThrough]
        [OperationContract]
        Task<bool> GetTwoFactorEnabledAsync(TUserIdType userId);
    }

}
