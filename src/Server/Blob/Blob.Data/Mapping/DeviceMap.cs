using Blob.Core.Domain;

namespace Blob.Data.Mapping
{
    public class DeviceMap : BlobEntityTypeConfiguration<Device>
    {
        public DeviceMap()
        {
            ToTable("Devices");
            HasKey(x => x.Id);
            HasRequired(d => d.DeviceType).WithMany().HasForeignKey(d => d.DeviceTypeId);
            HasRequired(d => d.Customer).WithMany(c => c.Devices).HasForeignKey(d => d.CustomerId);
        }
    }
}
