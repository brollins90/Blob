using log4net;
using Ninject.Modules;
using System.Configuration;

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
            Bind<Data.BlobDbContext>().ToSelf()//.InRequestScope() // todo: request scope is not working.  see if this is what i really want.
                .WithConstructorArgument("connectionString", ConfigurationManager.ConnectionStrings["BlobDbContext"].ConnectionString);

            // core
            Bind<Core.Data.IRepositoryAsync<Core.Domain.Status>>().To<Data.EfRepositoryAsync<Core.Domain.Status>>();
            Bind<Core.Data.IRepositoryAsync<Core.Domain.StatusPerf>>().To<Data.EfRepositoryAsync<Core.Domain.StatusPerf>>();

            // manager
            Bind<Managers.Status.IStatusManager>().To<Managers.Status.StatusManager>();

            // service
            // we wont use this layer, because it is specified in the config file.
            Bind<Contracts.Status.IStatusService>().To<Services.Status.StatusService>();

            // logging
            Bind<ILog>().ToMethod(context => LogManager.GetLogger(context.Request.Target.Member.ReflectedType));
        }
    }
}