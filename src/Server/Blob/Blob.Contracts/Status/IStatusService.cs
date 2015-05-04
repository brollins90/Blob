using System.ServiceModel;
using System.Threading.Tasks;
using Blob.Contracts.Dto;

namespace Blob.Contracts.Status
{
    [ServiceContract]
    public interface IStatusService
    {
        [OperationContract(IsOneWay = true)]
        Task SendStatusToServer(AddStatusRecordDto addStatusRecordDto);

        [OperationContract(IsOneWay = true)]
        Task SendStatusPerformanceToServer(AddPerformanceRecordDto addPerformanceRecordDto);
    }
}
