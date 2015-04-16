
namespace BMonitor.Common.Interfaces
{
    public interface IMonitor
    {
        MonitorResult Execute(bool collectPerfData = false);
    }
}
