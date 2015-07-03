namespace Blob.Contracts.Commands
{
    using Command;

    public class PrintLineCommand : IDeviceCommand
    {
        public string OutputString { get; set; }
    }
}