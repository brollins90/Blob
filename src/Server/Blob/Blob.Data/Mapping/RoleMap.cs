using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using Blob.Core.Domain;

namespace Blob.Data.Mapping
{
    public class RoleMap : BlobEntityTypeConfiguration<Role>
    {
        public RoleMap()
        {
            ToTable("Roles");

            // Id
            HasKey(x => x.Id);
            Property(x => x.Id)
                .HasColumnType("uniqueidentifier")
                .IsRequired();

            // Name
            Property(x => x.Name)
                .HasColumnType("nvarchar").HasMaxLength(256)
                .IsRequired()
                .HasColumnAnnotation(IndexAnnotation.AnnotationName,
                new IndexAnnotation(
                    new IndexAttribute("IX_RoleName", 1) { IsUnique = true }));

            // Users
        }
    }
}
