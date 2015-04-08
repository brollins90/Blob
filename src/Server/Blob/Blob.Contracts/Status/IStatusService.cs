using Blob.Contracts.Models;
using System.ServiceModel;
using System.Threading.Tasks;

namespace Blob.Contracts.Status
{
    [ServiceContract]
    public interface IStatusService
    {
        [OperationContract(IsOneWay = true)]
        Task SendStatusToServer(StatusData statusData);

        [OperationContract(IsOneWay = true)]
        Task SendStatusPerformanceToServer(StatusPerformanceData statusPerformanceData);
    }
}
