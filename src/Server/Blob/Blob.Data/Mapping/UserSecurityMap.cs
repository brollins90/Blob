using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using Blob.Core.Domain;

namespace Blob.Data.Mapping
{
    public class UserSecurityMap : BlobEntityTypeConfiguration<UserSecurity>
    {
        public UserSecurityMap()
        {
            ToTable("UserSecurities");

            // UserId is the Key
            HasKey(x => x.UserId);
            Property(x => x.UserId)
                .HasColumnType("uniqueidentifier")
                .IsRequired();
            // User also requires a Foreign Key, I dont think I can specifiy that UserId
            // is the name though.  I think it is done by convention.
            HasRequired(x => x.User).WithOptional();

            Property(x => x.Password)
                .HasColumnType("nvarchar").HasMaxLength(128)
                .IsRequired();
            Property(x => x.PasswordFormat)
                .HasColumnType("int")
                .IsRequired();
            Property(x => x.PasswordSalt)
                .HasColumnType("nvarchar").HasMaxLength(128)
                .IsRequired();
            Property(x => x.MobilePin)
                .HasColumnType("nvarchar").HasMaxLength(128)
                .IsOptional();
            Property(x => x.Email)
                .HasColumnType("nvarchar").HasMaxLength(256)
                .IsRequired()
                .HasColumnAnnotation(IndexAnnotation.AnnotationName,
                new IndexAnnotation(
                    new IndexAttribute("IX_UserSecurityEmail", 1) { IsUnique = true }));
            Property(x => x.PasswordQuestion)
                .HasColumnType("nvarchar").HasMaxLength(256)
                .IsOptional();
            Property(x => x.PasswordAnswer)
                .HasColumnType("nvarchar").HasMaxLength(128)
                .IsOptional();
            Property(x => x.HasVerifiedEmail)
                .HasColumnType("bit")
                .IsRequired();
            Property(x => x.IsApproved)
                .HasColumnType("bit")
                .IsRequired();
            Property(x => x.IsLockedOut)
                .HasColumnType("bit")
                .IsRequired();
            Property(x => x.CreateDate)
                .HasColumnType("datetime2")
                .IsRequired();
            Property(x => x.LastLoginDate)
                .HasColumnType("datetime2")
                .IsOptional();
            Property(x => x.LastPasswordChangedDate)
                .HasColumnType("datetime2")
                .IsOptional();
            Property(x => x.LastLockoutDate)
                .HasColumnType("datetime2")
                .IsOptional();
            Property(x => x.FailedPasswordAnswerAttemptCount)
                .HasColumnType("int")
                .IsOptional();
            Property(x => x.FailedPasswordAnswerAttemptWindowStart)
                .HasColumnType("datetime2")
                .IsOptional();
            Property(x => x.FailedPasswordAttemptCount)
                .HasColumnType("int")
                .IsOptional();
            Property(x => x.FailedPasswordAttemptWindowStart)
                .HasColumnType("datetime2")
                .IsOptional();
            Property(x => x.Comment)
                .HasColumnType("nvarchar").HasMaxLength(256)
                .IsOptional();
        }
    }
}
