using Blob.Core.Domain;

namespace Blob.Data.Mapping
{
    public class UserMap : BlobEntityTypeConfiguration<User>
    {
        public UserMap()
        {
            ToTable("Users");
            HasKey(x => x.Id);
        }
    }
}
