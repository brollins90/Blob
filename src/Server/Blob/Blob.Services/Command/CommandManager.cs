using Blob.Contracts.Command;
using log4net;
using System;
using System.Collections.Generic;

namespace Blob.Services.Command
{
    public class CommandManager
    {
        private readonly ILog _log;
        private static volatile CommandManager _connectionManager;
        private static readonly object SyncLock = new object();
        private readonly List<ICommandServiceCallback> _callbacks;

        private CommandManager()
        {
            _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            _callbacks = new List<ICommandServiceCallback>();
        }

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
            if (!_callbacks.Contains(callback))
            {
                _callbacks.Add(callback);
                callback.OnConnect("" + deviceId + " connected successfully.");
            }
            else
            {
                _log.Error(string.Format("Failed to store callback for device {0}.  It was already connected.", deviceId));
                throw new Exception("Device " + deviceId + " was already connected");
            }
        }

        public void RemoveCallback(Guid deviceId, ICommandServiceCallback callback)
        {
            if (_callbacks.Contains(callback))
            {
                _callbacks.Remove(callback);
                callback.OnDisconnect("" + deviceId + " disconnected successfully.");
            }
            else
            {
                _log.Error(string.Format("Failed to remove callback for device {0}.  It was not connected.", deviceId));
                throw new InvalidOperationException("Cannot find callback for " + deviceId);
            }
        }
    }
}
