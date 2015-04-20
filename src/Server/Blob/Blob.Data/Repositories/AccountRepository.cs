using Blob.Core.Data;
using System;
using System.Data.Entity;
using System.Threading.Tasks;
using Blob.Core.Domain;
using log4net;

namespace Blob.Data.Repositories
{
    public class AccountRepository : IAccountRepository, IDisposable
    {
        private readonly ILog _log;
        private GenericEntityStore<Customer> _customerStore;
        private GenericEntityStore<Device> _deviceStore;
        private bool _disposed;

        public AccountRepository(BlobDbContext context, ILog log)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }
            if (log == null)
            {
                throw new ArgumentNullException("log");
            }
            _log = log;
            Context = context;

            AutoSaveChanges = true;
            _customerStore = new GenericEntityStore<Customer>(context);
            _deviceStore = new GenericEntityStore<Device>(context);
        }

        protected internal BlobDbContext Context { get; private set; }

        public bool DisposeContext { get; set; }

        public bool AutoSaveChanges { get; set; }



        public Task CreateCustomerAsync(Customer customer)
        {
            ThrowIfDisposed();
            throw new NotImplementedException();
        }

        public Task<Device> FindCustomerByIdAsync(Customer customer)
        {
            throw new NotImplementedException();
        }

        public Task UpdateCustomerAsync(Customer customer)
        {
            throw new NotImplementedException();
        }

        public Task DeleteCustomerAsync(Customer customer)
        {
            throw new NotImplementedException();
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
            _deviceStore = null;
        }
    }
}
