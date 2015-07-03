namespace Blob.Core.Mapping
{
    using Data.Mapping;
    using Models;

    public class CustomerGroupRoleMap : BlobEntityTypeConfiguration<CustomerGroupRole>
    {
        public CustomerGroupRoleMap()
        {
            ToTable("CustomerGroupRoles");

            HasKey(x => new { x.GroupId, x.RoleId });

            Property(x => x.GroupId).HasColumnType("uniqueidentifier").IsRequired();
            Property(x => x.RoleId).HasColumnType("uniqueidentifier").IsRequired();
        }
    }
}