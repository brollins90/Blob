using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Blob.Contracts.Models;
using Blob.Contracts.Models.ViewModels;
using Blob.Contracts.Services;
using Blob.Core.Models;
using EntityFramework.Extensions;
using log4net;

namespace Blob.Core.Services
{
    public class BlobCustomerGroupManager : ICustomerGroupService
    {
        private readonly ILog _log;
        private BlobDbContext _context;

        private bool _disposed;


        public BlobCustomerGroupManager(ILog log, BlobDbContext context)
        {
            _log = log;
            _log.Debug("Constructing BlobCustomerGroupManager");
            _context = context;
        }

        public DbSet<CustomerGroup> CustomerGroups { get { return _context.Set<CustomerGroup>(); } }
        public DbSet<CustomerGroupRole> CustomerGroupRoles { get { return _context.Set<CustomerGroupRole>(); } }
        public DbSet<CustomerGroupUser> CustomerGroupUsers { get { return _context.Set<CustomerGroupUser>(); } }
        public DbSet<Role> Roles { get { return _context.Set<Role>(); } }
        public DbSet<User> Users { get { return _context.Set<User>(); } }



        public async Task<BlobResult> CreateCustomerGroupAsync(CreateCustomerGroupDto dto)
        {
            _log.Debug(string.Format("CreateCustomerGroupAsync"));
            ThrowIfDisposed();
            CustomerGroup group = new CustomerGroup
                                  {
                                      CustomerId = dto.CustomerId,
                                      Description = dto.Description,
                                      Id = dto.GroupId,
                                      Name = dto.Name
                                  };
            CustomerGroups.Add(group);
            await _context.SaveChangesAsync().ConfigureAwait(false);
           
            return BlobResult.Success;
        }

        public async Task<BlobResult> DeleteCustomerGroupAsync(DeleteCustomerGroupDto dto)
        {
            _log.Debug(string.Format("DeleteCustomerGroupAsync"));
            ThrowIfDisposed();
            CustomerGroup group = CustomerGroups.Find(dto.GroupId);
            CustomerGroups.Remove(group);
            await _context.SaveChangesAsync().ConfigureAwait(false);
            
            //////CustomerGroup group = await _customerStore.FindGroupByIdAsync(groupId);
            //////if (group == null)
            //////{
            //////    // the task is done, just not the way they wanted...
            //////    return new BlobResultDto("Group was not found"){Succeeded = true};
            //////}

            ////var currentGroupMembers = (await this.GetGroupUsersAsync(groupId)).ToList();
            ////// remove the roles from the group:
            ////group.Roles.Clear();

            ////// Remove all the users:
            ////group.Users.Clear();

            //// Remove the group itself:
            //await _customerStore.DeleteGroupAsync(group);

            //// Reset all the user roles:
            ////foreach (var user in currentGroupMembers)
            ////{
            ////    await this.RefreshUserGroupRolesAsync(user.Id);
            ////}
            return BlobResult.Success;
        }

        public async Task<BlobResult> UpdateCustomerGroupAsync(UpdateCustomerGroupDto dto)
        {
            _log.Debug(string.Format("UpdateCustomerGroupAsync"));
            ThrowIfDisposed();

            // Find the group
            CustomerGroup group = CustomerGroups.Find(dto.GroupId);
            // make entity updates
            group.Name = dto.Name;
            group.Description = dto.Description;

            // todo check this may be a redundant save
            _context.Entry(group).State = EntityState.Modified;
            await _context.SaveChangesAsync().ConfigureAwait(false);

            // compare roles
            var beforeRoles = (await GetCustomerGroupRolesAsync(group.Id)).Select(x => x.RoleId);
            var nowRoles = dto.RolesIdStrings.Select(x => Guid.Parse(x));
            var removedRoles = beforeRoles.Except(nowRoles);
            var addedRoles = nowRoles.Except(beforeRoles);

            foreach (var roleId in removedRoles)
            {
                await RemoveRoleFromCustomerGroupAsync(new RemoveRoleFromCustomerGroupDto { GroupId = group.Id, RoleId = roleId });
            }
            foreach (var roleId in addedRoles)
            {
                await AddRoleToCustomerGroupAsync(new AddRoleToCustomerGroupDto { GroupId = group.Id, RoleId = roleId });
            }

            // compare users
            var beforeUsers = (await GetCustomerGroupUsersAsync(group.Id)).Select(x => x.UserId);
            var nowUsers = dto.UsersIdStrings.Select(x => Guid.Parse(x));
            var removedUsers = beforeUsers.Except(nowUsers);
            var addedUsers = nowUsers.Except(beforeUsers);

            foreach (var userId in removedUsers)
            {
                await RemoveUserFromCustomerGroupAsync(new RemoveUserFromCustomerGroupDto { GroupId = group.Id, UserId = userId });
            }
            foreach (var userId in addedUsers)
            {
                await AddUserToCustomerGroupAsync(new AddUserToCustomerGroupDto { GroupId = group.Id, UserId = userId });
            }

            // or is soething like this better???
            //foreach (var groupUser in group.Users)
            //{
            //    await this.RefreshUserGroupRolesAsync(groupUser.UserId);
            //}
            return BlobResult.Success;
        }

        public async Task<BlobResult> AddRoleToCustomerGroupAsync(AddRoleToCustomerGroupDto dto)
        {
            _log.Debug(string.Format("AddRoleToCustomerGroupAsync"));
            ThrowIfDisposed();
            CustomerGroup group = CustomerGroups.Find(dto.GroupId);
            Role role = Roles.Find(dto.RoleId);
        
            var ur = new CustomerGroupRole { GroupId = group.Id, RoleId = role.Id };
            CustomerGroupRoles.Add(ur);
            await _context.SaveChangesAsync();
        
            return BlobResult.Success;
        }


        public async Task<BlobResult> AddUserToCustomerGroupAsync(AddUserToCustomerGroupDto dto)
        {
            _log.Debug(string.Format("AddUserToCustomerGroupAsync"));
            ThrowIfDisposed();
            CustomerGroup group = CustomerGroups.Find(dto.GroupId);
            var ur = new CustomerGroupUser { GroupId = group.Id, UserId = dto.UserId };
            _context.Set<CustomerGroupUser>().Add(ur);
            await _context.SaveChangesAsync();

            return BlobResult.Success;
        }

        public async Task<BlobResult> RemoveRoleFromCustomerGroupAsync(RemoveRoleFromCustomerGroupDto dto)
        {
            _log.Debug(string.Format("RemoveRoleFromCustomerGroupAsync"));
            ThrowIfDisposed();
            CustomerGroup group = CustomerGroups.Find(dto.GroupId);
            CustomerGroupRole cgr = _context.Set<CustomerGroupRole>().FirstOrDefault(r => dto.RoleId.Equals(r.RoleId) && r.GroupId.Equals(group.Id));
            _context.Set<CustomerGroupRole>().Remove(cgr);
            await _context.SaveChangesAsync();

            return BlobResult.Success;
        }

        public async Task<BlobResult> RemoveUserFromCustomerGroupAsync(RemoveUserFromCustomerGroupDto dto)
        {
            _log.Debug(string.Format("RemoveUserFromCustomerGroupAsync"));
            ThrowIfDisposed();
            CustomerGroup group = CustomerGroups.Find(dto.GroupId);
            CustomerGroupUser cgr = _context.Set<CustomerGroupUser>().FirstOrDefault(r => dto.UserId.Equals(r.UserId) && r.GroupId.Equals(group.Id));
            _context.Set<CustomerGroupUser>().Remove(cgr);
            await _context.SaveChangesAsync();

            return BlobResult.Success;
        }

        public async Task<CustomerGroupCreateVm> GetCustomerGroupCreateVmAsync(Guid customerId)
        {
            _log.Debug(string.Format("GetCustomerGroupCreateVmAsync({0})", customerId));
            ThrowIfDisposed();
            var roles = await GetCustomerRolesAsync(customerId);
            return new CustomerGroupCreateVm
            {
                CustomerId = customerId,
                GroupId = Guid.NewGuid(),
                AvailableRoles = roles
            };
        }

        public async Task<CustomerGroupDeleteVm> GetCustomerGroupDeleteVmAsync(Guid groupId)
        {
            _log.Debug(string.Format("GetCustomerGroupDeleteVmAsync({0})", groupId));
            ThrowIfDisposed();
            CustomerGroup group = await CustomerGroups.FindAsync(groupId);
            return new CustomerGroupDeleteVm
            {
                GroupId = group.Id,
                Name = group.Name
            };
        }
        public async Task<CustomerGroupPageVm> GetCustomerGroupPageVmAsync(Guid customerId, int pageNum, int pageSize)
        {
            _log.Debug(string.Format("GetCustomerGroupPageVmAsync({0}, {1}, {2})", customerId, pageNum, pageSize));
            var pNum = pageNum < 1 ? 0 : pageNum - 1;

            var count = CustomerGroups.Where(x => x.CustomerId.Equals(customerId)).FutureCount();
            var groups = CustomerGroups
                .Where(x => x.CustomerId.Equals(customerId))
                .OrderByDescending(x => x.Name)
                .Skip(pNum * pageSize).Take(pageSize).Future();

            // define future queries before any of them execute
            var pCount = ((count / pageSize) + (count % pageSize) == 0 ? 0 : 1);
            return await Task.FromResult(new CustomerGroupPageVm
            {
                TotalCount = count,
                PageCount = pCount,
                PageNum = pNum + 1,
                PageSize = pageSize,
                Items = groups.Select(x => new CustomerGroupListItemVm
                {
                    GroupId = x.Id,
                    Name = x.Name
                }),
            }).ConfigureAwait(false);
        }

        public async Task<CustomerGroupSingleVm> GetCustomerGroupSingleVmAsync(Guid groupId)
        {
            _log.Debug(string.Format("GetCustomerGroupSingleVmAsync({0})", groupId));
            ThrowIfDisposed();
            CustomerGroup group = CustomerGroups.Find(groupId);
            var roles = await GetCustomerGroupRolesAsync(groupId);
            var users = await GetCustomerGroupUsersAsync(groupId);
            return new CustomerGroupSingleVm
            {
                CustomerId = group.CustomerId,
                Description = group.Description,
                GroupId = group.Id,
                Name = group.Name,
                RoleCount = roles.Count(),
                Roles = roles,
                UserCount = users.Count(),
                Users = users
            };
        }

        public async Task<CustomerGroupUpdateVm> GetCustomerGroupUpdateVmAsync(Guid groupId)
        {
            _log.Debug(string.Format("GetCustomerGroupUpdateVmAsync({0})", groupId));
            ThrowIfDisposed();
            CustomerGroup group = await CustomerGroups.FindAsync(groupId);
            return new CustomerGroupUpdateVm
            {
                CustomerId = group.CustomerId,
                Description = group.Description,
                GroupId = group.Id,
                Name = group.Name
            };
        }

        public async Task<IEnumerable<CustomerGroupRoleListItem>> GetCustomerRolesAsync(Guid groupId)
        {
            _log.Debug(string.Format("GetCustomerRolesAsync({0})", groupId));
            ThrowIfDisposed();
            return await _context.Roles.Select(x => new CustomerGroupRoleListItem { Name = x.Name, RoleId = x.Id }).ToListAsync();
        }

        public async Task<IEnumerable<CustomerGroupRoleListItem>> GetCustomerGroupRolesAsync(Guid groupId)
        {
            _log.Debug(string.Format("GetCustomerGroupRolesAsync({0})", groupId));
            ThrowIfDisposed();
            var query = from gr in CustomerGroupRoles
                        where gr.GroupId.Equals(groupId)
                        join role in Roles on gr.RoleId equals role.Id
                        select new CustomerGroupRoleListItem { Name = role.Name, RoleId = role.Id };
            return await query.ToListAsync();
        }

        public async Task<IEnumerable<CustomerGroupUserListItem>> GetCustomerGroupUsersAsync(Guid groupId)
        {
            _log.Debug(string.Format("GetCustomerGroupUsersAsync({0})", groupId));
            ThrowIfDisposed();
            var query = from gr in CustomerGroupUsers
                        where gr.GroupId.Equals(groupId)
                        join user in Users on gr.UserId equals user.Id
                        select new CustomerGroupUserListItem { UserId = user.Id, UserName = user.UserName };
            return await query.ToListAsync();
        }

        //public async Task<CustomerGroup> FindGroupByIdAsync(Guid groupId)
        //{
        //    return await _customerStore.FindCustomerGroupByIdAsync(groupId);
        //}



        



        ////private async Task<BlobResultDto> SetGroupRolesAsync(Guid groupId, params string[] roleNames)
        ////{
        ////    // Clear all the roles associated with this group:
        ////    CustomerGroup thisGroup = await this.FindGroupByIdAsync(groupId);
        ////    thisGroup.Roles.Clear();
        ////    await _db.SaveChangesAsync();

        ////    // Add the new roles passed in:
        ////    var newRoles = _roleManager.Roles.Where(r => roleNames.Any(n => n == r.Name));
        ////    foreach (var role in newRoles)
        ////    {
        ////        thisGroup.Roles.Add(new CustomerGroupRole { GroupId = groupId, RoleId = role.Id });
        ////    }
        ////    await _db.SaveChangesAsync();

        ////    //// Reset the roles for all affected users:
        ////    //foreach (var groupUser in thisGroup.Users)
        ////    //{
        ////    //    await this.RefreshUserGroupRolesAsync(groupUser.UserId);
        ////    //}
        ////    return BlobResultDto.Success;
        ////}


        ////public async Task<IdentityResult> ClearUserGroupsAsync(Guid userId)
        ////{
        ////    return await this.SetUserGroupsAsync(userId, new Guid[] { });
        ////}

        ////public async Task<IEnumerable<CustomerGroupRole>> GetUserGroupRolesAsync(Guid userId)
        ////{
        ////    var userGroups = await this.GetUserGroupsAsync(userId).ConfigureAwait(true);
        ////    var userGroupRoles = new List<CustomerGroupRole>();
        ////    foreach (var group in userGroups)
        ////    {
        ////        userGroupRoles.AddRange(group.Roles.ToArray());
        ////    }
        ////    return userGroupRoles;
        ////}

        ////public async Task<IEnumerable<CustomerGroup>> GetUserGroupsAsync(Guid userId)
        ////{
        ////    var result = new List<CustomerGroup>();
        ////    var userGroups = (from g in this.Groups
        ////                      where g.Users.Any(u => u.UserId == userId)
        ////                      select g).ToListAsync();
        ////    return await userGroups;
        ////}

        ////public async Task<BlobResultDto> RefreshUserGroupRolesAsync(Guid userId)
        ////{
        ////    //var user = await _userManager.FindByIdAsync(userId);
        ////    //if (user == null)
        ////    //{
        ////    //    throw new ArgumentNullException("User");
        ////    //}
        ////    //// Remove user from previous roles:
        ////    //var oldUserRoles = await _userManager.GetRolesAsync(userId);
        ////    //if (oldUserRoles.Count > 0)
        ////    //{
        ////    //    await _userManager.RemoveFromRolesAsync(userId, oldUserRoles.ToArray());
        ////    //}

        ////    //// Find the roles this user is entitled to from group membership:
        ////    //var newGroupRoles = await this.GetUserGroupRolesAsync(userId);

        ////    //// Get the damn role names:
        ////    //var allRoles = await _roleManager.Roles.ToListAsync();
        ////    //var addTheseRoles = allRoles.Where(r => newGroupRoles.Any(gr => gr.RoleId == r.Id));
        ////    //var roleNames = addTheseRoles.Select(n => n.Name).ToArray();

        ////    //// Add the user to the proper roles
        ////    //await _userManager.AddToRolesAsync(userId, roleNames);

        ////    return BlobResultDto.Success;
        ////}

        ////public async Task<BlobResultDto> SetGroupRolesAsync(Guid groupId, params string[] roleNames)
        ////{
        ////    // Clear all the roles associated with this group:
        ////    CustomerGroup thisGroup = await this.FindGroupByIdAsync(groupId);
        ////    thisGroup.Roles.Clear();
        ////    await _db.SaveChangesAsync();

        ////    // Add the new roles passed in:
        ////    var newRoles = _roleManager.Roles.Where(r => roleNames.Any(n => n == r.Name));
        ////    foreach (var role in newRoles)
        ////    {
        ////        thisGroup.Roles.Add(new CustomerGroupRole { GroupId = groupId, RoleId = role.Id });
        ////    }
        ////    await _db.SaveChangesAsync();

        ////    // Reset the roles for all affected users:
        ////    foreach (var groupUser in thisGroup.Users)
        ////    {
        ////        await this.RefreshUserGroupRolesAsync(groupUser.UserId);
        ////    }
        ////    return BlobResultDto.Success;
        ////}

        ////public async Task<BlobResultDto> SetUserGroupsAsync(Guid userId, params Guid[] groupIds)
        ////{
        ////    // Clear current group membership:
        ////    var currentGroups = await this.GetUserGroupsAsync(userId);
        ////    foreach (var group in currentGroups)
        ////    {
        ////        group.Users.Remove(group.Users.FirstOrDefault(gr => gr.UserId == userId));
        ////    }
        ////    await _db.SaveChangesAsync();

        ////    // Add the user to the new groups:
        ////    foreach (var groupId in groupIds)
        ////    {
        ////        var newGroup = await this.FindGroupByIdAsync(groupId);
        ////        newGroup.Users.Add(new CustomerGroupUser { UserId = userId, GroupId = groupId });
        ////    }
        ////    await _db.SaveChangesAsync();

        ////    await this.RefreshUserGroupRolesAsync(userId);
        ////    return BlobResultDto.Success;
        ////}





        public bool DisposeContext { get; set; }

        private void ThrowIfDisposed()
        {
            if (_disposed)
                throw new ObjectDisposedException(GetType().Name);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (DisposeContext && disposing && _context != null)
            {
                _context.Dispose();
            }
            _disposed = true;
            _context = null;
            //_customerStore = null;
            //_customerGroupStore = null;
            //_userStore = null;
            //_roleStore = null;
        }


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