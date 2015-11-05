using System;
using System.Configuration;
using System.Data.Entity;
using Blob.Contracts.ServiceContracts;
using Blob.Contracts.Services;
using Blob.Core;
using Blob.Core.Audit;
//using Blob.Core.Blob;
using Blob.Core.Identity;
using Blob.Core.Identity.Store;
using Blob.Core.Models;
using Blob.Core.Services;
using Blob.Services.Before;
using log4net;
using Microsoft.AspNet.Identity;
using Ninject.Modules;
using Ninject.Web.Common;
using Blob.Services.Device;

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

            Bind<BlobDbContext>().ToSelf().InRequestScope() // each request will instantiate its own DBContext
                .WithConstructorArgument("connectionString", connectionString);

            Bind<DbContext>().To<BlobDbContext>().InRequestScope() // each request will instantiate its own DBContext
                .WithConstructorArgument("connectionString", connectionString);

            Bind<IRoleStore<Role, Guid>>().To<BlobRoleStore>();
            Bind<BlobRoleManager>().ToSelf();

            Bind<IBlobCommandManager>().To<BeforeCommandService>();
            Bind<IBlobQueryManager>().To<BeforeQueryService>();

            Bind<IBlobAuditor>().To<BlobAuditor>();
            Bind<ICustomerGroupService>().To<BlobCustomerGroupManager>();
            Bind<ICustomerService>().To<BlobCustomerManager>();
            Bind<IDashboardService>().To<BlobDashboardManager>();
            Bind<IDeviceCommandService>().To<BlobDeviceCommandManager>();
            Bind<IDeviceConnectionService>().To<DeviceConnectionService>();
            Bind<IDeviceService>().To<BlobDeviceManager>();
            Bind<IDeviceStatusService>().To<DeviceStatusService>();
            Bind<INotificationScheduleService>().To<BlobNotificationScheduleManager>();
            Bind<IPerformanceRecordService>().To<BlobPerformanceRecordManager>();
            Bind<IStatusRecordService>().To<BlobStatusRecordManager>();
            Bind<IUserManagerService>().To<BeforeUserManagerService>();
            Bind<IUserService>().To<BlobUserManager2>();

            // logging
            Bind<ILog>().ToMethod(context => LogManager.GetLogger(context.Request.Target.Member.ReflectedType));
        }
    }
}