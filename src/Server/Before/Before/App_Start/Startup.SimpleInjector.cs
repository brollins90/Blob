using System;
using System.Collections.Generic;
using System.Configuration;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Before.Infrastructure.Identity;
using Blob.Contracts.Blob;
using Blob.Contracts.Security;
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
            var container = GetInitializeContainer(app);
            container.Verify();
            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));

            return container;
        }

        public static Container GetInitializeContainer(IAppBuilder app)
        {
            var container = new Container();

            var logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
            container.RegisterSingle(logger);

            container.RegisterSingle<IAppBuilder>(app);


            String connectionString = ConfigurationManager.ConnectionStrings["BlobDbContext"].ConnectionString;
            
            //container.RegisterPerWebRequest<BlobDbContext>(() => new BlobDbContext(connectionString));

            container.RegisterPerWebRequest<Blob.Contracts.Security.IUserManagerService>(() => BeforeUserManager.Create("UserManagerService"));
            container.RegisterPerWebRequest<IBlobCommandManager>(() => new BeforeCommandClient("BeforeCommandService", "customerUser1", "password"));
            container.RegisterPerWebRequest<IBlobQueryManager>(() => new BeforeQueryClient("BeforeQueryService", "customerUser1", "password"));
            container.RegisterPerWebRequest<IAuthorizationManagerService>(() => new BeforeAuthorizationClient("AuthorizationService", "customerUser1", "password"));

            // This is kind of bad, all the identity proxying stuff I did was to make OWIN just work, but now I am 
            //    bypassing the OWIN context and injecting into the we context
            //container.RegisterPerWebRequest<BeforeUserManager>(); 

            //container.RegisterPerWebRequest<IAuthenticationManager>(() => AdvancedExtensions.IsVerifying(container)
            //    ? new OwinContext(new Dictionary<string, object>()).Authentication
            //    : HttpContext.Current.GetOwinContext().Authentication);

            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());

            return container;
        }
    }
}