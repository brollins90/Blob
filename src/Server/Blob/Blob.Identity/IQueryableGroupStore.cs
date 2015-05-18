using System.Linq;

namespace Blob.Identity
{
    public interface IQueryableGroupStore<TGroup, in TKey> : IGroupStore<TGroup, TKey> where TGroup : IGroup<TKey>
    {
        IQueryable<TGroup> Groups { get; }
    }
}
