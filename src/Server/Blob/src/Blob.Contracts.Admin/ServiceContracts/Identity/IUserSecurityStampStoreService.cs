namespace Blob.Contracts.ServiceContracts.Identity
{
    using System;
    using System.ServiceModel;
    using System.Threading.Tasks;

    [ServiceContract]
    public interface IUserSecurityStampStoreService<in TUserIdType> : IDisposable
        where TUserIdType : IEquatable<TUserIdType>
    {
        [OperationContract]
        Task<string> GetSecurityStampAsync(TUserIdType userId);

        [OperationContract]
        Task SetSecurityStampAsync(TUserIdType userId, string stamp);
    }
}