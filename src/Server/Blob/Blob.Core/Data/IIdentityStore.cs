using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Blob.Core.Domain;

namespace Blob.Core.Data
{
    public interface IIdentityStore : IIdentityStore<string> { }

    public interface IIdentityStore<in TUserIdType> :
        IUserClaimStoreService<TUserIdType>,
        IUserEmailStoreService<TUserIdType>,
        IUserLockoutStoreService<TUserIdType>,
        //IUserLoginStoreService<TUserIdType>,
        IUserPasswordStoreService<TUserIdType>,
        IUserPhoneNumberStoreService<TUserIdType>,
        IUserRoleStoreService<TUserIdType>,
        IUserSecurityStampStoreService<TUserIdType>,
        IUserStoreService<TUserIdType>,
        IUserTokenProviderService<TUserIdType>,
        IUserTwoFactorStoreService<TUserIdType>
        where TUserIdType : IEquatable<TUserIdType>
    {
        bool SupportsUserTwoFactor { get; }
        bool SupportsUserPassword { get; }
        bool SupportsUserSecurityStamp { get; }
        bool SupportsUserRole { get; }
        bool SupportsUserLogin { get; }
        bool SupportsUserEmail { get; }
        bool SupportsUserPhoneNumber { get; }
        bool SupportsUserClaim { get; }
        bool SupportsUserLockout { get; }
        bool SupportsQueryableUsers { get; }
    }

    public interface IUserClaimStoreService<in TUserIdType> : IDisposable
        where TUserIdType : IEquatable<TUserIdType>
    {
        Task<IList<Claim>> GetClaimsAsync(TUserIdType userId);

        Task AddClaimAsync(TUserIdType userId, Claim claim);

        Task RemoveClaimAsync(TUserIdType userId, Claim claim);
    }

    public interface IUserEmailStoreService<in TUserIdType> : IDisposable
        where TUserIdType : IEquatable<TUserIdType>
    {
        Task SetEmailAsync(TUserIdType userId, string email);

        Task<string> GetEmailAsync(TUserIdType userId);

        Task<bool> GetEmailConfirmedAsync(TUserIdType userId);

        Task SetEmailConfirmedAsync(TUserIdType userId, bool confirmed);

        Task<User> FindByEmailAsync(string email);
    }

    public interface IUserLockoutStoreService<in TUserIdType> : IDisposable
        where TUserIdType : IEquatable<TUserIdType>
    {
        Task<DateTimeOffset> GetLockoutEndDateAsync(TUserIdType userId);

        Task SetLockoutEndDateAsync(TUserIdType userId, DateTimeOffset lockoutEnd);

        Task<int> IncrementAccessFailedCountAsync(TUserIdType userId);

        Task ResetAccessFailedCountAsync(TUserIdType userId);

        Task<int> GetAccessFailedCountAsync(TUserIdType userId);

        Task<bool> GetLockoutEnabledAsync(TUserIdType userId);

        Task SetLockoutEnabledAsync(TUserIdType userId, bool enabled);
    }

    //public interface IUserLoginStoreService<in TUserIdType> : IDisposable
    //    where TUserIdType : IEquatable<TUserIdType>
    //{
    //    Task AddLoginAsync(TUserIdType userId, UserLoginInfoDto login);

    //    Task RemoveLoginAsync(TUserIdType userId, UserLoginInfoDto login);
    //    Task<IList<UserLoginInfoDto>> GetLoginsAsync(TUserIdType userId);

    //    Task<User> FindAsync(UserLoginInfoDto login);
    //}

    public interface IUserPasswordStoreService<in TUserIdType> : IDisposable
        where TUserIdType : IEquatable<TUserIdType>
    {
        Task SetPasswordHashAsync(TUserIdType userId, string passwordHash);

        Task<string> GetPasswordHashAsync(TUserIdType userId);

        Task<bool> HasPasswordAsync(TUserIdType userId);
    }

    public interface IUserPhoneNumberStoreService<in TUserIdType> : IDisposable
        where TUserIdType : IEquatable<TUserIdType>
    {
        Task<string> GetPhoneNumberAsync(TUserIdType userId);

        Task SetPhoneNumberAsync(TUserIdType userId, string phoneNumber);

        Task SetPhoneNumberConfirmedAsync(TUserIdType userId, bool confirmed);

        Task<bool> GetPhoneNumberConfirmedAsync(TUserIdType userId);
    }

    public interface IUserRoleStoreService<in TUserIdType> : IDisposable
        where TUserIdType : IEquatable<TUserIdType>
    {
        Task AddToRoleAsync(TUserIdType userId, string roleName);

        Task RemoveFromRoleAsync(TUserIdType userId, string roleName);

        Task<IList<string>> GetRolesAsync(TUserIdType userId);

        Task<bool> IsInRoleAsync(TUserIdType userId, string roleName);
    }

    public interface IUserSecurityStampStoreService<in TUserIdType> : IDisposable
        where TUserIdType : IEquatable<TUserIdType>
    {
        Task SetSecurityStampAsync(TUserIdType userId, string stamp);
        Task<string> GetSecurityStampAsync(TUserIdType userId);
    }

    public interface IUserStoreService<in TUserIdType> : IDisposable
        where TUserIdType : IEquatable<TUserIdType>
    {
        Task CreateAsync(User user);

        Task UpdateAsync(User user);

        Task DeleteAsync(TUserIdType userId);

        Task<User> FindByIdAsync(TUserIdType userId);

        Task<User> FindByNameAsync(string userName);
    }

    public interface IUserTokenProviderService<in TUserIdType> : IDisposable
        where TUserIdType : IEquatable<TUserIdType>
    {
        Task<string> GenerateAsync(string purpose, TUserIdType userId);

        Task<bool> ValidateAsync(string purpose, string token, TUserIdType userId);

        Task NotifyAsync(string token, TUserIdType userId);

        Task<bool> IsValidProviderForUserAsync(TUserIdType userId);
    }

    public interface IUserTwoFactorStoreService<in TUserIdType> : IDisposable
        where TUserIdType : IEquatable<TUserIdType>
    {
        Task SetTwoFactorEnabledAsync(TUserIdType userId, bool enabled);

        Task<bool> GetTwoFactorEnabledAsync(TUserIdType userId);
    }
}
