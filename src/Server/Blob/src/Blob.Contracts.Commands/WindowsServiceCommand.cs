namespace Blob.Contracts.Commands
{
    public class WindowsServiceCommand : IDeviceCommand
    {
        public string Action { get; set; }
        public string ServiceName { get; set; }
    }
}
