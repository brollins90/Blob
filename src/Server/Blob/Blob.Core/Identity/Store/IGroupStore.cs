using System;
using System.Threading.Tasks;
using Blob.Core.Identity.Models;

namespace Blob.Core.Identity.Store
{
    public interface IGroupStore<TGroup, in TKey> : IDisposable where TGroup : IGroup<TKey>
    {
        Task CreateAsync(TGroup group);
        Task DeleteAsync(TGroup group);
        Task<TGroup> FindByIdAsync(TKey groupId);
        Task<TGroup> FindByNameAsync(string groupName);
        Task UpdateAsync(TGroup group);
    }
}