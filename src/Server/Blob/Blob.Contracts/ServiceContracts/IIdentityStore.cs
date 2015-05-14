using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.ServiceModel;
using System.Threading.Tasks;
using Blob.Contracts.Models;

namespace Blob.Contracts.ServiceContracts
{
    [ServiceContract]
    public interface IIdentityStore : IIdentityStore<string> { }

    [ServiceContract]
    public interface IIdentityStore<in TUserIdType> :
        IUserClaimStoreService<TUserIdType>,
        IUserEmailStoreService<TUserIdType>,
        IUserLockoutStoreService<TUserIdType>,
        IUserLoginStoreService<TUserIdType>,
        IUserPasswordStoreService<TUserIdType>,
        //IUserPhoneNumberStoreService<TUserIdType>,
        IUserRoleStoreService<TUserIdType>,
        IUserSecurityStampStoreService<TUserIdType>,
        IUserStoreService<TUserIdType>
        //IUserTokenProviderService<TUserIdType>,
        //IUserTwoFactorStoreService<TUserIdType>
        where TUserIdType : IEquatable<TUserIdType>
    {
        bool SupportsUserTwoFactor { [OperationContract]get; }
        bool SupportsUserPassword { [OperationContract]get; }
        bool SupportsUserSecurityStamp { [OperationContract]get; }
        bool SupportsUserRole { [OperationContract]get; }
        bool SupportsUserLogin { [OperationContract] get; }
        bool SupportsUserEmail { [OperationContract] get; }
        bool SupportsUserPhoneNumber { [OperationContract]get; }
        bool SupportsUserClaim { [OperationContract]get; }
        bool SupportsUserLockout { [OperationContract]get; }
        bool SupportsQueryableUsers { [OperationContract] get; }
    }

    [ServiceContract]
    public interface IUserClaimStoreService<in TUserIdType> : IDisposable
        where TUserIdType : IEquatable<TUserIdType>
    {
        [OperationContract]
        Task AddClaimAsync(TUserIdType userId, Claim claim);

        [OperationContract]
        Task<IList<Claim>> GetClaimsAsync(TUserIdType userId);

        [OperationContract]
        Task RemoveClaimAsync(TUserIdType userId, Claim claim);
    }

    [ServiceContract]
    public interface IUserEmailStoreService<in TUserIdType> : IDisposable
        where TUserIdType : IEquatable<TUserIdType>
    {
        [OperationContract]
        Task<UserDto> FindByEmailAsync(string email);

        [OperationContract]
        Task<string> GetEmailAsync(TUserIdType userId);

        [OperationContract]
        Task<bool> GetEmailConfirmedAsync(TUserIdType userId);

        [OperationContract]
        Task SetEmailAsync(TUserIdType userId, string email);

        [OperationContract]
        Task SetEmailConfirmedAsync(TUserIdType userId, bool confirmed);
    }

    [ServiceContract]
    public interface IUserLockoutStoreService<in TUserIdType> : IDisposable
        where TUserIdType : IEquatable<TUserIdType>
    {
        [OperationContract]
        Task<int> GetAccessFailedCountAsync(TUserIdType userId);

        [OperationContract]
        Task<bool> GetLockoutEnabledAsync(TUserIdType userId);

        [OperationContract]
        Task<DateTimeOffset> GetLockoutEndDateAsync(TUserIdType userId);

        [OperationContract]
        Task<int> IncrementAccessFailedCountAsync(TUserIdType userId);

        [OperationContract]
        Task ResetAccessFailedCountAsync(TUserIdType userId);

        [OperationContract]
        Task SetLockoutEnabledAsync(TUserIdType userId, bool enabled);

        [OperationContract]
        Task SetLockoutEndDateAsync(TUserIdType userId, DateTimeOffset lockoutEnd);
    }

    [ServiceContract]
    public interface IUserLoginStoreService<in TUserIdType> : IDisposable
        where TUserIdType : IEquatable<TUserIdType>
    {
        [OperationContract]
        Task AddLoginAsync(TUserIdType userId, UserLoginInfoDto login);

        [OperationContract]
        Task<UserDto> FindAsync(UserLoginInfoDto login);

        [OperationContract]
        Task<IList<UserLoginInfoDto>> GetLoginsAsync(TUserIdType userId);

        [OperationContract]
        Task RemoveLoginAsync(TUserIdType userId, UserLoginInfoDto login);
    }

    [ServiceContract]
    public interface IUserPasswordStoreService<in TUserIdType> : IDisposable
        where TUserIdType : IEquatable<TUserIdType>
    {
        [OperationContract]
        Task<string> GetPasswordHashAsync(TUserIdType userId);

        [OperationContract]
        Task<bool> HasPasswordAsync(TUserIdType userId);

        [OperationContract]
        Task SetPasswordHashAsync(TUserIdType userId, string passwordHash);
    }

    //[ServiceContract]
    //public interface IUserPhoneNumberStoreService<in TUserIdType> : IDisposable
    //    where TUserIdType : IEquatable<TUserIdType>
    //{
    // [OperationContract]
    //    Task<string> GetPhoneNumberAsync(TUserIdType userId);

    // [OperationContract]
    //    Task<bool> GetPhoneNumberConfirmedAsync(TUserIdType userId);

    // [OperationContract]
    //    Task SetPhoneNumberAsync(TUserIdType userId, string phoneNumber);

    // [OperationContract]
    //    Task SetPhoneNumberConfirmedAsync(TUserIdType userId, bool confirmed);
    //}

    [ServiceContract]
    public interface IUserRoleStoreService<in TUserIdType> : IDisposable
        where TUserIdType : IEquatable<TUserIdType>
    {
        [OperationContract]
        Task AddToRoleAsync(TUserIdType userId, string roleName);

        [OperationContract]
        Task<IList<string>> GetRolesAsync(TUserIdType userId);

        [OperationContract]
        Task<bool> IsInRoleAsync(TUserIdType userId, string roleName);

        [OperationContract]
        Task RemoveFromRoleAsync(TUserIdType userId, string roleName);
    }

    [ServiceContract]
    public interface IUserSecurityStampStoreService<in TUserIdType> : IDisposable
        where TUserIdType : IEquatable<TUserIdType>
    {
        [OperationContract]
        Task<string> GetSecurityStampAsync(TUserIdType userId);

        [OperationContract]
        Task SetSecurityStampAsync(TUserIdType userId, string stamp);
    }

    [ServiceContract]
    public interface IUserStoreService<in TUserIdType> : IDisposable
        where TUserIdType : IEquatable<TUserIdType>
    {
        [OperationContract]
        Task CreateAsync(UserDto user);

        [OperationContract]
        Task DeleteAsync(TUserIdType userId);

        [OperationContract]
        Task<UserDto> FindByIdAsync(TUserIdType userId);

        [OperationContract]
        Task<UserDto> FindByNameAsync(string userName);

        [OperationContract]
        Task UpdateAsync(UserDto user);
    }

    //[ServiceContract]
    //public interface IUserTokenProviderService<in TUserIdType> : IDisposable
    //    where TUserIdType : IEquatable<TUserIdType>
    //{
    // [OperationContract]
    //    Task<string> GenerateAsync(string purpose, TUserIdType userId);

    // [OperationContract]
    //    Task<bool> IsValidProviderForUserAsync(TUserIdType userId);

    // [OperationContract]
    //    Task NotifyAsync(string token, TUserIdType userId);

    // [OperationContract]
    //    Task<bool> ValidateAsync(string purpose, string token, TUserIdType userId);
    //}
    
//    [ServiceContract]
    //public interface IUserTwoFactorStoreService<in TUserIdType> : IDisposable
    //    where TUserIdType : IEquatable<TUserIdType>
    //{
    // [OperationContract]
    //    Task<bool> GetTwoFactorEnabledAsync(TUserIdType userId);

    // [OperationContract]
    //    Task SetTwoFactorEnabledAsync(TUserIdType userId, bool enabled);
    //}
}
