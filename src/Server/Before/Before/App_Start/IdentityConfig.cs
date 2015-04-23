using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
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
using Blob.Contracts.Security;
using Blob.Proxies;

namespace Before
{
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

    public class ApplicationUser : UserDto, IUser, IUser<string>
    {
        public ApplicationUser() { }
        public ApplicationUser(UserDto userDto)
        {
            // populate other fields if we want to
            this.Id = userDto.Id;
            this.UserName = userDto.UserName;
        }

        //public async Task<ClaimsIdentity> GenerateUserIdentityAsync(ApplicationUserManager manager)
        //{
        //    // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
        //    var userIdentity = await manager.CreateIdentityAsync(this as UserDto, DefaultAuthenticationTypes.ApplicationCookie);
        //    // Add custom user claims here
        //    return userIdentity;
        //}

        //public string Id { get; private set; }

        //public string UserName { get; set; }

        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string PhoneNumber { get; set; }
    }


    
    //{
    ////    public ApplicationUser FindById(string userId)
    ////    {
    ////        return (base.FindByIdAsync(userId)).Result.ToApplicationUser();
    ////    }

    ////    public static ApplicationIdentityManagerProxy Create(IdentityFactoryOptions<ApplicationIdentityManagerProxy> options, IOwinContext context)
    ////    {
    ////        var manager = new ApplicationIdentityManagerProxy();
    ////        NameValueCollection config = (NameValueCollection)ConfigurationManager.GetSection("BlobProxy");
    ////        if (config == null)
    ////        {
    ////            throw new Exception();
    ////        }
    ////        manager.Initialize("IdentityManagerClient", config);
    ////        return manager;
    ////    }
    //}

    // Configure the application user manager used in this application. UserManager is defined in ASP.NET Identity and is used by the application.
    public class ApplicationUserManager : IdentityManagerClient
    //public class ApplicationUserManager : UserManager<IUser>
    {
    //    private readonly ApplicationIdentityManagerProxy _userManagerProxy;

    //    public ApplicationUserManager() : base(new NullStore())
    //    {
    //        _userManagerProxy = new ApplicationIdentityManagerProxy();
    //    }

        public ApplicationUser FindById(string userId)
        {
            return new ApplicationUser(FindByIdAsync(userId).Result);
        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
        {
            var manager = new ApplicationUserManager();
            //var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(context.Get<ApplicationDbContext>()));
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
            return manager;
        }
    }

    //// Configure the application sign-in manager which is used in this application.
    //public class ApplicationSignInManager : IUserSignInService
    //public class ApplicationSignInManager : SignInManager<IUser, string>
    //{
    //    public ApplicationSignInManager(ApplicationUserManager userManager, IAuthenticationManager authenticationManager)
    //        : base(userManager, authenticationManager)
    //    {
    //    }

    //    //public override Task<ClaimsIdentity> CreateUserIdentityAsync(IUser user)
    //    //{
    //    //    return user.ToApplicationUser().GenerateUserIdentityAsync();
    //    //}

    //    public static ApplicationSignInManager Create(IdentityFactoryOptions<ApplicationSignInManager> options, IOwinContext context)
    //    {
    //        return new ApplicationSignInManager(context.GetUserManager<ApplicationUserManager>(), context.Authentication);
    //    }
    //}
    //internal class NullStore : IUserStore<IUser>
    //{
    //    public void Dispose() { return; }
    //    public Task CreateAsync(IUser user) { throw new NotImplementedException(); }
    //    public Task UpdateAsync(IUser user) { throw new NotImplementedException(); }
    //    public Task DeleteAsync(IUser user) { throw new NotImplementedException(); }
    //    public Task<IUser> FindByIdAsync(string userId) { throw new NotImplementedException(); }
    //    public Task<IUser> FindByNameAsync(string userName) { throw new NotImplementedException(); }
    //}
}
