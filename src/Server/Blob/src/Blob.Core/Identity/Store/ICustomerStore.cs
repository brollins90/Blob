//namespace Blob.Core.Identity.Store
//{
//    using System;
//    using System.Threading.Tasks;
//    using Core.Models;

//    public interface ICustomerStore : IDisposable
//    {
//        Task CreateCustomerAsync(Customer customer);
//        Task DeleteCustomerAsync(Customer customer);
//        Task<Customer> FindCustomerByIdAsync(Guid customerId);
//        Task<Customer> FindCustomerByNameAsync(string customerName);
//        Task UpdateCustomerAsync(Customer customer);
//    }
//}