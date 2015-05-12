using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Blob.Contracts.Command;
using log4net;

namespace Blob.Managers.Command
{
    public class CommandQueueManager : ICommandQueueManager
    {
        private readonly ILog _log;
        private static volatile CommandQueueManager _queueManager;
        private static readonly object SyncLock = new object();
        private static ConcurrentQueue<Tuple<Guid, ICommand>> _commandQueue;

        private static ManualResetEvent _stopEvent;
        private static int _secondsToWaitBeforeForCommandJobProcessingCheck;
        private static Thread _testThread;


        private CommandQueueManager()
        {
            _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            _commandQueue = new ConcurrentQueue<Tuple<Guid, ICommand>>();

            _secondsToWaitBeforeForCommandJobProcessingCheck = 5;
            if (_testThread == null)
            {
                _testThread = new Thread(ProcessQueueThreadStart);
                _testThread.Start();
                //_testThread2 = new Thread(RunTest2);
                //_testThread2.Start();
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

        public async Task<bool> QueueCommandAsync(Guid deviceId, ICommand command)
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
                        _commandQueue.Enqueue(new Tuple<Guid, ICommand>(deviceId, command));
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
                Tuple<Guid, ICommand> cmd;
                while (_commandQueue.TryDequeue(out cmd))
                {
                    processedCount++;
                    Tuple<Guid, ICommand> cmdCopy = new Tuple<Guid, ICommand>(cmd.Item1, cmd.Item2);
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

        private void ExecuteCommand(Tuple<Guid, ICommand> cmd)
        {
            try
            {
                var callback = ConnectionManager.GetCallback(cmd.Item1);
                _log.Debug("executing " + cmd.Item2 + " on " + cmd.Item1);
                callback.ExecuteCommand(cmd.Item2);
            }
            catch (Exception e)
            {
                _log.Error(string.Format("Error executing command on {0}: {1}", cmd.Item1, cmd.Item2), e);
                ConnectionManager.RemoveCallback(cmd.Item1);
            }
        }

        //private int blakei = 0;
        //void timer_tick_add()
        //{
        //    _log.Debug("CommandManager tick2 " + blakei++);

        //    ICommand cmd;
        //    if ((blakei % 3) == 0)
        //        cmd = new PrintLineCommand { OutputString = "command 1 execution" };
        //    else if (((blakei + 1) % 3) == 0)
        //        cmd = new CmdExecuteCommand { CommandString = @"dir >> c:\_\fromACommand.txt" };
        //    else
        //        cmd = new PrintLine2Command { DifferentOutputString = "the other execution (2)" };

        //    //if (blakei == 0)
        //    //    cmd = new PrintLine2Command { DifferentOutputString = "the other execution (2)" };
        //    //else if (blakei == 1)
        //    //    cmd = new CmdExecuteCommand { CommandString = @"dir >> c:\_\fromACommand.txt" };
        //    //else 
        //    //    cmd = new PrintLineCommand {OutputString = "command 1 execution"};
        //    //blakei++ ;
        //    //blakei %= 2;
        //    // hard code test device id
        //    Guid x = Guid.Parse("1c6f0042-750e-4f5a-b1fa-41dd4ca9368a");
        //    _log.Debug("queueing " + cmd + " on " + x);
        //    Task queueTask = QueueCommandAsync(x, cmd);
        //    Task.WaitAll(queueTask);
        //}

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
