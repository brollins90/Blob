using System;
using System.ServiceModel;
using System.Threading.Tasks;
using Blob.Contracts.Models;

namespace Blob.Contracts.Services
{
    [ServiceContract]
    public interface ICustomerGroupCommandManager
    {
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
    }
}
