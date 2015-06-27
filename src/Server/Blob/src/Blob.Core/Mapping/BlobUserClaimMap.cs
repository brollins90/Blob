using Blob.Core.Models;
using Blob.Data.Mapping;

namespace Blob.Core.Mapping
{
    public class BlobUserClaimMap : BlobEntityTypeConfiguration<BlobUserClaim>
    {
        public BlobUserClaimMap()
        {
            ToTable("UserClaims");

            HasKey(x => x.Id);

            Property(x => x.Id).HasColumnType("int").IsRequired();
        }
    }
}
