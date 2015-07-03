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
        Task<BlobResult> DisableCustomerAsync(DisableCustomerRequest dto);
        Task<BlobResult> EnableCustomerAsync(EnableCustomerRequest dto);
        Task<BlobResult> RegisterCustomerAsync(RegisterCustomerRequest dto);
        Task<BlobResult> UpdateCustomerAsync(UpdateCustomerRequest dto);

        // Query
        Task<CustomerDisableViewModel> GetCustomerDisableVmAsync(Guid customerId);
        Task<CustomerEnableViewModel> GetCustomerEnableVmAsync(Guid customerId);
        Task<CustomerPageViewModel> GetCustomerPageVmAsync(Guid searchId, int pageNum = 1, int pageSize = 10);
        Task<CustomerSingleViewModel> GetCustomerSingleVmAsync(Guid customerId);
        Task<CustomerUpdateViewModel> GetCustomerUpdateVmAsync(Guid customerId);
    }
}