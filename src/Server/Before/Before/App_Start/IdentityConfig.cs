using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Before.Models;
using Blob.Proxies;

namespace Before
{
    public static class IdentityUtil
    {
        public static ApplicationUser ToApplicationUser(this IUser<string> iUser)
        {
            return iUser as ApplicationUser;
        }
    }

    public class EmailService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            // Plug in your email service here to send an email.
            return Task.FromResult(0);
        }
    }

    public class SmsService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            // Plug in your SMS service here to send a text message.
            return Task.FromResult(0);
        }
    }

    public class ApplicationUser : IUser, IUser<string>
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(ApplicationIdentityManager manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        public string Id { get; private set; }

        public string UserName { get; set; }

        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string PhoneNumber { get; set; }
    }


    public class ApplicationIdentityManager : IdentityManagerClient
    {
        public ApplicationUser FindById(string userId)
        {
            return (base.FindByIdAsync(userId)).Result.ToApplicationUser();
        }

        public static ApplicationIdentityManager Create(IdentityFactoryOptions<ApplicationIdentityManager> options, IOwinContext context)
        {
            var manager = new ApplicationIdentityManager();
            return manager;
        }
    }

    // Configure the application user manager used in this application. UserManager is defined in ASP.NET Identity and is used by the application.
    //public class ApplicationUserManager : IdentityManagerClient
    //{
    //    public ApplicationUser FindById(string userId)
    //    {
    //        return (base.FindByIdAsync(userId)).Result.ToApplicationUser();
    //    }

    //    public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
    //    {
    //        var manager = new ApplicationUserManager();
    //        //var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(context.Get<ApplicationDbContext>()));
    //        //// Configure validation logic for usernames
    //        //manager.UserValidator = new UserValidator<ApplicationUser>(manager)
    //        //{
    //        //    AllowOnlyAlphanumericUserNames = false,
    //        //    RequireUniqueEmail = true
    //        //};

    //        //// Configure validation logic for passwords
    //        //manager.PasswordValidator = new PasswordValidator
    //        //{
    //        //    RequiredLength = 6,
    //        //    RequireNonLetterOrDigit = true,
    //        //    RequireDigit = true,
    //        //    RequireLowercase = true,
    //        //    RequireUppercase = true,
    //        //};

    //        //// Configure user lockout defaults
    //        //manager.UserLockoutEnabledByDefault = true;
    //        //manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
    //        //manager.MaxFailedAccessAttemptsBeforeLockout = 5;

    //        //// Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
    //        //// You can write your own provider and plug it in here.
    //        //manager.RegisterTwoFactorProvider("Phone Code", new PhoneNumberTokenProvider<ApplicationUser>
    //        //{
    //        //    MessageFormat = "Your security code is {0}"
    //        //});
    //        //manager.RegisterTwoFactorProvider("Email Code", new EmailTokenProvider<ApplicationUser>
    //        //{
    //        //    Subject = "Security Code",
    //        //    BodyFormat = "Your security code is {0}"
    //        //});
    //        //manager.EmailService = new EmailService();
    //        //manager.SmsService = new SmsService();
    //        //var dataProtectionProvider = options.DataProtectionProvider;
    //        //if (dataProtectionProvider != null)
    //        //{
    //        //    manager.UserTokenProvider = 
    //        //        new DataProtectorTokenProvider<ApplicationUser>(dataProtectionProvider.Create("ASP.NET Identity"));
    //        //}
    //        return manager;
    //    }
    //}

    //// Configure the application sign-in manager which is used in this application.
    //public class ApplicationSignInManager : SignInManager<IUser, string>
    //{
    //    public ApplicationSignInManager(ApplicationUserManager userManager, IAuthenticationManager authenticationManager)
    //        : base(userManager, authenticationManager)
    //    {
    //    }

    //    public override Task<ClaimsIdentity> CreateUserIdentityAsync(IUser user)
    //    {
    //        return user.ToApplicationUser().GenerateUserIdentityAsync((ApplicationUserManager)UserManager);
    //    }

    //    public static ApplicationSignInManager Create(IdentityFactoryOptions<ApplicationSignInManager> options, IOwinContext context)
    //    {
    //        return new ApplicationSignInManager(context.GetUserManager<ApplicationUserManager>(), context.Authentication);
    //    }
    //}
}
