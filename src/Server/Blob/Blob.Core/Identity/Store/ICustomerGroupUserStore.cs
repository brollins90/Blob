using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Blob.Core.Models;

namespace Blob.Core.Identity.Store
{
    public interface ICustomerGroupUserStore
    {
        Task AddUserAsync(CustomerGroup group, Guid userId);
        Task RemoveUserAsync(CustomerGroup group, Guid userId);
        Task<IList<User>> GetUsersAsync(CustomerGroup group);
        Task<bool> HasUserAsync(CustomerGroup group, Guid userId);
    }
}