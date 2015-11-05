using Blob.Core.Models;
using Blob.Data.Mapping;

namespace Blob.Core.Mapping
{
    public class BlobUserLoginMap : BlobEntityTypeConfiguration<BlobUserLogin>
    {
        public BlobUserLoginMap()
        {
            ToTable("UserLogins");

            HasKey(x => x.UserId);

            Property(x => x.UserId).HasColumnType("uniqueidentifier").IsRequired();
        }
    }
}
