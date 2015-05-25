using System.ComponentModel.DataAnnotations.Schema;
using Blob.Core.Models;

namespace Blob.Data.Mapping
{
    public class StatusRecordMap : BlobEntityTypeConfiguration<StatusRecord>
    {
        public StatusRecordMap()
        {
            // Table
            ToTable("StatusRecords");

            // Keys
            HasKey(x => x.Id);

            // Id
            Property(x => x.Id).HasColumnType("bigint").IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            // AlertLevel
            Property(x => x.AlertLevel).HasColumnType("int").IsRequired();
            // CurrentValue
            Property(x => x.CurrentValue).HasColumnType("nvarchar").HasMaxLength(4000).IsRequired();
            // MonitorDescription
            Property(x => x.MonitorDescription).HasColumnType("nvarchar").HasMaxLength(256).IsOptional();
            // MonitorName
            Property(x => x.MonitorName).HasColumnType("nvarchar").HasMaxLength(128).IsRequired();
            // TimeGeneratedUtc
            Property(x => x.TimeGeneratedUtc).HasColumnType("datetime2").IsRequired();
            // TimeSentUtc
            Property(x => x.TimeSentUtc).HasColumnType("datetime2").IsRequired();

            // Device
            Property(x => x.DeviceId).HasColumnType("uniqueidentifier").IsRequired();
            HasRequired(s => s.Device).WithMany(d => d.StatusRecords).HasForeignKey(s => s.DeviceId);
        }
    }
}
