﻿using System;
using System.Threading.Tasks;
using Blob.Contracts.Command;
using log4net;
using Ninject;
using Ninject.Activation.Blocks;

namespace BMonitor.Service
{
    public class CommandServiceCallbackHandler : IDeviceConnectionServiceCallback
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

        // https://simpleinjector.codeplex.com/discussions/566865
        // https://www.cuttingedge.it/blogs/steven/pivot/entry.php?id=95

        public void ExecuteCommand(dynamic command)
        {
            _log.Debug(string.Format("Client received ExecuteCommand callback: {0}, starting execution thread.", command.ToString()));

            Task t = new Task(() =>
            {
                _log.Debug("Starting execution thread");

                using (IActivationBlock activation = _kernel.BeginBlock())
                {
                    Type commandType = command.GetType();
                    Type commandHandlerType = typeof(Blob.Contracts.Command.ICommandHandler<>).MakeGenericType(commandType);

                    // Load the correct handler for the resolved command
                    dynamic commandHandler = _kernel.Get(commandHandlerType);
                    commandHandler.Handle(command);
                }
            });
            t.Start();
        }
    }
}