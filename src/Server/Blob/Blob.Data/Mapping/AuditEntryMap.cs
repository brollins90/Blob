using System.ComponentModel.DataAnnotations.Schema;
using Blob.Core.Domain;

namespace Blob.Data.Mapping
{
    public class AuditEntryMap : BlobEntityTypeConfiguration<AuditEntry>
    {
        public AuditEntryMap()
        {
            ToTable("Audit");

            // Id
            HasKey(x => x.Id);
            Property(x => x.Id)
                .HasColumnType("bigint")
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            // Time
            Property(x => x.Time)
                .HasColumnType("datetime2")
                .IsRequired();

            Property(x => x.Initiator)
                .HasColumnType("nvarchar").HasMaxLength(128)
                .IsRequired();

            Property(x => x.AuditLevel)
                .HasColumnType("int")
                .IsRequired();

            Property(x => x.Operation)
                .HasColumnType("nvarchar").HasMaxLength(128)
                .IsRequired();

            Property(x => x.ResourceType)
                .HasColumnType("nvarchar").HasMaxLength(128)
                .IsRequired();

            Property(x => x.Resource)
                .HasColumnType("nvarchar").HasMaxLength(128)
                .IsRequired();
        }
    }
}
