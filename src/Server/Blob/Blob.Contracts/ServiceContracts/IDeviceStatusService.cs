using System.ServiceModel;
using System.Threading.Tasks;
using Blob.Contracts.Dto;

namespace Blob.Contracts.ServiceContracts
{
    [ServiceContract]
    public interface IDeviceStatusService
    {
        [OperationContract]
        Task<RegisterDeviceResponseDto> RegisterDeviceAsync(RegisterDeviceDto dto);

        [OperationContract]
        Task AddStatusRecordAsync(AddStatusRecordDto dto);

        [OperationContract]
        Task AddPerformanceRecordAsync(AddPerformanceRecordDto dto);
    }
}
