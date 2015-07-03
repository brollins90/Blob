namespace Blob.Core.Mapping
{
    using Data.Mapping;
    using Models;

    public class BlobPermissionMap : BlobEntityTypeConfiguration<BlobPermission>
    {
        public BlobPermissionMap()
        {
            ToTable("BlobPermissions");

            HasKey(x => x.Id);

            Property(x => x.Id).HasColumnType("uniqueidentifier").IsRequired();
            Property(x => x.Operation).HasColumnType("nvarchar").HasMaxLength(128).IsRequired();
            Property(x => x.Resource).HasColumnType("nvarchar").HasMaxLength(128).IsRequired();
        }
    }
}