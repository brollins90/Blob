namespace Blob.Contracts.ServiceContracts
{
    using System.ServiceModel;
    using System.Threading.Tasks;
    using Request;
    using Response;

    [ServiceContract]
    public interface IBlobCommandManager
    {
        // Customer
        [OperationContract]
        Task<BlobResult> DisableCustomerAsync(DisableCustomerRequest dto);
        [OperationContract]
        Task<BlobResult> EnableCustomerAsync(EnableCustomerRequest dto);
        [OperationContract]
        Task<BlobResult> RegisterCustomerAsync(RegisterCustomerRequest dto);
        [OperationContract]
        Task<BlobResult> UpdateCustomerAsync(UpdateCustomerRequest dto);

        // Customer Group
        [OperationContract]
        Task<BlobResult> CreateCustomerGroupAsync(CreateCustomerGroupRequest dto);
        [OperationContract]
        Task<BlobResult> DeleteCustomerGroupAsync(DeleteCustomerGroupRequest dto);
        [OperationContract]
        Task<BlobResult> UpdateCustomerGroupAsync(UpdateCustomerGroupRequest dto);
        [OperationContract]
        Task<BlobResult> AddRoleToCustomerGroupAsync(AddRoleToCustomerGroupRequest dto);
        [OperationContract]
        Task<BlobResult> AddUserToCustomerGroupAsync(AddUserToCustomerGroupRequest dto);
        [OperationContract]
        Task<BlobResult> RemoveRoleFromCustomerGroupAsync(RemoveRoleFromCustomerGroupRequest dto);
        [OperationContract]
        Task<BlobResult> RemoveUserFromCustomerGroupAsync(RemoveUserFromCustomerGroupRequest dto);

        // Commands
        [OperationContract]
        Task<BlobResult> IssueCommandAsync(IssueDeviceCommandRequest dto);

        // Device
        [OperationContract]
        Task<BlobResult> DisableDeviceAsync(DisableDeviceRequest dto);
        [OperationContract]
        Task<BlobResult> EnableDeviceAsync(EnableDeviceRequest dto);
        [OperationContract]
        Task<RegisterDeviceResponse> RegisterDeviceAsync(RegisterDeviceRequest dto);
        [OperationContract]
        Task<BlobResult> UpdateDeviceAsync(UpdateDeviceRequest dto);

        // PerformanceRecord
        [OperationContract]
        Task<BlobResult> AddPerformanceRecordAsync(AddPerformanceRecordRequest statusPerformanceData);
        [OperationContract]
        Task<BlobResult> DeletePerformanceRecordAsync(DeletePerformanceRecordRequest dto);

        // StatusRecord
        [OperationContract]
        Task<BlobResult> AddStatusRecordAsync(AddStatusRecordRequest statusData);
        [OperationContract]
        Task<BlobResult> DeleteStatusRecordAsync(DeleteStatusRecordRequest dto);

        // User
        [OperationContract]
        Task<BlobResult> CreateUserAsync(CreateUserRequest dto);
        [OperationContract]
        Task<BlobResult> DisableUserAsync(DisableUserRequest dto);
        [OperationContract]
        Task<BlobResult> EnableUserAsync(EnableUserRequest dto);
        [OperationContract]
        Task<BlobResult> UpdateUserAsync(UpdateUserRequest dto);

        //Task<BlobResultDto> UpdateUserActivityTimeAsync(Guid userId);
    }
}