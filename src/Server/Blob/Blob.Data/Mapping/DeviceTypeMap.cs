using Blob.Core.Domain;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blob.Data.Mapping
{
    public class DeviceTypeMap : BlobEntityTypeConfiguration<DeviceType>
    {
        public DeviceTypeMap()
        {
            ToTable("DeviceTypes");
            HasKey(x => x.Id);
            Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }
    }
}
