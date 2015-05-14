using System;
using Blob.Contracts.Commands;
using log4net;

namespace BMonitor.Handlers
{
    public class UnknownCommandHandler<TCmd> : IDeviceCommandHandler<TCmd>
        where TCmd : IDeviceCommand
    {
        private readonly ILog _log;

        public UnknownCommandHandler(ILog log)
        {
            _log = log;
        }

        public void Handle(TCmd command)
        {
            string msg = string.Format("Received an unknown command.  The type is {0}", command.GetType());
            _log.Error(msg);
            throw new InvalidOperationException(msg);
        }
    }
}
