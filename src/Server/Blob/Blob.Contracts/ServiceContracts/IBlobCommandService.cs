using System.ServiceModel;
using System.Threading.Tasks;
using Blob.Contracts.Dto;

namespace Blob.Contracts.ServiceContracts
{
    [ServiceContract]
    public interface IBlobCommandManager
    {
        // Customer
        [OperationContract]
        Task DisableCustomerAsync(DisableCustomerDto dto);
        [OperationContract]
        Task EnableCustomerAsync(EnableCustomerDto dto);
        [OperationContract]
        Task UpdateCustomerAsync(UpdateCustomerDto dto);

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
        Task DisableUserAsync(DisableUserDto dto);
        [OperationContract]
        Task EnableUserAsync(EnableUserDto dto);
        [OperationContract]
        Task UpdateUserAsync(UpdateUserDto dto);

    }
}
