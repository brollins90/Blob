//using Microsoft.AspNet.Identity;
//using Microsoft.AspNet.Identity.Owin;
//using System;
//using System.Collections.Generic;
//using System.Data.Entity;
//using System.Linq;
//using System.Threading.Tasks;

//namespace Blob.Core.Services
//{
//    public class CustomerGroupManager
//    {
//        private ICustomerGroupStore _groupStore;
//        private BlobDbContext _db;
//        private BlobUserManager _userManager;
//        private BlobRoleManager _roleManager;

//        public BlobGroupManager()
//        {
//            _db = HttpContext.Current
//                .GetOwinContext().Get<BlobDbContext>();
//            _userManager = HttpContext.Current
//                .GetOwinContext().GetUserManager<BlobUserManager>();
//            _roleManager = HttpContext.Current
//                .GetOwinContext().Get<BlobRoleManager>();
//            _groupStore = new BlobGroupStore(_db);
//        }


//        public IQueryable<BlobGroup> Groups
//        {
//            get
//            {
//                return _groupStore.Groups;
//            }
//        }


//        public async Task<IdentityResult> CreateGroupAsync(BlobGroup group)
//        {
//            await _groupStore.CreateAsync(group);
//            return IdentityResult.Success;
//        }


//        public IdentityResult CreateGroup(BlobGroup group)
//        {
//            _groupStore.Create(group);
//            return IdentityResult.Success;
//        }


//        public IdentityResult SetGroupRoles(string groupId, params string[] roleNames)
//        {
//            // Clear all the roles associated with this group:
//            var thisGroup = this.FindById(groupId);
//            thisGroup.BlobRoles.Clear();
//            _db.SaveChanges();

//            // Add the new roles passed in:
//            var newRoles = _roleManager.Roles.Where(r => roleNames.Any(n => n == r.Name));
//            foreach (var role in newRoles)
//            {
//                thisGroup.BlobRoles.Add(new BlobGroupRole { GroupId = groupId, RoleId = role.Id });
//            }
//            _db.SaveChanges();

//            // Reset the roles for all affected users:
//            foreach (var groupUser in thisGroup.BlobUsers)
//            {
//                this.RefreshUserGroupRoles(groupUser.UserId);
//            }
//            return IdentityResult.Success;
//        }


//        public async Task<IdentityResult> SetGroupRolesAsync(string groupId, params string[] roleNames)
//        {
//            // Clear all the roles associated with this group:
//            var thisGroup = await this.FindByIdAsync(groupId);
//            thisGroup.BlobRoles.Clear();
//            await _db.SaveChangesAsync();

//            // Add the new roles passed in:
//            var newRoles = _roleManager.Roles.Where(r => roleNames.Any(n => n == r.Name));
//            foreach (var role in newRoles)
//            {
//                thisGroup.BlobRoles.Add(new BlobGroupRole { GroupId = groupId, RoleId = role.Id });
//            }
//            await _db.SaveChangesAsync();

//            // Reset the roles for all affected users:
//            foreach (var groupUser in thisGroup.BlobUsers)
//            {
//                await this.RefreshUserGroupRolesAsync(groupUser.UserId);
//            }
//            return IdentityResult.Success;
//        }


//        public async Task<IdentityResult> SetUserGroupsAsync(string userId, params string[] groupIds)
//        {
//            // Clear current group membership:
//            var currentGroups = await this.GetUserGroupsAsync(userId);
//            foreach (var group in currentGroups)
//            {
//                group.BlobUsers
//                    .Remove(group.BlobUsers.FirstOrDefault(gr => gr.UserId == userId));
//            }
//            await _db.SaveChangesAsync();

//            // Add the user to the new groups:
//            foreach (string groupId in groupIds)
//            {
//                var newGroup = await this.FindByIdAsync(groupId);
//                newGroup.BlobUsers.Add(new BlobUserGroup { UserId = userId, GroupId = groupId });
//            }
//            await _db.SaveChangesAsync();

//            await this.RefreshUserGroupRolesAsync(userId);
//            return IdentityResult.Success;
//        }


//        public IdentityResult SetUserGroups(string userId, params string[] groupIds)
//        {
//            // Clear current group membership:
//            var currentGroups = this.GetUserGroups(userId);
//            foreach (var group in currentGroups)
//            {
//                group.BlobUsers
//                    .Remove(group.BlobUsers.FirstOrDefault(gr => gr.UserId == userId));
//            }
//            _db.SaveChanges();

//            // Add the user to the new groups:
//            foreach (string groupId in groupIds)
//            {
//                var newGroup = this.FindById(groupId);
//                newGroup.BlobUsers.Add(new BlobUserGroup { UserId = userId, GroupId = groupId });
//            }
//            _db.SaveChanges();

//            this.RefreshUserGroupRoles(userId);
//            return IdentityResult.Success;
//        }


//        public IdentityResult RefreshUserGroupRoles(string userId)
//        {
//            var user = _userManager.FindById(userId);
//            if (user == null)
//            {
//                throw new ArgumentNullException("User");
//            }
//            // Remove user from previous roles:
//            var oldUserRoles = _userManager.GetRoles(userId);
//            if (oldUserRoles.Count > 0)
//            {
//                _userManager.RemoveFromRoles(userId, oldUserRoles.ToArray());
//            }

//            // Find teh roles this user is entitled to from group membership:
//            var newGroupRoles = this.GetUserGroupRoles(userId);

//            // Get the damn role names:
//            var allRoles = _roleManager.Roles.ToList();
//            var addTheseRoles = allRoles.Where(r => newGroupRoles.Any(gr => gr.RoleId == r.Id));
//            var roleNames = addTheseRoles.Select(n => n.Name).ToArray();

//            // Add the user to the proper roles
//            _userManager.AddToRoles(userId, roleNames);

//            return IdentityResult.Success;
//        }


//        public async Task<IdentityResult> RefreshUserGroupRolesAsync(string userId)
//        {
//            var user = await _userManager.FindByIdAsync(userId);
//            if (user == null)
//            {
//                throw new ArgumentNullException("User");
//            }
//            // Remove user from previous roles:
//            var oldUserRoles = await _userManager.GetRolesAsync(userId);
//            if (oldUserRoles.Count > 0)
//            {
//                await _userManager.RemoveFromRolesAsync(userId, oldUserRoles.ToArray());
//            }

//            // Find the roles this user is entitled to from group membership:
//            var newGroupRoles = await this.GetUserGroupRolesAsync(userId);

//            // Get the damn role names:
//            var allRoles = await _roleManager.Roles.ToListAsync();
//            var addTheseRoles = allRoles.Where(r => newGroupRoles.Any(gr => gr.RoleId == r.Id));
//            var roleNames = addTheseRoles.Select(n => n.Name).ToArray();

//            // Add the user to the proper roles
//            await _userManager.AddToRolesAsync(userId, roleNames);

//            return IdentityResult.Success;
//        }


//        public async Task<IdentityResult> DeleteGroupAsync(string groupId)
//        {
//            var group = await this.FindByIdAsync(groupId);
//            if (group == null)
//            {
//                throw new ArgumentNullException("User");
//            }

//            var currentGroupMembers = (await this.GetGroupUsersAsync(groupId)).ToList();
//            // remove the roles from the group:
//            group.BlobRoles.Clear();

//            // Remove all the users:
//            group.BlobUsers.Clear();

//            // Remove the group itself:
//            _db.ApplicationGroups.Remove(group);

//            await _db.SaveChangesAsync();

//            // Reset all the user roles:
//            foreach (var user in currentGroupMembers)
//            {
//                await this.RefreshUserGroupRolesAsync(user.Id);
//            }
//            return IdentityResult.Success;
//        }


//        public IdentityResult DeleteGroup(string groupId)
//        {
//            var group = this.FindById(groupId);
//            if (group == null)
//            {
//                throw new ArgumentNullException("User");
//            }

//            var currentGroupMembers = this.GetGroupUsers(groupId).ToList();
//            // remove the roles from the group:
//            group.BlobRoles.Clear();

//            // Remove all the users:
//            group.BlobUsers.Clear();

//            // Remove the group itself:
//            _db.ApplicationGroups.Remove(group);

//            _db.SaveChanges();

//            // Reset all the user roles:
//            foreach (var user in currentGroupMembers)
//            {
//                this.RefreshUserGroupRoles(user.Id);
//            }
//            return IdentityResult.Success;
//        }


//        public async Task<IdentityResult> UpdateGroupAsync(BlobGroup group)
//        {
//            await _groupStore.UpdateAsync(group);
//            foreach (var groupUser in group.BlobUsers)
//            {
//                await this.RefreshUserGroupRolesAsync(groupUser.UserId);
//            }
//            return IdentityResult.Success;
//        }


//        public IdentityResult UpdateGroup(BlobGroup group)
//        {
//            _groupStore.Update(group);
//            foreach (var groupUser in group.BlobUsers)
//            {
//                this.RefreshUserGroupRoles(groupUser.UserId);
//            }
//            return IdentityResult.Success;
//        }


//        public IdentityResult ClearUserGroups(string userId)
//        {
//            return this.SetUserGroups(userId, new string[] { });
//        }


//        public async Task<IdentityResult> ClearUserGroupsAsync(string userId)
//        {
//            return await this.SetUserGroupsAsync(userId, new string[] { });
//        }


//        public async Task<IEnumerable<BlobGroup>> GetUserGroupsAsync(string userId)
//        {
//            var result = new List<BlobGroup>();
//            var userGroups = (from g in this.Groups
//                              where g.BlobUsers.Any(u => u.UserId == userId)
//                              select g).ToListAsync();
//            return await userGroups;
//        }


//        public IEnumerable<BlobGroup> GetUserGroups(string userId)
//        {
//            var result = new List<BlobGroup>();
//            var userGroups = (from g in this.Groups
//                              where g.BlobUsers.Any(u => u.UserId == userId)
//                              select g).ToList();
//            return userGroups;
//        }


//        public async Task<IEnumerable<BlobRole>> GetGroupRolesAsync(string groupId)
//        {
//            var grp = await _db.ApplicationGroups.FirstOrDefaultAsync(g => g.Id == groupId);
//            var roles = await _roleManager.Roles.ToListAsync();
//            var groupRoles = (from r in roles
//                              where grp.BlobRoles.Any(ap => ap.RoleId == r.Id)
//                              select r).ToList();
//            return groupRoles;
//        }


//        public IEnumerable<BlobRole> GetGroupRoles(string groupId)
//        {
//            var grp = _db.ApplicationGroups.FirstOrDefault(g => g.Id == groupId);
//            var roles = _roleManager.Roles.ToList();
//            var groupRoles = from r in roles
//                             where grp.BlobRoles.Any(ap => ap.RoleId == r.Id)
//                             select r;
//            return groupRoles;
//        }


//        public IEnumerable<BlobUser> GetGroupUsers(string groupId)
//        {
//            var group = this.FindById(groupId);
//            var users = new List<BlobUser>();
//            foreach (var groupUser in group.BlobUsers)
//            {
//                var user = _db.Users.Find(groupUser.UserId);
//                users.Add(user);
//            }
//            return users;
//        }


//        public async Task<IEnumerable<BlobUser>> GetGroupUsersAsync(string groupId)
//        {
//            var group = await this.FindByIdAsync(groupId);
//            var users = new List<BlobUser>();
//            foreach (var groupUser in group.BlobUsers)
//            {
//                var user = await _db.Users
//                    .FirstOrDefaultAsync(u => u.Id == groupUser.UserId);
//                users.Add(user);
//            }
//            return users;
//        }


//        public IEnumerable<BlobGroupRole> GetUserGroupRoles(string userId)
//        {
//            var userGroups = this.GetUserGroups(userId);
//            var userGroupRoles = new List<BlobGroupRole>();
//            foreach (var group in userGroups)
//            {
//                userGroupRoles.AddRange(group.BlobRoles.ToArray());
//            }
//            return userGroupRoles;
//        }


//        public async Task<IEnumerable<BlobGroupRole>> GetUserGroupRolesAsync(string userId)
//        {
//            var userGroups = await this.GetUserGroupsAsync(userId);
//            var userGroupRoles = new List<BlobGroupRole>();
//            foreach (var group in userGroups)
//            {
//                userGroupRoles.AddRange(group.BlobRoles.ToArray());
//            }
//            return userGroupRoles;
//        }


//        public async Task<BlobGroup> FindByIdAsync(string id)
//        {
//            return await _groupStore.FindByIdAsync(id);
//        }


//        public BlobGroup FindById(string id)
//        {
//            return _groupStore.FindById(id);
//        }
//    }
//}