using System.Linq;
using Blob.Core.Identity.Models;

namespace Blob.Core.Identity.Store
{
    public interface IQueryableGroupStore<TGroup, in TKey> : IGroupStore<TGroup, TKey> where TGroup : IGroup<TKey>
    {
        IQueryable<TGroup> Groups { get; }
    }
}
