namespace Blob.Core.Mapping
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Infrastructure.Annotations;
    using Data.Mapping;
    using Models;

    public class RoleMap : BlobEntityTypeConfiguration<Role>
    {
        public RoleMap()
        {
            ToTable("Roles");

            HasKey(x => x.Id);

            Property(x => x.Id).HasColumnType("uniqueidentifier").IsRequired();
            Property(x => x.Name).HasColumnType("nvarchar").HasMaxLength(256).IsRequired()
                .HasColumnAnnotation(IndexAnnotation.AnnotationName,
                new IndexAnnotation(new IndexAttribute("IX_RoleName", 1) { IsUnique = true }));
        }
    }
}