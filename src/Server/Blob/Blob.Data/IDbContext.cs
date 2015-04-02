using System.Data.Entity;
using Blob.Core;

namespace Blob.Data
{
    public interface IDbContext
    {
        IDbSet<TEntity> Set<TEntity>() where TEntity : BaseEntity;

        int SaveChanges();
    }
}
