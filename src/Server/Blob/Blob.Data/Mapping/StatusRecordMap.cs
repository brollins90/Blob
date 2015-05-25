using System.ComponentModel.DataAnnotations.Schema;
using Blob.Core.Models;

namespace Blob.Data.Mapping
{
    public class StatusRecordMap : BlobEntityTypeConfiguration<StatusRecord>
    {
        public StatusRecordMap()
        {
            ToTable("StatusRecords");

            HasKey(x => x.Id);

            Property(x => x.Id).HasColumnType("bigint").IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.AlertLevel).HasColumnType("int").IsRequired();
            Property(x => x.CurrentValue).HasColumnType("nvarchar").HasMaxLength(4000).IsRequired();
            Property(x => x.MonitorDescription).HasColumnType("nvarchar").HasMaxLength(256).IsOptional();
            Property(x => x.MonitorName).HasColumnType("nvarchar").HasMaxLength(128).IsRequired();
            Property(x => x.TimeGeneratedUtc).HasColumnType("datetime2").IsRequired();
            Property(x => x.TimeSentUtc).HasColumnType("datetime2").IsRequired();
        }
    }
}
