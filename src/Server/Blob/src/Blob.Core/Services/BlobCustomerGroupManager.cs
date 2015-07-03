namespace Blob.Core.Services
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using Common.Services;
    using Contracts.Request;
    using Contracts.Response;
    using Contracts.ViewModel;
    using Models;
    using EntityFramework.Extensions;
    using log4net;

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



        public async Task<BlobResult> CreateCustomerGroupAsync(CreateCustomerGroupRequest dto)
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

        public async Task<BlobResult> DeleteCustomerGroupAsync(DeleteCustomerGroupRequest dto)
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

        public async Task<BlobResult> UpdateCustomerGroupAsync(UpdateCustomerGroupRequest dto)
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
                await RemoveRoleFromCustomerGroupAsync(new RemoveRoleFromCustomerGroupRequest { GroupId = group.Id, RoleId = roleId });
            }
            foreach (var roleId in addedRoles)
            {
                await AddRoleToCustomerGroupAsync(new AddRoleToCustomerGroupRequest { GroupId = group.Id, RoleId = roleId });
            }

            // compare users
            var beforeUsers = (await GetCustomerGroupUsersAsync(group.Id)).Select(x => x.UserId);
            var nowUsers = dto.UsersIdStrings.Select(x => Guid.Parse(x));
            var removedUsers = beforeUsers.Except(nowUsers);
            var addedUsers = nowUsers.Except(beforeUsers);

            foreach (var userId in removedUsers)
            {
                await RemoveUserFromCustomerGroupAsync(new RemoveUserFromCustomerGroupRequest { GroupId = group.Id, UserId = userId });
            }
            foreach (var userId in addedUsers)
            {
                await AddUserToCustomerGroupAsync(new AddUserToCustomerGroupRequest { GroupId = group.Id, UserId = userId });
            }

            // or is soething like this better???
            //foreach (var groupUser in group.Users)
            //{
            //    await this.RefreshUserGroupRolesAsync(groupUser.UserId);
            //}
            return BlobResult.Success;
        }

        public async Task<BlobResult> AddRoleToCustomerGroupAsync(AddRoleToCustomerGroupRequest dto)
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


        public async Task<BlobResult> AddUserToCustomerGroupAsync(AddUserToCustomerGroupRequest dto)
        {
            _log.Debug(string.Format("AddUserToCustomerGroupAsync"));
            ThrowIfDisposed();
            CustomerGroup group = CustomerGroups.Find(dto.GroupId);
            var ur = new CustomerGroupUser { GroupId = group.Id, UserId = dto.UserId };
            _context.Set<CustomerGroupUser>().Add(ur);
            await _context.SaveChangesAsync();

            return BlobResult.Success;
        }

        public async Task<BlobResult> RemoveRoleFromCustomerGroupAsync(RemoveRoleFromCustomerGroupRequest dto)
        {
            _log.Debug(string.Format("RemoveRoleFromCustomerGroupAsync"));
            ThrowIfDisposed();
            CustomerGroup group = CustomerGroups.Find(dto.GroupId);
            CustomerGroupRole cgr = _context.Set<CustomerGroupRole>().FirstOrDefault(r => dto.RoleId.Equals(r.RoleId) && r.GroupId.Equals(group.Id));
            _context.Set<CustomerGroupRole>().Remove(cgr);
            await _context.SaveChangesAsync();

            return BlobResult.Success;
        }

        public async Task<BlobResult> RemoveUserFromCustomerGroupAsync(RemoveUserFromCustomerGroupRequest dto)
        {
            _log.Debug(string.Format("RemoveUserFromCustomerGroupAsync"));
            ThrowIfDisposed();
            CustomerGroup group = CustomerGroups.Find(dto.GroupId);
            CustomerGroupUser cgr = _context.Set<CustomerGroupUser>().FirstOrDefault(r => dto.UserId.Equals(r.UserId) && r.GroupId.Equals(group.Id));
            _context.Set<CustomerGroupUser>().Remove(cgr);
            await _context.SaveChangesAsync();

            return BlobResult.Success;
        }

        public async Task<CustomerGroupCreateViewModel> GetCustomerGroupCreateViewModelAsync(Guid customerId)
        {
            _log.Debug(string.Format("GetCustomerGroupCreateVmAsync({0})", customerId));
            ThrowIfDisposed();
            var roles = await GetCustomerRolesAsync(customerId);
            return new CustomerGroupCreateViewModel
            {
                CustomerId = customerId,
                GroupId = Guid.NewGuid(),
                AvailableRoles = roles
            };
        }

        public async Task<CustomerGroupDeleteViewModel> GetCustomerGroupDeleteViewModelAsync(Guid groupId)
        {
            _log.Debug(string.Format("GetCustomerGroupDeleteVmAsync({0})", groupId));
            ThrowIfDisposed();
            CustomerGroup group = await CustomerGroups.FindAsync(groupId);
            return new CustomerGroupDeleteViewModel
            {
                GroupId = group.Id,
                Name = group.Name
            };
        }
        public async Task<CustomerGroupPageViewModel> GetCustomerGroupPageViewModelAsync(Guid customerId, int pageNum, int pageSize)
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
            return await Task.FromResult(new CustomerGroupPageViewModel
            {
                TotalCount = count,
                PageCount = pCount,
                PageNum = pNum + 1,
                PageSize = pageSize,
                Items = groups.Select(x => new CustomerGroupListItem
                {
                    GroupId = x.Id,
                    Name = x.Name
                }),
            }).ConfigureAwait(false);
        }

        public async Task<CustomerGroupSingleViewModel> GetCustomerGroupSingleViewModelAsync(Guid groupId)
        {
            _log.Debug(string.Format("GetCustomerGroupSingleVmAsync({0})", groupId));
            ThrowIfDisposed();
            CustomerGroup group = CustomerGroups.Find(groupId);
            var roles = await GetCustomerGroupRolesAsync(groupId);
            var users = await GetCustomerGroupUsersAsync(groupId);
            return new CustomerGroupSingleViewModel
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

        public async Task<CustomerGroupUpdateViewModel> GetCustomerGroupUpdateViewModelAsync(Guid groupId)
        {
            _log.Debug(string.Format("GetCustomerGroupUpdateVmAsync({0})", groupId));
            ThrowIfDisposed();
            CustomerGroup group = await CustomerGroups.FindAsync(groupId);
            return new CustomerGroupUpdateViewModel
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
        }
    }
}