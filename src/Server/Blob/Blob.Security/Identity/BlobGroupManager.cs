using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Blob.Contracts.Models;
using Blob.Core.Domain;
using Blob.Data;
using Blob.Data.Identity;
using log4net;
using Microsoft.AspNet.Identity;

namespace Blob.Security.Identity
{
    // http://typecastexception.com/post/2014/08/10/ASPNET-Identity-20-Implementing-Group-Based-Permissions-Management.aspx

    public interface IGroupManagerService { }

    public class BlobGroupManager : IGroupManagerService
    {
        private readonly ILog _log;
        private readonly BlobGroupStore _groupStore;
        private readonly BlobDbContext _context;
        private readonly BlobUserManager _userManager;
        private readonly BlobRoleManager _roleManager;

        public BlobGroupManager(ILog log, BlobGroupStore groupStore, BlobDbContext context, BlobUserManager userManager, BlobRoleManager roleManager)
        {
            _log = log;
            _groupStore = groupStore;
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        //public BlobGroupManager(BlobGroupStore groupStore)
        //{
        //    _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        //    _log.Debug("Constructing BlobGroupManager");

        //    if (groupStore == null)
        //    {
        //        throw new ArgumentNullException("groupStore");
        //    }
        //    _groupStore = groupStore;
        //}
        public IQueryable<BlobGroup> Groups
        {
            get
            {
                return _groupStore.Groups;
            }
        }


        public async Task<IdentityResult> ClearUserGroupsAsync(Guid userId)
        {
            return await SetUserGroupsAsync(userId).ConfigureAwait(false);
        }

        public async Task<IdentityResult> CreateGroupAsync(BlobGroup group)
        {
            await _groupStore.CreateAsync(group).ConfigureAwait(false);
            return IdentityResult.Success;
        }

        public async Task<IdentityResult> DeleteGroupAsync(Guid groupId)
        {
            BlobGroup group = await this.FindByIdAsync(groupId).ConfigureAwait(true);
            if (group == null)
            {
                throw new ArgumentNullException("User");
            }

            var currentGroupMembers = (await this.GetGroupUsersAsync(groupId)).ToList();
            // remove the roles from the group:
            group.GroupRoles.Clear();

            // Remove all the users:
            group.GroupUsers.Clear();

            // Remove the group itself:
            _context.Groups.Remove(group);

            await _context.SaveChangesAsync().ConfigureAwait(true);

            // Reset all the user roles:
            foreach (var user in currentGroupMembers)
            {
                await this.RefreshUserGroupRolesAsync(user.Id).ConfigureAwait(true);
            }
            return IdentityResult.Success;
        }

        public async Task<BlobGroup> FindByIdAsync(Guid groupId)
        {
            return await _groupStore.FindByIdAsync(groupId).ConfigureAwait(false);
        }


        public async Task<IEnumerable<Role>> GetGroupRolesAsync(Guid groupId)
        {
            BlobGroup theGroup = await _context.Groups.FirstOrDefaultAsync(g => g.Id == groupId).ConfigureAwait(true);
            List<Role> allRoles = await _roleManager.Roles.ToListAsync().ConfigureAwait(true);
            List<Role> groupRoles = (from r in allRoles
                                     where theGroup.GroupRoles
                                       .Any(ap => ap.RoleId == r.Id)
                                     select r).ToList();
            return groupRoles;
        }

        public async Task<IEnumerable<User>> GetGroupUsersAsync(Guid groupId)
        {
            BlobGroup group = await FindByIdAsync(groupId).ConfigureAwait(true);
            List<User> users = new List<User>();
            foreach (BlobUserGroup groupUser in group.GroupUsers)
            {
                User user = await _context.Users.FirstOrDefaultAsync(u => u.Id == groupUser.UserId);
                users.Add(user);
            }
            return users;
        }

        public async Task<IEnumerable<BlobGroup>> GetUserGroupsAsync(Guid userId)
        {
            return await (from g in Groups
                          where g.GroupUsers
                            .Any(u => u.UserId == userId)
                          select g).ToListAsync().ConfigureAwait(false);
        }

        public async Task<IEnumerable<BlobGroupRole>> GetUserGroupRolesAsync(Guid userId)
        {
            IEnumerable<BlobGroup> userGroups = await this.GetUserGroupsAsync(userId).ConfigureAwait(true);
            List<BlobGroupRole> userGroupRoles = new List<BlobGroupRole>();
            foreach (BlobGroup group in userGroups)
            {
                userGroupRoles.AddRange(group.GroupRoles.ToArray());
            }
            return userGroupRoles;
        }

        public async Task<IdentityResult> RefreshUserGroupRolesAsync(Guid userId)
        {
            UserDto user = await _userManager.FindByIdAsync(userId).ConfigureAwait(true);
            if (user == null)
            {
                throw new ArgumentNullException("User");
            }

            // Remove user from previous roles:
            IList<string> oldUserRoles = await _userManager.GetRolesAsync(userId).ConfigureAwait(true);
            if (oldUserRoles.Count > 0)
            {
                await _userManager.RemoveFromRolesAsync(userId, oldUserRoles.ToArray());
            }

            // Find the roles this user is entitled to from group membership:
            var newGroupRoles = await this.GetUserGroupRolesAsync(userId).ConfigureAwait(true);

            // Get the damn role names:
            var allRoles = await _roleManager.Roles.ToListAsync().ConfigureAwait(true);
            var addTheseRoles = allRoles.Where(r => newGroupRoles.Any(gr => gr.RoleId == r.Id));
            var roleNames = addTheseRoles.Select(n => n.Name).ToArray();

            // Add the user to the proper roles
            await _userManager.AddToRolesAsync(userId, roleNames).ConfigureAwait(true);

            return IdentityResult.Success;
        }

        public async Task<IdentityResult> SetGroupRolesAsync(Guid groupId, params string[] roleNames)
        {
            // Clear all the roles associated with this group:
            BlobGroup thisGroup = await FindByIdAsync(groupId);
            thisGroup.GroupRoles.Clear();
            await _context.SaveChangesAsync();

            // Add the new roles passed in:
            var newRoles = _roleManager.Roles.Where(r => roleNames.Any(n => n == r.Name));
            foreach (var role in newRoles)
            {
                thisGroup.GroupRoles.Add(new BlobGroupRole
                                        {
                                            GroupId = groupId,
                                            RoleId = role.Id
                                        });
            }
            await _context.SaveChangesAsync();

            // Reset the roles for all affected users:
            foreach (var groupUser in thisGroup.GroupUsers)
            {
                await this.RefreshUserGroupRolesAsync(groupUser.UserId);
            }
            return IdentityResult.Success;
        }

        public async Task<IdentityResult> SetUserGroupsAsync(Guid userId, params Guid[] groupIds)
        {
            // Clear current group membership:
            var currentGroups = await GetUserGroupsAsync(userId);
            foreach (var group in currentGroups)
            {
                group.GroupUsers.Remove(group.GroupUsers.FirstOrDefault(gr => gr.UserId == userId));
            }
            await _context.SaveChangesAsync();

            // Add the user to the new groups:
            foreach (Guid groupId in groupIds)
            {
                BlobGroup newGroup = await this.FindByIdAsync(groupId);
                newGroup.GroupUsers.Add(new BlobUserGroup
                {
                    UserId = userId,
                    GroupId = groupId
                });
            }
            await _context.SaveChangesAsync();

            await this.RefreshUserGroupRolesAsync(userId);
            return IdentityResult.Success;
        }

        public async Task<IdentityResult> UpdateGroupAsync(BlobGroup group)
        {
            await _groupStore.UpdateAsync(group).ConfigureAwait(true);
            foreach (var groupUser in group.GroupUsers)
            {
                await this.RefreshUserGroupRolesAsync(groupUser.UserId).ConfigureAwait(true);
            }
            return IdentityResult.Success;
        }
    }
}
