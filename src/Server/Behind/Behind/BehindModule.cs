using log4net;
using Ninject.Modules;

namespace Behind
{
    public class BehindModule : NinjectModule
    {
        public override void Load()
        {
            // Logging
            //Bind<ILog>().ToProvider<LogProvider>();
            Bind<ILog>().ToMethod(context => LogManager.GetLogger(context.Request.Target.Member.ReflectedType));

            // Service
            Kernel.Load(new [] { new Behind.Service.BehindServiceModule() });
        }
    }

    //internal class LogProvider : Provider<ILog>
    //{
    //    protected override ILog CreateInstance(IContext context)
    //    {
    //        Type typeLoggerIsInjectedInto = context.Request.ParentContext.Plan.Type;
    //        return LogManager.GetLogger(typeLoggerIsInjectedInto);
    //    }
    //}
}
