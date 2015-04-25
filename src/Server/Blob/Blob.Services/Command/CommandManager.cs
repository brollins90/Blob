using System;
using System.Collections.Generic;
using System.Threading;
using Blob.Contracts.Command;
using log4net;

namespace Blob.Services.Command
{
    public class CommandManager : IDisposable
    {
        private readonly ILog _log;
        private static volatile CommandManager _connectionManager;
        private static readonly object SyncLock = new object();
        private readonly List<ICommandServiceCallback> _callbacks;

        //private bool runTestThread = true;
        //private ManualResetEvent _stopEvent;
        //private Thread _testThread;

        private CommandManager()
        {
            _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            _callbacks = new List<ICommandServiceCallback>();
        }
        protected internal bool IsDisposed { get; private set; }

        public static CommandManager Instance
        {
            get
            {
                lock (SyncLock)
                {
                    if (_connectionManager == null)
                    {
                        _connectionManager = new CommandManager();
                    }
                }
                return _connectionManager;
            }
        }

        public void AddCallback(Guid deviceId, ICommandServiceCallback callback)
        {
            _log.Debug(string.Format("Adding callback to the CommandManager for device {0}.", deviceId));
            ThrowIfDisposed();

            if (!_callbacks.Contains(callback))
            {
                _callbacks.Add(callback);
                callback.OnConnect("" + deviceId + " connected successfully.");

                //if (runTestThread)
                //{
                //    _testThread = new Thread(RunTest);
                //    _testThread.Start();
                //}
            }
            else
            {
                _log.Error(string.Format("Failed to store callback for device {0}.  It was already connected.", deviceId));
                throw new InvalidOperationException("A callback has already been registered for this device.");
            }
        }

        /// <summary>
        /// Removes a callback from the CommandManager
        /// </summary>
        /// <param name="deviceId">the id of the remote device</param>
        /// <param name="callback">the callback object</param>
        /// <exception cref="InvalidOperationException">A callback for this device was not found.</exception>
        public void RemoveCallback(Guid deviceId, ICommandServiceCallback callback)
        {
            ThrowIfDisposed();

            if (_callbacks.Contains(callback))
            {
                _callbacks.Remove(callback);
                callback.OnDisconnect("" + deviceId + " disconnected successfully.");
            }
            else
            {
                _log.Error(string.Format("Failed to remove callback for device {0}.  It was not connected.", deviceId));
                throw new InvalidOperationException("A callback for this device was not found.");
            }
        }

        //void timer_tick()
        //{
        //    foreach (var x in _callbacks)
        //    {
        //        x.OnConnect("Test tick");
        //    }
        //}

        //void RunTest()
        //{
        //    _stopEvent = new ManualResetEvent(false);

        //    do
        //    {
        //        timer_tick();
        //    } while (!_stopEvent.WaitOne(1000 * 4));
        //}

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void ThrowIfDisposed()
        {
            if (IsDisposed)
            {
                throw new ObjectDisposedException(GetType().Name);
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing && !IsDisposed)
            {
                //Store.Dispose();
                IsDisposed = true;
            }
        }
    }
}
