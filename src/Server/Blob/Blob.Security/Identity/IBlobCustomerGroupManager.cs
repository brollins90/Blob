using System;
namespace Blob.Security.Identity
{
    interface IBlobCustomerGroupManager
    {
        System.Threading.Tasks.Task<Blob.Contracts.Models.BlobResultDto> AddRoleToCustomerGroup(Blob.Contracts.Models.AddRoleToCustomerGroupDto dto);
        System.Threading.Tasks.Task<Blob.Contracts.Models.BlobResultDto> AddUserToCustomerGroup(Blob.Contracts.Models.AddUserToCustomerGroupDto dto);
        System.Threading.Tasks.Task<Blob.Contracts.Models.BlobResultDto> CreateGroupAsync(Blob.Contracts.Models.CreateCustomerGroupDto dto);
        System.Threading.Tasks.Task<Blob.Contracts.Models.BlobResultDto> DeleteGroupAsync(Guid groupId);
        System.Threading.Tasks.Task<Blob.Contracts.Models.BlobResultDto> RemoveRoleFromCustomerGroupAsync(Blob.Contracts.Models.RemoveRoleFromCustomerGroupDto dto);
        System.Threading.Tasks.Task<Blob.Contracts.Models.BlobResultDto> RemoveUserFromCustomerGroupAsync(Blob.Contracts.Models.RemoveUserFromCustomerGroupDto dto);
        System.Threading.Tasks.Task<Blob.Contracts.Models.BlobResultDto> UpdateGroupAsync(Blob.Contracts.Models.UpdateCustomerGroupDto dto);
    }
}
