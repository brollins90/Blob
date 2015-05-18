using System;
using System.Data.Entity;
using System.Data.Entity.SqlServer.Utilities;
using System.Linq;
using System.Threading.Tasks;
using Blob.Identity;

namespace Blob.Data.Identity
{

    public class GenericGroupStore<TGroup, TKey, TUserGroup, TGroupRole> : IQueryableGroupStore<TGroup, TKey>
        where TUserGroup : GenericUserGroup<TKey>, new()
        where TGroupRole : GenericGroupRole<TKey>, new()
        where TGroup : GenericGroup<TKey, TUserGroup, TGroupRole>, new()
    {
        private bool _disposed;
        private GenericEntityStore<TGroup> _groupStore;

        public GenericGroupStore(DbContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            Context = context;
            _groupStore = new GenericEntityStore<TGroup>(context);
        }

        public DbContext Context { get; private set; }
        public bool DisposeContext { get; set; }

        public Task<TGroup> FindByIdAsync(TKey groupId)
        {
            ThrowIfDisposed();
            return _groupStore.GetByIdAsync(groupId);
        }

        public Task<TGroup> FindByNameAsync(string groupName)
        {
            ThrowIfDisposed();
            return _groupStore.EntitySet.FirstOrDefaultAsync(u => u.Name.ToUpper().Equals(groupName.ToUpper()));
        }

        public virtual async Task CreateAsync(TGroup group)
        {
            ThrowIfDisposed();
            if (group == null)
                throw new ArgumentNullException("group");

            _groupStore.Create(group);
            await Context.SaveChangesAsync().WithCurrentCulture();
        }

        public virtual async Task DeleteAsync(TGroup group)
        {
            ThrowIfDisposed();
            if (group == null)
                throw new ArgumentNullException("group");

            _groupStore.Delete(group);
            await Context.SaveChangesAsync().WithCurrentCulture();
        }

        public virtual async Task UpdateAsync(TGroup group)
        {
            ThrowIfDisposed();
            if (group == null)
                throw new ArgumentNullException("group");

            _groupStore.Update(group);
            await Context.SaveChangesAsync().WithCurrentCulture();
        }

        public IQueryable<TGroup> Groups
        {
            get { return _groupStore.EntitySet; }
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
            _groupStore = null;
        }
    }
}
