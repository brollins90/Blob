using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Blob.Core.Domain;

namespace Blob.Core.Data
{
    public interface IAccountRepository
    {
        Task CreateCustomerAsync(Customer customer);
        Task<Customer> FindCustomerByIdAsync(Guid customerId);
        Task<IList<Customer>> GetAllCustomersAsync(); 
        Task UpdateCustomerAsync(Customer customer);
        Task DeleteCustomerAsync(Guid customerId);


        Task<Device> FindDeviceByIdAsync(Guid deviceId);
        Task<IList<Device>> GetAllDevicesAsync(); 
    }
}
