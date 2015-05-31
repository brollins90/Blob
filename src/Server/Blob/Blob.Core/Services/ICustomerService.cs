using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Blob.Contracts.Models;
using Blob.Contracts.Models.ViewModels;

namespace Blob.Core.Services
{
    public interface ICustomerService
    {
        // Command
        Task<BlobResultDto> DisableCustomerAsync(DisableCustomerDto dto);
        Task<BlobResultDto> EnableCustomerAsync(EnableCustomerDto dto);
        Task<BlobResultDto> RegisterCustomerAsync(RegisterCustomerDto dto);
        Task<BlobResultDto> UpdateCustomerAsync(UpdateCustomerDto dto);


        // Query
        Task<CustomerDisableVm> GetCustomerDisableVmAsync(Guid customerId);
        Task<CustomerEnableVm> GetCustomerEnableVmAsync(Guid customerId);
        Task<CustomerSingleVm> GetCustomerSingleVmAsync(Guid customerId);
        Task<CustomerUpdateVm> GetCustomerUpdateVmAsync(Guid customerId);

        Task<IEnumerable<CustomerGroupRoleListItem>> GetCustomerRolesAsync(Guid customerId);
        Task<CustomerGroupPageVm> GetCustomerGroupPageVmAsync(Guid customerId, int pageNum = 1, int pageSize = 10);
    }
}
