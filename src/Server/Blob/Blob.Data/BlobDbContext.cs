using Blob.Core;
using Blob.Data.Mapping;
using Blob.Data.Migrations;
using log4net;
using System.Data.Entity;

namespace Blob.Data
{
    public class BlobDbContext : DbContext, IDbContext
    {
        private readonly ILog _log;

        public BlobDbContext()
        {
            _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<BlobDbContext, Configuration>());
        }

        public BlobDbContext(string connectionString, ILog log)
            : base(connectionString)
        {
            _log = log;
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<BlobDbContext, Configuration>());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            _log.Info("Creating model");
            modelBuilder.Configurations.Add(new CustomerMap());
            modelBuilder.Configurations.Add(new DeviceMap());
            modelBuilder.Configurations.Add(new DeviceTypeMap());
            modelBuilder.Configurations.Add(new StatusMap());
            modelBuilder.Configurations.Add(new UserMap());

            base.OnModelCreating(modelBuilder);
        }

        public new DbSet<TEntity> Set<TEntity>() where TEntity : BaseEntity
        {
            return base.Set<TEntity>();
        }
    }
}
