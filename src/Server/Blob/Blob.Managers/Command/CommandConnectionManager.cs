using System;
using System.Collections.Generic;
using System.ServiceModel;
using Blob.Contracts.Command;
using log4net;

namespace Blob.Managers.Command
{
    public class CommandConnectionManager : ICommandConnectionManager
    {
        private readonly ILog _log;
        private static volatile CommandConnectionManager _connectionManager;
        private static readonly object SyncLock = new object();
        private static Dictionary<Guid, IDeviceConnectionServiceCallback> _callbacks;

        private CommandConnectionManager()
        {
            _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            _callbacks = new Dictionary<Guid, IDeviceConnectionServiceCallback>();
        }

        public static CommandConnectionManager Instance
        {
            get
            {
                lock (SyncLock)
                {
                    return _connectionManager ?? (_connectionManager = new CommandConnectionManager());
                }
            }
        }

        /// <summary>
        /// Adds a callback to the CommandManager
        /// </summary>
        /// <param name="deviceId">the id of the remote device</param>
        /// <param name="callback">the callback object</param>
        /// <exception cref="InvalidOperationException">A callback has already been registered for this device.</exception>
        public void AddCallback(Guid deviceId, IDeviceConnectionServiceCallback callback)
        {
            _log.Debug(string.Format("Adding callback to the CommandManager for device {0}.", deviceId));
            ThrowIfDisposed();
            bool added = false;

            //try
            //{
                lock (SyncLock)
                {
                    if (!_callbacks.ContainsKey(deviceId))
                    {
                        _callbacks.Add(deviceId, callback);
                        callback.OnConnect(string.Format("{0} connected successfully.", deviceId));
                        added = true;
                    }
                }
            //}
            //catch (Exception e)
            //{
            //    _log.Error("Error getting callback.", e);
            //    throw;
            //}
            if (!added)
            {
                //_callbacks[deviceId] = callback;
                //callback.OnConnect(string.Format("{0} connected successfully.", deviceId));
                _log.Error(string.Format("Failed to store callback for device {0}.  It was already connected.", deviceId));
                throw new InvalidOperationException("A callback has already been registered for this device.");
            }
        }

        /// <summary>
        /// Returns a callback from the CommandManager
        /// </summary>
        /// <param name="deviceId">the id of the remote device</param>
        /// <returns>the callback for the remote device or null if a callback is not found</returns>
        public IDeviceConnectionServiceCallback GetCallback(Guid deviceId)
        {
            _log.Debug(string.Format("returning callback for device {0}.", deviceId));
            ThrowIfDisposed();
            //try
            //{
            //    lock (SyncLock)
            //    {
                    if (_callbacks.ContainsKey(deviceId))
                    {
                        return _callbacks[deviceId];
                    }
                //}
            //}
            //catch (Exception e)
            //{
            //    _log.Error("Error getting callback.", e);
            //    throw;
            //}
            return null;
        }

        /// <summary>
        /// Check for the existance of a callback
        /// </summary>
        /// <param name="deviceId">the id of the remote device</param>
        /// <returns>true if the callback exists, otherwise returns false.</returns>
        public bool HasCallback(Guid deviceId)
        {
            _log.Debug(string.Format("returning status of callback for device {0}.", deviceId));
            ThrowIfDisposed();
            bool contains = false;
            //try
            //{
                lock (SyncLock)
                {
                    contains = _callbacks.ContainsKey(deviceId);
                }
            //}
            //catch (Exception e)
            //{
            //    _log.Error("Error getting callback.", e);
            //    throw;
            //}
            return contains;
        }

        /// <summary>
        /// Removes a callback from the CommandManager
        /// </summary>
        /// <param name="deviceId">the id of the remote device</param>
        /// <exception cref="InvalidOperationException">A callback for this device was not found.</exception>
        public void RemoveCallback(Guid deviceId)
        {
            _log.Debug(string.Format("removing callback from the CommandManager for device {0}.", deviceId));
            ThrowIfDisposed();

            bool removed = false;
            try
            {
                lock (SyncLock)
                {
                    if (_callbacks.ContainsKey(deviceId))
                    {
                        var callback = _callbacks[deviceId];
                        _callbacks.Remove(deviceId);
                        if (((ICommunicationObject) callback).State == CommunicationState.Opened)
                        {
                            ((ICommunicationObject) callback).Close();
                        }
                        removed = true;
                    }
                }
            }
            catch (TimeoutException e)
            {
                _log.Error("The channel timed out on close", e);
                // todo:  should i find the callback and abort?
            }
            catch (CommunicationObjectFaultedException e)
            {
                _log.Error("The channel was faulted.", e);
                // todo:  should i find the callback and abort?
            }
            if (!removed)
            {
                _log.Error(string.Format("Failed to remove callback for device {0}.  It was not connected.", deviceId));
                throw new InvalidOperationException("A callback for this device was not found.");
            }
        }


        protected internal bool IsDisposed { get; private set; }
        private void ThrowIfDisposed()
        {
            if (IsDisposed)
            {
                throw new ObjectDisposedException(GetType().Name);
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing && !IsDisposed)
            {
                IsDisposed = true;
            }
        }
    }
}
