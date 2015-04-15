using Blob.Data.Mapping;
using Blob.Data.Migrations;
using log4net;
using System.Data.Entity;

namespace Blob.Data
{
    public class BlobDbContext : DbContext
    {
        private readonly ILog _log;

        public BlobDbContext()
        {
            _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<BlobDbContext, Configuration>());
        }

        public BlobDbContext(string connectionString)
            : base(connectionString)
        {
            _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<BlobDbContext, Configuration>());
        }

        public BlobDbContext(string connectionString, ILog log)
            : base(connectionString)
        {
            _log = log;
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<BlobDbContext, Configuration>());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            _log.Info("Creating model");
            modelBuilder.Configurations.Add(new CustomerMap());
            modelBuilder.Configurations.Add(new DeviceMap());
            modelBuilder.Configurations.Add(new DeviceTypeMap());
            modelBuilder.Configurations.Add(new RoleMap());
            modelBuilder.Configurations.Add(new StatusMap());
            modelBuilder.Configurations.Add(new StatusPerfMap());
            modelBuilder.Configurations.Add(new UserMap());
            modelBuilder.Configurations.Add(new UserSecurityMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}
