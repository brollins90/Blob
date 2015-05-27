using System;
using System.Threading.Tasks;
using Blob.Core.Models;

namespace Blob.Core.Identity.Store
{
    public interface ICustomerGroupStore
    {
        Task CreateGroupAsync(CustomerGroup group);
        Task DeleteGroupAsync(CustomerGroup group);
        Task<CustomerGroup> FindGroupByIdAsync(Guid groupId);
        Task UpdateGroupAsync(CustomerGroup group);
    }
}