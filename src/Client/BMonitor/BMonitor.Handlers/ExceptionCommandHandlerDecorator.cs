using System;
using System.ServiceModel;
using Blob.Contracts.Commands;
using log4net;

namespace BMonitor.Handlers
{
    public class ExceptionCommandHandlerDecorator<TCmd> : IDeviceCommandHandler<TCmd>
        where TCmd : IDeviceCommand
    {
        private readonly IDeviceCommandHandler<TCmd> _wrappedHandler;
        private readonly ILog _log;

        public ExceptionCommandHandlerDecorator(ILog log, IDeviceCommandHandler<TCmd> wrappedHandler)
        {
            _log = log;
            _wrappedHandler = wrappedHandler;
        }

        public void Handle(TCmd command)
        {
            _log.Debug(string.Format("Handle in ExceptionCommandHandlerDecorator{0}", typeof(TCmd).Name));
            try
            {
                _wrappedHandler.Handle(command);
            }
            catch (Exception e)
            {
                // This ensures that validation errors are communicated to the client,
                // while other exceptions are filtered by WCF (if configured correctly).
                throw new FaultException(e.Message, new FaultCode("CommandExecutionError"));
            }
        }
    }
}
