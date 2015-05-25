using System;
using System.Data.Entity;
using Blob.Core.Models;
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
            : this("BlobDbContext") { }

        public BlobDbContext(string connectionString)
            : this(connectionString, LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType)) { }

        public BlobDbContext(string connectionString, ILog log)
            : base(connectionString)
        {
            _log = log;
            _log.Debug("Constructing BlobDbContext");
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<BlobDbContext, Configuration>());
        }

        public DbSet<AuditRecord> AuditLog { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<DeviceType> DeviceTypes { get; set; }
        public DbSet<StatusRecord> DeviceStatuses { get; set; }
        public DbSet<PerformanceRecord> DevicePerfDatas { get; set; }
        public DbSet<CustomerRole> Groups { get; set; }
        //public DbSet<KeyPair> Keys { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            _log.Debug("Creating model");

            modelBuilder.Configurations.Add(new BlobUserClaimMap());
            modelBuilder.Configurations.Add(new BlobUserLoginMap());
            modelBuilder.Configurations.Add(new BlobUserRoleMap());
            modelBuilder.Configurations.Add(new AuditRecordMap());
            modelBuilder.Configurations.Add(new BlobPermissionMap());
            modelBuilder.Configurations.Add(new CustomerMap());
            modelBuilder.Configurations.Add(new CustomerRoleMap());
            modelBuilder.Configurations.Add(new CustomerUserMap());
            modelBuilder.Configurations.Add(new DeviceMap());
            modelBuilder.Configurations.Add(new DeviceTypeMap());
            modelBuilder.Configurations.Add(new PerformanceRecordMap());
            modelBuilder.Configurations.Add(new RoleMap());
            modelBuilder.Configurations.Add(new StatusRecordMap());
            modelBuilder.Configurations.Add(new UserMap());
        }
    }
}
