namespace BMonitor.Configuration
{
    public interface IBMonitorConfigurationProvider
    {
        IBMonitorServiceConfiguration Read();
    }
}
