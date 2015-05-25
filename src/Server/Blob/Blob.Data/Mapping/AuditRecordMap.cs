using System.ComponentModel.DataAnnotations.Schema;
using Blob.Core.Models;

namespace Blob.Data.Mapping
{
    public class AuditRecordMap : BlobEntityTypeConfiguration<AuditRecord>
    {
        public AuditRecordMap()
        {
            // Table
            ToTable("AuditRecords");

            // Keys
            HasKey(x => x.Id);

            // Id
            Property(x => x.Id).HasColumnType("bigint").IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            // AuditLevel
            Property(x => x.AuditLevel).HasColumnType("int").IsRequired();
            // Initiator
            Property(x => x.Initiator).HasColumnType("nvarchar").HasMaxLength(128).IsRequired();
            // Operation
            Property(x => x.Operation).HasColumnType("nvarchar").HasMaxLength(128).IsRequired();
            // RecordTimeUtc
            Property(x => x.RecordTimeUtc).HasColumnType("datetime2").IsRequired();
            // ResourceScope
            Property(x => x.ResourceType).HasColumnType("nvarchar").HasMaxLength(128).IsRequired();
            // Resource
            Property(x => x.Resource).HasColumnType("nvarchar").HasMaxLength(128).IsRequired();
        }
    }
}
