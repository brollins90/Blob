using Blob.Core.Domain;

namespace Blob.Data.Mapping
{
    public class DeviceMap : BlobEntityTypeConfiguration<Device>
    {
        public DeviceMap()
        {
            ToTable("Devices");

            HasKey(x => x.Id);
            Property(x => x.Id)
                .HasColumnType("uniqueidentifier")
                .IsRequired();

            Property(x => x.DeviceName)
                .HasColumnType("nvarchar").HasMaxLength(256)
                .IsRequired();
            Property(x => x.LastActivityDate)
                .HasColumnType("datetime2")
                .IsRequired();

            Property(x => x.DeviceTypeId).HasColumnType("uniqueidentifier");
            HasRequired(d => d.DeviceType).WithMany().HasForeignKey(d => d.DeviceTypeId);

            Property(x => x.CustomerId).HasColumnType("uniqueidentifier");
            HasRequired(d => d.Customer).WithMany(c => c.Devices).HasForeignKey(d => d.CustomerId);
        }
    }
}
