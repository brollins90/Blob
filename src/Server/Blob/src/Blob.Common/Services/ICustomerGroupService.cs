namespace Blob.Common.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Contracts.Request;
    using Contracts.Response;
    using Contracts.ViewModel;

    public interface ICustomerGroupService
    {
        // Command
        Task<BlobResult> CreateCustomerGroupAsync(CreateCustomerGroupRequest dto);
        Task<BlobResult> DeleteCustomerGroupAsync(DeleteCustomerGroupRequest dto);
        Task<BlobResult> UpdateCustomerGroupAsync(UpdateCustomerGroupRequest dto);
        Task<BlobResult> AddRoleToCustomerGroupAsync(AddRoleToCustomerGroupRequest dto);
        Task<BlobResult> AddUserToCustomerGroupAsync(AddUserToCustomerGroupRequest dto);
        Task<BlobResult> RemoveRoleFromCustomerGroupAsync(RemoveRoleFromCustomerGroupRequest dto);
        Task<BlobResult> RemoveUserFromCustomerGroupAsync(RemoveUserFromCustomerGroupRequest dto);

        // Query
        Task<CustomerGroupCreateViewModel> GetCustomerGroupCreateVmAsync(Guid customerId);
        Task<CustomerGroupDeleteViewModel> GetCustomerGroupDeleteVmAsync(Guid groupId);
        Task<CustomerGroupSingleViewModel> GetCustomerGroupSingleVmAsync(Guid groupId);
        Task<CustomerGroupUpdateViewModel> GetCustomerGroupUpdateVmAsync(Guid groupId);
        Task<IEnumerable<CustomerGroupRoleListItem>> GetCustomerGroupRolesAsync(Guid groupId);
        Task<IEnumerable<CustomerGroupUserListItem>> GetCustomerGroupUsersAsync(Guid groupId);


        Task<IEnumerable<CustomerGroupRoleListItem>> GetCustomerRolesAsync(Guid customerId);
        Task<CustomerGroupPageViewModel> GetCustomerGroupPageVmAsync(Guid customerId, int pageNum = 1, int pageSize = 10);
    }
}