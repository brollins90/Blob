﻿using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;

namespace Blob.Contracts.ServiceContracts.Identity
{
    [ServiceContract]
    public interface IUserRoleStoreService<in TUserIdType> : IDisposable
        where TUserIdType : IEquatable<TUserIdType>
    {
        [OperationContract]
        Task AddToRoleAsync(TUserIdType userId, string roleName);

        [OperationContract]
        Task AddToRolesAsync(TUserIdType userId, string[] roleNames);

        [OperationContract]
        Task<IList<string>> GetRolesAsync(TUserIdType userId);

        [OperationContract]
        Task<bool> IsInRoleAsync(TUserIdType userId, string roleName);

        [OperationContract]
        Task RemoveFromRoleAsync(TUserIdType userId, string roleName);
    }
}
