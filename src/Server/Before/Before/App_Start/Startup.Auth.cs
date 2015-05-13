using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Blob.Contracts.Security;
using Blob.Proxies;
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
            app.Use(typeof(BeforeAuthorizationManagerMiddleware));
            //app.UseBeforeAuthorization(new BeforeAuthorizationClient("AuthorizationService", "customerUser1", "password"));
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                ExpireTimeSpan = TimeSpan.FromMinutes(20),
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

            //app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);
        }
    }

    //public static class AppBuilderExtensions
    //{
    //    public static IAppBuilder UseBeforeAuthorization(this IAppBuilder app, IAuthorizationManagerService authorizationManager)
    //    {
    //        var options = new BeforeAuthorizationMiddlewareOptions
    //        {
    //            Manager = authorizationManager
    //        };

    //        app.UseBeforeAuthorization(options);
    //        return app;
    //    }

    //    public static IAppBuilder UseBeforeAuthorization(this IAppBuilder app, BeforeAuthorizationMiddlewareOptions options)
    //    {
    //        app.Use(typeof(BeforeAuthorizationClient), "AuthorizationService", "customerUser1", "password");
    //        return app;
    //    }
    //}

    public class BeforeAuthorizationManagerMiddleware : OwinMiddleware
    {
        public BeforeAuthorizationManagerMiddleware(OwinMiddleware next)
        : base(next) 
        {
            
        }

        public async override Task Invoke(IOwinContext context)
        {
            context.Set("BeforeAuthorizationClient",new BeforeAuthorizationClient("AuthorizationService", "customerUser1", "password"));
            await Next.Invoke(context);
        }
    }

    //    public const string Key = "idm:resourceAuthorizationManager";

    //    private readonly Func<IDictionary<string, object>, Task> _next;
    //    private BeforeAuthorizationMiddlewareOptions _options;

    //    public BeforeAuthorizationManagerMiddleware(Func<IDictionary<string, object>, Task> next )
    //    {
    //        _next = next;
    //    }

    //    public async Task Invoke(IDictionary<string, object> env)
    //    {
    //        env[Key] = new BeforeAuthorizationClient("AuthorizationService", "customerUser1", "password");//_options.Manager ?? _options.ManagerProvider(env);
    //        await _next(env);
    //    }
    //}
    //public class BeforeAuthorizationMiddlewareOptions
    //{
    //    public BeforeAuthorizationMiddlewareOptions()
    //    {
    //        ManagerProvider = (env) => null;
    //    }
    //    public IAuthorizationManagerService Manager { get; set; }
    //    public Func<IDictionary<string, object>, IAuthorizationManagerService> ManagerProvider { get; set; }
    //}
}
