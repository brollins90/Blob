using Blob.Core;
using System.Data.Entity;

namespace Blob.Data
{
    public class BlobDbContext : DbContext, IDbContext
    {
        public BlobDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
        }

        public new IDbSet<TEntity> Set<TEntity>() where TEntity : BaseEntity
        {
            return base.Set<TEntity>();
        }
    }
}
