//using System;
//using System.Collections.Generic;
//using System.Data.Entity;
//using System.Linq;
//using System.Threading.Tasks;
//using Blob.Contracts.Models;
//using Blob.Core.Domain;
//using Blob.Data;
//using Blob.Data.Identity;
//using log4net;
//using Microsoft.AspNet.Identity;

//namespace Blob.Security.Identity
//{
//    // http://typecastexception.com/post/2014/08/10/ASPNET-Identity-20-Implementing-Group-Based-Permissions-Management.aspx

//    public interface IGroupManagerService { }

//    public class BlobGroupManager : IGroupManagerService
//    {
//        private readonly ILog _log;
//        private readonly BlobGroupStore _groupStore;
//        private readonly BlobDbContext _context;
//        private readonly BlobUserManager _userManager;
//        private readonly BlobRoleManager _roleManager;

//        public BlobGroupManager(ILog log, BlobGroupStore groupStore, BlobDbContext context, BlobUserManager userManager, BlobRoleManager roleManager)
//        {
//            _log = log;
//            _groupStore = groupStore;
//            _context = context;
//            _userManager = userManager;
//            _roleManager = roleManager;
//        }

//        //public BlobGroupManager(BlobGroupStore groupStore)
//        //{
//        //    _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
//        //    _log.Debug("Constructing BlobGroupManager");

//        //    if (groupStore == null)
//        //    {
//        //        throw new ArgumentNullException("groupStore");
//        //    }
//        //    _groupStore = groupStore;
//        //}
//        public IQueryable<CustomerGroup> Groups
//        {
//            get
//            {
//                return _groupStore.Groups;
//            }
//        }


//        public async Task<IdentityResult> ClearUserGroupsAsync(Guid userId)
//        {
//            return await SetUserGroupsAsync(userId).ConfigureAwait(false);
//        }

//        public async Task<IdentityResult> CreateGroupAsync(CustomerGroup group)
//        {
//            await _groupStore.CreateAsync(group).ConfigureAwait(false);
//            return IdentityResult.Success;
//        }

//        public async Task<IdentityResult> DeleteGroupAsync(Guid groupId)
//        {
//            CustomerGroup group = await this.FindByIdAsync(groupId).ConfigureAwait(true);
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
//            _context.Groups.Remove(group);

//            await _context.SaveChangesAsync().ConfigureAwait(true);

//            // Reset all the user roles:
//            foreach (var user in currentGroupMembers)
//            {
//                await this.RefreshUserGroupRolesAsync(user.Id).ConfigureAwait(true);
//            }
//            return IdentityResult.Success;
//        }

//        public async Task<CustomerGroup> FindByIdAsync(Guid groupId)
//        {
//            return await _groupStore.FindByIdAsync(groupId).ConfigureAwait(false);
//        }


//        public async Task<IEnumerable<Role>> GetGroupRolesAsync(Guid groupId)
//        {
//            CustomerGroup theGroup = await _context.Groups.FirstOrDefaultAsync(g => g.Id == groupId).ConfigureAwait(true);
//            List<Role> allRoles = await _roleManager.Roles.ToListAsync().ConfigureAwait(true);
//            List<Role> groupRoles = (from r in allRoles
//                                     where theGroup.Roles
//                                       .Any(ap => ap.RoleId == r.Id)
//                                     select r).ToList();
//            return groupRoles;
//        }

//        public async Task<IEnumerable<User>> GetGroupUsersAsync(Guid groupId)
//        {
//            CustomerGroup group = await FindByIdAsync(groupId).ConfigureAwait(true);
//            List<User> users = new List<User>();
//            foreach (BlobUserGroup groupUser in group.Users)
//            {
//                User user = await _context.Users.FirstOrDefaultAsync(u => u.Id == groupUser.UserId);
//                users.Add(user);
//            }
//            return users;
//        }

//        public async Task<IEnumerable<CustomerGroup>> GetUserGroupsAsync(Guid userId)
//        {
//            return await (from g in Groups
//                          where g.Users
//                            .Any(u => u.UserId == userId)
//                          select g).ToListAsync().ConfigureAwait(false);
//        }

//        public async Task<IEnumerable<BlobGroupRole>> GetUserGroupRolesAsync(Guid userId)
//        {
//            IEnumerable<CustomerGroup> userGroups = await this.GetUserGroupsAsync(userId).ConfigureAwait(true);
//            List<BlobGroupRole> userGroupRoles = new List<BlobGroupRole>();
//            foreach (CustomerGroup group in userGroups)
//            {
//                userGroupRoles.AddRange(group.Roles.ToArray());
//            }
//            return userGroupRoles;
//        }

//        public async Task<IdentityResult> RefreshUserGroupRolesAsync(Guid userId)
//        {
//            UserDto user = await _userManager.FindByIdAsync(userId).ConfigureAwait(true);
//            if (user == null)
//            {
//                throw new ArgumentNullException("User");
//            }

//            // Remove user from previous roles:
//            IList<string> oldUserRoles = await _userManager.GetRolesAsync(userId).ConfigureAwait(true);
//            if (oldUserRoles.Count > 0)
//            {
//                await _userManager.RemoveFromRolesAsync(userId, oldUserRoles.ToArray());
//            }

//            // Find the roles this user is entitled to from group membership:
//            var newGroupRoles = await this.GetUserGroupRolesAsync(userId).ConfigureAwait(true);

//            // Get the damn role names:
//            var allRoles = await _roleManager.Roles.ToListAsync().ConfigureAwait(true);
//            var addTheseRoles = allRoles.Where(r => newGroupRoles.Any(gr => gr.RoleId == r.Id));
//            var roleNames = addTheseRoles.Select(n => n.Name).ToArray();

//            // Add the user to the proper roles
//            await _userManager.AddToRolesAsync(userId, roleNames).ConfigureAwait(true);

//            return IdentityResult.Success;
//        }

//        public async Task<IdentityResult> SetGroupRolesAsync(Guid groupId, params string[] roleNames)
//        {
//            // Clear all the roles associated with this group:
//            CustomerGroup thisGroup = await FindByIdAsync(groupId);
//            thisGroup.Roles.Clear();
//            await _context.SaveChangesAsync();

//            // Add the new roles passed in:
//            var newRoles = _roleManager.Roles.Where(r => roleNames.Any(n => n == r.Name));
//            foreach (var role in newRoles)
//            {
//                thisGroup.Roles.Add(new BlobGroupRole
//                                        {
//                                            GroupId = groupId,
//                                            RoleId = role.Id
//                                        });
//            }
//            await _context.SaveChangesAsync();

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
//            var currentGroups = await GetUserGroupsAsync(userId);
//            foreach (var group in currentGroups)
//            {
//                group.Users.Remove(group.Users.FirstOrDefault(gr => gr.UserId == userId));
//            }
//            await _context.SaveChangesAsync();

//            // Add the user to the new groups:
//            foreach (Guid groupId in groupIds)
//            {
//                CustomerGroup newGroup = await this.FindByIdAsync(groupId);
//                newGroup.Users.Add(new BlobUserGroup
//                {
//                    UserId = userId,
//                    GroupId = groupId
//                });
//            }
//            await _context.SaveChangesAsync();

//            await this.RefreshUserGroupRolesAsync(userId);
//            return IdentityResult.Success;
//        }

//        public async Task<IdentityResult> UpdateGroupAsync(CustomerGroup group)
//        {
//            await _groupStore.UpdateAsync(group).ConfigureAwait(true);
//            foreach (var groupUser in group.Users)
//            {
//                await this.RefreshUserGroupRolesAsync(groupUser.UserId).ConfigureAwait(true);
//            }
//            return IdentityResult.Success;
//        }
//    }
//}
