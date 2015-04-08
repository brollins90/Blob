using Blob.Contracts.Models;

namespace Blob.Managers.Status
{
    public interface IStatusManager
    {
        void StoreStatusData(StatusData statusData);
        void StoreStatusPerformanceData(StatusPerformanceData statusPerformanceData);
    }
}
