using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using Blob.Core.Models;
using Blob.Data.Mapping;

namespace Blob.Core.Mapping
{
    public class CustomerGroupMap : BlobEntityTypeConfiguration<CustomerGroup>
    {
        public CustomerGroupMap()
        {
            ToTable("CustomerGroups");

            HasKey(x => x.Id);

            Property(x => x.Id).HasColumnType("uniqueidentifier").IsRequired();
            Property(x => x.Name).HasColumnType("nvarchar").HasMaxLength(256).IsRequired()
                .HasColumnAnnotation(IndexAnnotation.AnnotationName,
                new IndexAnnotation(new IndexAttribute("IX_CustomerGroupName", 1) { IsUnique = true }));
            Property(x => x.Description).HasColumnType("nvarchar").HasMaxLength(256).IsRequired();

            HasMany(x => x.Roles).WithRequired().HasForeignKey(r => r.GroupId);
            HasMany(x => x.Users).WithRequired().HasForeignKey(u => u.GroupId);
        }
    }
}
