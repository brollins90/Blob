using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.ServiceModel;
//using Blob.Commands;
using Blob.Contracts.Command;
using Blob.Contracts.Commands;
using BMonitor.Common;
using BMonitor.Common.Interfaces;
using log4net;
using Ninject;

namespace BMonitor.Service
{
    //[ServiceContract]
    //[ServiceKnownType("GetKnownTypes")]
    public class CommandServiceCallbackHandler : ICommandServiceCallback
    {
        private readonly IKernel _kernel;
        private readonly ILog _log;

        //private readonly ICommandHandler<PrintLineCommand> _printLineHandler;

        public CommandServiceCallbackHandler(IKernel kernel, ILog log/*, ICommandHandler<PrintLineCommand> printLineHandler*/)
        {
            _kernel = kernel;
            _log = log;

            //_printLineHandler = printLineHandler;
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

        //[KnownType("GetKnownTypes")]
        public void ExecuteCommand(dynamic command)
        {
            _log.Debug(string.Format("Client received ExecuteCommand callback: {0}", command.ToString()));
            Type commandType = command.GetType();
            Type commandHandlerType = typeof(Blob.Contracts.Command.ICommandHandler<>).MakeGenericType(commandType);

            dynamic commandHandler = _kernel.TryGet(commandHandlerType);
            if (commandHandler == null)
            {
                _log.Error(string.Format("Received an unknown command: {0}", commandType.ToString()));
            }
            else
            {
                commandHandler.Handle(command);
            }


            //Type commandHandlerType = typeof(BCommandHandler<>).MakeGenericType(commandType);

            //dynamic commandHandler = _kernel.Get(commandHandlerType);
            ////var commandHandler = _printLineHandler;

        }

        //public static IEnumerable<Type> GetKnownTypes(ICustomAttributeProvider provider)
        //{
        //    var coreAssembly = typeof(BCommandHandler<>).Assembly;

        //    var commandTypes =
        //        from type in coreAssembly.GetExportedTypes()
        //        where type.Name.EndsWith("Command")
        //        select type;

        //    return commandTypes.ToArray();
        //}
    }
}
