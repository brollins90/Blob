namespace Blob.Core.Mapping
{
    using Data.Mapping;
    using Models;

    public class CustomerGroupUserMap : BlobEntityTypeConfiguration<CustomerGroupUser>
    {
        public CustomerGroupUserMap()
        {
            ToTable("CustomerGroupUsers");

            HasKey(x => new { x.GroupId, x.UserId });

            Property(x => x.GroupId).HasColumnType("uniqueidentifier").IsRequired();
            Property(x => x.UserId).HasColumnType("uniqueidentifier").IsRequired();
        }
    }
}