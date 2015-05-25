using Blob.Core.Models;

namespace Blob.Data.Mapping
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
