using Blob.Core.Domain;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blob.Data.Mapping
{
    public class StatusMap : BlobEntityTypeConfiguration<Status>
    {
        public StatusMap()
        {
            ToTable("Statuses");

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
            Property(x => x.AlertLevel)
                .HasColumnType("int")
                .IsRequired();
            Property(x => x.TimeGenerated)
                .HasColumnType("datetime2")
                .IsRequired();
            Property(x => x.TimeSent)
                .HasColumnType("datetime2")
                .IsRequired();
            Property(x => x.CurrentValue)
                .HasColumnType("nvarchar").HasMaxLength(4000)
                .IsRequired();

            Property(x => x.DeviceId)
                .HasColumnType("uniqueidentifier")
                .IsRequired();
            HasRequired(s => s.Device).WithMany(d => d.Statuses).HasForeignKey(s => s.DeviceId);
        }
    }
}
