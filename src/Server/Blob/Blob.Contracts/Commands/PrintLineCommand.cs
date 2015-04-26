using Blob.Contracts.Command;

namespace Blob.Contracts.Commands
{
    public class PrintLineCommand : ICommand
    {
        public string OutputString { get; set; }
    }
}
