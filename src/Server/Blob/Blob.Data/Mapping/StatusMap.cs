using Blob.Core.Domain;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blob.Data.Mapping
{
    public class StatusMap : BlobEntityTypeConfiguration<Status>
    {
        public StatusMap()
        {
            ToTable("Statuses");

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

            // AlertLevel
            Property(x => x.AlertLevel)
                .HasColumnType("int")
                .IsRequired();

            // TimeGenerated
            Property(x => x.TimeGenerated)
                .HasColumnType("datetime2")
                .IsRequired();

            // TimeSent
            Property(x => x.TimeSent)
                .HasColumnType("datetime2")
                .IsRequired();

            // CurrentValue
            Property(x => x.CurrentValue)
                .HasColumnType("nvarchar").HasMaxLength(4000)
                .IsRequired();

            // Device
            HasRequired(s => s.Device).WithMany(d => d.Statuses).HasForeignKey(s => s.DeviceId);

            //Property(x => x.DeviceId)
            //    .HasColumnType("uniqueidentifier")
            //    .IsRequired();
            //HasRequired(s => s.Device).WithMany(d => d.Statuses).HasForeignKey(s => s.DeviceId);
        }
    }
}
