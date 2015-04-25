using Blob.Core.Domain;

namespace Blob.Data.Mapping
{
    public class KeyPairMap : BlobEntityTypeConfiguration<KeyPair>
    {
        public KeyPairMap()
        {
            ToTable("KeyPairs");

            // Id
            HasKey(x => x.Id);
            Property(x => x.Id)
                .HasColumnType("uniqueidentifier")
                .IsRequired();

            // DeviceName
            Property(x => x.AssociatedEntity)
                .HasColumnType("nvarchar").HasMaxLength(256)
                .IsRequired();

            // Keys
            Property(x => x.PrivateKey)
                .HasColumnType("varbinary")
                .IsRequired();
            Property(x => x.PublicKey)
                .HasColumnType("varbinary")
                .IsOptional();
        }
    }
}
