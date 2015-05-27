using System.ServiceModel;
using System.Threading.Tasks;
using Blob.Contracts.Models;
using Blob.Contracts.Services;

namespace Blob.Contracts.ServiceContracts
{
    [ServiceContract]
    public interface IBlobCommandManager : 
        ICustomerCommandManager, 
        ICustomerGroupCommandManager
    {
        // Commands
        [OperationContract]
        Task<BlobResultDto> IssueCommandAsync(IssueDeviceCommandDto dto);

        // Device
        [OperationContract]
        Task<BlobResultDto> DisableDeviceAsync(DisableDeviceDto dto);
        [OperationContract]
        Task<BlobResultDto> EnableDeviceAsync(EnableDeviceDto dto);
        [OperationContract]
        Task<RegisterDeviceResponseDto> RegisterDeviceAsync(RegisterDeviceDto dto);
        [OperationContract]
        Task<BlobResultDto> UpdateDeviceAsync(UpdateDeviceDto dto);

        // PerformanceRecord
        [OperationContract]
        Task<BlobResultDto> AddPerformanceRecordAsync(AddPerformanceRecordDto statusPerformanceData);
        [OperationContract]
        Task<BlobResultDto> DeletePerformanceRecordAsync(DeletePerformanceRecordDto dto);

        // StatusRecord
        [OperationContract]
        Task<BlobResultDto> AddStatusRecordAsync(AddStatusRecordDto statusData);
        [OperationContract]
        Task<BlobResultDto> DeleteStatusRecordAsync(DeleteStatusRecordDto dto);

        // User
        [OperationContract]
        Task<BlobResultDto> CreateUserAsync(CreateUserDto dto);
        [OperationContract]
        Task<BlobResultDto> DisableUserAsync(DisableUserDto dto);
        [OperationContract]
        Task<BlobResultDto> EnableUserAsync(EnableUserDto dto);
        [OperationContract]
        Task<BlobResultDto> UpdateUserAsync(UpdateUserDto dto);
    }
}
