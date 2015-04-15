using Blob.Core.Domain;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;

namespace Blob.Data.Mapping
{
    public class CustomerMap : BlobEntityTypeConfiguration<Customer>
    {
        public CustomerMap()
        {
            ToTable("Customers");

            HasKey(x => x.Id);
            Property(x => x.Id)
                .HasColumnType("uniqueidentifier")
                .IsRequired();

            Property(x => x.Name)
                .HasColumnType("nvarchar").HasMaxLength(256)
                .IsRequired()
                .HasColumnAnnotation(IndexAnnotation.AnnotationName,
                new IndexAnnotation(
                    new IndexAttribute("IX_CustomerName", 1) { IsUnique = true }));
            Property(x => x.CreateDate).HasColumnType("datetime2")
                .IsRequired();
        }
    }
}
