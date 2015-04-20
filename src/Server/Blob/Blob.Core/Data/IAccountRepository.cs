using Blob.Core.Domain;
using System.Threading.Tasks;

namespace Blob.Core.Data
{
    public interface IAccountRepository
    {
        Task CreateCustomerAsync(Customer customer);
        Task<Device> FindCustomerByIdAsync(Customer customer);
        Task UpdateCustomerAsync(Customer customer);
        Task DeleteCustomerAsync(Customer customer);
    }
}
