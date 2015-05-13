
namespace BMonitor.Service.Quartz
{
    public interface IJobHandler
    {
        bool LoadConfig();
        void Start();
        void Stop();
        void Tick();
    }
}
