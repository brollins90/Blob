using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using Blob.Core.Models;

namespace Blob.Data.Mapping
{
    public class RoleMap : BlobEntityTypeConfiguration<Role>
    {
        public RoleMap()
        {
            // Table
            ToTable("Roles");

            // Keys
            HasKey(x => x.Id);

            // Id
            Property(x => x.Id).HasColumnType("uniqueidentifier").IsRequired();
            // Name
            Property(x => x.Name).HasColumnType("nvarchar").HasMaxLength(256).IsRequired()
                .HasColumnAnnotation(IndexAnnotation.AnnotationName,
                new IndexAnnotation(
                    new IndexAttribute("IX_RoleName", 1) { IsUnique = true }));
        }
    }
}
