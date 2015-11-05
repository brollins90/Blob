//using System;
//using System.Collections.Generic;
//using System.Data.Entity;
//using System.Globalization;
//using System.Linq;
//using System.Threading.Tasks;
//using Blob.Core.Models;

//namespace Blob.Core.Identity.Store
//{
//    public class BlobCustomerStore : ICustomerStore, ICustomerGroupStore, ICustomerGroupRoleStore, ICustomerGroupUserStore
//    {
//        private bool _disposed;

//        private GenericEntityStore<Customer> _customerStore;
//        private GenericEntityStore<CustomerGroup> _customerGroupStore;
//        private readonly IDbSet<CustomerGroupRole> _customerGroupRoles;
//        private readonly IDbSet<CustomerGroupUser> _customerGroupUsers;
//        private GenericEntityStore<Role> _roleStore;
//        private GenericEntityStore<User> _userStore;

//        public BlobCustomerStore(DbContext context)
//        {
//            if (context == null)
//                throw new ArgumentNullException("context");

//            Context = context;
//            _customerStore = new GenericEntityStore<Customer>(context);
//            _customerGroupStore = new GenericEntityStore<CustomerGroup>(context);
//            _customerGroupRoles = Context.Set<CustomerGroupRole>();
//            _customerGroupUsers = Context.Set<CustomerGroupUser>();
//            _roleStore = new GenericEntityStore<Role>(context);
//            _userStore = new GenericEntityStore<User>(context);
//        }

//        public IQueryable<Customer> Customers { get { return _customerStore.EntitySet; } }
//        public IQueryable<CustomerGroup> Groups { get { return _customerGroupStore.EntitySet; } }

//        public DbContext Context { get; private set; }

//        public virtual async Task CreateCustomerAsync(Customer customer)
//        {
//            ThrowIfDisposed();
//            if (customer == null)
//                throw new ArgumentNullException("customer");

//            _customerStore.Create(customer);
//            await Context.SaveChangesAsync();
//        }

//        public virtual async Task DeleteCustomerAsync(Customer customer)
//        {
//            ThrowIfDisposed();
//            if (customer == null)
//                throw new ArgumentNullException("customer");

//            _customerStore.Delete(customer);
//            await Context.SaveChangesAsync();
//        }

//        public Task<Customer> FindCustomerByIdAsync(Guid customerId)
//        {
//            ThrowIfDisposed();
//            return _customerStore.GetByIdAsync(customerId);
//        }

//        public Task<Customer> FindCustomerByNameAsync(string customerName)
//        {
//            ThrowIfDisposed();
//            return _customerStore.EntitySet.FirstOrDefaultAsync(u => u.Name.ToUpper().Equals(customerName.ToUpper()));
//        }

//        public virtual async Task UpdateCustomerAsync(Customer customer)
//        {
//            ThrowIfDisposed();
//            if (customer == null)
//                throw new ArgumentNullException("customer");

//            _customerStore.Update(customer);
//            await Context.SaveChangesAsync();
//        }


//        public bool DisposeContext { get; set; }

//        private void ThrowIfDisposed()
//        {
//            if (_disposed)
//                throw new ObjectDisposedException(GetType().Name);
//        }

//        public void Dispose()
//        {
//            Dispose(true);
//            GC.SuppressFinalize(this);
//        }

//        protected virtual void Dispose(bool disposing)
//        {
//            if (DisposeContext && disposing && Context != null)
//            {
//                Context.Dispose();
//            }
//            _disposed = true;
//            Context = null;
//            _customerStore = null;
//            _customerGroupStore = null;
//            _userStore = null;
//            _roleStore = null;
//        }

//        public async Task CreateCustomerGroupAsync(CustomerGroup group)
//        {
//            ThrowIfDisposed();
//            if (group == null)
//                throw new ArgumentNullException("group");

//            _customerGroupStore.Create(group);
//            await Context.SaveChangesAsync();
//        }

//        public async Task DeleteCustomerGroupAsync(CustomerGroup group)
//        {
//            ThrowIfDisposed();
//            if (group == null)
//                throw new ArgumentNullException("group");

//            _customerGroupStore.Delete(group);
//            await Context.SaveChangesAsync();
//        }

//        public async Task<CustomerGroup> FindCustomerGroupByIdAsync(Guid groupId)
//        {
//            ThrowIfDisposed();
//            return await _customerGroupStore.GetByIdAsync(groupId);
//        }

//        public async Task UpdateCustomerGroupAsync(CustomerGroup group)
//        {
//            ThrowIfDisposed();
//            if (group == null)
//                throw new ArgumentNullException("group");

//            _customerGroupStore.Update(group);
//            await Context.SaveChangesAsync();
//        }

//        public async Task AddRoleToGroupAsync(CustomerGroup group, string roleName)
//        {
//            ThrowIfDisposed();
//            if (group == null)
//            {
//                throw new ArgumentNullException("group");
//            }
//            if (String.IsNullOrWhiteSpace(roleName))
//            {
//                throw new ArgumentException("IdentityResources.ValueCannotBeNullOrEmpty", "roleName");
//            }
//            var roleEntity = await _roleStore.DbEntitySet.SingleOrDefaultAsync(r => r.Name.ToUpper() == roleName.ToUpper());
//            if (roleEntity == null)
//            {
//                throw new InvalidOperationException(String.Format(CultureInfo.CurrentCulture, "IdentityResources.RoleNotFound", roleName));
//            }

//            var ur = new CustomerGroupRole { GroupId = group.Id, RoleId = roleEntity.Id };
//            _customerGroupRoles.Add(ur);
//            await Context.SaveChangesAsync();
//        }

//        public async Task AddRoleToGroupAsync(CustomerGroup group, Guid roleId)
//        {
//            ThrowIfDisposed();
//            if (group == null)
//            {
//                throw new ArgumentNullException("group");
//            }
//            var roleEntity = await _roleStore.DbEntitySet.SingleOrDefaultAsync(r => r.Id == roleId);
//            if (roleEntity == null)
//            {
//                throw new InvalidOperationException(String.Format(CultureInfo.CurrentCulture, "IdentityResources.RoleNotFound", roleId));
//            }

//            var ur = new CustomerGroupRole { GroupId = group.Id, RoleId = roleEntity.Id };
//            _customerGroupRoles.Add(ur);
//            await Context.SaveChangesAsync();
//        }

//        public async Task RemoveRoleFromGroupAsync(CustomerGroup group, string roleName)
//        {
//            ThrowIfDisposed();
//            if (group == null)
//            {
//                throw new ArgumentNullException("group");
//            }
//            if (String.IsNullOrWhiteSpace(roleName))
//            {
//                throw new ArgumentException("IdentityResources.ValueCannotBeNullOrEmpty", "roleName");
//            }
//            var roleEntity = await _roleStore.DbEntitySet.SingleOrDefaultAsync(r => r.Name.ToUpper() == roleName.ToUpper());
//            if (roleEntity != null)
//            {
//                var roleId = roleEntity.Id;
//                var groupId = group.Id;
//                var userRole = await _customerGroupRoles.FirstOrDefaultAsync(r => roleId.Equals(r.RoleId) && r.GroupId.Equals(groupId));
//                if (userRole != null)
//                {
//                    _customerGroupRoles.Remove(userRole);
//                }
//            }
//            await Context.SaveChangesAsync();
//        }

//        public async Task RemoveRoleFromGroupAsync(CustomerGroup group, Guid roleId)
//        {
//            ThrowIfDisposed();
//            if (group == null)
//            {
//                throw new ArgumentNullException("group");
//            }
//            var userRole = await _customerGroupRoles.FirstOrDefaultAsync(r => roleId.Equals(r.RoleId) && r.GroupId.Equals(group.Id));
//            if (userRole != null)
//            {
//                _customerGroupRoles.Remove(userRole);
//            }
//            await Context.SaveChangesAsync();
//        }

//        public async Task<IList<Role>> GetRolesForGroupAsync(Guid groupId)
//        {
//            ThrowIfDisposed();
//            var query = from gr in _customerGroupRoles
//                        where gr.GroupId.Equals(groupId)
//                        join role in _roleStore.DbEntitySet on gr.RoleId equals role.Id
//                        select role;
//            return await query.ToListAsync();
//        }

//        public async Task<bool> HasRoleAsync(CustomerGroup group, string roleName)
//        {
//            ThrowIfDisposed();
//            if (group == null)
//            {
//                throw new ArgumentNullException("group");
//            }
//            if (String.IsNullOrWhiteSpace(roleName))
//            {
//                throw new ArgumentException("IdentityResources.ValueCannotBeNullOrEmpty", "roleName");
//            }
//            var role = await _roleStore.DbEntitySet.SingleOrDefaultAsync(r => r.Name.ToUpper() == roleName.ToUpper());
//            if (role != null)
//            {
//                var groupId = group.Id;
//                var roleId = role.Id;
//                return await _customerGroupRoles.AnyAsync(ur => ur.RoleId.Equals(roleId) && ur.GroupId.Equals(groupId));
//            }
//            return false;
//        }

//        public async Task AddUserToGroupAsync(CustomerGroup group, Guid userId)
//        {
//            ThrowIfDisposed();
//            if (group == null)
//            {
//                throw new ArgumentNullException("group");
//            }
//            var user = await _userStore.GetByIdAsync(userId);

//            if (user == null)
//            {
//                throw new InvalidOperationException(String.Format(CultureInfo.CurrentCulture, "IdentityResources.UserNotFound", ""));
//            }

//            var ur = new CustomerGroupUser { GroupId = group.Id, UserId = user.Id };
//            _customerGroupUsers.Add(ur);
//            await Context.SaveChangesAsync();
//        }

//        public async Task RemoveUserFromGroupAsync(CustomerGroup group, Guid userId)
//        {
//            ThrowIfDisposed();
//            if (group == null)
//            {
//                throw new ArgumentNullException("group");
//            }
//            var userRole = await _customerGroupUsers.FirstOrDefaultAsync(x => userId.Equals(x.UserId) && x.GroupId.Equals(group.Id));
//            if (userRole != null)
//            {
//                _customerGroupUsers.Remove(userRole);
//            }
//            await Context.SaveChangesAsync();
//        }

//        public async Task<IList<User>> GetUsersInGroupAsync(Guid groupId)
//        {
//            ThrowIfDisposed();
//            var query = from gr in _customerGroupUsers
//                        where gr.GroupId.Equals(groupId)
//                        join user in _userStore.DbEntitySet on gr.UserId equals user.Id
//                        select user;
//            return await query.ToListAsync();
//        }

//        public async Task<bool> HasUserAsync(CustomerGroup group, Guid userId)
//        {
//            ThrowIfDisposed();
//            var users = await GetUsersInGroupAsync(group.Id);
//            return users.Any(x => x.Id.Equals(userId));
//        }
//    }
//}
