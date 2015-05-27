using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Blob.Core.Models;

namespace Blob.Core.Identity.Store
{
    public class BlobCustomerStore : ICustomerStore, ICustomerGroupStore, ICustomerGroupRoleStore, ICustomerGroupUserStore
    {
        private bool _disposed;

        private readonly GenericEntityStore<Customer> _customerStore;
        private readonly GenericEntityStore<CustomerGroup> _customerGroupStore;
        private readonly IDbSet<CustomerGroupRole> _customerGroupRoles;
        private readonly IDbSet<CustomerGroupUser> _customerGroupUsers;
        private readonly GenericEntityStore<Role> _roleStore;
        private GenericEntityStore<User> _userStore;

        public BlobCustomerStore(DbContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            Context = context;
            _customerStore = new GenericEntityStore<Customer>(context);
            _customerGroupStore = new GenericEntityStore<CustomerGroup>(context);
            _customerGroupRoles = Context.Set<CustomerGroupRole>();
            _customerGroupUsers = Context.Set<CustomerGroupUser>();
            _roleStore = new GenericEntityStore<Role>(context);
            _userStore = new GenericEntityStore<User>(context);
        }

        public IQueryable<Customer> Customers { get { return _customerStore.EntitySet; } }
        public IQueryable<CustomerGroup> Groups { get { return _customerGroupStore.EntitySet; } }

        public DbContext Context { get; private set; }

        public virtual async Task CreateCustomerAsync(Customer customer)
        {
            ThrowIfDisposed();
            if (customer == null)
                throw new ArgumentNullException("customer");

            _customerStore.Create(customer);
            await Context.SaveChangesAsync().ConfigureAwait(false);
        }

        public virtual async Task DeleteCustomerAsync(Customer customer)
        {
            ThrowIfDisposed();
            if (customer == null)
                throw new ArgumentNullException("customer");

            _customerStore.Delete(customer);
            await Context.SaveChangesAsync().ConfigureAwait(false);
        }

        public Task<Customer> FindCustomerByIdAsync(Guid customerId)
        {
            ThrowIfDisposed();
            return _customerStore.GetByIdAsync(customerId);
        }

        public Task<Customer> FindCustomerByNameAsync(string customerName)
        {
            ThrowIfDisposed();
            return _customerStore.EntitySet.FirstOrDefaultAsync(u => u.Name.ToUpper().Equals(customerName.ToUpper()));
        }

        public virtual async Task UpdateCustomerAsync(Customer customer)
        {
            ThrowIfDisposed();
            if (customer == null)
                throw new ArgumentNullException("customer");

            _customerStore.Update(customer);
            await Context.SaveChangesAsync().ConfigureAwait(false);
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
            if (DisposeContext && disposing && Context != null)
            {
                Context.Dispose();
            }
            _disposed = true;
            Context = null;
            //_customerStore = null;
        }

        public async Task CreateGroupAsync(CustomerGroup group)
        {
            ThrowIfDisposed();
            if (group == null)
                throw new ArgumentNullException("group");

            _customerGroupStore.Create(group);
            await Context.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task DeleteGroupAsync(CustomerGroup group)
        {
            ThrowIfDisposed();
            if (group == null)
                throw new ArgumentNullException("group");

            _customerGroupStore.Delete(group);
            await Context.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task<CustomerGroup> FindGroupByIdAsync(Guid groupId)
        {
            ThrowIfDisposed();
            return await _customerGroupStore.GetByIdAsync(groupId);
        }

        public async Task UpdateGroupAsync(CustomerGroup group)
        {
            ThrowIfDisposed();
            if (group == null)
                throw new ArgumentNullException("group");

            _customerGroupStore.Update(group);
            await Context.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task AddRoleAsync(CustomerGroup group, string roleName)
        {
            ThrowIfDisposed();
            if (group == null)
            {
                throw new ArgumentNullException("group");
            }
            if (String.IsNullOrWhiteSpace(roleName))
            {
                throw new ArgumentException("IdentityResources.ValueCannotBeNullOrEmpty", "roleName");
            }
            var roleEntity = await _roleStore.DbEntitySet.SingleOrDefaultAsync(r => r.Name.ToUpper() == roleName.ToUpper());
            if (roleEntity == null)
            {
                throw new InvalidOperationException(String.Format(CultureInfo.CurrentCulture, "IdentityResources.RoleNotFound", roleName));
            }

            var ur = new CustomerGroupRole {GroupId = group.Id, RoleId = roleEntity.Id };
            _customerGroupRoles.Add(ur);
        }

        public async Task RemoveRoleAsync(CustomerGroup group, string roleName)
        {
            ThrowIfDisposed();
            if (group == null)
            {
                throw new ArgumentNullException("group");
            }
            if (String.IsNullOrWhiteSpace(roleName))
            {
                throw new ArgumentException("IdentityResources.ValueCannotBeNullOrEmpty", "roleName");
            }
            var roleEntity = await _roleStore.DbEntitySet.SingleOrDefaultAsync(r => r.Name.ToUpper() == roleName.ToUpper());
            if (roleEntity != null)
            {
                var roleId = roleEntity.Id;
                var groupId = group.Id;
                var userRole = await _customerGroupRoles.FirstOrDefaultAsync(r => roleId.Equals(r.RoleId) && r.GroupId.Equals(groupId));
                if (userRole != null)
                {
                    _customerGroupRoles.Remove(userRole);
                }
            }
        }

        public async Task<IList<string>> GetRolesAsync(CustomerGroup group)
        {
            ThrowIfDisposed();
            if (group == null)
            {
                throw new ArgumentNullException("group");
            }
            var groupId = group.Id;
            var query = from gr in _customerGroupRoles
                        where gr.GroupId.Equals(groupId)
                        join role in _roleStore.DbEntitySet on gr.RoleId equals role.Id
                        select role.Name;
            return await query.ToListAsync();
        }

        public async Task<bool> HasRoleAsync(CustomerGroup group, string roleName)
        {
            ThrowIfDisposed();
            if (group == null)
            {
                throw new ArgumentNullException("group");
            }
            if (String.IsNullOrWhiteSpace(roleName))
            {
                throw new ArgumentException("IdentityResources.ValueCannotBeNullOrEmpty", "roleName");
            }
            var role = await _roleStore.DbEntitySet.SingleOrDefaultAsync(r => r.Name.ToUpper() == roleName.ToUpper());
            if (role != null)
            {
                var groupId = group.Id;
                var roleId = role.Id;
                return await _customerGroupRoles.AnyAsync(ur => ur.RoleId.Equals(roleId) && ur.GroupId.Equals(groupId));
            }
            return false;
        }

        public async Task AddUserAsync(CustomerGroup group, Guid userId)
        {
            ThrowIfDisposed();
            if (group == null)
            {
                throw new ArgumentNullException("group");
            }
            var user = await _userStore.GetByIdAsync(userId);

            if (user == null)
            {
                throw new InvalidOperationException(String.Format(CultureInfo.CurrentCulture, "IdentityResources.UserNotFound", ""));
            }

            var ur = new CustomerGroupUser { GroupId = group.Id, UserId = user.Id };
            _customerGroupUsers.Add(ur);
        }

        public Task RemoveUserAsync(CustomerGroup group, Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<IList<User>> GetUsersAsync(CustomerGroup group)
        {
            throw new NotImplementedException();
        }

        public Task<bool> HasUserAsync(CustomerGroup group, Guid userId)
        {
            throw new NotImplementedException();
        }
    }
}
