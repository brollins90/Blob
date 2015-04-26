using System;
using Blob.Contracts.Command;
using log4net;
using Ninject;

namespace BMonitor.Service
{
    public class CommandServiceCallbackHandler : ICommandServiceCallback
    {
        private readonly IKernel _kernel;
        private readonly ILog _log;

        // we need to keep an instance of the ninject container because we dont know what command handlers to 
        // load until we receive the commands
        public CommandServiceCallbackHandler(IKernel kernel, ILog log)
        {
            _kernel = kernel;
            _log = log;
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

        public void ExecuteCommand(dynamic command)
        {
            _log.Debug(string.Format("Client received ExecuteCommand callback: {0}", command.ToString()));
            Type commandType = command.GetType();
            Type commandHandlerType = typeof(Blob.Contracts.Command.ICommandHandler<>).MakeGenericType(commandType);

            // Load the correct handler for the resolved command
            dynamic commandHandler = _kernel.Get(commandHandlerType);
            commandHandler.Handle(command);
        }
    }
}
