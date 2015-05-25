using System.ComponentModel.DataAnnotations.Schema;
using Blob.Core.Models;

namespace Blob.Data.Mapping
{
    public class AuditRecordMap : BlobEntityTypeConfiguration<AuditRecord>
    {
        public AuditRecordMap()
        {
            ToTable("AuditRecords");

            HasKey(x => x.Id);

            Property(x => x.Id).HasColumnType("bigint").IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.AuditLevel).HasColumnType("int").IsRequired();
            Property(x => x.Initiator).HasColumnType("nvarchar").HasMaxLength(128).IsRequired();
            Property(x => x.Operation).HasColumnType("nvarchar").HasMaxLength(128).IsRequired();
            Property(x => x.RecordTimeUtc).HasColumnType("datetime2").IsRequired();
            Property(x => x.ResourceType).HasColumnType("nvarchar").HasMaxLength(128).IsRequired();
            Property(x => x.Resource).HasColumnType("nvarchar").HasMaxLength(128).IsRequired();
        }
    }
}
