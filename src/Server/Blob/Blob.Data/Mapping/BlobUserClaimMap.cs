using Blob.Core.Models;

namespace Blob.Data.Mapping
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
