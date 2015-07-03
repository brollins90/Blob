namespace Blob.Common.Services
{
    using System;
    using System.Threading.Tasks;
    using Contracts.Request;
    using Contracts.Response;
    using Contracts.ViewModel;

    public interface IUserService
    {
        Task<BlobResult> CreateUserAsync(CreateUserRequest request);
        Task<BlobResult> DisableUserAsync(DisableUserRequest request);
        Task<BlobResult> EnableUserAsync(EnableUserRequest request);
        Task<BlobResult> UpdateUserAsync(UpdateUserRequest request);

        Task<UserDisableViewModel> GetUserDisableViewModelAsync(Guid id);
        Task<UserEnableViewModel> GetUserEnableViewModelAsync(Guid id);
        Task<UserPageViewModel> GetUserPageViewModelAsync(Guid customerId, int pageNum = 1, int pageSize = 10);
        Task<UserSingleViewModel> GetUserSingleViewModelAsync(Guid id);
        Task<UserUpdateVm> GetUserUpdateViewModelAsync(Guid id);
        Task<UserUpdatePasswordViewModel> GetUserUpdatePasswordViewModelAsync(Guid id);
    }
}