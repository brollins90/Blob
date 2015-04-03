using Blob.Core.Domain;

namespace Blob.Data.Mapping
{
    public class DeviceMap : BlobEntityTypeConfiguration<Device>
    {
        public DeviceMap()
        {
            ToTable("Devices");
            HasKey(x => x.Id);
        }
    }
}
