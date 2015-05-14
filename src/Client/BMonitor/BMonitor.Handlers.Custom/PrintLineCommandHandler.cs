using System;
using Blob.Contracts.Commands;

namespace BMonitor.Handlers.Custom
{
    public class PrintLineCommandHandler : IDeviceCommandHandler<PrintLineCommand>
    {
        public void Handle(PrintLineCommand command)
        {
            Console.WriteLine(command.OutputString);
        }
    }
}
