using Blob.Contracts.Security;
using Blob.Core.Domain;
using Microsoft.AspNet.Identity;
using System;
using Blob.Data;
using Microsoft.Owin;

namespace Blob.Security
{
    public class BlobUserManager : UserManager<User, Guid>, IUserManagerService<User,Guid>
    {
        public BlobUserManager(BlobUserStore store) : base(store)
        {

            this.ClaimsIdentityFactory = new ClaimsIdentityFactory<User, Guid>();
            //this.DefaultAccountLockoutTimeSpan;
            //this.EmailService;
            //this.MaxFailedAccessAttemptsBeforeLockout;
            this.PasswordHasher = new PasswordHasher() { };
            this.PasswordValidator = new PasswordValidator
                    {
                        RequiredLength = 3,
                        RequireNonLetterOrDigit = false,
                        RequireDigit = false,
                        RequireLowercase = false,
                        RequireUppercase = false,
                    };
            //this.SmsService;
            //this.Store;
            //this.SupportsQueryableUsers;
            //this.SupportsUserClaim;
            //this.SupportsUserEmail;
            //this.SupportsUserLockout;
            //this.SupportsUserLogin;
            //this.SupportsUserPassword;
            //this.SupportsUserPhoneNumber;
            //this.SupportsUserRole;
            //this.SupportsUserSecurityStamp;
            //this.SupportsUserTwoFactor;
            //this.TwoFactorProviders;
            //this.UserLockoutEnabledByDefault;
            //this.UserTokenProvider;
            this.UserValidator = new BlobUserValidator(this)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };
            //this.Users;

        }

        public static BlobUserManager Create()
        {
            var manager = new BlobUserManager(new BlobUserStore(new BlobDbContext()));

            return manager;
        }

        //public static BlobUserManager Create(IdentityFactoryOptions<BlobUserManager> options, IOwinContext context)
        //{
        //    var manager = new BlobUserManager(new BlobUserStore(context.Get<BlobDbContext>("BlobDbContext")));

        //    // Configure validation logic for usernames
        //    manager.UserValidator = new UserValidator<User>(manager)
        //                            {
        //                                AllowOnlyAlphanumericUserNames = false,
        //                                RequireUniqueEmail = true
        //                            };

        //    // Configure validation logic for passwords
        //    manager.PasswordValidator = new PasswordValidator
        //                                {
        //                                    RequiredLength = 6,
        //                                    RequireNonLetterOrDigit = true,
        //                                    RequireDigit = true,
        //                                    RequireLowercase = true,
        //                                    RequireUppercase = true,
        //                                };

        //    var dataProtectionProvider = options.DataProtectionProvider;
        //    if (dataProtectionProvider != null)
        //    {
        //        manager.UserTokenProvider = new DataProtectorTokenProvider<BlobUser>(dataProtectionProvider.Create("ASP.NET Identity"));
        //    }
        //    return manager;
        //}
    }
}
