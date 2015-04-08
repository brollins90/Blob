using Blob.Core.Domain;

namespace Blob.Data.Mapping
{
    public class StatusPerfMap : BlobEntityTypeConfiguration<StatusPerf>
    {
        public StatusPerfMap()
        {
            ToTable("StatusPerfs");
            HasKey(x => x.Id);
        }
    }
}
