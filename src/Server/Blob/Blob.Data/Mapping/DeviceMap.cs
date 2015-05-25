using Blob.Core.Models;

namespace Blob.Data.Mapping
{
    public class DeviceMap : BlobEntityTypeConfiguration<Device>
    {
        public DeviceMap()
        {
            // Table
            ToTable("Devices");

            // Keys
            HasKey(x => x.Id);

            // Id
            Property(x => x.Id).HasColumnType("uniqueidentifier").IsRequired();
            // DeviceName
            Property(x => x.DeviceName).HasColumnType("nvarchar").HasMaxLength(256).IsRequired();
            // LastActivityDateUtc
            Property(x => x.LastActivityDateUtc).HasColumnType("datetime2").IsRequired();
            // AlertLevel
            Property(x => x.AlertLevel).HasColumnType("int").IsRequired();
            // CreateDateUtc
            Property(x => x.CreateDateUtc).HasColumnType("datetime2").IsRequired();
            // Enabled
            Property(x => x.Enabled).HasColumnType("bit").IsRequired();

            //// DeviceType
            //Property(x => x.DeviceTypeId).HasColumnType("uniqueidentifier");
            //HasRequired(d => d.DeviceType).WithMany().HasForeignKey(d => d.DeviceTypeId);

            //// Customer
            //Property(x => x.CustomerId).HasColumnType("uniqueidentifier");
            //HasRequired(d => d.Customer).WithMany(c => c.Devices).HasForeignKey(d => d.CustomerId);

            //// StatusRecords
            //HasMany(x => x.StatusRecords).WithRequired(sr => sr.Device).HasForeignKey(x => x.DeviceId);
            //// PerformanceRecords
            //HasMany(x => x.PerformanceRecords).WithRequired(sr => sr.Device).HasForeignKey(x => x.DeviceId);
        }
    }
}
