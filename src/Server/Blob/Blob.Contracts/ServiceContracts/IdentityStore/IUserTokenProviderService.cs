using System;
using System.ServiceModel;
using System.Threading.Tasks;

namespace Blob.Contracts.ServiceContracts.Identity
{
    [ServiceContract]
    public interface IUserTokenProviderService<in TUserIdType> : IDisposable
        where TUserIdType : IEquatable<TUserIdType>
    {
        [OperationContract]
        Task<string> GenerateAsync(string purpose, TUserIdType userId);

        [OperationContract]
        Task<bool> IsValidProviderForUserAsync(TUserIdType userId);

        [OperationContract]
        Task NotifyAsync(string token, TUserIdType userId);

        [OperationContract]
        Task<bool> ValidateAsync(string purpose, string token, TUserIdType userId);
    }
}
