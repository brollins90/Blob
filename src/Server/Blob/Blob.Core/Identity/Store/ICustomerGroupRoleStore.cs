using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Blob.Core.Models;

namespace Blob.Core.Identity.Store
{
    public interface ICustomerGroupRoleStore
    {
        Task AddRoleToGroupAsync(CustomerGroup group, string roleName);
        Task RemoveRoleFromGroupAsync(CustomerGroup group, string roleName);
        Task<IList<Role>> GetRolesForGroupAsync(Guid groupId);
        Task<bool> HasRoleAsync(CustomerGroup group, string roleName);
    }
}
