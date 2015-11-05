using System.ComponentModel.DataAnnotations.Schema;
using Blob.Core.Models;
using Blob.Data.Mapping;

namespace Blob.Core.Mapping
{
    public class PerformanceRecordMap : BlobEntityTypeConfiguration<PerformanceRecord>
    {
        public PerformanceRecordMap()
        {
            ToTable("PerformanceRecords");

            HasKey(x => x.Id);

            Property(x => x.Critical).HasColumnType("decimal").HasPrecision(18, 5).IsOptional();
            Property(x => x.Id).HasColumnType("bigint").IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.Label).HasColumnType("nvarchar").HasMaxLength(128).IsRequired();
            Property(x => x.Max).HasColumnType("decimal").HasPrecision(18, 5).IsOptional();
            Property(x => x.Min).HasColumnType("decimal").HasPrecision(18, 5).IsOptional();
            Property(x => x.MonitorDescription).HasColumnType("nvarchar").HasMaxLength(256).IsOptional();
            Property(x => x.MonitorName).HasColumnType("nvarchar").HasMaxLength(128).IsRequired();
            Property(x => x.TimeGeneratedUtc).HasColumnType("datetime2").IsRequired();
            Property(x => x.UnitOfMeasure).HasColumnType("nvarchar").HasMaxLength(128).IsRequired();
            Property(x => x.Value).HasColumnType("decimal").HasPrecision(18, 5).IsRequired();
            Property(x => x.Warning).HasColumnType("decimal").HasPrecision(18, 5).IsOptional();
        }
    }
}
