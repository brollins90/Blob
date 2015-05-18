using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using Blob.Core.Domain;

namespace Blob.Data.Mapping
{

    public class BlobGroupMap : BlobEntityTypeConfiguration<BlobGroup>
    {
        public BlobGroupMap()
        {
            ToTable("Groups");

            // Id
            HasKey(x => x.Id);
            Property(x => x.Id)
                .HasColumnType("uniqueidentifier")
                .IsRequired();

            // Name
            Property(x => x.Name)
                .HasColumnType("nvarchar").HasMaxLength(256)
                .IsRequired()
                .HasColumnAnnotation(IndexAnnotation.AnnotationName,
                new IndexAnnotation(
                    new IndexAttribute("IX_GroupName", 1) { IsUnique = true }));

            // Users
            HasMany<BlobUserGroup>((BlobGroup g) => g.Users)
                .WithRequired()
                .HasForeignKey<Guid>((BlobUserGroup ug) => ug.GroupId);

            // Users
            HasMany<BlobGroupRole>((BlobGroup g) => g.Roles)
                .WithRequired()
                .HasForeignKey<Guid>((BlobGroupRole gr) => gr.GroupId);
        }
    }

    public class BlobGroupRoleMap : BlobEntityTypeConfiguration<BlobGroupRole>
    {
        public BlobGroupRoleMap()
        {
            ToTable("GroupRoles");

            HasKey(x => new { x.GroupId, x.RoleId });

            Property(x => x.GroupId)
                .HasColumnType("uniqueidentifier")
                .IsRequired();
            Property(x => x.RoleId)
                .HasColumnType("uniqueidentifier")
                .IsRequired();
        }
    }

    public class BlobUserGroupMap : BlobEntityTypeConfiguration<BlobUserGroup>
    {
        public BlobUserGroupMap()
        {
            ToTable("UserGroups");

            HasKey(x => new { x.UserId, x.GroupId });

            Property(x => x.UserId)
                .HasColumnType("uniqueidentifier")
                .IsRequired();
            Property(x => x.GroupId)
                .HasColumnType("uniqueidentifier")
                .IsRequired();
        }
    }
}
