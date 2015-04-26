using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Blob.Contracts.Command;
using Blob.Contracts.Commands;
using log4net;

namespace Blob.Managers.Command
{
    public class CommandManager : IDisposable
    {
        private readonly ILog _log;
        private static volatile CommandManager _connectionManager;
        private static readonly object SyncLock = new object();
        private readonly Dictionary<Guid, ICommandServiceCallback> _callbacks;
        private Queue<Tuple<Guid, ICommand>> _commandQueue;

        private bool runTestThread = true;
        private ManualResetEvent _stopEvent;
        private Thread _testThread;
        private Thread _testThread2;

        private CommandManager()
        {
            _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            _callbacks = new Dictionary<Guid, ICommandServiceCallback>();
            _commandQueue = new Queue<Tuple<Guid, ICommand>>();
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

        /// <summary>
        /// Adds a callback to the CommandManager
        /// </summary>
        /// <param name="deviceId">the id of the remote device</param>
        /// <param name="callback">the callback object</param>
        /// <exception cref="InvalidOperationException">A callback for this device was not found.</exception>
        public void AddCallback(Guid deviceId, ICommandServiceCallback callback)
        {
            _log.Debug(string.Format("Adding callback to the CommandManager for device {0}.", deviceId));
            ThrowIfDisposed();

            if (!_callbacks.ContainsKey(deviceId))
            {
                _callbacks.Add(deviceId, callback);
                callback.OnConnect("" + deviceId + " connected successfully.");

                if (runTestThread && _testThread == null)
                {
                    _testThread = new Thread(RunTest);
                    _testThread.Start();
                    _testThread2 = new Thread(RunTest2);
                    _testThread2.Start();
                }
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

            if (_callbacks.ContainsKey(deviceId))
            {
                _callbacks.Remove(deviceId);
                callback.OnDisconnect("" + deviceId + " disconnected successfully.");
            }
            else
            {
                _log.Error(string.Format("Failed to remove callback for device {0}.  It was not connected.", deviceId));
                throw new InvalidOperationException("A callback for this device was not found.");
            }
        }

        public void QueueCommand(Guid deviceId, ICommand command)
        {
            // check if there is a valid callback
            if (_callbacks.ContainsKey(deviceId))
            {
                _commandQueue.Enqueue(new Tuple<Guid, ICommand>(deviceId, command));
            }
            else
            {
                throw new InvalidOperationException("A callback for this device was not found.");
            }
        }

        private int blakei = 0;
        void timer_tick_add()
        {
            _log.Debug("CommandManager tick2 " + blakei++);

            ICommand cmd;
            if ((blakei % 3) == 0)
                cmd = new PrintLineCommand { OutputString = "command 1 execution" };
            else
                cmd = new PrintLine2Command { DifferentOutputString = "the other execution (2)" };

            Guid x = Guid.Parse("1c6f0042-750e-4f5a-b1fa-41dd4ca9368a");
            _log.Debug("queueing " + cmd + " on " + x);
            QueueCommand(x, cmd);

        }
        void timer_tick()
        {
            _log.Debug("CommandManager tick " + blakei++);

            while (_commandQueue.Any())
            {
                var c = _commandQueue.Dequeue();

                foreach (var x in _callbacks)
                {
                    if (x.Key.Equals(c.Item1))
                    {
                        _log.Debug("executing " + c + " on " + x);
                        x.Value.ExecuteCommand(c.Item2);
                    }
                }
            }
        }

        void RunTest2()
        {

            do
            {
                timer_tick_add();
            } while (!_stopEvent.WaitOne(1000 * 5));
        }

        void RunTest()
        {
            _stopEvent = new ManualResetEvent(false);

            do
            {
                timer_tick();
            } while (!_stopEvent.WaitOne(1000 * 4));
        }

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
