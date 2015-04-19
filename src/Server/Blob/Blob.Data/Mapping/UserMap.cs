using Blob.Core.Domain;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;

namespace Blob.Data.Mapping
{
    public class BlobUserClaimMap : BlobEntityTypeConfiguration<BlobUserClaim>
    {
        public BlobUserClaimMap()
        {
            ToTable("UserClaims");

            // Id
            HasKey(x => x.Id);
            Property(x => x.Id)
                .HasColumnType("int")
                .IsRequired();
        }
    }

    public class BlobUserLoginMap : BlobEntityTypeConfiguration<BlobUserLogin>
    {
        public BlobUserLoginMap()
        {
            ToTable("UserLogins");

            // UserId
            HasKey(x => x.UserId);
            Property(x => x.UserId)
                .HasColumnType("uniqueidentifier")
                .IsRequired();
        }
    }

    public class BlobUserRoleMap : BlobEntityTypeConfiguration<BlobUserRole>
    {
        public BlobUserRoleMap()
        {
            ToTable("UserRoles");

            HasKey(x => new { x.RoleId, x.UserId });
            Property(x => x.RoleId)
                .HasColumnType("uniqueidentifier")
                .IsRequired();
            Property(x => x.UserId)
                .HasColumnType("uniqueidentifier")
                .IsRequired();
        }
    }

    public class UserMap : BlobEntityTypeConfiguration<User>
    {
        public UserMap()
        {
            ToTable("Users");
            
            // Id
            HasKey(x => x.Id);
            Property(x => x.Id)
                .HasColumnType("uniqueidentifier")
                .IsRequired();

            // UserName
            Property(x => x.UserName)
                .HasColumnType("nvarchar").HasMaxLength(256)
                .IsRequired()
                .HasColumnAnnotation(IndexAnnotation.AnnotationName,
                new IndexAnnotation(
                    new IndexAttribute("IX_UserUsername", 1) { IsUnique = true }));
            // LastActivityDate
            Property(x => x.LastActivityDate)
                .HasColumnType("datetime2")
                .IsRequired();

            //// Roles
            //HasMany(u => u.Roles)
            //    .WithMany(r => r.Users)
            //    .Map(m => m.MapLeftKey("UserId")
            //               .MapRightKey("RoleId")
            //               .ToTable("UsersInRoles"));

            // Customer
            Property(x => x.CustomerId)
                .HasColumnType("uniqueidentifier")
                .IsRequired();
            HasRequired(u => u.Customer).WithMany(c => c.Users).HasForeignKey(u => u.CustomerId);
        }
    }
}
