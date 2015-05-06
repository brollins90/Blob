using System.ComponentModel.DataAnnotations.Schema;
using Blob.Core.Domain;

namespace Blob.Data.Mapping
{
    public class StatusPerfMap : BlobEntityTypeConfiguration<StatusPerf>
    {
        public StatusPerfMap()
        {
            ToTable("StatusPerfs");

            // Id
            HasKey(x => x.Id);
            Property(x => x.Id)
                .HasColumnType("bigint")
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            // MonitorName
            Property(x => x.MonitorName)
                .HasColumnType("nvarchar").HasMaxLength(128)
                .IsRequired();

            // MonitorDescription
            Property(x => x.MonitorDescription)
                .HasColumnType("nvarchar").HasMaxLength(256)
                .IsOptional();

            // TimeGenerated
            Property(x => x.TimeGenerated)
                .HasColumnType("datetime2")
                .IsRequired();

            // Label
            Property(x => x.Label)
                .HasColumnType("nvarchar").HasMaxLength(128)
                .IsRequired();

            // UnitOfMeasure
            Property(x => x.UnitOfMeasure)
                .HasColumnType("nvarchar").HasMaxLength(128)
                .IsRequired();

            // Value
            Property(x => x.Value)
                .HasColumnType("decimal").HasPrecision(18, 5)
                .IsRequired();

            // Warning
            Property(x => x.Warning)
                .HasColumnType("decimal").HasPrecision(18, 5)
                .IsOptional();

            // Critical
            Property(x => x.Critical)
                .HasColumnType("decimal").HasPrecision(18, 5)
                .IsOptional();

            // Min
            Property(x => x.Min)
                .HasColumnType("decimal").HasPrecision(18, 5)
                .IsOptional();

            // Max
            Property(x => x.Max)
                .HasColumnType("decimal").HasPrecision(18, 5)
                .IsOptional();

            // Device
            Property(x => x.DeviceId)
                .HasColumnType("uniqueidentifier")
                .IsRequired();
            HasRequired(s => s.Device).WithMany(d => d.StatusPerfs).HasForeignKey(s => s.DeviceId);

            // Status
            Property(x => x.StatusId)
                .HasColumnType("bigint")
                .IsOptional();
            HasOptional(s => s.Status).WithMany(d => d.StatusPerfs).HasForeignKey(s => s.StatusId).WillCascadeOnDelete(false);
        }
    }
}
