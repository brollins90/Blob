using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Blob.Contracts.Models;
using Blob.Contracts.ServiceContracts;
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
            app.Use(async (context, next) =>
            {
                await next.Invoke();
            });
            app.UseBeforeAuthorization();
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
    public static class AppBuilderExtensions
    {
        public static IAppBuilder UseBeforeAuthorization(this IAppBuilder app)
        {
            BeforeAuthorizationMiddlewareOptions options;
            options = new BeforeAuthorizationMiddlewareOptions
            {
                Manager = new BeforeAuthorizationClient("AuthorizationService", "customerUser1", "password")
            };

            app.Use(typeof(BeforeAuthorizationManagerMiddleware), options);
            return app;
        }
    }

    public class BeforeAuthorizationManagerMiddleware
    {
        public const string Key = "idm:authorizationManagerService";

        private readonly Func<IDictionary<string, object>, Task> _next;
        private BeforeAuthorizationMiddlewareOptions _options;

        public BeforeAuthorizationManagerMiddleware(Func<IDictionary<string, object>, Task> next, BeforeAuthorizationMiddlewareOptions options)
        {
            _options = options;
            _next = next;
        }

        public async Task Invoke(IDictionary<string, object> env)
        {
            env[Key] = _options.Manager ?? _options.ManagerProvider(env);
            await _next(env);
        }
    }

    public class BeforeAuthorizationMiddlewareOptions
    {
        public BeforeAuthorizationMiddlewareOptions()
        {
            ManagerProvider = (env) => null;
        }
        public IAuthorizationManagerService Manager { get; set; }
        public Func<IDictionary<string, object>, IAuthorizationManagerService> ManagerProvider { get; set; }
    }
}
