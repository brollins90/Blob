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
            Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            HasRequired(s => s.Device).WithMany(d => d.Statuses).HasForeignKey(s => s.DeviceId);
        }
    }
}
