using Blob.Contracts.Command;
using log4net;

namespace BMonitor.Handlers
{
    public class LoggingCommandHandlerDecorator<TCmd> : ICommandHandler<TCmd>
        where TCmd : ICommand
    {
        private readonly ICommandHandler<TCmd> _wrappedHandler;
        private readonly ILog _log;

        public LoggingCommandHandlerDecorator(ILog log, ICommandHandler<TCmd> wrappedHandler)
        {
            _log = log;
            _wrappedHandler = wrappedHandler;
        }

        public void Handle(TCmd command)
        {
            _log.Debug(string.Format("Handling command of type {0}", command.GetType()));
            _wrappedHandler.Handle(command);
        }
    }
}
