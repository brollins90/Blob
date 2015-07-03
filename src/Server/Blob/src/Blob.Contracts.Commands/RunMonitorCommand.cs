namespace Blob.Contracts.Commands
{
    using Command;

    public class RunMonitorCommand : IDeviceCommand
    {
        public string MonitorName { get; set; }
    }
}