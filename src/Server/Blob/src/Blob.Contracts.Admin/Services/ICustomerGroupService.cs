using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Blob.Contracts.Models;
using Blob.Contracts.Models.ViewModels;

namespace Blob.Contracts.Services
{
    public interface ICustomerGroupService
    {
        // Command
        Task<BlobResult> CreateCustomerGroupAsync(CreateCustomerGroupDto dto);
        Task<BlobResult> DeleteCustomerGroupAsync(DeleteCustomerGroupDto dto);
        Task<BlobResult> UpdateCustomerGroupAsync(UpdateCustomerGroupDto dto);
        Task<BlobResult> AddRoleToCustomerGroupAsync(AddRoleToCustomerGroupDto dto);
        Task<BlobResult> AddUserToCustomerGroupAsync(AddUserToCustomerGroupDto dto);
        Task<BlobResult> RemoveRoleFromCustomerGroupAsync(RemoveRoleFromCustomerGroupDto dto);
        Task<BlobResult> RemoveUserFromCustomerGroupAsync(RemoveUserFromCustomerGroupDto dto); 
        
        // Query
        Task<CustomerGroupCreateVm> GetCustomerGroupCreateVmAsync(Guid customerId);
        Task<CustomerGroupDeleteVm> GetCustomerGroupDeleteVmAsync(Guid groupId);
        Task<CustomerGroupSingleVm> GetCustomerGroupSingleVmAsync(Guid groupId);
        Task<CustomerGroupUpdateVm> GetCustomerGroupUpdateVmAsync(Guid groupId);
        Task<IEnumerable<CustomerGroupRoleListItem>> GetCustomerGroupRolesAsync(Guid groupId);
        Task<IEnumerable<CustomerGroupUserListItem>> GetCustomerGroupUsersAsync(Guid groupId);


        Task<IEnumerable<CustomerGroupRoleListItem>> GetCustomerRolesAsync(Guid customerId);
        Task<CustomerGroupPageVm> GetCustomerGroupPageVmAsync(Guid customerId, int pageNum = 1, int pageSize = 10);
    }
}
