using Blob.Core.Models;
using Blob.Data.Mapping;

namespace Blob.Core.Mapping
{
    public class DeviceMap : BlobEntityTypeConfiguration<Device>
    {
        public DeviceMap()
        {
            ToTable("Devices");

            HasKey(x => x.Id);

            Property(x => x.Id).HasColumnType("uniqueidentifier").IsRequired();
            Property(x => x.DeviceName).HasColumnType("nvarchar").HasMaxLength(256).IsRequired();
            Property(x => x.LastActivityDateUtc).HasColumnType("datetime2").IsRequired();
            Property(x => x.AlertLevel).HasColumnType("int").IsRequired();
            Property(x => x.CreateDateUtc).HasColumnType("datetime2").IsRequired();
            Property(x => x.Enabled).HasColumnType("bit").IsRequired();
        }
    }
}
