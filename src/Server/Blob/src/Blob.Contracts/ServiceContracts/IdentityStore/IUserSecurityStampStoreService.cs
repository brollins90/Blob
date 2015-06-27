using System;
using System.ServiceModel;
using System.Threading.Tasks;

namespace Blob.Contracts.ServiceContracts.Identity
{
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
