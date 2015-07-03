namespace Blob.Core.Mapping
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Infrastructure.Annotations;
    using Data.Mapping;
    using Models;

    public class NotificationScheduleMap : BlobEntityTypeConfiguration<NotificationSchedule>
    {
        public NotificationScheduleMap()
        {
            ToTable("NotificationSchedules");

            HasKey(x => x.Id);

            Property(x => x.Id).HasColumnType("uniqueidentifier").IsRequired();
            Property(x => x.Name).HasColumnType("nvarchar").HasMaxLength(256).IsRequired()
                .HasColumnAnnotation(IndexAnnotation.AnnotationName,
                new IndexAnnotation(new IndexAttribute("IX_NotificationScheduleName", 1) { IsUnique = true }));
            Property(x => x.Description).HasColumnType("nvarchar").HasMaxLength(256).IsRequired();
        }
    }
}