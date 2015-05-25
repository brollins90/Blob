using Blob.Core.Models;

namespace Blob.Data.Mapping
{
    public class CustomerRoleMap : BlobEntityTypeConfiguration<CustomerRole>
    {
        public CustomerRoleMap()
        {
            ToTable("CustomerRoles");

            HasKey(x => new { x.CustomerId, x.RoleId });

            Property(x => x.CustomerId).HasColumnType("uniqueidentifier").IsRequired();
            Property(x => x.RoleId).HasColumnType("uniqueidentifier").IsRequired();
        }
    }
}
