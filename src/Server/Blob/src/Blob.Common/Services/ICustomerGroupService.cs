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
        Task<BlobResult> CreateCustomerGroupAsync(CreateCustomerGroupRequest request);
        Task<BlobResult> DeleteCustomerGroupAsync(DeleteCustomerGroupRequest request);
        Task<BlobResult> UpdateCustomerGroupAsync(UpdateCustomerGroupRequest request);
        Task<BlobResult> AddRoleToCustomerGroupAsync(AddRoleToCustomerGroupRequest request);
        Task<BlobResult> AddUserToCustomerGroupAsync(AddUserToCustomerGroupRequest request);
        Task<BlobResult> RemoveRoleFromCustomerGroupAsync(RemoveRoleFromCustomerGroupRequest request);
        Task<BlobResult> RemoveUserFromCustomerGroupAsync(RemoveUserFromCustomerGroupRequest request);

        // Query
        Task<CustomerGroupCreateViewModel> GetCustomerGroupCreateViewModelAsync(Guid customerId);
        Task<CustomerGroupDeleteViewModel> GetCustomerGroupDeleteViewModelAsync(Guid groupId);
        Task<CustomerGroupSingleViewModel> GetCustomerGroupSingleViewModelAsync(Guid groupId);
        Task<CustomerGroupUpdateViewModel> GetCustomerGroupUpdateViewModelAsync(Guid groupId);
        Task<IEnumerable<CustomerGroupRoleListItem>> GetCustomerGroupRolesAsync(Guid groupId);
        Task<IEnumerable<CustomerGroupUserListItem>> GetCustomerGroupUsersAsync(Guid groupId);


        Task<IEnumerable<CustomerGroupRoleListItem>> GetCustomerRolesAsync(Guid customerId);
        Task<CustomerGroupPageViewModel> GetCustomerGroupPageViewModelAsync(Guid customerId, int pageNum = 1, int pageSize = 10);
    }
}