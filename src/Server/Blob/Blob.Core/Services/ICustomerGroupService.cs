using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Blob.Contracts.Models;
using Blob.Contracts.Models.ViewModels;

namespace Blob.Core.Services
{
    public interface ICustomerGroupService
    {
        // Command
        Task<BlobResultDto> CreateCustomerGroupAsync(CreateCustomerGroupDto dto);
        Task<BlobResultDto> DeleteCustomerGroupAsync(DeleteCustomerGroupDto dto);
        Task<BlobResultDto> UpdateCustomerGroupAsync(UpdateCustomerGroupDto dto);
        Task<BlobResultDto> AddRoleToCustomerGroupAsync(AddRoleToCustomerGroupDto dto);
        Task<BlobResultDto> AddUserToCustomerGroupAsync(AddUserToCustomerGroupDto dto);
        Task<BlobResultDto> RemoveRoleFromCustomerGroupAsync(RemoveRoleFromCustomerGroupDto dto);
        Task<BlobResultDto> RemoveUserFromCustomerGroupAsync(RemoveUserFromCustomerGroupDto dto); 


        // Query
        Task<CustomerGroupCreateVm> GetCustomerGroupCreateVmAsync(Guid customerId);
        Task<CustomerGroupDeleteVm> GetCustomerGroupDeleteVmAsync(Guid groupId);
        Task<CustomerGroupSingleVm> GetCustomerGroupSingleVmAsync(Guid groupId);
        Task<CustomerGroupUpdateVm> GetCustomerGroupUpdateVmAsync(Guid groupId);
        Task<IEnumerable<CustomerGroupRoleListItem>> GetCustomerGroupRolesAsync(Guid groupId);
        Task<IEnumerable<CustomerGroupUserListItem>> GetCustomerGroupUsersAsync(Guid groupId);
    }
}
