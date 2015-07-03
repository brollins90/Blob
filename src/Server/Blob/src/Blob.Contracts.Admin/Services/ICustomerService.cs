using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Blob.Contracts.Models;
using Blob.Contracts.Models.ViewModels;

namespace Blob.Contracts.Services
{
    public interface ICustomerService
    {
        // Command
        Task<BlobResult> DisableCustomerAsync(DisableCustomerDto dto);
        Task<BlobResult> EnableCustomerAsync(EnableCustomerDto dto);
        Task<BlobResult> RegisterCustomerAsync(RegisterCustomerDto dto);
        Task<BlobResult> UpdateCustomerAsync(UpdateCustomerDto dto);
        
        // Query
        Task<CustomerDisableVm> GetCustomerDisableVmAsync(Guid customerId);
        Task<CustomerEnableVm> GetCustomerEnableVmAsync(Guid customerId);
        Task<CustomerPageVm> GetCustomerPageVmAsync(Guid searchId, int pageNum = 1, int pageSize = 10);
        Task<CustomerSingleVm> GetCustomerSingleVmAsync(Guid customerId);
        Task<CustomerUpdateVm> GetCustomerUpdateVmAsync(Guid customerId);

    }
}
