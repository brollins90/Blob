using System;
using Blob.Contracts.Command;
using log4net;

namespace BMonitor.Handlers.Default
{
    public class UnknownCommandHandler<TCmd> : ICommandHandler<TCmd>
        where TCmd : ICommand
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
