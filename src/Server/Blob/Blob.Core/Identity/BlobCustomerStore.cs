using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Blob.Core.Models;

namespace Blob.Core.Identity
{
    public class BlobCustomerStore : ICustomerStore, IDisposable
    {
        private bool _disposed;
        private GenericEntityStore<Customer> _customerStore;

        public BlobCustomerStore(DbContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            Context = context;
            _customerStore = new GenericEntityStore<Customer>(context);
        }

        public IQueryable<Customer> Customers
        {
            get { return _customerStore.EntitySet; }
        }

        public DbContext Context { get; private set; }

        public virtual async Task CreateAsync(Customer customer)
        {
            ThrowIfDisposed();
            if (customer == null)
                throw new ArgumentNullException("customer");

            _customerStore.Create(customer);
            await Context.SaveChangesAsync();
        }

        public virtual async Task DeleteAsync(Customer customer)
        {
            ThrowIfDisposed();
            if (customer == null)
                throw new ArgumentNullException("customer");

            _customerStore.Delete(customer);
            await Context.SaveChangesAsync();
        }

        public Task<Customer> FindByIdAsync(Guid customerId)
        {
            ThrowIfDisposed();
            return _customerStore.GetByIdAsync(customerId);
        }

        public Task<Customer> FindByNameAsync(string customerName)
        {
            ThrowIfDisposed();
            return _customerStore.EntitySet.FirstOrDefaultAsync(u => u.Name.ToUpper().Equals(customerName.ToUpper()));
        }

        public virtual async Task UpdateAsync(Customer customer)
        {
            ThrowIfDisposed();
            if (customer == null)
                throw new ArgumentNullException("customer");

            _customerStore.Update(customer);
            await Context.SaveChangesAsync();
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
            _customerStore = null;
        }
    }
}
