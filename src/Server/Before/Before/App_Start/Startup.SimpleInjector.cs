using System.Collections.Generic;
using System.Reflection;
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
            container.Verify();
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
            container.RegisterPerWebRequest<IUserManagerService>(() => BeforeUserManager.Create("UserManagerService"));
            container.RegisterPerWebRequest<BeforeUserManager>(() => BeforeUserManager.Create("UserManagerService"));
            container.RegisterPerWebRequest<BeforeSignInManager>();
            container.RegisterPerWebRequest<IAuthenticationManager>(() => AdvancedExtensions.IsVerifying(container)
                ? new OwinContext(new Dictionary<string, object>()).Authentication
                : HttpContext.Current.GetOwinContext().Authentication);

            // Blob
            container.RegisterPerWebRequest<IBlobCommandManager>(() => new BeforeCommandClient("BeforeCommandService", "customerUser1", "password"));
            container.RegisterPerWebRequest<IBlobQueryManager>(() => new BeforeQueryClient("BeforeQueryService", "customerUser1", "password"));

            // MVC
            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());

            return container;
        }
    }
}
