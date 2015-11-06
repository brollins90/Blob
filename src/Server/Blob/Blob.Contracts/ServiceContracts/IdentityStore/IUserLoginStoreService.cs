using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;
using Blob.Contracts.Models;

namespace Blob.Contracts.ServiceContracts.Identity
{
    [ServiceContract]
    public interface IUserLoginStoreService<in TUserIdType> : IDisposable
        where TUserIdType : IEquatable<TUserIdType>
    {
        [OperationContract]
        Task AddLoginAsync(TUserIdType userId, UserLoginInfoDto login);

        [OperationContract]
        Task<UserDto> FindAsync(UserLoginInfoDto login);

        [OperationContract]
        Task<IList<UserLoginInfoDto>> GetLoginsAsync(TUserIdType userId);

        [OperationContract]
        Task RemoveLoginAsync(TUserIdType userId, UserLoginInfoDto login);
    }
}
