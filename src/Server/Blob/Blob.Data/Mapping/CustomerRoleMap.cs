using Blob.Core.Models;

namespace Blob.Data.Mapping
{
    public class CustomerRoleMap : BlobEntityTypeConfiguration<CustomerRole>
    {
        public CustomerRoleMap()
        {
            // Table
            ToTable("CustomerRoles");

            // Keys
            HasKey(x => new { x.CustomerId, x.RoleId });

            // CustomerId
            Property(x => x.CustomerId).HasColumnType("uniqueidentifier").IsRequired();
            // RoleId
            Property(x => x.RoleId).HasColumnType("uniqueidentifier").IsRequired();
        }
    }
}
