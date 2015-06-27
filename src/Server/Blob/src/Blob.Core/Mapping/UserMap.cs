using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using Blob.Core.Models;
using Blob.Data.Mapping;

namespace Blob.Core.Mapping
{
    public class UserMap : BlobEntityTypeConfiguration<User>
    {
        public UserMap()
        {
            ToTable("Users");

            HasKey(x => x.Id);

            Property(x => x.Id).HasColumnType("uniqueidentifier").IsRequired();
            Property(x => x.CreateDateUtc).HasColumnType("datetime2").IsRequired();
            Property(x => x.LastActivityDate).HasColumnType("datetime2").IsRequired();
            Property(x => x.UserName).HasColumnType("nvarchar").HasMaxLength(256).IsRequired()
                .HasColumnAnnotation(IndexAnnotation.AnnotationName,
                new IndexAnnotation(new IndexAttribute("IX_UserUsername", 1) { IsUnique = true }));
        }
    }
}
