using Blob.Core.Domain;

namespace Blob.Data.Mapping
{
    public class StatusMap : BlobEntityTypeConfiguration<Status>
    {
        public StatusMap()
        {
            ToTable("Statuses");
            HasKey(x => x.Id);
        }
    }
}
