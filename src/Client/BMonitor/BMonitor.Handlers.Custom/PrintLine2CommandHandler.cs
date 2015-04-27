using System;
using Blob.Contracts.Command;
using Blob.Contracts.Commands;

namespace BMonitor.Handlers.Custom
{
    public class PrintLine2CommandHandler : ICommandHandler<PrintLine2Command>
    {
        public void Handle(PrintLine2Command command)
        {
            Console.WriteLine(" -------" + command.DifferentOutputString);
        }
    }
}
