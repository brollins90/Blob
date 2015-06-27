using System.ServiceModel;
using System.Threading.Tasks;
using Blob.Contracts.Models;

namespace Blob.Contracts.ServiceContracts
{
    [ServiceContract]
    public interface IBlobCommandManager
    {
        // Customer
        [OperationContract]
        Task<BlobResultDto> DisableCustomerAsync(DisableCustomerDto dto);
        [OperationContract]
        Task<BlobResultDto> EnableCustomerAsync(EnableCustomerDto dto);
        [OperationContract]
        Task<BlobResultDto> RegisterCustomerAsync(RegisterCustomerDto dto);
        [OperationContract]
        Task<BlobResultDto> UpdateCustomerAsync(UpdateCustomerDto dto);

        // Customer Group
        [OperationContract]
        Task<BlobResultDto> CreateCustomerGroupAsync(CreateCustomerGroupDto dto);
        [OperationContract]
        Task<BlobResultDto> DeleteCustomerGroupAsync(DeleteCustomerGroupDto dto);
        [OperationContract]
        Task<BlobResultDto> UpdateCustomerGroupAsync(UpdateCustomerGroupDto dto);
        [OperationContract]
        Task<BlobResultDto> AddRoleToCustomerGroupAsync(AddRoleToCustomerGroupDto dto);
        [OperationContract]
        Task<BlobResultDto> AddUserToCustomerGroupAsync(AddUserToCustomerGroupDto dto);
        [OperationContract]
        Task<BlobResultDto> RemoveRoleFromCustomerGroupAsync(RemoveRoleFromCustomerGroupDto dto);
        [OperationContract]
        Task<BlobResultDto> RemoveUserFromCustomerGroupAsync(RemoveUserFromCustomerGroupDto dto);

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

        //Task<BlobResultDto> UpdateUserActivityTimeAsync(Guid userId);
    }
}
