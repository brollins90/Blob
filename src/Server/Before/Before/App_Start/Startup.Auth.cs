using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using SimpleInjector;

namespace Before
{
    public partial class Startup
    {
        public void ConfigureAuth(IAppBuilder app, Container container)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/account/login"),
                //CookieSecure = CookieSecureOption.Always,
                ////    Provider = new CookieAuthenticationProvider
                ////    {
                ////        // Enables the application to validate the security stamp when the user logs in.
                ////        // This is a security feature which is used when you change a password or add an external login to your account.  
                ////        OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<ApplicationUserManager, ApplicationUser>(
                ////            validateInterval: TimeSpan.FromMinutes(30),
                ////            regenerateIdentity: (manager, user) => user.GenerateUserIdentityAsync(manager))
                ////    }
            });

            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);
        }
    }
}
