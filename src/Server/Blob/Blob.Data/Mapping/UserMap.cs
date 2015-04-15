using Blob.Core.Domain;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;

namespace Blob.Data.Mapping
{
    public class UserMap : BlobEntityTypeConfiguration<User>
    {
        public UserMap()
        {
            ToTable("Users");

            HasKey(x => x.Id);
            Property(x => x.Id)
                .HasColumnType("uniqueidentifier")
                .IsRequired();

            Property(x => x.Username)
                .HasColumnType("nvarchar").HasMaxLength(256)
                .IsRequired()
                .HasColumnAnnotation(IndexAnnotation.AnnotationName,
                new IndexAnnotation(
                    new IndexAttribute("IX_UserUsername", 1) { IsUnique = true }));
            Property(x => x.LastActivityDate)
                .HasColumnType("datetime2")
                .IsRequired();

            HasMany(u => u.Roles)
                .WithMany(r => r.Users)
                .Map(m => m.MapLeftKey("UserId")
                           .MapRightKey("RoleId")
                           .ToTable("UsersInRoles"));

            Property(x => x.CustomerId)
                .HasColumnType("uniqueidentifier")
                .IsRequired();
            HasRequired(u => u.Customer).WithMany(c => c.Users).HasForeignKey(u => u.CustomerId);
        }
    }
}
