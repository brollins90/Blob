﻿using System;
using System.ServiceModel;
using System.Threading.Tasks;

namespace Blob.Contracts.ServiceContracts.Identity
{
    [ServiceContract]
    public interface IUserTwoFactorStoreService<in TUserIdType> : IDisposable
        where TUserIdType : IEquatable<TUserIdType>
    {
        [OperationContract]
        Task<bool> GetTwoFactorEnabledAsync(TUserIdType userId);

        [OperationContract]
        Task SetTwoFactorEnabledAsync(TUserIdType userId, bool enabled);
    }
}
