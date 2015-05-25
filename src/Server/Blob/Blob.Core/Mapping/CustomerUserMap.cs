using Blob.Core.Models;
using Blob.Data.Mapping;

namespace Blob.Core.Mapping
{
    public class CustomerUserMap : BlobEntityTypeConfiguration<CustomerUser>
    {
        public CustomerUserMap()
        {
            ToTable("CustomerUsers");

            HasKey(x => new { x.CustomerId, x.UserId });

            Property(x => x.CustomerId).HasColumnType("uniqueidentifier").IsRequired();
            Property(x => x.UserId).HasColumnType("uniqueidentifier").IsRequired();
        }
    }
}
