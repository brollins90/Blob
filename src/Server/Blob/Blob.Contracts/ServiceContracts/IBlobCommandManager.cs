using System.ServiceModel;
using System.Threading.Tasks;
using Blob.Contracts.Models;
using Blob.Contracts.Services;

namespace Blob.Contracts.ServiceContracts
{
    [ServiceContract]
    public interface IBlobCommandManager : ICustomerCommandManager
    {
        // Commands
        [OperationContract]
        Task IssueCommandAsync(IssueDeviceCommandDto dto);

        // Device
        [OperationContract]
        Task DisableDeviceAsync(DisableDeviceDto dto);
        [OperationContract]
        Task EnableDeviceAsync(EnableDeviceDto dto);
        [OperationContract]
        Task<RegisterDeviceResponseDto> RegisterDeviceAsync(RegisterDeviceDto dto);
        [OperationContract]
        Task UpdateDeviceAsync(UpdateDeviceDto dto);

        // PerformanceRecord
        [OperationContract]
        Task AddPerformanceRecordAsync(AddPerformanceRecordDto statusPerformanceData);
        [OperationContract]
        Task DeletePerformanceRecordAsync(DeletePerformanceRecordDto dto);

        // StatusRecord
        [OperationContract]
        Task AddStatusRecordAsync(AddStatusRecordDto statusData);
        [OperationContract]
        Task DeleteStatusRecordAsync(DeleteStatusRecordDto dto);

        // User
        [OperationContract]
        Task CreateUserAsync(CreateUserDto dto);
        [OperationContract]
        Task DisableUserAsync(DisableUserDto dto);
        [OperationContract]
        Task EnableUserAsync(EnableUserDto dto);
        [OperationContract]
        Task UpdateUserAsync(UpdateUserDto dto);

    }
}
