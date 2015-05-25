using System.ComponentModel.DataAnnotations.Schema;
using Blob.Core.Models;

namespace Blob.Data.Mapping
{
    public class PerformanceRecordMap : BlobEntityTypeConfiguration<PerformanceRecord>
    {
        public PerformanceRecordMap()
        {
            ToTable("PerformanceRecords");

            // Keys
            HasKey(x => x.Id);

            // Critical
            Property(x => x.Critical).HasColumnType("decimal").HasPrecision(18, 5).IsOptional();
            // Id
            Property(x => x.Id).HasColumnType("bigint").IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            // Label
            Property(x => x.Label).HasColumnType("nvarchar").HasMaxLength(128).IsRequired();
            // Max
            Property(x => x.Max).HasColumnType("decimal").HasPrecision(18, 5).IsOptional();
            // Min
            Property(x => x.Min).HasColumnType("decimal").HasPrecision(18, 5).IsOptional();
            // MonitorDescription
            Property(x => x.MonitorDescription).HasColumnType("nvarchar").HasMaxLength(256).IsOptional();
            // MonitorName
            Property(x => x.MonitorName).HasColumnType("nvarchar").HasMaxLength(128).IsRequired();
            // TimeGeneratedUtc
            Property(x => x.TimeGeneratedUtc).HasColumnType("datetime2").IsRequired();
            // UnitOfMeasure
            Property(x => x.UnitOfMeasure).HasColumnType("nvarchar").HasMaxLength(128).IsRequired();
            // Value
            Property(x => x.Value).HasColumnType("decimal").HasPrecision(18, 5).IsRequired();
            // Warning
            Property(x => x.Warning).HasColumnType("decimal").HasPrecision(18, 5).IsOptional();

            //// Device
            //Property(x => x.DeviceId).HasColumnType("uniqueidentifier").IsRequired();
            //HasRequired(s => s.Device).WithMany(d => d.PerformanceRecords).HasForeignKey(s => s.DeviceId);

            //// Status
            //Property(x => x.StatusId).HasColumnType("bigint").IsOptional();
            //HasOptional(x => x.Status).WithMany(d => d.PerformanceRecords).HasForeignKey(x => x.StatusId).WillCascadeOnDelete(false);
        }
    }
}
