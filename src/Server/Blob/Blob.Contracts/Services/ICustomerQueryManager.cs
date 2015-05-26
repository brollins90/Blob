using System;
using System.ServiceModel;
using System.Threading.Tasks;
using Blob.Contracts.Models.ViewModels;

namespace Blob.Contracts.Services
{
    [ServiceContract]
    public interface ICustomerQueryManager
    {
        [OperationContract]
        Task<CustomerDisableVm> GetCustomerDisableVmAsync(Guid customerId);

        [OperationContract]
        Task<CustomerEnableVm> GetCustomerEnableVmAsync(Guid customerId);

        [OperationContract]
        Task<CustomerSingleVm> GetCustomerSingleVmAsync(Guid customerId);

        [OperationContract]
        Task<CustomerUpdateVm> GetCustomerUpdateVmAsync(Guid customerId);
    }
}
