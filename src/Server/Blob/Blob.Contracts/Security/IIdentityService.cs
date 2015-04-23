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
        where TUserIdType : IEquatable<TUserIdType>
    {
        [OperationContract]
        void SetProvider(string providerName);

        bool UserLockoutEnabledByDefault { [OperationContract] get; }
        int MaxFailedAccessAttemptsBeforeLockout { [OperationContract] get; }
        TimeSpan DefaultAccountLockoutTimeSpan { [OperationContract] get; }

        [OperationContract]
        Task<ClaimsIdentity> CreateIdentityAsync(UserDto user, string authenticationType);

        [OperationContract(Name = "CreateUserAsyncWithPassword")]
        Task<IdentityResultDto> CreateAsync(UserDto user, string password);
    }
}
