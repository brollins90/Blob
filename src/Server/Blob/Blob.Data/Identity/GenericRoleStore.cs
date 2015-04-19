﻿using Blob.Core.Identity;
using Microsoft.AspNet.Identity;
using System;
using System.Data.Entity;
using System.Data.Entity.SqlServer.Utilities;
using System.Linq;
using System.Threading.Tasks;

namespace Blob.Data.Identity
{
    //public class GenericRoleStore<TRole> : GenericRoleStore<TRole, string, GenericUserRole>, IQueryableRoleStore<TRole>
    //    where TRole : GenericRole, new()
    //{
    //    public GenericRoleStore()
    //        :base(new GenericDbContext())
    //    {
    //        DisposeContext = true;
    //    }

    //    public GenericRoleStore(DbContext context) : base(context) { } 
    //}

    public class GenericRoleStore<TRole, TKey, TUserRole> : IQueryableRoleStore<TRole, TKey>
        where TUserRole : GenericUserRole<TKey>, new()
        where TRole : GenericRole<TKey, TUserRole>, new()
    {
        private bool _disposed;
        private GenericEntityStore<TRole> _roleStore;

        public GenericRoleStore(DbContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            Context = context;
            _roleStore = new GenericEntityStore<TRole>(context);
        }

        public DbContext Context { get; private set; }
        public bool DisposeContext { get; set; }

        public Task<TRole> FindByIdAsync(TKey roleId)
        {
            ThrowIfDisposed();
            return _roleStore.GetByIdAsync(roleId);
        }

        public Task<TRole> FindByNameAsync(string roleName)
        {
            ThrowIfDisposed();
            return _roleStore.EntitySet.FirstOrDefaultAsync(u => u.Name.ToUpper().Equals(roleName.ToUpper()));
        }

        public virtual async Task CreateAsync(TRole role)
        {
            ThrowIfDisposed();
            if (role == null)
                throw new ArgumentNullException("role");

            _roleStore.Create(role);
            await Context.SaveChangesAsync().WithCurrentCulture();
        }

        public virtual async Task DeleteAsync(TRole role)
        {
            ThrowIfDisposed();
            if (role == null)
                throw new ArgumentNullException("role");

            _roleStore.Delete(role);
            await Context.SaveChangesAsync().WithCurrentCulture();
        }

        public virtual async Task UpdateAsync(TRole role)
        {
            ThrowIfDisposed();
            if (role == null)
                throw new ArgumentNullException("role");

            _roleStore.Update(role);
            await Context.SaveChangesAsync().WithCurrentCulture();
        }

        public IQueryable<TRole> Roles
        {
            get { return _roleStore.EntitySet; }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void ThrowIfDisposed()
        {
            if (_disposed)
                throw new ObjectDisposedException(GetType().Name);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (DisposeContext && disposing && Context != null)
            {
                Context.Dispose();
            }
            _disposed = true;
            Context = null;
            _roleStore = null;
        }
    }
}