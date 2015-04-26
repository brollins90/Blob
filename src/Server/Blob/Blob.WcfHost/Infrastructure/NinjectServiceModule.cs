using System;
using System.Configuration;
using System.Data.Entity;
using Blob.Security;
using log4net;
using Ninject.Modules;
using Ninject.Web.Common;

namespace Blob.WcfHost.Infrastructure
{
    /// <summary>
    /// Ninject Container
    /// </summary>
    public class NinjectServiceModule : NinjectModule
    {
        private readonly ILog _log;

        public NinjectServiceModule()
        {
            _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        }

        /// <summary>
        /// Registers the required dependencies with the service
        /// </summary>
        public override void Load()
        {
            _log.Info("Registering Ninject dependencies");

            String connectionString = ConfigurationManager.ConnectionStrings["BlobDbContext"].ConnectionString;
            
            Bind<Data.BlobDbContext>().ToSelf().InRequestScope() // each request will instantiate its own DBContext
                .WithConstructorArgument("connectionString", connectionString);

            Bind<DbContext>().To<Data.BlobDbContext>().InRequestScope() // each request will instantiate its own DBContext
                .WithConstructorArgument("connectionString", connectionString);

            Bind<Managers.Blob.IBlobManager>().To<Managers.Blob.BlobManager>();
            Bind<Managers.Status.IStatusManager>().To<Managers.Status.StatusManager>();

            Bind<Contracts.Command.ICommandService>().To<Services.Command.CommandService>();
            Bind<Contracts.Security.IIdentityService>().To<BlobUserManagerAdapter>();
            Bind<Contracts.Registration.IRegistrationService>().To<Services.Registration.RegistrationService>();
            Bind<Contracts.Status.IStatusService>().To<Services.Status.StatusService>();

            // logging
            Bind<ILog>().ToMethod(context => LogManager.GetLogger(context.Request.Target.Member.ReflectedType));
        }
    }
}