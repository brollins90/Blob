namespace Blob.Contracts.ServiceContracts.Identity
{
    using System;
    using System.ServiceModel;
    using System.Threading.Tasks;

    [ServiceContract]
    public interface IUserPhoneNumberStoreService<in TUserIdType> : IDisposable
        where TUserIdType : IEquatable<TUserIdType>
    {
        [OperationContract]
        Task<string> GetPhoneNumberAsync(TUserIdType userId);

        [OperationContract]
        Task<bool> GetPhoneNumberConfirmedAsync(TUserIdType userId);

        [OperationContract]
        Task SetPhoneNumberAsync(TUserIdType userId, string phoneNumber);

        [OperationContract]
        Task SetPhoneNumberConfirmedAsync(TUserIdType userId, bool confirmed);
    }
}