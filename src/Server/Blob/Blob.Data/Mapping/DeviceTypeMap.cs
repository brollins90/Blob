using Blob.Core.Domain;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;

namespace Blob.Data.Mapping
{
    public class DeviceTypeMap : BlobEntityTypeConfiguration<DeviceType>
    {
        public DeviceTypeMap()
        {
            ToTable("DeviceTypes");

            HasKey(x => x.Id);
            Property(x => x.Id)
                .HasColumnType("uniqueidentifier")
                .IsRequired();

            Property(x => x.Value)
                .HasColumnType("nvarchar").HasMaxLength(256)
                .IsRequired()
                .HasColumnAnnotation(IndexAnnotation.AnnotationName,
                new IndexAnnotation(
                    new IndexAttribute("IX_DeviceTypeValue", 1) { IsUnique = true }));
        }
    }
}
