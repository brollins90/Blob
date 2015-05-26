using Blob.Core.Models;
using Blob.Data.Mapping;

namespace Blob.Core.Mapping
{
    public class CustomerGroupRoleMap : BlobEntityTypeConfiguration<CustomerGroupRole>
    {
        public CustomerGroupRoleMap()
        {
            ToTable("CustomerRoles");

            HasKey(x => new { x.GroupId, x.RoleId });

            Property(x => x.GroupId).HasColumnType("uniqueidentifier").IsRequired();
            Property(x => x.RoleId).HasColumnType("uniqueidentifier").IsRequired();
        }
    }
}
