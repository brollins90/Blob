using Blob.Core.Models;
using Blob.Data.Mapping;

namespace Blob.Core.Mapping
{
    public class BlobUserRoleMap : BlobEntityTypeConfiguration<BlobUserRole>
    {
        public BlobUserRoleMap()
        {
            ToTable("UserRoles");

            HasKey(x => new { x.RoleId, x.UserId });

            Property(x => x.RoleId).HasColumnType("uniqueidentifier").IsRequired();
            Property(x => x.UserId).HasColumnType("uniqueidentifier").IsRequired();
        }
    }
}
