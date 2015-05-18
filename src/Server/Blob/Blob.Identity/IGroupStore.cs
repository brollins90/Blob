using System;
using System.Threading.Tasks;

namespace Blob.Identity
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