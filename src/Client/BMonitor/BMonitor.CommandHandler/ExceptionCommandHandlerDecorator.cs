using System;
using System.ServiceModel;
using Blob.Contracts.Command;

namespace BMonitor.CommandHandler
{
    public class ExceptionCommandHandlerDecorator<TCmd> : ICommandHandler<TCmd>
    {
        private readonly ICommandHandler<TCmd> _wrappedHandler;

        public ExceptionCommandHandlerDecorator(ICommandHandler<TCmd> wrappedHandler)
        {
            _wrappedHandler = wrappedHandler;
        }

        public void Handle(TCmd command)
        {
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
