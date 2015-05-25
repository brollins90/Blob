using Blob.Core.Models;

namespace Blob.Data.Mapping
{
    public class BlobPermissionMap : BlobEntityTypeConfiguration<BlobPermission>
    {
        public BlobPermissionMap()
        {
            // Table
            ToTable("BlobPermissions");

            // Keys
            HasKey(x => x.Id);

            // Id 
            Property(x => x.Id).HasColumnType("uniqueidentifier").IsRequired();
            // Operation
            Property(x => x.Operation).HasColumnType("nvarchar").HasMaxLength(128).IsRequired();
            // Resource
            Property(x => x.Resource).HasColumnType("nvarchar").HasMaxLength(128).IsRequired();
        }
    }
}
