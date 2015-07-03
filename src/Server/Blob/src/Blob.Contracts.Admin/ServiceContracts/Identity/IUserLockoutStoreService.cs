namespace Blob.Contracts.ServiceContracts.Identity
{
    using System;
    using System.ServiceModel;
    using System.Threading.Tasks;

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
}