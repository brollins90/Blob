using Blob.Core.Models;

namespace Blob.Data.Mapping
{
    public class CustomerUserMap : BlobEntityTypeConfiguration<CustomerUser>
    {
        public CustomerUserMap()
        {
            // Table
            ToTable("CustomerUsers");

            // Keys
            HasKey(x => new { x.CustomerId, x.UserId });

            // GroupId
            Property(x => x.CustomerId).HasColumnType("uniqueidentifier").IsRequired();
            // UserId
            Property(x => x.UserId).HasColumnType("uniqueidentifier").IsRequired();
        }
    }
}
