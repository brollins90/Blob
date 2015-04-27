using System;
using Blob.Contracts.Command;
using Blob.Contracts.Commands;

namespace BMonitor.Handlers.Custom
{
    public class PrintLineCommandHandler : ICommandHandler<PrintLineCommand>
    {
        public void Handle(PrintLineCommand command)
        {
            Console.WriteLine(command.OutputString);
        }
    }
}
