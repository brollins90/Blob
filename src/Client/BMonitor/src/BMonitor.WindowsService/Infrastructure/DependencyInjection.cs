using log4net;
using Ninject.Modules;

namespace BMonitor.WindowsService.Infrastructure
{
    public class DependencyInjection : NinjectModule
    {
        public override void Load()
        {
            // Logging
            Bind<ILog>().ToMethod(context => LogManager.GetLogger(context.Request.Target.Member.ReflectedType));

            // Service
            Bind<MonitorService>().ToSelf();
        }
    }
}
