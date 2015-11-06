using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.Validation;
using System.Globalization;
using System.Linq;
using Blob.Core.Identity.Models;
using Blob.Core.Identity.Store;

namespace Blob.Core.Identity
{
    public class GenericDbContext<TUser, TRole, TKey, TUserLogin, TUserRole, TUserClaim> : DbContext
        where TUser : GenericUser<TKey, TUserLogin, TUserRole, TUserClaim>
        where TRole : GenericRole<TKey, TUserRole>
        where TUserLogin : GenericUserLogin<TKey>
        where TUserRole : GenericUserRole<TKey>
        where TUserClaim : GenericUserClaim<TKey>
    {
        public GenericDbContext(string connectionString)
            : base(connectionString) { }

        public GenericDbContext(DbConnection existingConnection, DbCompiledModel model, bool contextOwnsConnection)
            : base(existingConnection, model, contextOwnsConnection) { }

        public GenericDbContext(DbCompiledModel model)
            : base(model) { }

        public GenericDbContext(DbConnection existingConnection, bool contextOwnsConnection)
            : base(existingConnection, contextOwnsConnection) { }

        public GenericDbContext(string connectionString, DbCompiledModel model)
            : base(connectionString, model) { }

        public virtual IDbSet<TUser> Users { get; set; }
        public virtual IDbSet<TRole> Roles { get; set; }
        public bool RequireUniqueEmail { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            if (modelBuilder == null)
            {
                throw new ArgumentNullException("modelBuilder");
            }

            // Needed to ensure subclasses share the same table
            var user = modelBuilder.Entity<TUser>().ToTable("Users");
            user.HasMany(u => u.Roles).WithRequired().HasForeignKey(ur => ur.UserId);
            user.HasMany(u => u.Claims).WithRequired().HasForeignKey(uc => uc.UserId);
            user.HasMany(u => u.Logins).WithRequired().HasForeignKey(ul => ul.UserId);
            user.Property(u => u.UserName)
                .IsRequired()
                .HasMaxLength(256)
                .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("UserNameIndex") { IsUnique = true }));
            var genericUser = modelBuilder.Entity<GenericUser>().ToTable("Users");
            genericUser.HasMany(u => u.Roles).WithRequired().HasForeignKey(ur => ur.UserId);
            genericUser.HasMany(u => u.Claims).WithRequired().HasForeignKey(uc => uc.UserId);
            genericUser.HasMany(u => u.Logins).WithRequired().HasForeignKey(ul => ul.UserId);
            genericUser.Property(u => u.UserName)
                .IsRequired()
                .HasMaxLength(256)
                .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("UserNameIndex") { IsUnique = true }));

            if (RequireUniqueEmail)
            {
                user.Property(u => u.Email)
                    .IsRequired()
                    .HasMaxLength(256)
                    .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("EmailIndex") { IsUnique = true }));
                genericUser.Property(u => u.Email)
                    .IsRequired()
                    .HasMaxLength(256)
                    .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("EmailIndex") { IsUnique = true }));
            }
            else
            {
                user.Property(u => u.Email).HasMaxLength(256);
                genericUser.Property(u => u.Email).HasMaxLength(256);
            }

            modelBuilder.Entity<TUserRole>()
                .HasKey(r => new { r.UserId, r.RoleId })
                .ToTable("UserRoles");
            modelBuilder.Entity<GenericUserRole>()
                .HasKey(r => new { r.UserId, r.RoleId })
                .ToTable("UserRoles");

            modelBuilder.Entity<TUserLogin>()
                .HasKey(l => new { l.LoginProvider, l.ProviderKey, l.UserId })
                .ToTable("UserLogins");
            modelBuilder.Entity<GenericUserLogin>()
                 .HasKey(l => new { l.LoginProvider, l.ProviderKey, l.UserId })
                 .ToTable("UserLogins");

            modelBuilder.Entity<TUserClaim>()
                .ToTable("UserClaims");
            modelBuilder.Entity<GenericUserClaim>()
                .ToTable("UserClaims");

            var role = modelBuilder.Entity<TRole>()
                .ToTable("Roles");
            role.Property(r => r.Name)
                .IsRequired()
                .HasMaxLength(256)
                .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("RoleNameIndex") { IsUnique = true }));
            role.HasMany(r => r.Users).WithRequired().HasForeignKey(ur => ur.RoleId);
            var genericRole = modelBuilder.Entity<GenericRole>()
                .ToTable("Roles");
            genericRole.Property(r => r.Name)
                .IsRequired()
                .HasMaxLength(256)
                .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("RoleNameIndex") { IsUnique = true }));
            genericRole.HasMany(r => r.Users).WithRequired().HasForeignKey(ur => ur.RoleId);
        }

        protected override DbEntityValidationResult ValidateEntity(DbEntityEntry entityEntry, IDictionary<object, object> items)
        {
            if (entityEntry != null && entityEntry.State == EntityState.Added)
            {
                var errors = new List<DbValidationError>();
                var user = entityEntry.Entity as TUser;
                //check for uniqueness of user name and email
                if (user != null)
                {
                    if (Users.Any(u => String.Equals(u.UserName, user.UserName)))
                    {
                        errors.Add(new DbValidationError("User",
                            String.Format(CultureInfo.CurrentCulture, "IdentityResource.DuplicateUserName", user.UserName)));
                    }
                    if (RequireUniqueEmail && Users.Any(u => String.Equals(u.Email, user.Email)))
                    {
                        errors.Add(new DbValidationError("User",
                            String.Format(CultureInfo.CurrentCulture, "IdentityResource.DuplicateEmail", user.Email)));
                    }
                }
                else
                {
                    var role = entityEntry.Entity as TRole;
                    //check for uniqueness of role name
                    if (role != null && Roles.Any(r => String.Equals(r.Name, role.Name)))
                    {
                        errors.Add(new DbValidationError("Role",
                            String.Format(CultureInfo.CurrentCulture, "IdentityResource.RoleAlreadyExists", role.Name)));
                    }
                }
                if (errors.Any())
                {
                    return new DbEntityValidationResult(entityEntry, errors);
                }
            }
            return base.ValidateEntity(entityEntry, items);
        }
    }
}
