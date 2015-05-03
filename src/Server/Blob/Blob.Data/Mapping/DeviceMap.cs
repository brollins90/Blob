using Blob.Core.Domain;

namespace Blob.Data.Mapping
{
    public class DeviceMap : BlobEntityTypeConfiguration<Device>
    {
        public DeviceMap()
        {
            ToTable("Devices");

            // Id
            HasKey(x => x.Id);
            Property(x => x.Id)
                .HasColumnType("uniqueidentifier")
                .IsRequired();

            // DeviceName
            Property(x => x.DeviceName)
                .HasColumnType("nvarchar").HasMaxLength(256)
                .IsRequired();

            // LastActivityDate
            Property(x => x.LastActivityDate)
                .HasColumnType("datetime2")
                .IsRequired();

            // AlertLevel
            Property(x => x.AlertLevel)
                .HasColumnType("int")
                .IsRequired();

            // CreateDate
            Property(x => x.CreateDate).HasColumnType("datetime2")
                .IsRequired();

            // Enabled
            Property(x => x.Enabled).HasColumnType("bit")
                .IsRequired();

            // DeviceType
            Property(x => x.DeviceTypeId).HasColumnType("uniqueidentifier");
            HasRequired(d => d.DeviceType).WithMany().HasForeignKey(d => d.DeviceTypeId);

            // Customer
            Property(x => x.CustomerId).HasColumnType("uniqueidentifier");
            HasRequired(d => d.Customer).WithMany(c => c.Devices).HasForeignKey(d => d.CustomerId);

            // Status ??
            // StatusPerf ??
        }
    }
}
