namespace Blob.Common.Services
{
    using System;
    using System.Threading.Tasks;
    using Contracts.Request;
    using Contracts.Response;
    using Contracts.ViewModel;

    public interface IUserService
    {
        //Task<ClaimsIdentity> CreateUserIdentityAsync(CreateUserIdentityDto dto);
        //Task<IdentityResultDto> CreateAsync(UserDto user, string password);

        //Task<IdentityResultDto> ChangePasswordAsync(Guid userId, string currentPassword, string newPassword);
        // Command
        Task<BlobResult> DisableUserAsync(DisableUserRequest dto);
        Task<BlobResult> EnableUserAsync(EnableUserRequest dto);
        Task<BlobResult> CreateUserAsync(CreateUserRequest dto);
        Task<BlobResult> UpdateUserAsync(UpdateUserRequest dto);

        // Query
        Task<UserDisableViewModel> GetUserDisableVmAsync(Guid userId);
        Task<UserEnableViewModel> GetUserEnableVmAsync(Guid userId);
        Task<UserPageViewModel> GetUserPageVmAsync(Guid customerId, int pageNum = 1, int pageSize = 10);
        Task<UserSingleViewModel> GetUserSingleVmAsync(Guid userId);
        Task<UserUpdateVm> GetUserUpdateVmAsync(Guid userId);
        Task<UserUpdatePasswordViewModel> GetUserUpdatePasswordVmAsync(Guid userId);
    }
}