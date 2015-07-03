namespace Blob.Contracts.ServiceContracts.Identity
{
    using System;
    using System.ServiceModel;
    using System.Threading.Tasks;
    using Models;

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
}