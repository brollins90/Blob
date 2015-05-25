using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using Blob.Core.Models;

namespace Blob.Data.Mapping
{
    public class CustomerMap : BlobEntityTypeConfiguration<Customer>
    {
        public CustomerMap()
        {
            // Table
            ToTable("Customers");

            // Keys
            HasKey(x => x.Id);

            // Id 
            Property(x => x.Id).HasColumnType("uniqueidentifier").IsRequired();
            // Name
            Property(x => x.Name).HasColumnType("nvarchar").HasMaxLength(256).IsRequired()
                .HasColumnAnnotation(IndexAnnotation.AnnotationName,
                new IndexAnnotation(
                    new IndexAttribute("IX_CustomerName", 1) { IsUnique = true }));
            // CreatedDateUtc
            Property(x => x.CreateDateUtc).HasColumnType("datetime2").IsRequired();
            // Enabled
            Property(x => x.Enabled).HasColumnType("bit").IsRequired();

            // Devices
            HasMany(x => x.Devices).WithRequired().HasForeignKey(d => d.CustomerId);
            // Customer Roles
            HasMany(x => x.CustomerRoles).WithRequired().HasForeignKey(x2 => x2.CustomerId);
            // Customer Users
            HasMany(x => x.CustomerUsers).WithRequired().HasForeignKey(x2 => x2.CustomerId);
        }
    }
}
