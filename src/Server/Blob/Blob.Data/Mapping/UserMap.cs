using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using Blob.Core.Models;

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
            // Table
            ToTable("Users");

            // Keys
            HasKey(x => x.Id);

            // Id
            Property(x => x.Id).HasColumnType("uniqueidentifier").IsRequired();
            // CreateDateUtc
            Property(x => x.CreateDateUtc).HasColumnType("datetime2").IsRequired();
            // LastActivityDateUtc
            Property(x => x.LastActivityDate).HasColumnType("datetime2").IsRequired();
            // Username
            Property(x => x.UserName).HasColumnType("nvarchar").HasMaxLength(256).IsRequired()
                .HasColumnAnnotation(IndexAnnotation.AnnotationName,
                new IndexAnnotation(
                    new IndexAttribute("IX_UserUsername", 1) { IsUnique = true }));
        }
    }
}
