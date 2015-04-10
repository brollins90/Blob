using Blob.Core.Domain;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blob.Data.Mapping
{
    public class UserMap : BlobEntityTypeConfiguration<User>
    {
        public UserMap()
        {
            ToTable("Users");
            HasKey(x => x.Id);
            Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            HasRequired(u => u.Customer).WithMany(c => c.Users).HasForeignKey(u => u.CustomerId);
        }
    }
}
