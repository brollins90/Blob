namespace BMonitor.Service.Connection
{
    public interface IConnectionThread
    {
        bool IsConnected();
        bool Start();
        void Stop();
    }
}
