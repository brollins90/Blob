using System.Collections.Generic;
using System.Threading.Tasks;
using Blob.Core.Models;
using Microsoft.AspNet.Identity;

namespace Blob.Core.Identity.Store
{
    public interface ICustomerGroupManager
    {
        Task<IdentityResult> ClearUserGroupsAsync(string userId);
        Task<IdentityResult> CreateGroupAsync(CustomerGroup group);
        Task<IdentityResult> DeleteGroupAsync(string groupId);
        Task<CustomerGroup> FindByIdAsync(string id);
        Task<IEnumerable<Role>> GetGroupRolesAsync(string groupId);
        Task<IEnumerable<User>> GetGroupUsersAsync(string groupId);
        Task<IEnumerable<CustomerGroupRole>> GetUserGroupRolesAsync(string userId);
        Task<IEnumerable<CustomerGroup>> GetUserGroupsAsync(string userId);
        Task<IdentityResult> RefreshUserGroupRolesAsync(string userId);
        Task<IdentityResult> SetGroupRolesAsync(string groupId, params string[] roleNames);
        Task<IdentityResult> SetUserGroupsAsync(string userId, params string[] groupIds);
        Task<IdentityResult> UpdateGroupAsync(CustomerGroup group);
    }
}