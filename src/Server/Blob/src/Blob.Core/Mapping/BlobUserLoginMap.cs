namespace Blob.Core.Mapping
{
    using Data.Mapping;
    using Models;

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