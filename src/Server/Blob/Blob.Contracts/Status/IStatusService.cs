using Blob.Contracts.Models;
using System.ServiceModel;

namespace Blob.Contracts.Status
{
    [ServiceContract]
    public interface IStatusService
    {
        [OperationContract(IsOneWay = true)]
        void SendStatusToServer(StatusData statusData);

        [OperationContract(IsOneWay = true)]
        void SendStatusPerformanceToServer(StatusPerformanceData statusPerformanceData);
    }
}
