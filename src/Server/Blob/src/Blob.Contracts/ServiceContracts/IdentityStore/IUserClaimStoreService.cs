using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.ServiceModel;
using System.Threading.Tasks;

namespace Blob.Contracts.ServiceContracts.Identity
{
    [ServiceContract]
    public interface IUserClaimStoreService<in TUserIdType> : IDisposable
        where TUserIdType : IEquatable<TUserIdType>
    {
        [OperationContract]
        Task AddClaimAsync(TUserIdType userId, Claim claim);

        [OperationContract]
        Task<IList<Claim>> GetClaimsAsync(TUserIdType userId);

        [OperationContract]
        Task RemoveClaimAsync(TUserIdType userId, Claim claim);
    }
}
