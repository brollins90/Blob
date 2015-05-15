using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading;
using System.Threading.Tasks;
using Blob.Contracts.Commands;
using log4net;

namespace Blob.Managers.Command
{
    public class CommandQueueManager : ICommandQueueManager
    {
        private readonly ILog _log;
        private static volatile CommandQueueManager _queueManager;
        private static readonly object SyncLock = new object();
        private static ConcurrentQueue<Tuple<Guid, IDeviceCommand>> _commandQueue;

        private static ManualResetEvent _stopEvent;
        private static int _secondsToWaitBeforeForCommandJobProcessingCheck;
        private static Thread _testThread;


        private CommandQueueManager()
        {
            _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            _commandQueue = new ConcurrentQueue<Tuple<Guid, IDeviceCommand>>();

            _secondsToWaitBeforeForCommandJobProcessingCheck = 5;
            if (_testThread == null)
            {
                _testThread = new Thread(ProcessQueueThreadStart);
                _testThread.Start();
            }
        }

        public static CommandQueueManager Instance
        {
            get
            {
                lock (SyncLock)
                {
                    return _queueManager ?? (_queueManager = new CommandQueueManager());
                }
            }
        }
        
        private ICommandConnectionManager ConnectionManager
        {
            get { return CommandConnectionManager.Instance; }
        }

        public async Task<bool> QueueCommandAsync(Guid deviceId, IDeviceCommand command)
        {
            _log.Debug(string.Format("Queueing command for: {0} - {1}", deviceId, command.GetType().ToString()));
            return await Task.Run(() =>
            {
                // check if there is a valid callback
                if (ConnectionManager.HasCallback(deviceId))
                {
                    _log.Debug(string.Format("Found a valid callback for : {0} - {1}", deviceId, command.GetType().ToString()));
                    //lock (SyncLock)
                    //{
                    _commandQueue.Enqueue(new Tuple<Guid, IDeviceCommand>(deviceId, command));
                    //}
                    return true;
                }
                else
                {
                    _log.Error(string.Format("Could not find a callback for {0}", deviceId));
                    return false;
                }
            });
        }

        private void ProcessQueue()
        {
            _log.Debug("ProcessQueue");
            int processedCount = 0;

            try
            {
                List<Task> tasks = new List<Task>();
                Tuple<Guid, IDeviceCommand> cmd;
                while (_commandQueue.TryDequeue(out cmd))
                {
                    processedCount++;
                    Tuple<Guid, IDeviceCommand> cmdCopy = new Tuple<Guid, IDeviceCommand>(cmd.Item1, cmd.Item2);
                    Task t = Task.Factory.StartNew(() => ExecuteCommand(cmdCopy));
                    tasks.Add(t);
                }
                Task.WaitAll(tasks.ToArray());
            }
            catch (AggregateException e)
            {
                _log.Error(string.Format("There were errors executing some commands."), e);
            }
            _log.Debug(string.Format("Finished Queue processing.  processed {0} commands", processedCount));
        }

        private void ExecuteCommand(Tuple<Guid, IDeviceCommand> cmd)
        {
            try
            {
                var callback = ConnectionManager.GetCallback(cmd.Item1);
                _log.Debug("executing " + cmd.Item2 + " on " + cmd.Item1);
                callback.ExecuteCommand(cmd.Item2);
            }
            catch (CommunicationObjectAbortedException e)
            {
                _log.Error(string.Format("Error executing command {1} on {0}: the callback was aborted.", cmd.Item1, cmd.Item2), e);
                ConnectionManager.RemoveCallback(cmd.Item1);
            }
            catch (CommunicationObjectFaultedException e)
            {
                _log.Error(string.Format("Error executing command {1} on {0}: the callback was faulted.", cmd.Item1, cmd.Item2), e);
                ConnectionManager.RemoveCallback(cmd.Item1);
            }
            catch (Exception e)
            {
                _log.Error(string.Format("Error executing command on {0}: {1}", cmd.Item1, cmd.Item2), e);
                ConnectionManager.RemoveCallback(cmd.Item1);
            }
        }

        private void ProcessQueueThreadStart()
        {
            _stopEvent = new ManualResetEvent(false);

            do
            {
                ProcessQueue();
            } while (!_stopEvent.WaitOne(_secondsToWaitBeforeForCommandJobProcessingCheck * 1000));
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected internal bool IsDisposed { get; private set; }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing && !IsDisposed)
            {
                IsDisposed = true;
            }
        }
    }
}
