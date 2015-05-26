using System;
using System.Threading.Tasks;
using Blob.Core.Models;

namespace Blob.Core.Identity.Store
{
    public interface ICustomerGroupStore
    {
        Task CreateAsync(CustomerGroup group);
        Task DeleteAsync(CustomerGroup group);
        Task<CustomerGroup> FindByIdAsync(Guid groupId);
        Task UpdateAsync(CustomerGroup group);
    }
}