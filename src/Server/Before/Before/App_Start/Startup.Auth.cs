using Before.Owin.Authorization;
using Blob.Proxies;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using Owin;
using SimpleInjector;

namespace Before
{
    public partial class Startup
    {
        public void ConfigureAuth(IAppBuilder app, Container container)
        {
            app.UseBeforeAuthorization(new BeforeAuthorizationClient(SiteGlobalConfig.AuthorizationService, SiteGlobalConfig.AuthorizationServiceUsername, SiteGlobalConfig.AuthorizationServicePassword));
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = BeforeAuthorizationConstants.CookieType,
                LoginPath = new PathString("/account/login"),
            });
        }
    }
}
