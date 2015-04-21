using Blob.Core.Data;
using Blob.Core.Domain;
using log4net;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Utilities;
using System.Linq;
using System.Threading.Tasks;

namespace Blob.Data.Repositories
{
    public class StatusRepository : IStatusRepository, IDisposable
    {
        private readonly ILog _log;
        private readonly IDbSet<Status> _statusStore;
        private readonly IDbSet<StatusPerf> _performanceStore;
        private GenericEntityStore<Device> _deviceStore;
        private bool _disposed;

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
            _deviceStore = new GenericEntityStore<Device>(context);
            _statusStore = Context.Set<Status>();
            _performanceStore = Context.Set<StatusPerf>();
        }

        protected internal BlobDbContext Context { get; private set; }

        public bool DisposeContext { get; set; }

        public bool AutoSaveChanges { get; set; }


        #region Device

        public Task CreateDeviceAsync(Device device)
        {
            _log.Debug("CreateDeviceAsync");
            ThrowIfDisposed();
            throw new NotImplementedException();
        }

        public async Task<Device> FindDeviceByIdAsync(Guid deviceId)
        {
            _log.Debug("FindDeviceByIdAsync");
            ThrowIfDisposed();
            ThrowIfDisposed();

            Device device = await _deviceStore.GetByIdAsync(deviceId).WithCurrentCulture();
            if (device != null)
            {
                await EnsureStatusLoaded(device).WithCurrentCulture();
                //await EnsurePerformanceLoaded(device).WithCurrentCulture();
                //await EnsureLogsLoaded(device).WithCurrentCulture();
            }
            return device;
        }

        public Task UpdateDeviceAsync(Device device)
        {
            _log.Debug("UpdateDeviceAsync");
            ThrowIfDisposed();
            throw new NotImplementedException();
        }

        public Task DeleteDeviceAsync(Device device)
        {
            _log.Debug("DeleteDeviceAsync");
            ThrowIfDisposed();
            throw new NotImplementedException();
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

        #endregion


        #region Status

        public virtual async Task<IList<Status>> GetStatusAsync(Device device)
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
            Context.SaveChangesAsync(); // ??
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

        private bool AreStatusLoaded(Device device)
        {
            return Context.Entry(device).Collection(u => u.Statuses).IsLoaded;
        }

        private async Task EnsureStatusLoaded(Device device)
        {
            if (!AreStatusLoaded(device))
            {
                var deviceId = device.Id;
                await _statusStore.Where(x => x.DeviceId.Equals(deviceId)).LoadAsync().WithCurrentCulture();
                Context.Entry(device).Collection(u => u.Statuses).IsLoaded = true;
            }
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
            Context.SaveChangesAsync(); // ??
            return Task.FromResult(0);
        }

        public Task RemovePerformanceAsync(Device device, StatusPerf statusPerf)
        {
            _log.Debug("RemovePerformanceAsync");
            ThrowIfDisposed();
            throw new NotImplementedException();
        }

        #endregion


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
