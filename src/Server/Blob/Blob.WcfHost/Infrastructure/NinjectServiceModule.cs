using System;
using System.Configuration;
using System.Data.Entity;
using Blob.Contracts.ServiceContracts;
using Blob.Managers.Audit;
using Blob.Security.Identity;
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

            Bind<IBlobAuditor>().To<BlobAuditor>();
            Bind<IBlobCommandManager>().To<Managers.Blob.BlobCommandManager>();
            Bind<IBlobQueryManager>().To<Managers.Blob.BlobQueryManager>();

            Bind<IDeviceConnectionService>().To<Services.Device.DeviceConnectionService>();
            Bind<IDeviceStatusService>().To<Services.Device.DeviceStatusService>();
            Bind<IUserManagerService>().To<BlobUserManagerAdapter>();

            // logging
            Bind<ILog>().ToMethod(context => LogManager.GetLogger(context.Request.Target.Member.ReflectedType));
        }
    }
}