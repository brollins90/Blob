using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blob.Contracts.Models;
using Blob.Core.Identity.Store;
using Blob.Core.Models;

namespace Blob.Core.Identity
{
    public class BlobCustomerGroupManager : IBlobCustomerGroupManager
    {
        private readonly BlobCustomerStore _customerStore;
        private readonly BlobDbContext _db;
        private readonly BlobUserManager _userManager;
        private readonly BlobRoleManager _roleManager;

        public BlobCustomerGroupManager(BlobCustomerStore customerStore, BlobDbContext context, BlobUserManager userManager, BlobRoleManager roleManager)
        {
            _db = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _customerStore = customerStore;
        }
        public async Task<CustomerGroup> FindGroupByIdAsync(Guid groupId)
        {
            return await _customerStore.FindGroupByIdAsync(groupId);
        }

        public async Task<BlobResultDto> AddRoleToCustomerGroup(AddRoleToCustomerGroupDto dto)
        {
            CustomerGroup group = await _customerStore.FindGroupByIdAsync(dto.GroupId);
            Role role = await _roleManager.FindByIdAsync(dto.RoleId);
            await _customerStore.AddRoleToGroupAsync(group, role.Name);
            return BlobResultDto.Success;
        }


        public async Task<BlobResultDto> AddUserToCustomerGroup(AddUserToCustomerGroupDto dto)
        {
            CustomerGroup group = await _customerStore.FindGroupByIdAsync(dto.GroupId);
            await _customerStore.AddUserToGroupAsync(group, dto.UserId);
            return BlobResultDto.Success;
        }

        public async Task<BlobResultDto> CreateGroupAsync(CreateCustomerGroupDto dto)
        {
            CustomerGroup group = new CustomerGroup
                                  {
                                      CustomerId = dto.CustomerId,
                                      Description = dto.Description,
                                      Id = dto.GroupId,
                                      Name = dto.Name
                                  };
            await _customerStore.CreateGroupAsync(group).ConfigureAwait(false);
            return BlobResultDto.Success;
        }

        public async Task<BlobResultDto> DeleteGroupAsync(Guid groupId)
        {
            CustomerGroup group = await _customerStore.FindGroupByIdAsync(groupId);
            if (group == null)
            {
                // the task is done, just not the way they wanted...
                return new BlobResultDto("Group was not found"){Succeeded = true};
            }

            //var currentGroupMembers = (await this.GetGroupUsersAsync(groupId)).ToList();
            //// remove the roles from the group:
            //group.Roles.Clear();

            //// Remove all the users:
            //group.Users.Clear();

            // Remove the group itself:
            await _customerStore.DeleteGroupAsync(group);

            // Reset all the user roles:
            //foreach (var user in currentGroupMembers)
            //{
            //    await this.RefreshUserGroupRolesAsync(user.Id);
            //}
            return BlobResultDto.Success;
        }

        public async Task<IEnumerable<Role>> GetGroupRolesAsync(Guid groupId)
        {
            return await _customerStore.GetRolesForGroupAsync(groupId);
        }

        public async Task<IEnumerable<User>> GetGroupUsersAsync(Guid groupId)
        {
            return await _customerStore.GetUsersInGroupAsync(groupId);
        }

        public async Task<BlobResultDto> RemoveRoleFromCustomerGroupAsync(RemoveRoleFromCustomerGroupDto dto)
        {
            CustomerGroup group = await _customerStore.FindGroupByIdAsync(dto.GroupId);
            Role role = await _roleManager.FindByIdAsync(dto.RoleId);
            await _customerStore.RemoveRoleFromGroupAsync(group, role.Name);
            return BlobResultDto.Success;
        }

        public async Task<BlobResultDto> RemoveUserFromCustomerGroupAsync(RemoveUserFromCustomerGroupDto dto)
        {
            CustomerGroup group = await _customerStore.FindGroupByIdAsync(dto.GroupId);
            await _customerStore.RemoveUserFromGroupAsync(group, dto.UserId);
            return BlobResultDto.Success;
        }
        
        public async Task<BlobResultDto> UpdateGroupAsync(UpdateCustomerGroupDto dto)
        {
            CustomerGroup group = await _customerStore.FindGroupByIdAsync(dto.GroupId);
            group.Name = dto.Name;
            group.Description = dto.Description;

            var beforeRoles = (await GetGroupRolesAsync(group.Id)).Select(x => x.Id);
            var nowRoles = dto.RolesIdStrings.Select(x=>Guid.Parse(x));

            var removedRoles = beforeRoles.Except(nowRoles);
            var addedRoles = nowRoles.Except(beforeRoles);

            await _customerStore.UpdateGroupAsync(group);
            foreach (var roleId in removedRoles)
            {
                await _customerStore.RemoveRoleFromGroupAsync(group, roleId);
            } 
            foreach (var roleId in addedRoles)
            {
                await _customerStore.AddRoleToGroupAsync(group, roleId);
            }

            //foreach (var groupUser in group.Users)
            //{
            //    await this.RefreshUserGroupRolesAsync(groupUser.UserId);
            //}
            return BlobResultDto.Success;
        }



        //private async Task<BlobResultDto> SetGroupRolesAsync(Guid groupId, params string[] roleNames)
        //{
        //    // Clear all the roles associated with this group:
        //    CustomerGroup thisGroup = await this.FindGroupByIdAsync(groupId);
        //    thisGroup.Roles.Clear();
        //    await _db.SaveChangesAsync();

        //    // Add the new roles passed in:
        //    var newRoles = _roleManager.Roles.Where(r => roleNames.Any(n => n == r.Name));
        //    foreach (var role in newRoles)
        //    {
        //        thisGroup.Roles.Add(new CustomerGroupRole { GroupId = groupId, RoleId = role.Id });
        //    }
        //    await _db.SaveChangesAsync();

        //    //// Reset the roles for all affected users:
        //    //foreach (var groupUser in thisGroup.Users)
        //    //{
        //    //    await this.RefreshUserGroupRolesAsync(groupUser.UserId);
        //    //}
        //    return BlobResultDto.Success;
        //}


        //public async Task<IdentityResult> ClearUserGroupsAsync(Guid userId)
        //{
        //    return await this.SetUserGroupsAsync(userId, new Guid[] { });
        //}

        //public async Task<IEnumerable<CustomerGroupRole>> GetUserGroupRolesAsync(Guid userId)
        //{
        //    var userGroups = await this.GetUserGroupsAsync(userId).ConfigureAwait(true);
        //    var userGroupRoles = new List<CustomerGroupRole>();
        //    foreach (var group in userGroups)
        //    {
        //        userGroupRoles.AddRange(group.Roles.ToArray());
        //    }
        //    return userGroupRoles;
        //}

        //public async Task<IEnumerable<CustomerGroup>> GetUserGroupsAsync(Guid userId)
        //{
        //    var result = new List<CustomerGroup>();
        //    var userGroups = (from g in this.Groups
        //                      where g.Users.Any(u => u.UserId == userId)
        //                      select g).ToListAsync();
        //    return await userGroups;
        //}

        //public async Task<BlobResultDto> RefreshUserGroupRolesAsync(Guid userId)
        //{
        //    //var user = await _userManager.FindByIdAsync(userId);
        //    //if (user == null)
        //    //{
        //    //    throw new ArgumentNullException("User");
        //    //}
        //    //// Remove user from previous roles:
        //    //var oldUserRoles = await _userManager.GetRolesAsync(userId);
        //    //if (oldUserRoles.Count > 0)
        //    //{
        //    //    await _userManager.RemoveFromRolesAsync(userId, oldUserRoles.ToArray());
        //    //}

        //    //// Find the roles this user is entitled to from group membership:
        //    //var newGroupRoles = await this.GetUserGroupRolesAsync(userId);

        //    //// Get the damn role names:
        //    //var allRoles = await _roleManager.Roles.ToListAsync();
        //    //var addTheseRoles = allRoles.Where(r => newGroupRoles.Any(gr => gr.RoleId == r.Id));
        //    //var roleNames = addTheseRoles.Select(n => n.Name).ToArray();

        //    //// Add the user to the proper roles
        //    //await _userManager.AddToRolesAsync(userId, roleNames);

        //    return BlobResultDto.Success;
        //}

        //public async Task<BlobResultDto> SetGroupRolesAsync(Guid groupId, params string[] roleNames)
        //{
        //    // Clear all the roles associated with this group:
        //    CustomerGroup thisGroup = await this.FindGroupByIdAsync(groupId);
        //    thisGroup.Roles.Clear();
        //    await _db.SaveChangesAsync();

        //    // Add the new roles passed in:
        //    var newRoles = _roleManager.Roles.Where(r => roleNames.Any(n => n == r.Name));
        //    foreach (var role in newRoles)
        //    {
        //        thisGroup.Roles.Add(new CustomerGroupRole { GroupId = groupId, RoleId = role.Id });
        //    }
        //    await _db.SaveChangesAsync();

        //    // Reset the roles for all affected users:
        //    foreach (var groupUser in thisGroup.Users)
        //    {
        //        await this.RefreshUserGroupRolesAsync(groupUser.UserId);
        //    }
        //    return BlobResultDto.Success;
        //}

        //public async Task<BlobResultDto> SetUserGroupsAsync(Guid userId, params Guid[] groupIds)
        //{
        //    // Clear current group membership:
        //    var currentGroups = await this.GetUserGroupsAsync(userId);
        //    foreach (var group in currentGroups)
        //    {
        //        group.Users.Remove(group.Users.FirstOrDefault(gr => gr.UserId == userId));
        //    }
        //    await _db.SaveChangesAsync();

        //    // Add the user to the new groups:
        //    foreach (var groupId in groupIds)
        //    {
        //        var newGroup = await this.FindGroupByIdAsync(groupId);
        //        newGroup.Users.Add(new CustomerGroupUser { UserId = userId, GroupId = groupId });
        //    }
        //    await _db.SaveChangesAsync();

        //    await this.RefreshUserGroupRolesAsync(userId);
        //    return BlobResultDto.Success;
        //}




    }
}

//using Microsoft.AspNet.Identity;
//using System;
//using System.Collections.Generic;
//using System.Data.Entity;
//using System.Linq;
//using System.Threading.Tasks;
//using Blob.Core.Identity.Store;
//using Blob.Core.Models;
//using Blob.Security.Identity;

//namespace Blob.Core.Services
//{
//    public class BlobCustomerGroupManager : ICustomerGroupManager
//    {
//        private readonly BlobCustomerStore _customerStore;
//        private readonly BlobDbContext _db;
//        private readonly BlobUserManager _userManager;
//        private readonly BlobRoleManager _roleManager;

//        public BlobCustomerGroupManager(BlobCustomerStore customerStore, BlobDbContext context, BlobUserManager userManager, BlobRoleManager roleManager)
//        {
//            _db = context;
//            _userManager = userManager;
//            _roleManager = roleManager;
//            _customerStore = customerStore;
//        }


//        public IQueryable<CustomerGroup> Groups { get { return _customerStore.Groups; } }

//        public async Task<IdentityResult> ClearUserGroupsAsync(Guid userId)
//        {
//            return await this.SetUserGroupsAsync(userId, new Guid[] { });
//        }

//        public async Task<IdentityResult> CreateGroupAsync(CustomerGroup group)
//        {
//            await _customerStore.CreateGroupAsync(group);
//            return IdentityResult.Success;
//        }

//        public async Task<IdentityResult> DeleteGroupAsync(Guid groupId)
//        {
//            var group = await this.FindGroupByIdAsync(groupId);
//            if (group == null)
//            {
//                throw new ArgumentNullException("User");
//            }

//            var currentGroupMembers = (await this.GetGroupUsersAsync(groupId)).ToList();
//            // remove the roles from the group:
//            group.Roles.Clear();

//            // Remove all the users:
//            group.Users.Clear();

//            // Remove the group itself:
//            await _customerStore.DeleteGroupAsync(group);

//            // Reset all the user roles:
//            foreach (var user in currentGroupMembers)
//            {
//                await this.RefreshUserGroupRolesAsync(user.Id);
//            }
//            return IdentityResult.Success;
//        }

//        public async Task<CustomerGroup> FindGroupByIdAsync(Guid id)
//        {
//            return await _customerStore.FindGroupByIdAsync(id).ConfigureAwait(false);
//        }

//        public async Task<IEnumerable<Role>> GetGroupRolesAsync(Guid groupId)
//        {
//            var grp = await FindGroupByIdAsync(groupId).ConfigureAwait(true);
//            var roles = await _roleManager.Roles.ToListAsync().ConfigureAwait(true);
//            var groupRoles = (from r in roles
//                              where grp.Roles.Any(ap => ap.RoleId == r.Id)
//                              select r).ToList();
//            return groupRoles;
//        }

//        public async Task<IEnumerable<User>> GetGroupUsersAsync(Guid groupId)
//        {
//            var group = await this.FindGroupByIdAsync(groupId).ConfigureAwait(true);
//            var users = new List<User>();
//            foreach (var groupUser in group.Users)
//            {
//                var user = await _db.Users
//                    .FirstOrDefaultAsync(u => u.Id == groupUser.UserId);
//                users.Add(user);
//            }
//            return users;
//        }

//        public async Task<IEnumerable<CustomerGroupRole>> GetUserGroupRolesAsync(Guid userId)
//        {
//            var userGroups = await this.GetUserGroupsAsync(userId).ConfigureAwait(true);
//            var userGroupRoles = new List<CustomerGroupRole>();
//            foreach (var group in userGroups)
//            {
//                userGroupRoles.AddRange(group.Roles.ToArray());
//            }
//            return userGroupRoles;
//        }

//        public async Task<IEnumerable<CustomerGroup>> GetUserGroupsAsync(Guid userId)
//        {
//            var result = new List<CustomerGroup>();
//            var userGroups = (from g in this.Groups
//                              where g.Users.Any(u => u.UserId == userId)
//                              select g).ToListAsync();
//            return await userGroups;
//        }

//        public async Task<IdentityResult> RefreshUserGroupRolesAsync(Guid userId)
//        {
//            //var user = await _userManager.FindByIdAsync(userId);
//            //if (user == null)
//            //{
//            //    throw new ArgumentNullException("User");
//            //}
//            //// Remove user from previous roles:
//            //var oldUserRoles = await _userManager.GetRolesAsync(userId);
//            //if (oldUserRoles.Count > 0)
//            //{
//            //    await _userManager.RemoveFromRolesAsync(userId, oldUserRoles.ToArray());
//            //}

//            //// Find the roles this user is entitled to from group membership:
//            //var newGroupRoles = await this.GetUserGroupRolesAsync(userId);

//            //// Get the damn role names:
//            //var allRoles = await _roleManager.Roles.ToListAsync();
//            //var addTheseRoles = allRoles.Where(r => newGroupRoles.Any(gr => gr.RoleId == r.Id));
//            //var roleNames = addTheseRoles.Select(n => n.Name).ToArray();

//            //// Add the user to the proper roles
//            //await _userManager.AddToRolesAsync(userId, roleNames);

//            return IdentityResult.Success;
//        }

//        public async Task<IdentityResult> SetGroupRolesAsync(Guid groupId, params string[] roleNames)
//        {
//            // Clear all the roles associated with this group:
//            CustomerGroup thisGroup = await this.FindGroupByIdAsync(groupId);
//            thisGroup.Roles.Clear();
//            await _db.SaveChangesAsync();

//            // Add the new roles passed in:
//            var newRoles = _roleManager.Roles.Where(r => roleNames.Any(n => n == r.Name));
//            foreach (var role in newRoles)
//            {
//                thisGroup.Roles.Add(new CustomerGroupRole { GroupId = groupId, RoleId = role.Id });
//            }
//            await _db.SaveChangesAsync();

//            // Reset the roles for all affected users:
//            foreach (var groupUser in thisGroup.Users)
//            {
//                await this.RefreshUserGroupRolesAsync(groupUser.UserId);
//            }
//            return IdentityResult.Success;
//        }

//        public async Task<IdentityResult> SetUserGroupsAsync(Guid userId, params Guid[] groupIds)
//        {
//            // Clear current group membership:
//            var currentGroups = await this.GetUserGroupsAsync(userId);
//            foreach (var group in currentGroups)
//            {
//                group.Users.Remove(group.Users.FirstOrDefault(gr => gr.UserId == userId));
//            }
//            await _db.SaveChangesAsync();

//            // Add the user to the new groups:
//            foreach (var groupId in groupIds)
//            {
//                var newGroup = await this.FindGroupByIdAsync(groupId);
//                newGroup.Users.Add(new CustomerGroupUser { UserId = userId, GroupId = groupId });
//            }
//            await _db.SaveChangesAsync();

//            await this.RefreshUserGroupRolesAsync(userId);
//            return IdentityResult.Success;
//        }

//        public async Task<IdentityResult> UpdateGroupAsync(CustomerGroup group)
//        {
//            await _customerStore.UpdateGroupAsync(group);
//            foreach (var groupUser in group.Users)
//            {
//                await this.RefreshUserGroupRolesAsync(groupUser.UserId);
//            }
//            return IdentityResult.Success;
//        }
//    }
//}