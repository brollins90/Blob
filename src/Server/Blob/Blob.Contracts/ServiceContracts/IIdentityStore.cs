using System;
using System.ServiceModel;
using Blob.Contracts.ServiceContracts.Identity;

namespace Blob.Contracts.ServiceContracts
{
    [ServiceContract]
    public interface IIdentityStore : IIdentityStore<string> { }

    [ServiceContract]
    public interface IIdentityStore<in TUserIdType> :
        //IUserClaimStoreService<TUserIdType>,
        IUserEmailStoreService<TUserIdType>,
        //IUserLockoutStoreService<TUserIdType>,
        //IUserLoginStoreService<TUserIdType>,
        IUserPasswordStoreService<TUserIdType>,
        //IUserPhoneNumberStoreService<TUserIdType>,
        IUserRoleStoreService<TUserIdType>,
        //IUserSecurityStampStoreService<TUserIdType>,
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
}
