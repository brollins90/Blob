namespace Blob.Contracts.ServiceContracts
{
    using System;
    using System.ServiceModel;
    using Identity;

    [ServiceContract]
    public interface IIdentityStore : IIdentityStore<string> { }

    [ServiceContract]
    public interface IIdentityStore<in TUserIdType> :
        IUserEmailStoreService<TUserIdType>,
        IUserPasswordStoreService<TUserIdType>,
        IUserRoleStoreService<TUserIdType>,
        IUserStoreService<TUserIdType>
        where TUserIdType : IEquatable<TUserIdType>
    {
        bool SupportsUserTwoFactor {[OperationContract]get; }
        bool SupportsUserPassword {[OperationContract]get; }
        bool SupportsUserSecurityStamp {[OperationContract]get; }
        bool SupportsUserRole {[OperationContract]get; }
        bool SupportsUserLogin {[OperationContract] get; }
        bool SupportsUserEmail {[OperationContract] get; }
        bool SupportsUserPhoneNumber {[OperationContract]get; }
        bool SupportsUserClaim {[OperationContract]get; }
        bool SupportsUserLockout {[OperationContract]get; }
        bool SupportsQueryableUsers {[OperationContract] get; }
    }
}