using Blob.Core.Domain;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blob.Data.Mapping
{
    public class StatusPerfMap : BlobEntityTypeConfiguration<StatusPerf>
    {
        public StatusPerfMap()
        {
            ToTable("StatusPerfs");

            HasKey(x => x.Id);
            Property(x => x.Id)
                .HasColumnType("bigint")
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(x => x.MonitorName)
                .HasColumnType("nvarchar").HasMaxLength(128)
                .IsRequired();
            Property(x => x.MonitorDescription)
                .HasColumnType("nvarchar").HasMaxLength(256)
                .IsOptional();
            Property(x => x.TimeGenerated)
                .HasColumnType("datetime2")
                .IsRequired();
            Property(x => x.Label)
                .HasColumnType("nvarchar").HasMaxLength(128)
                .IsRequired();
            Property(x => x.UnitOfMeasure)
                .HasColumnType("nvarchar").HasMaxLength(128)
                .IsRequired();
            Property(x => x.Value)
                .HasColumnType("decimal").HasPrecision(18, 5)
                .IsRequired();
            Property(x => x.Warning)
                .HasColumnType("decimal").HasPrecision(18, 5)
                .IsOptional();
            Property(x => x.Critical)
                .HasColumnType("decimal").HasPrecision(18, 5)
                .IsOptional();
            Property(x => x.Min)
                .HasColumnType("decimal").HasPrecision(18, 5)
                .IsOptional();
            Property(x => x.Max)
                .HasColumnType("decimal").HasPrecision(18, 5)
                .IsOptional();

            Property(x => x.DeviceId)
                .HasColumnType("uniqueidentifier")
                .IsRequired();
            HasRequired(s => s.Device).WithMany(d => d.StatusPerfs).HasForeignKey(s => s.DeviceId);
        }
    }
}
