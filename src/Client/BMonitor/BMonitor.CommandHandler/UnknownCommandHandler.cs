using System;
using Blob.Contracts.Command;
using log4net;

namespace BMonitor.CommandHandler
{
    public class UnknownCommandHandler : ICommandHandler<ICommand>
    {
        private readonly ILog _log;

        public UnknownCommandHandler(ILog log)
        {
            _log = log;
        }

        public void Handle(ICommand command)
        {
            string msg = string.Format("Received an unknown command.  The type is {0}", command.GetType());
            _log.Error(msg);
            //throw new InvalidOperationException(msg);
        }
    }
}
