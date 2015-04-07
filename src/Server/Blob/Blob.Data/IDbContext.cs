using System.Data.Entity;
using Blob.Core;

namespace Blob.Data
{
    public interface IDbContext
    {
        DbSet<TEntity> Set<TEntity>() where TEntity : class;

        int SaveChanges();
    }
}
