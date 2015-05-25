using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using Blob.Core.Models;

namespace Blob.Data.Mapping
{
    public class CustomerMap : BlobEntityTypeConfiguration<Customer>
    {
        public CustomerMap()
        {
            ToTable("Customers");

            HasKey(x => x.Id);

            Property(x => x.Id).HasColumnType("uniqueidentifier").IsRequired();
            Property(x => x.Name).HasColumnType("nvarchar").HasMaxLength(256).IsRequired()
                .HasColumnAnnotation(IndexAnnotation.AnnotationName,
                new IndexAnnotation(new IndexAttribute("IX_CustomerName", 1) { IsUnique = true }));
            Property(x => x.CreateDateUtc).HasColumnType("datetime2").IsRequired();
            Property(x => x.Enabled).HasColumnType("bit").IsRequired();

            HasMany(x => x.Devices).WithRequired().HasForeignKey(d => d.CustomerId);
            HasMany(x => x.CustomerRoles).WithRequired().HasForeignKey(x2 => x2.CustomerId);
            HasMany(x => x.CustomerUsers).WithRequired().HasForeignKey(x2 => x2.CustomerId);
        }
    }
}
