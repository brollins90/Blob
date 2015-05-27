using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Blob.Core.Models;
using Microsoft.AspNet.Identity;

namespace Blob.Security.Identity
{
    public interface ICustomerGroupManager
    {
        Task<IdentityResult> ClearUserGroupsAsync(Guid userId);
        Task<IdentityResult> CreateGroupAsync(CustomerGroup group);
        Task<IdentityResult> DeleteGroupAsync(Guid groupId);
        Task<CustomerGroup> FindGroupByIdAsync(Guid id);
        Task<IEnumerable<Role>> GetGroupRolesAsync(Guid groupId);
        Task<IEnumerable<User>> GetGroupUsersAsync(Guid groupId);
        Task<IEnumerable<CustomerGroupRole>> GetUserGroupRolesAsync(Guid userId);
        Task<IEnumerable<CustomerGroup>> GetUserGroupsAsync(Guid userId);
        Task<IdentityResult> RefreshUserGroupRolesAsync(Guid userId);
        Task<IdentityResult> SetGroupRolesAsync(Guid groupId, params string[] roleNames);
        Task<IdentityResult> SetUserGroupsAsync(Guid userId, params Guid[] groupIds);
        Task<IdentityResult> UpdateGroupAsync(CustomerGroup group);
    }
}