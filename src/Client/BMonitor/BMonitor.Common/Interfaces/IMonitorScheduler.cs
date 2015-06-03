
namespace BMonitor.Common.Interfaces
{
    public interface IMonitorScheduler
    {
        bool LoadConfig();
        void Start();
        void Stop();
        void Tick();
        void RunJob(string jobName);
    }
}
