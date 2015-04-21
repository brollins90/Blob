using log4net;
using Ninject.Modules;
using Ninject.Web.Common;
using System.Configuration;
using System.Data.Entity;
using Blob.Data;
using Blob.Security;

namespace Blob.WcfHost.Infrastructure
{
    public class NinjectServiceModule : NinjectModule
    {
        /// <summary>
        /// Loads the module into the kernel.
        /// </summary>
        public override void Load()
        {
            LogManager.GetLogger(typeof(BlobHostFactory)).Info("Registering Ninject dependencies");

            // data
            Bind<Data.BlobDbContext>().ToSelf().InRequestScope() // each request will instantiate its own DBContext
                .WithConstructorArgument("connectionString", ConfigurationManager.ConnectionStrings["BlobDbContext"].ConnectionString);


            Bind<DbContext>().To<Data.BlobDbContext>().InRequestScope() // each request will instantiate its own DBContext
                .WithConstructorArgument("connectionString", ConfigurationManager.ConnectionStrings["BlobDbContext"].ConnectionString);

            Bind<Security.BlobUserStore>().ToSelf().InRequestScope();
            Bind<Security.BlobUserManager>().ToSelf().InRequestScope();


            // core
            Bind<Core.Data.IAccountRepository>().To<Data.Repositories.AccountRepository>();
            Bind<Core.Data.IStatusRepository>().To<Data.Repositories.StatusRepository>();

            // manager
            // todo: decide where i am going to store the callbacks
            Bind<Managers.Registration.IRegistrationManager>().To<Managers.Registration.RegistrationManager>();
            Bind<Managers.Status.IStatusManager>().To<Managers.Status.StatusManager>();

            // service
            // we wont use this layer, because it is specified in the config file.
            Bind<Contracts.Command.ICommandService>().To<Services.Command.CommandService>();
            Bind<Contracts.Security.IUserManagerService>().To<BlobUserManagerAdapter>();
            Bind<Contracts.Registration.IRegistrationService>().To<Services.Registration.RegistrationService>();
            Bind<Contracts.Status.IStatusService>().To<Services.Status.StatusService>();

            // logging
            Bind<ILog>().ToMethod(context => LogManager.GetLogger(context.Request.Target.Member.ReflectedType));
        }
    }
}