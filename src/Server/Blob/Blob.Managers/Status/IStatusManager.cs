using System.Threading.Tasks;
using Blob.Contracts.Models;

namespace Blob.Managers.Status
{
    public interface IStatusManager
    {
        Task StoreStatusData(StatusData statusData);
        Task StoreStatusPerformanceData(StatusPerformanceData statusPerformanceData);
    }
}
