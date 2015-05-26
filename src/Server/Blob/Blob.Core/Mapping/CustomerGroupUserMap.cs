using Blob.Core.Models;
using Blob.Data.Mapping;

namespace Blob.Core.Mapping
{
    public class CustomerGroupUserMap : BlobEntityTypeConfiguration<CustomerGroupUser>
    {
        public CustomerGroupUserMap()
        {
            ToTable("CustomerUsers");

            HasKey(x => new { x.GroupId, x.UserId });

            Property(x => x.GroupId).HasColumnType("uniqueidentifier").IsRequired();
            Property(x => x.UserId).HasColumnType("uniqueidentifier").IsRequired();
        }
    }
}
