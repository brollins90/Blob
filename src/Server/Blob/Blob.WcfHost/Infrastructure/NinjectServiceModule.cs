using System;
using System.Configuration;
using System.Data.Entity;
using log4net;
using Ninject.Modules;
using Ninject.Web.Common;

namespace Blob.WcfHost.Infrastructure
{
    public class NinjectServiceModule : NinjectModule
    {
        public override void Load()
        {
            LogManager.GetLogger(typeof(BlobHostFactory)).Info("Registering Ninject dependencies");

            String connectionString = ConfigurationManager.ConnectionStrings["BlobDbContext"].ConnectionString;
            // data
            Bind<Data.BlobDbContext>().ToSelf().InRequestScope() // each request will instantiate its own DBContext
                .WithConstructorArgument("connectionString", connectionString);

            Bind<DbContext>().To<Data.BlobDbContext>().InRequestScope() // each request will instantiate its own DBContext
                .WithConstructorArgument("connectionString", connectionString);

            //Bind<BlobUserStore>().ToSelf().InRequestScope();
            //Bind<BlobUserManager>().ToSelf().InRequestScope();

            // core
            Bind<Core.Data.IAccountRepository>().To<Data.Repositories.AccountRepository>();
            Bind<Core.Data.IStatusRepository>().To<Data.Repositories.StatusRepository>();

            // manager
            // todo: decide where i am going to store the callbacks
            Bind<Managers.Blob.IBlobManager>().To<Managers.Blob.BlobManager>();
            Bind<Managers.Status.IStatusManager>().To<Managers.Status.StatusManager>();

            // service
            // we wont use this layer, because it is specified in the config file.
            Bind<Contracts.Command.ICommandService>().To<Services.Command.CommandService>();
            //Bind<Contracts.Security.IUserManagerService>().To<BlobUserManagerAdapter>();
            Bind<Contracts.Registration.IRegistrationService>().To<Services.Registration.RegistrationService>();
            Bind<Contracts.Status.IStatusService>().To<Services.Status.StatusService>();

            // logging
            Bind<ILog>().ToMethod(context => LogManager.GetLogger(context.Request.Target.Member.ReflectedType));
        }
    }
}