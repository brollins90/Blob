using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Blob.Core.Data;
using Blob.Core.Domain;
using log4net;

namespace Blob.Data.Repositories
{
    public class AccountRepository : IAccountRepository, IDisposable
    {
        private readonly ILog _log;
        private readonly IDbSet<DeviceType> _deviceTypeStore;
        private readonly IDbSet<Status> _statusStore;
        private GenericEntityStore<Customer> _customerStore;
        private GenericEntityStore<Device> _deviceStore;
        private bool _disposed;

        public AccountRepository(BlobDbContext context) 
            :this(context,
            LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType)) { }

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
            _log.Debug("Constructing AccountRepository");

            Context = context;

            AutoSaveChanges = true;
            _customerStore = new GenericEntityStore<Customer>(context);
            _deviceStore = new GenericEntityStore<Device>(context);
            _deviceTypeStore = Context.Set<DeviceType>();
            _statusStore = Context.Set<Status>();
        }

        protected internal BlobDbContext Context { get; private set; }

        public bool DisposeContext { get; protected set; }

        public bool AutoSaveChanges { get; protected set; }


        #region Customer
        public async Task CreateCustomerAsync(Customer customer)
        {
            _log.Debug(string.Format("CreateCustomerAsync({0})", customer));
            ThrowIfDisposed();
            if (customer == null)
            {
                throw new ArgumentNullException("customer");
            }
            _customerStore.Create(customer);
            await SaveChanges();
        }

        public async Task DeleteCustomerAsync(Guid customerId)
        {
            _log.Debug(string.Format("DeleteCustomerAsync({0})", customerId));
            ThrowIfDisposed();
            if (customerId == null)
            {
                throw new ArgumentNullException("customerId");
            }
            Customer customer = await _customerStore.GetByIdAsync(customerId);
            _customerStore.Delete(customer);
            await SaveChanges();
        }

        public async Task<Customer> FindCustomerByIdAsync(Guid customerId)
        {
            _log.Debug(string.Format("FindCustomerByIdAsync({0})", customerId));
            ThrowIfDisposed();
            return await _customerStore.GetByIdAsync(customerId);
        }

        public async Task<IList<Customer>> GetAllCustomersAsync()
        {
            _log.Debug(string.Format("GetAllCustomersAsync"));
            ThrowIfDisposed();
            return await _customerStore.DbEntitySet.ToListAsync();
        }

        public async Task UpdateCustomerAsync(Customer customer)
        {
            _log.Debug(string.Format("UpdateCustomerAsync({0})", customer));
            ThrowIfDisposed();
            if (customer == null)
            {
                throw new ArgumentNullException("customer");
            }
            _customerStore.Update(customer);
            await SaveChanges();
        }
        #endregion
        
        #region Device

        public async Task CreateDeviceAsync(Device device)
        {
            _log.Debug(string.Format("CreateDeviceAsync({0})", device));
            ThrowIfDisposed();
            if (device == null)
            {
                throw new ArgumentNullException("device");
            }
            _deviceStore.Create(device);
            await SaveChanges();
        }

        public async Task DeleteDeviceAsync(Guid deviceId)
        {
            _log.Debug(string.Format("DeleteDeviceAsync({0})", deviceId));
            ThrowIfDisposed();
            if (deviceId == null)
            {
                throw new ArgumentNullException("deviceId");
            }
            Device device = await _deviceStore.GetByIdAsync(deviceId);
            _deviceStore.Delete(device);
            await SaveChanges();
        }

        public async Task<Device> FindDeviceByIdAsync(Guid deviceId)
        {
            _log.Debug(string.Format("FindDeviceByIdAsync({0})", deviceId));
            ThrowIfDisposed();

            Device device = await _deviceStore.GetByIdAsync(deviceId);
            if (device != null)
            {
                await _customerStore.DbEntitySet.Where(x => x.Id.Equals(device.CustomerId)).LoadAsync();
                await _deviceTypeStore.Where(x => x.Id.Equals(device.DeviceTypeId)).LoadAsync();

                await EnsureStatusLoaded(device);
                //await EnsurePerformanceLoaded(device);
                //await EnsureLogsLoaded(device);
            }
            return device;
        }

        public async Task<IList<Device>> FindDevicesByCustomerIdAsync(Guid customerId)
        {
            _log.Debug(string.Format("FindDeviceByIdAsync({0})", customerId));
            ThrowIfDisposed();

            Customer customer = await _customerStore.GetByIdAsync(customerId);
            if (customer != null)
            {
                return AreDevicesLoaded(customer)
                    ? customer.Devices.Where(x => x.CustomerId == customerId).ToList()
                    : await _deviceStore.DbEntitySet.Where(x => x.CustomerId == customerId).ToListAsync();
            }
            return null;
        }

        public async Task<IList<Device>> GetAllDevicesAsync()
        {
            _log.Debug("GetAllDevicesAsync");
            ThrowIfDisposed();
            return await _deviceStore.DbEntitySet.ToListAsync();
        }

        public async Task UpdateDeviceAsync(Device device)
        {
            _log.Debug(string.Format("UpdateDeviceAsync({0})", device));
            ThrowIfDisposed();
            if (device == null)
            {
                throw new ArgumentNullException("device");
            }
            _deviceStore.Update(device);
            await SaveChanges();
        }

        private bool AreDevicesLoaded(Customer customer)
        {
            return Context.Entry(customer).Collection(u => u.Devices).IsLoaded;
        }

        private async Task EnsureDevicesLoaded(Customer customer)
        {
            if (!AreDevicesLoaded(customer))
            {
                var customerId = customer.Id;
                await _deviceStore.DbEntitySet.Where(x => x.CustomerId.Equals(customerId)).LoadAsync();
                Context.Entry(customer).Collection(u => u.Devices).IsLoaded = true;
            }
        }

        private bool AreStatusLoaded(Device device)
        {
            return Context.Entry(device).Collection(u => u.Statuses).IsLoaded;
        }

        private async Task EnsureStatusLoaded(Device device)
        {
            if (!AreStatusLoaded(device))
            {
                var deviceId = device.Id;
                await _statusStore.Where(x => x.DeviceId.Equals(deviceId)).LoadAsync();
                Context.Entry(device).Collection(u => u.Statuses).IsLoaded = true;
            }
        }

        #endregion

        #region DeviceType

        public async Task CreateDeviceTypeAsync(DeviceType deviceType)
        {
            _log.Debug(string.Format("CreateDeviceTypeAsync({0})", deviceType));
            ThrowIfDisposed();
            throw new NotImplementedException();
        }

        public async Task DeleteDeviceTypeAsync(Guid deviceTypeId)
        {
            _log.Debug(string.Format("DeleteDeviceTypeAsync({0})", deviceTypeId));
            ThrowIfDisposed();
            throw new NotImplementedException();
        }

        public async Task FindDeviceTypeByIdAsync(Guid deviceTypeId)
        {
            _log.Debug(string.Format("FindDeviceTypeByIdAsync({0})", deviceTypeId));
            ThrowIfDisposed();
            throw new NotImplementedException();
        }

        public async Task FindDeviceTypeByValueAsync(string value)
        {
            _log.Debug(string.Format("FindDeviceTypeByValueAsync({0})", value));
            ThrowIfDisposed();
            throw new NotImplementedException();
        }

        public async Task UpdateDeviceTypeAsync(DeviceType deviceType)
        {
            _log.Debug(string.Format("UpdateDeviceTypeAsync({0})", deviceType));
            ThrowIfDisposed();
            throw new NotImplementedException();
        }

        #endregion



        // Only call save changes if AutoSaveChanges is true
        private async Task SaveChanges()
        {
            if (AutoSaveChanges)
            {
                await Context.SaveChangesAsync();
            }
        }


        public void Dispose()
        {
            _log.Debug("Dispose");
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
            _log.Debug("Dispose(" + disposing + ")");
            if (DisposeContext && disposing && Context != null)
            {
                Context.Dispose();
            }
            _disposed = true;
            Context = null;
            _customerStore = null;
            _deviceStore = null;
        }
    }
}
