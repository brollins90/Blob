using System;
using System.Data.Entity;
using System.Data.Entity.SqlServer.Utilities;
using System.Linq;
using System.Threading.Tasks;
using Blob.Core.Domain;

namespace Blob.Data.Identity
{
    public class BlobGroupStore : IDisposable
    {
        private bool _disposed;
        private GenericEntityStore<BlobGroup> _groupStore;

        public BlobGroupStore(DbContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            Context = context;
            _groupStore = new GenericEntityStore<BlobGroup>(context);
        }

        public IQueryable<BlobGroup> Groups
        {
            get { return _groupStore.EntitySet; }
        }

        public DbContext Context { get; private set; }

        public virtual async Task CreateAsync(BlobGroup group)
        {
            ThrowIfDisposed();
            if (group == null)
                throw new ArgumentNullException("group");

            _groupStore.Create(group);
            await Context.SaveChangesAsync().WithCurrentCulture();
        }

        public virtual async Task DeleteAsync(BlobGroup group)
        {
            ThrowIfDisposed();
            if (group == null)
                throw new ArgumentNullException("group");

            _groupStore.Delete(group);
            await Context.SaveChangesAsync().WithCurrentCulture();
        }

        public Task<BlobGroup> FindByIdAsync(Guid groupId)
        {
            ThrowIfDisposed();
            return _groupStore.GetByIdAsync(groupId);
        }

        public Task<BlobGroup> FindByNameAsync(string groupName)
        {
            ThrowIfDisposed();
            return _groupStore.EntitySet.FirstOrDefaultAsync(u => u.Name.ToUpper().Equals(groupName.ToUpper()));
        }

        public virtual async Task UpdateAsync(BlobGroup group)
        {
            ThrowIfDisposed();
            if (group == null)
                throw new ArgumentNullException("group");

            _groupStore.Update(group);
            await Context.SaveChangesAsync().WithCurrentCulture();
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
            _groupStore = null;
        }
    }
}
