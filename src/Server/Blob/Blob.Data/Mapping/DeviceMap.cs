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
            HasRequired(d => d.DeviceType).WithMany();

            // Customer
            HasRequired(d => d.Customer).WithMany(c => c.Devices);

            // Statuses

            // StatusPerfs
        }
    }
}
