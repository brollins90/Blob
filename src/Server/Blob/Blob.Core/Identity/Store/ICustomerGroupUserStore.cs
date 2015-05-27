using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Blob.Core.Models;

namespace Blob.Core.Identity.Store
{
    public interface ICustomerGroupUserStore
    {
        Task AddUserToGroupAsync(CustomerGroup group, Guid userId);
        Task RemoveUserFromGroupAsync(CustomerGroup group, Guid userId);
        Task<IList<User>> GetUsersInGroupAsync(Guid groupId);
        Task<bool> HasUserAsync(CustomerGroup group, Guid userId);
    }
}