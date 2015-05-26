using System;
using System.ServiceModel;
using System.Threading.Tasks;
using Blob.Contracts.Models;

namespace Blob.Contracts.Services
{
    [ServiceContract]
    public interface ICustomerCommandManager
    {
        //[OperationContract]
        //Task DeleteCustomerAsync(Guid customerId);
        [OperationContract]
        Task DisableCustomerAsync(DisableCustomerDto dto);
        
        [OperationContract]
        Task EnableCustomerAsync(EnableCustomerDto dto);
        
        [OperationContract]
        Task RegisterCustomerAsync(RegisterCustomerDto dto);
        
        //[OperationContract]
        //Task RemoveUserFromCustomersAsync(Guid userId);

        [OperationContract]
        Task UpdateCustomerAsync(UpdateCustomerDto dto);
    }
}
