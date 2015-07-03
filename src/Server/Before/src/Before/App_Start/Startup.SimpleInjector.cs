namespace Before
{
    using System.Collections.Generic;
    using System.Reflection;
    using System.Security.Claims;
    using System.Web;
    using System.Web.Mvc;
    using Blob.Contracts.ServiceContracts;
    using Blob.Proxies;
    using global::Owin;
    using Infrastructure.Identity;
    using log4net;
    using Microsoft.Owin;
    using Microsoft.Owin.Security;
    using SimpleInjector;
    using SimpleInjector.Advanced;
    using SimpleInjector.Integration.Web.Mvc;

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
            // Logging
            container.RegisterWithContext<ILog>(dc =>
            {
                if (dc.ImplementationType == null)
                {
                    return LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
                }
                return LogManager.GetLogger(dc.ImplementationType);
            });
            //container.RegisterSingle(LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType));

            // OWIN stuff ?
            container.RegisterSingle<IAppBuilder>(app);

            // Auth
            container.RegisterPerWebRequest<IUserManagerService>(() => SimpleInjectorFactory.CreateUserManagerClient());
            container.RegisterPerWebRequest<ISignInManager, BeforeSignInManager>();
            container.RegisterPerWebRequest<IAuthenticationManager>(() => AdvancedExtensions.IsVerifying(container)
                ? new OwinContext(new Dictionary<string, object>()).Authentication
                : HttpContext.Current.GetOwinContext().Authentication);

            // Blob
            container.RegisterPerWebRequest<IBlobCommandManager>(() => SimpleInjectorFactory.CreateBeforeCommandClient());
            container.RegisterPerWebRequest<IBlobQueryManager>(() => SimpleInjectorFactory.CreateBeforeQueryClient());

            // MVC
            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());

            return container;
        }
    }

    public static class SimpleInjectorFactory
    {
        public static IdentityManagerClient CreateUserManagerClient()
        {
            string username = SiteGlobalConfig.AuthorizationServiceUsername;
            string password = SiteGlobalConfig.AuthorizationServicePassword;
            return new IdentityManagerClient("UserManagerService", username, password);
        }

        public static BeforeCommandClient CreateBeforeCommandClient()
        {
            ClaimsPrincipal principal = ClaimsPrincipal.Current;
            string username = string.Empty;
            string password = string.Empty;
            if (principal.Identity.IsAuthenticated)
            {
                username = principal.FindFirst("username").Value.ToString();
                password = principal.FindFirst("password").Value.ToString();
            }
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                username = SiteGlobalConfig.AuthorizationServiceUsername;
                password = SiteGlobalConfig.AuthorizationServicePassword;
            }
            return new BeforeCommandClient("BeforeCommandService", username, password);
        }
        public static BeforeQueryClient CreateBeforeQueryClient()
        {
            ClaimsPrincipal principal = ClaimsPrincipal.Current;
            string username = string.Empty;
            string password = string.Empty;
            if (principal.Identity.IsAuthenticated)
            {
                username = principal.FindFirst("username").Value.ToString();
                password = principal.FindFirst("password").Value.ToString();
            }
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                username = SiteGlobalConfig.AuthorizationServiceUsername;
                password = SiteGlobalConfig.AuthorizationServicePassword;
            }
            return new BeforeQueryClient("BeforeQueryService", username, password);
        }
    }
}