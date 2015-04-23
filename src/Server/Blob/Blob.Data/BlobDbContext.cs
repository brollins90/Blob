using System;
using System.Data.Entity;
using Blob.Core.Domain;
using Blob.Data.Identity;
using Blob.Data.Mapping;
using Blob.Data.Migrations;
using log4net;

namespace Blob.Data
{
    public class BlobDbContext : GenericDbContext<User, Role, Guid, BlobUserLogin, BlobUserRole, BlobUserClaim>
    {
        private readonly ILog _log;

        public BlobDbContext()
            : base("BlobDbContext")
        {
            _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            _log.Debug("Constructing BlobDbContext");
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<BlobDbContext, Configuration>());
        }

        public BlobDbContext(string connectionString)
            : base(connectionString)
        {
            _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            _log.Debug("Constructing BlobDbContext");
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<BlobDbContext, Configuration>());
        }

        public BlobDbContext(string connectionString, ILog log)
            : base(connectionString)
        {
            _log = log;
            _log.Debug("Constructing BlobDbContext");
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<BlobDbContext, Configuration>());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            _log.Debug("Creating model");

            modelBuilder.Configurations.Add(new BlobUserClaimMap());
            modelBuilder.Configurations.Add(new BlobUserLoginMap());
            modelBuilder.Configurations.Add(new BlobUserRoleMap());
            modelBuilder.Configurations.Add(new CustomerMap());
            modelBuilder.Configurations.Add(new DeviceMap());
            modelBuilder.Configurations.Add(new DeviceTypeMap());
            modelBuilder.Configurations.Add(new RoleMap());
            modelBuilder.Configurations.Add(new StatusMap());
            modelBuilder.Configurations.Add(new StatusPerfMap());
            modelBuilder.Configurations.Add(new UserMap());
        }
    }
}
