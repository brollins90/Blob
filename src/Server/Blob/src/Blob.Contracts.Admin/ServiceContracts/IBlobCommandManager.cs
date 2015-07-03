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
        Task<BlobResult> DisableCustomerAsync(DisableCustomerDto dto);
        [OperationContract]
        Task<BlobResult> EnableCustomerAsync(EnableCustomerDto dto);
        [OperationContract]
        Task<BlobResult> RegisterCustomerAsync(RegisterCustomerDto dto);
        [OperationContract]
        Task<BlobResult> UpdateCustomerAsync(UpdateCustomerDto dto);

        // Customer Group
        [OperationContract]
        Task<BlobResult> CreateCustomerGroupAsync(CreateCustomerGroupDto dto);
        [OperationContract]
        Task<BlobResult> DeleteCustomerGroupAsync(DeleteCustomerGroupDto dto);
        [OperationContract]
        Task<BlobResult> UpdateCustomerGroupAsync(UpdateCustomerGroupDto dto);
        [OperationContract]
        Task<BlobResult> AddRoleToCustomerGroupAsync(AddRoleToCustomerGroupDto dto);
        [OperationContract]
        Task<BlobResult> AddUserToCustomerGroupAsync(AddUserToCustomerGroupDto dto);
        [OperationContract]
        Task<BlobResult> RemoveRoleFromCustomerGroupAsync(RemoveRoleFromCustomerGroupDto dto);
        [OperationContract]
        Task<BlobResult> RemoveUserFromCustomerGroupAsync(RemoveUserFromCustomerGroupDto dto);

        // Commands
        [OperationContract]
        Task<BlobResult> IssueCommandAsync(IssueDeviceCommandDto dto);

        // Device
        [OperationContract]
        Task<BlobResult> DisableDeviceAsync(DisableDeviceDto dto);
        [OperationContract]
        Task<BlobResult> EnableDeviceAsync(EnableDeviceDto dto);
        [OperationContract]
        Task<RegisterDeviceResponse> RegisterDeviceAsync(RegisterDeviceRequest dto);
        [OperationContract]
        Task<BlobResult> UpdateDeviceAsync(UpdateDeviceDto dto);

        // PerformanceRecord
        [OperationContract]
        Task<BlobResult> AddPerformanceRecordAsync(AddPerformanceRecordRequest statusPerformanceData);
        [OperationContract]
        Task<BlobResult> DeletePerformanceRecordAsync(DeletePerformanceRecordDto dto);

        // StatusRecord
        [OperationContract]
        Task<BlobResult> AddStatusRecordAsync(AddStatusRecordRequest statusData);
        [OperationContract]
        Task<BlobResult> DeleteStatusRecordAsync(DeleteStatusRecordDto dto);

        // User
        [OperationContract]
        Task<BlobResult> CreateUserAsync(CreateUserDto dto);
        [OperationContract]
        Task<BlobResult> DisableUserAsync(DisableUserDto dto);
        [OperationContract]
        Task<BlobResult> EnableUserAsync(EnableUserDto dto);
        [OperationContract]
        Task<BlobResult> UpdateUserAsync(UpdateUserDto dto);

        //Task<BlobResultDto> UpdateUserActivityTimeAsync(Guid userId);
    }
}
