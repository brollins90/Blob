using System;
using System.Diagnostics;
using Blob.Contracts.Command;
using log4net;

namespace BMonitor.Handlers
{
    public class LoggingCommandHandlerDecorator<TCmd> : ICommandHandler<TCmd>
        where TCmd : ICommand
    {
        private readonly ICommandHandler<TCmd> _wrappedHandler;
        private readonly ILog _log;
        private readonly Stopwatch _stopWatch;

        public LoggingCommandHandlerDecorator(ILog log, ICommandHandler<TCmd> wrappedHandler)
        {
            _log = log;
            _wrappedHandler = wrappedHandler;
            _stopWatch = new Stopwatch();
            _stopWatch.Start();
        }

        public void Handle(TCmd command)
        {
            _stopWatch.Restart();
            Guid instanceId = Guid.NewGuid();
            _log.Debug(string.Format("Handle in LoggingCommandHandlerDecorator{0} - {1}", typeof(TCmd).Name, instanceId.ToString()));
            _wrappedHandler.Handle(command);
            long elapsed = _stopWatch.ElapsedMilliseconds;
            _log.Debug(string.Format("execution of {0} completed in {1}.", instanceId.ToString(), elapsed));
        }
    }
}
