using BMonitor.Common.Models;

namespace BMonitor.Common.Interfaces
{
    public interface IMonitorScheduler
    {
        bool LoadConfig();
        void Start();
        void Stop();
        void Tick();

        void AddJob(JobSettings settings);
        void RemoveJob(string jobName);
        void RunJob(string jobName);
    }
}
