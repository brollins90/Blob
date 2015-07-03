namespace Blob.Contracts.Commands
{
    public class PrintLineCommand : IDeviceCommand
    {
        public string OutputString { get; set; }
    }
}