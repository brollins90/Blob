using log4net;
using Ninject.Modules;
using Ninject.Web.Common;
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
            Bind<Data.BlobDbContext>().ToSelf().InRequestScope() // each request will instantiate its own DBContext
                .WithConstructorArgument("connectionString", ConfigurationManager.ConnectionStrings["BlobDbContext"].ConnectionString);

            // core
            Bind<Core.Data.IRepositoryAsync<Core.Domain.Customer>>().To<Data.EfRepositoryAsync<Core.Domain.Customer>>();
            Bind<Core.Data.IRepositoryAsync<Core.Domain.Device>>().To<Data.EfRepositoryAsync<Core.Domain.Device>>();
            Bind<Core.Data.IRepositoryAsync<Core.Domain.DeviceSecurity>>().To<Data.EfRepositoryAsync<Core.Domain.DeviceSecurity>>();
            Bind<Core.Data.IRepositoryAsync<Core.Domain.DeviceType>>().To<Data.EfRepositoryAsync<Core.Domain.DeviceType>>();
            Bind<Core.Data.IRepositoryAsync<Core.Domain.Role>>().To<Data.EfRepositoryAsync<Core.Domain.Role>>();
            Bind<Core.Data.IRepositoryAsync<Core.Domain.Status>>().To<Data.EfRepositoryAsync<Core.Domain.Status>>();
            Bind<Core.Data.IRepositoryAsync<Core.Domain.StatusPerf>>().To<Data.EfRepositoryAsync<Core.Domain.StatusPerf>>();
            Bind<Core.Data.IRepositoryAsync<Core.Domain.User>>().To<Data.EfRepositoryAsync<Core.Domain.User>>();
            //Bind<Core.Data.IRepositoryAsync<Core.Domain.UserSecurity>>().To<Data.EfRepositoryAsync<Core.Domain.UserSecurity>>();

            // manager
            // todo: decide where i am going to store the callbacks
            Bind<Managers.Registration.IRegistrationManager>().To<Managers.Registration.RegistrationManager>();
            Bind<Managers.Status.IStatusManager>().To<Managers.Status.StatusManager>();

            // service
            // we wont use this layer, because it is specified in the config file.
            Bind<Contracts.Command.ICommandService>().To<Services.Command.CommandService>();
            Bind<Contracts.Registration.IRegistrationService>().To<Services.Registration.RegistrationService>();
            Bind<Contracts.Status.IStatusService>().To<Services.Status.StatusService>();

            // logging
            Bind<ILog>().ToMethod(context => LogManager.GetLogger(context.Request.Target.Member.ReflectedType));
        }
    }
}