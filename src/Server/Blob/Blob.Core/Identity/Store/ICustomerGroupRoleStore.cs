using System.Collections.Generic;
using System.Threading.Tasks;
using Blob.Core.Models;

namespace Blob.Core.Identity.Store
{
    public interface ICustomerGroupRoleStore
    {
        Task AddRoleAsync(CustomerGroup group, string roleName);
        Task RemoveRoleAsync(CustomerGroup group, string roleName);
        Task<IList<string>> GetRolesAsync(CustomerGroup group);
        Task<bool> HasRoleAsync(CustomerGroup group, string roleName);
    }
}
