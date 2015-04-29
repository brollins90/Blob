using Blob.Contracts.Command;

namespace Blob.Contracts.Commands
{
    public class WindowsBackupCommand : ICommand
    {
        public string JobName { get; set; }
    }
}
