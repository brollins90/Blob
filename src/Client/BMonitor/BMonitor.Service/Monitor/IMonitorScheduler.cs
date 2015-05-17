
namespace BMonitor.Service.Monitor
{
    public interface IMonitorScheduler
    {
        bool LoadConfig();
        void Start();
        void Stop();
        void Tick();
    }
}
