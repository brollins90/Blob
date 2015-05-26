namespace BMonitor.Service.Connection
{
    public interface IConnectionThread
    {
        bool IsConnected();
        void Start();
        void Stop();
    }
}
