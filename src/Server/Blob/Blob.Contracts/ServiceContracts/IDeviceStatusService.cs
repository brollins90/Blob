using System.ServiceModel;
using System.Threading.Tasks;
using Blob.Contracts.Models;

namespace Blob.Contracts.ServiceContracts
{
    [ServiceContract]
    public interface IDeviceStatusService
    {
        [OperationContract]
        Task<BlobResultDto> AuthenticateDeviceAsync(AuthenticateDeviceDto dto);

        [OperationContract]
        Task<RegisterDeviceResponseDto> RegisterDeviceAsync(RegisterDeviceDto dto);

        [OperationContract]
        Task AddStatusRecordAsync(AddStatusRecordDto dto);

        [OperationContract]
        Task AddPerformanceRecordAsync(AddPerformanceRecordDto dto);
    }
}
