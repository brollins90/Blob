using System.Security.Claims;
using System.Web.Helpers;
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
            AntiForgeryConfig.UniqueClaimTypeIdentifier = ClaimTypes.NameIdentifier;
            app.UseBeforeAuthorization(new BeforeAuthorizationClient(SiteGlobalConfig.AuthorizationService, SiteGlobalConfig.AuthorizationServiceUsername, SiteGlobalConfig.AuthorizationServicePassword));
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/account/login"),
            });
        }
    }
}
