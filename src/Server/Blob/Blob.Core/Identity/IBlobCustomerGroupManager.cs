using System;
using Blob.Contracts.Models;

namespace Blob.Core.Identity
{
    interface IBlobCustomerGroupManager
    {
        System.Threading.Tasks.Task<BlobResultDto> AddRoleToCustomerGroup(global::Blob.Contracts.Models.AddRoleToCustomerGroupDto dto);
        System.Threading.Tasks.Task<BlobResultDto> AddUserToCustomerGroup(global::Blob.Contracts.Models.AddUserToCustomerGroupDto dto);
        System.Threading.Tasks.Task<BlobResultDto> CreateGroupAsync(global::Blob.Contracts.Models.CreateCustomerGroupDto dto);
        System.Threading.Tasks.Task<BlobResultDto> DeleteGroupAsync(Guid groupId);
        System.Threading.Tasks.Task<BlobResultDto> RemoveRoleFromCustomerGroupAsync(global::Blob.Contracts.Models.RemoveRoleFromCustomerGroupDto dto);
        System.Threading.Tasks.Task<BlobResultDto> RemoveUserFromCustomerGroupAsync(global::Blob.Contracts.Models.RemoveUserFromCustomerGroupDto dto);
        System.Threading.Tasks.Task<BlobResultDto> UpdateGroupAsync(global::Blob.Contracts.Models.UpdateCustomerGroupDto dto);
    }
}
