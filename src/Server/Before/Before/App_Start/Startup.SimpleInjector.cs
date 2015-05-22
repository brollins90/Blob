using System.Collections.Generic;
using System.Reflection;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using Before.Infrastructure.Identity;
using Blob.Contracts.ServiceContracts;
using Blob.Proxies;
using log4net;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Owin;
using SimpleInjector;
using SimpleInjector.Advanced;
using SimpleInjector.Integration.Web.Mvc;

namespace Before
{
    public partial class Startup
    {
        public static Container RegisterSimpleInjector(IAppBuilder app)
        {
            Container container = GetInitializeContainer(app);
            //container.Verify();
            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));

            return container;
        }

        public static Container GetInitializeContainer(IAppBuilder app)
        {
            var container = new Container();
            container.RegisterSingle(LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType));

            // OWIN stuff ?
            container.RegisterSingle<IAppBuilder>(app);

            // Auth
            //container.RegisterPerWebRequest<IUserManagerService, BeforeUserManager>();
            //container.RegisterPerWebRequest<BeforeUserManager>();
            container.RegisterPerWebRequest<IUserManagerService>(() => BeforeUserManager.Create("UserManagerService"));
            //{
            //    ClaimsPrincipal principal = ClaimsPrincipal.Current;
            //    string username = principal.FindFirst(x => x.Type.Equals("username")).ToString();
            //    string password = principal.FindFirst(x => x.Type.Equals("password")).ToString();
            //    return new BeforeUserManager("UserManagerService", username, password);
            //});
            container.RegisterPerWebRequest<BeforeUserManagerLocal>(() => BeforeUserManagerLocal.Create("UserManagerService"));
            //{
            //    ClaimsPrincipal principal = ClaimsPrincipal.Current;
            //    string username = principal.FindFirst(x => x.Type.Equals("username")).ToString();
            //    string password = principal.FindFirst(x => x.Type.Equals("password")).ToString();
            //    return new BeforeUserManagerAcct("UserManagerService", SiteGlobalConfig., password);
            //});
            container.RegisterPerWebRequest<BeforeSignInManager>();
            container.RegisterPerWebRequest<IAuthenticationManager>(() => AdvancedExtensions.IsVerifying(container)
                ? new OwinContext(new Dictionary<string, object>()).Authentication
                : HttpContext.Current.GetOwinContext().Authentication);

            // Blob
            container.RegisterPerWebRequest<IBlobCommandManager>(() => SIFact.CreateBeforeCommandClient());
            //container.RegisterPerWebRequest<IBlobCommandManager>(() => new BeforeCommandClient("BeforeCommandService", "customerUser1", "password"));
            container.RegisterPerWebRequest<IBlobQueryManager>(() => SIFact.CreateBeforeQueryClient());
            //{
            //    ClaimsPrincipal principal = ClaimsPrincipal.Current;
            //    string username = principal.FindFirst(x => x.Type.Equals("username")).ToString();
            //    string password = principal.FindFirst(x => x.Type.Equals("password")).ToString();
            //    return new BeforeQueryClient("BeforeQueryService", username, password);
            //});
            //container.RegisterPerWebRequest<IBlobQueryManager>(() => new BeforeQueryClient("BeforeQueryService", "customerUser1", "password"));

            // MVC
            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());

            return container;
        }
    }

    public static class SIFact
    {
        public static BeforeCommandClient CreateBeforeCommandClient()
        {
            //ClaimsPrincipal principal = ClaimsPrincipal.Current;
            //string username = (principal.Identity.IsAuthenticated) ? principal.FindFirst(x => x.Type.Equals("username")).ToString() : "";
            //string password = (principal.Identity.IsAuthenticated) ? principal.FindFirst(x => x.Type.Equals("password")).ToString() : "";
            string username = SiteGlobalConfig.AuthorizationServiceUsername;
            string password = SiteGlobalConfig.AuthorizationServicePassword;
            return new BeforeCommandClient("BeforeCommandService", username, password);
        }
        public static BeforeQueryClient CreateBeforeQueryClient()
        {
            //ClaimsPrincipal principal = ClaimsPrincipal.Current;
            //string username = (principal.Identity.IsAuthenticated) ? principal.FindFirst(x => x.Type.Equals("username")).ToString() : "";
            //string password = (principal.Identity.IsAuthenticated) ? principal.FindFirst(x => x.Type.Equals("password")).ToString() : "";
            string username = SiteGlobalConfig.AuthorizationServiceUsername;
            string password = SiteGlobalConfig.AuthorizationServicePassword;
            return new BeforeQueryClient("BeforeQueryService", username, password);
        }
    }
}
