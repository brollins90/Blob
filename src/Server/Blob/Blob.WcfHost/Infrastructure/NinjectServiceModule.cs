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
            // data
            this.Bind<Blob.Data.IDbContext>().To<Blob.Data.BlobDbContext>()
                .WithConstructorArgument("connectionString", ConfigurationManager.ConnectionStrings["BlobDbContext"].ConnectionString);

            // core
            this.Bind<Blob.Core.Data.IRepository<Blob.Core.Domain.Status>>().To<Blob.Data.EfRepository<Blob.Core.Domain.Status>>();

            // manager
            this.Bind<Blob.Managers.Status.IStatusManager>().To<Blob.Managers.Status.StatusManager>();

            // service
            // we wont use this layer, because it is specified in the config file.
            this.Bind<Blob.Contracts.Status.IStatusService>().To<Blob.Services.Status.StatusService>();
        }
    }
}