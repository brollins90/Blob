using Blob.Core;
using System.Data.Entity;
using Blob.Data.Mapping;
using Blob.Data.Migrations;

namespace Blob.Data
{
    public class BlobDbContext : DbContext, IDbContext
    {
        public BlobDbContext()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<BlobDbContext, Configuration>());
        }

        public BlobDbContext(string connectionString)
            : base(connectionString)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<BlobDbContext, Configuration>());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new CustomerMap());
            modelBuilder.Configurations.Add(new DeviceMap());
            modelBuilder.Configurations.Add(new DeviceTypeMap());
            modelBuilder.Configurations.Add(new StatusMap());
            modelBuilder.Configurations.Add(new UserMap());

            base.OnModelCreating(modelBuilder);
        }

        public new IDbSet<TEntity> Set<TEntity>() where TEntity : BaseEntity
        {
            return base.Set<TEntity>();
        }
    }
}
