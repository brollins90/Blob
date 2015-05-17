using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using Blob.Contracts.ServiceContracts;
using log4net;
using Ninject;
using Ninject.Activation.Blocks;

namespace BMonitor.Service.Connection
{
    public enum CommandStatus
    {
        Other, Running, Completed, Failed
    }

    public class CommandServiceCallbackHandler : IDeviceConnectionServiceCallback
    {
        private readonly IKernel _kernel;
        private readonly ILog _log;
        private readonly IDictionary<Guid, CommandStatus> _commandStatuses;
        // we need to keep an instance of the ninject container because we dont know what command handlers to 
        // load until we receive the commands
        public CommandServiceCallbackHandler(IKernel kernel, ILog log)
        {
            _kernel = kernel;
            _log = log;
            _commandStatuses = new ConcurrentDictionary<Guid, CommandStatus>();
        }

        public void OnConnect(string message)
        {
            _log.Debug(string.Format("Client received OnConnect callback: {0}", message));
        }

        public void OnDisconnect(string message)
        {
            _log.Debug(string.Format("Client received OnDisconnect callback: {0}", message));
        }

        public void OnReceivedPing(string message)
        {
            _log.Debug(string.Format("Client received OnReceivedPing callback: {0}", message));
        }

        // https://simpleinjector.codeplex.com/discussions/566865
        // https://www.cuttingedge.it/blogs/steven/pivot/entry.php?id=95

        public void ExecuteCommand(Guid commandId, dynamic command)
        {
            _log.Debug(string.Format("Client received ExecuteCommand callback: {0}, starting execution thread.", command.ToString()));
            _commandStatuses.Add(commandId, CommandStatus.Other);

            Task t = new Task(() =>
            {
                _log.Debug("Starting execution thread");

                using (IActivationBlock activation = _kernel.BeginBlock())
                {
                    Type commandType = command.GetType();
                    Type commandHandlerType = typeof(Blob.Contracts.Commands.IDeviceCommandHandler<>).MakeGenericType(commandType);

                    // Load the correct handler for the resolved command
                    dynamic commandHandler = _kernel.Get(commandHandlerType);
                    commandHandler.Handle(command);
                }
            });
            _commandStatuses[commandId] = CommandStatus.Running;

            t.Start();
            
            t.ContinueWith(task =>
            {
                _commandStatuses[commandId] = CommandStatus.Completed;
            }, TaskContinuationOptions.OnlyOnRanToCompletion);
            t.ContinueWith(task =>
            {
                _commandStatuses[commandId] = CommandStatus.Failed;
            }, TaskContinuationOptions.OnlyOnFaulted);
        }
    }
}
