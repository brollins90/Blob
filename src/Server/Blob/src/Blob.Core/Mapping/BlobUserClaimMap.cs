namespace Blob.Core.Mapping
{
    using Data.Mapping;
    using Models;

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