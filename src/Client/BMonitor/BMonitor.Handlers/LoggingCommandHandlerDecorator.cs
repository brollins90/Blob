using System;
using System.Diagnostics;
using Blob.Contracts.Commands;
using log4net;

namespace BMonitor.Handlers
{
    public class LoggingCommandHandlerDecorator<TCmd> : IDeviceCommandHandler<TCmd>
        where TCmd : IDeviceCommand
    {
        private readonly IDeviceCommandHandler<TCmd> _wrappedHandler;
        private readonly ILog _log;
        private readonly Stopwatch _stopWatch;

        public LoggingCommandHandlerDecorator(ILog log, IDeviceCommandHandler<TCmd> wrappedHandler)
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
