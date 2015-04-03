using Blob.Core.Domain;

namespace Blob.Data.Mapping
{
    public class DeviceTypeMap : BlobEntityTypeConfiguration<DeviceType>
    {
        public DeviceTypeMap()
        {
            ToTable("DeviceTypes");
            HasKey(x => x.Id);
        }
    }
}
