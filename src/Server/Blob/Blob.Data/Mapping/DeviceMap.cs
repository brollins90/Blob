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

            // DeviceType
            Property(x => x.DeviceTypeId).HasColumnType("uniqueidentifier");
            HasRequired(d => d.DeviceType).WithMany().HasForeignKey(d => d.DeviceTypeId);

            // Customer
            Property(x => x.CustomerId).HasColumnType("uniqueidentifier");
            HasRequired(d => d.Customer).WithMany(c => c.Devices).HasForeignKey(d => d.CustomerId);

            // Statuses
            //HasMany(d => d.Statuses).WithRequired().HasForeignKey(s => s.DeviceId);

            // StatusPerfs
            //HasMany(d => d.StatusPerfs).WithRequired().HasForeignKey(s => s.DeviceId);
        }
    }
}
