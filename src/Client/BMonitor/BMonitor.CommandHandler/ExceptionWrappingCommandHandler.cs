//using System;
//using System.ServiceModel;
//using Blob.Contracts.Command;

//namespace BMonitor.CommandHandler
//{
//    public class ExceptionWrappingCommandHandler<TCommand> : ICommandHandler<TCommand>
//    {
//        private readonly ICommandHandler<TCommand> _wrappedHandler;

//        public ExceptionWrappingCommandHandler(ICommandHandler<TCommand> wrappedHandler)
//        {
//            _wrappedHandler = wrappedHandler;
//        }

//        public void Handle(TCommand command)
//        {
//            try
//            {
//                _wrappedHandler.Handle(command);
//            }
//            catch (Exception e)
//            {
//                // This ensures that validation errors are communicated to the client,
//                // while other exceptions are filtered by WCF (if configured correctly).
//                throw new FaultException(e.Message, new FaultCode("CommandExecutionError"));
//            }
//        }
//    }
//}
