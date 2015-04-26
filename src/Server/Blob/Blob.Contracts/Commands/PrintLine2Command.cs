using Blob.Contracts.Command;

namespace Blob.Contracts.Commands
{
    public class PrintLine2Command : ICommand
    {
        public string OutputString { get; set; }
    }
}
