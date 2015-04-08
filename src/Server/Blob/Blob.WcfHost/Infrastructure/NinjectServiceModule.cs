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
            Bind<Data.BlobDbContext>().ToSelf()//.InScope()
                .WithConstructorArgument("connectionString", ConfigurationManager.ConnectionStrings["BlobDbContext"].ConnectionString);

            // core
            Bind<Core.Data.IRepository<Core.Domain.Status>>().To<Data.EfRepository<Core.Domain.Status>>();
            Bind<Core.Data.IRepositoryAsync<Core.Domain.Status>>().To<Data.EfRepositoryAsync<Core.Domain.Status>>();

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