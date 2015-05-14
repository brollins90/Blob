using System;
using System.Security.Claims;
using System.ServiceModel;
using System.Threading.Tasks;
using Blob.Contracts.Models;

namespace Blob.Contracts.ServiceContracts
{
    [ServiceContract]
    public interface IUserManagerService : IUserManagerService<string> { }

    [ServiceContract]
    public interface IUserManagerService<in TUserIdType> :
        IIdentityStore<TUserIdType>
        //IAuthenticationManagerService
        where TUserIdType : IEquatable<TUserIdType>
    {
        bool UserLockoutEnabledByDefault { [OperationContract] get; }
        int MaxFailedAccessAttemptsBeforeLockout { [OperationContract] get; }
        TimeSpan DefaultAccountLockoutTimeSpan { [OperationContract] get; }

        [OperationContract]
        Task<ClaimsIdentity> CreateIdentityAsync(UserDto user, string authenticationType);

        [OperationContract(Name = "CreateUserAsyncWithPassword")]
        Task<IdentityResultDto> CreateAsync(UserDto user, string password);

        [OperationContract]
        Task<IdentityResultDto> ChangePasswordAsync(Guid userId, string currentPassword, string newPassword);
    }

}
