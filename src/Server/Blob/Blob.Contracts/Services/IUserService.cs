using System;
using System.Threading.Tasks;
using Blob.Contracts.Models;
using Blob.Contracts.Models.ViewModels;

namespace Blob.Contracts.Services
{
    public interface IUserService
    {
        //Task<ClaimsIdentity> CreateUserIdentityAsync(CreateUserIdentityDto dto);
        //Task<IdentityResultDto> CreateAsync(UserDto user, string password);

        //Task<IdentityResultDto> ChangePasswordAsync(Guid userId, string currentPassword, string newPassword);
        // Command
        Task<BlobResultDto> DisableUserAsync(DisableUserDto dto);
        Task<BlobResultDto> EnableUserAsync(EnableUserDto dto);
        Task<BlobResultDto> CreateUserAsync(CreateUserDto dto);
        Task<BlobResultDto> UpdateUserAsync(UpdateUserDto dto);

        // Query
        Task<UserDisableVm> GetUserDisableVmAsync(Guid userId);
        Task<UserEnableVm> GetUserEnableVmAsync(Guid userId);
        Task<UserPageVm> GetUserPageVmAsync(Guid customerId, int pageNum = 1, int pageSize = 10);
        Task<UserSingleVm> GetUserSingleVmAsync(Guid userId);
        Task<UserUpdateVm> GetUserUpdateVmAsync(Guid userId);


    }
}
