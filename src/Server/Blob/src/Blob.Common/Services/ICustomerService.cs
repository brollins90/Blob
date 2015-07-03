namespace Blob.Common.Services
{
    using System;
    using System.Threading.Tasks;
    using Contracts.Request;
    using Contracts.Response;
    using Contracts.ViewModel;

    public interface ICustomerService
    {
        // Command
        Task<BlobResult> DisableCustomerAsync(DisableCustomerRequest request);
        Task<BlobResult> EnableCustomerAsync(EnableCustomerRequest request);
        Task<BlobResult> RegisterCustomerAsync(RegisterCustomerRequest request);
        Task<BlobResult> UpdateCustomerAsync(UpdateCustomerRequest request);

        // Query
        Task<CustomerDisableViewModel> GetCustomerDisableViewModelAsync(Guid id);
        Task<CustomerEnableViewModel> GetCustomerEnableViewModelAsync(Guid id);
        Task<CustomerPageViewModel> GetCustomerPageViewModelAsync(Guid searchId, int pageNum = 1, int pageSize = 10);
        Task<CustomerSingleViewModel> GetCustomerSingleViewModelAsync(Guid id);
        Task<CustomerUpdateViewModel> GetCustomerUpdateViewModelAsync(Guid id);
    }
}