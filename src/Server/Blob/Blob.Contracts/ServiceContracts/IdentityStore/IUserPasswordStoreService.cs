using System;
using System.ServiceModel;
using System.Threading.Tasks;

namespace Blob.Contracts.ServiceContracts.Identity
{
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
}
