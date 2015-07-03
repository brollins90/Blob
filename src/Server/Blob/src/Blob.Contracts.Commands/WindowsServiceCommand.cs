namespace Blob.Contracts.Commands
{
    using Command;

    public class WindowsServiceCommand : IDeviceCommand
    {
        public string Action { get; set; }
        public string ServiceName { get; set; }
    }
}