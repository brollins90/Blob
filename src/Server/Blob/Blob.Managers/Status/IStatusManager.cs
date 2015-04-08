using Blob.Contracts.Models;
using System.Threading.Tasks;

namespace Blob.Managers.Status
{
    public interface IStatusManager
    {
        Task StoreStatusData(StatusData statusData);
        Task StoreStatusPerformanceData(StatusPerformanceData statusPerformanceData);
    }
}
