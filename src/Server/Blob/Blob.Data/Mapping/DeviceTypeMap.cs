using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using Blob.Core.Models;

namespace Blob.Data.Mapping
{
    public class DeviceTypeMap : BlobEntityTypeConfiguration<DeviceType>
    {
        public DeviceTypeMap()
        {
            ToTable("DeviceTypes");

            HasKey(x => x.Id);

            Property(x => x.Id).HasColumnType("uniqueidentifier").IsRequired();
            Property(x => x.Name).HasColumnType("nvarchar").HasMaxLength(256).IsRequired()
                .HasColumnAnnotation(IndexAnnotation.AnnotationName,
                new IndexAnnotation(new IndexAttribute("IX_DeviceTypeName", 1) { IsUnique = true }));
            Property(x => x.Description).HasColumnType("nvarchar").HasMaxLength(256).IsRequired();
        }
    }
}
