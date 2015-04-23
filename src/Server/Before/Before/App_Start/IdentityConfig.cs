using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
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
    //public class EmailService : IIdentityMessageService
    //{
    //    public Task SendAsync(IdentityMessage message)
    //    {
    //        // Plug in your email service here to send an email.
    //        return Task.FromResult(0);
    //    }
    //}

    //public class SmsService : IIdentityMessageService
    //{
    //    public Task SendAsync(IdentityMessage message)
    //    {
    //        // Plug in your SMS service here to send a text message.
    //        return Task.FromResult(0);
    //    }
    //}

    public class ApplicationUser : UserDto, IUser, IUser<string>
    {
        public ApplicationUser()
        {
            Id = Guid.NewGuid().ToString();
        }
        public ApplicationUser(UserDto userDto)
        {
            // populate other fields if we want to
            Id = userDto.Id;
            UserName = userDto.UserName;
        }

        public UserDto ToDto()
        {
            return new UserDto {Id = Id, UserName = UserName};
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(ApplicationUserManager manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            ClaimsIdentity userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string PhoneNumber { get; set; }
    }

    public class ApplicationUserManager : IdentityManagerClient
    {
        private ApplicationUserManager() { }

        public ApplicationUser FindById(string userId)
        {
            return new ApplicationUser(FindByIdAsync(userId).Result);
        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
        {
            ApplicationUserManager manager = new ApplicationUserManager();
            NameValueCollection config = (NameValueCollection)ConfigurationManager.GetSection("BlobProxy");
            if (config == null)
            {
                throw new Exception();
            }
            manager.Initialize("IdentityManagerClient", config);

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

    // Configure the application sign-in manager which is used in this application.
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


    public static class Helper
    {
        public static string GetCustomerId(this IIdentity identity)
        {
            return Guid.Parse("79720728-171c-48a4-a866-5f905c8fdb9f").ToString();
            if (identity == null)
            {
                throw new ArgumentNullException("identity");
            }
            var ci = identity as ClaimsIdentity;
            if (ci != null)
            {
                return ci.FindFirstValue("customerid");
                //return ci.FindFirstValue("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name");
            }
            return null;
        }
    }
}
