using System;
using System.ServiceModel;
using System.Threading.Tasks;
using Blob.Contracts.Models;

namespace Blob.Contracts.ServiceContracts.Identity
{
    [ServiceContract]
    public interface IUserStoreService<in TUserIdType> : IDisposable
        where TUserIdType : IEquatable<TUserIdType>
    {
        [OperationContract]
        Task CreateAsync(UserDto user);

        [OperationContract]
        Task DeleteAsync(TUserIdType userId);

        [OperationContract]
        Task<UserDto> FindByIdAsync(TUserIdType userId);

        [OperationContract]
        Task<UserDto> FindByNameAsync(string userName);

        [OperationContract]
        Task UpdateAsync(UserDto user);
    }
}
