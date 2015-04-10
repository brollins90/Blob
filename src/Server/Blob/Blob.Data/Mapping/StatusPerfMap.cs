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
            Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            HasRequired(s => s.Device).WithMany(d => d.StatusPerfs).HasForeignKey(s => s.DeviceId);
        }
    }
}
