using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Utilities;
using System.Linq;
using System.Threading.Tasks;
using Blob.Core.Data;
using Blob.Core.Domain;
using log4net;

namespace Blob.Data.Repositories
{
    public class StatusRepository : IStatusRepository, IDisposable
    {
        private readonly ILog _log;
        private readonly IDbSet<DeviceType> _deviceTypeStore; 
        private readonly IDbSet<Status> _statusStore;
        private readonly IDbSet<StatusPerf> _performanceStore;
        private GenericEntityStore<Customer> _customerStore;
        private GenericEntityStore<Device> _deviceStore;
        private bool _disposed;

        public StatusRepository(BlobDbContext context) 
            :this(context,
            LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType)) { }

        public StatusRepository(BlobDbContext context, ILog log)
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
            _log.Debug("Constructing StatusRepository");

            Context = context;

            AutoSaveChanges = true;
            _customerStore = new GenericEntityStore<Customer>(context);
            _deviceStore = new GenericEntityStore<Device>(context);
            _deviceTypeStore = Context.Set<DeviceType>();
            _statusStore = Context.Set<Status>();
            _performanceStore = Context.Set<StatusPerf>();
        }

        protected internal BlobDbContext Context { get; private set; }

        public bool DisposeContext { get; set; }

        public bool AutoSaveChanges { get; set; }


        #region Device

        public Task DeleteDeviceAsync(Device device)
        {
            _log.Debug("DeleteDeviceAsync");
            //ThrowIfDisposed();
            //if (device == null)
            //{
            //    throw new ArgumentNullException("device");
            //}
            //var d = await _deviceStore.DbEntitySet.SingleOrDefaultAsync(x => x.Id.Equals(device.Id);
            //if (d != null)
            //{
            //    var deviceId = d.Id;
            //    var customerId = user.Id;
            //    var userRole = await _userRoles.FirstOrDefaultAsync(r => roleId.Equals(r.RoleId) && r.UserId.Equals(userId)).WithCurrentCulture();
            //    if (userRole != null)
            //    {
            //        _userRoles.Remove(userRole);
            //    }
            //}


            ThrowIfDisposed();
            throw new NotImplementedException();
        }

        private bool AreDevicesLoaded(Customer customer)
        {
            return Context.Entry(customer).Collection(u => u.Devices).IsLoaded;
        }

        

        #endregion


        #region DeviceType


        public Task CreateDeviceTypeAsync(DeviceType deviceType)
        {
            _log.Debug("CreateDeviceTypeAsync");
            ThrowIfDisposed();
            throw new NotImplementedException();
        }

        public Task<DeviceType> FindDeviceTypeByIdAsync(Guid id)
        {
            _log.Debug("FindDeviceTypeByIdAsync");
            ThrowIfDisposed();
            throw new NotImplementedException();
            // return a NOT SET object type if null
        }

        public Task<DeviceType> FindDeviceTypeByValueAsync(string value)
        {
            _log.Debug("FindDeviceTypeByValueAsync");
            ThrowIfDisposed();
            throw new NotImplementedException();
        }

        public Task UpdateDeviceTypeAsync(DeviceType deviceType)
        {
            _log.Debug("UpdateDeviceTypeAsync");
            ThrowIfDisposed();
            throw new NotImplementedException();
        }

        public Task DeleteDeviceTypeAsync(DeviceType deviceType)
        {
            _log.Debug("DeleteDeviceTypeAsync");
            ThrowIfDisposed();
            throw new NotImplementedException();
        }

        public Task<IList<Status>> FindStatusesForDeviceAsync(Guid deviceId)
        {
            throw new NotImplementedException();
        }

        #endregion


        #region Status

        public virtual async Task<IList<Status>> FindStatusByIdAsync(Device device)
        {
            _log.Debug("GetStatusAsync");
            ThrowIfDisposed();
            if (device == null)
                throw new ArgumentNullException("device");

            await EnsureStatusLoaded(device).WithCurrentCulture();
            return device.Statuses.ToList();
        }

        public virtual Task AddStatusAsync(Device device, Status status)
        {
            _log.Debug("AddStatusAsync");
            ThrowIfDisposed();
            if (device == null)
                throw new ArgumentNullException("device");
            if (status == null)
                throw new ArgumentNullException("status");

            _statusStore.Add(status);
            SaveChanges();
            return Task.FromResult(0);
        }

        public virtual Task RemoveStatusAsync(Device device, Status status)
        {
            _log.Debug("RemoveStatusAsync");
            ThrowIfDisposed();
            throw new NotImplementedException();
            //    ThrowIfDisposed();
            //    if (device == null)
            //        throw new ArgumentNullException("device");
            //    if (statusIn == null)
            //        throw new ArgumentNullException("statusIn");

            //    // need to load the status before we remove it
            //    _statusStore.Remove(status);
            //    return Task.FromResult(0);
        }

        public Task<IList<StatusPerf>> FindPerformanceForDeviceAsync(Guid deviceId)
        {
            _log.Debug("FindPerformanceForDeviceAsync");
            ThrowIfDisposed();
            throw new NotImplementedException();
        }

        Task<IList<StatusPerf>> IStatusRepository.GetPerformanceByIdAsync(Device device)
        {
            _log.Debug("GetPerformanceAsync");
            ThrowIfDisposed();
            throw new NotImplementedException();
        }

        #endregion


        #region Performance


        public Task<IList<Status>> GetPerformanceAsync(Device device)
        {
            _log.Debug("GetPerformanceAsync");
            ThrowIfDisposed();
            throw new NotImplementedException();
        }

        public Task AddPerformanceAsync(Device device, StatusPerf statusPerf)
        {
            _log.Debug("AddPerformanceAsync");
            ThrowIfDisposed();
            if (device == null)
                throw new ArgumentNullException("device");
            if (statusPerf == null)
                throw new ArgumentNullException("statusPerf");

            _performanceStore.Add(statusPerf);
            SaveChanges();
            return Task.FromResult(0);
        }

        public Task RemovePerformanceAsync(Device device, StatusPerf statusPerf)
        {
            _log.Debug("RemovePerformanceAsync");
            ThrowIfDisposed();
            throw new NotImplementedException();
        }

        #endregion

        protected int SaveChanges()
        {
            _log.Debug("Saving changes...");
            ThrowIfDisposed();
            int numChanges = Context.SaveChanges(); // todo, is this async??
            _log.Debug(string.Format("Saved {0} changes", numChanges));
            return numChanges;
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
            _deviceStore = null;
        }
    }
}
