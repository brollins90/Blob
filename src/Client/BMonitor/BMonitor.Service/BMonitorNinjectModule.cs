using System.ServiceModel;
using Blob.Contracts.Command;
using Blob.Contracts.Commands;
using Blob.Proxies;
using BMonitor.CommandHandler;
using log4net;
using Ninject;
using Ninject.Modules;

namespace BMonitor.Service
{
    public class BMonitorNinjectModule : NinjectModule
    {
        private readonly ILog _log;

        public BMonitorNinjectModule()
        {
            _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        }

        public override void Load()
        {
            _log.Info("Registering Ninject dependencies");

            // Logging
            Bind<ILog>().ToMethod(context => LogManager.GetLogger(context.Request.Target.Member.ReflectedType));

            // Service
            Bind<MonitorService>().ToSelf();
            Bind<MonitorManager>().ToSelf();

            // Callback
            Bind<ICommandServiceCallback>().To<CommandServiceCallbackHandler>();
            Bind<CommandClient>().ToSelf()
                .WithConstructorArgument("callbackInstance", x => new InstanceContext((x.Kernel.Get<ICommandServiceCallback>())))
                .WithConstructorArgument("endpointName", "CommandService");

            // Command handlers
            //Bind<BCommandHandler<PrintLineCommand>>().To<PrintLineCommandHandler>();
            Bind(typeof(Blob.Contracts.Command.ICommandHandler<PrintLineCommand>)).To(typeof(BMonitor.CommandHandler.PrintLineCommandHandler));
            //Bind(typeof(ExceptionWrappingCommandHandler<PrintLineCommand>)).To(typeof(BMonitor.CommandHandler.PrintLineCommandHandler));
            //Bind(typeof(ICommandHandler<>)).To(typeof(BMonitor.CommandHandler.ExceptionWrappingCommandHandler<>));
            //Bind(typeof(ICommandHandler<PrintLineCommand>)).To(typeof(BMonitor.CommandHandler.ExceptionWrappingCommandHandler<PrintLineCommand>));



            //Bind<ProxyGenerator>().ToConstant(new ProxyGenerator());

            ////ChannelFactoryCache.Add<ICommandService>(Uri,Binding(endpoint), null));
            //Bind<ICommandService>().ToWcfClient();

            //Bind<CommandClient>().ToSelf()
            //    .WithConstructorArgument("callbackInstance", x => x.Kernel.Get<ICommandServiceCallback>());

            //Bind<Func<ICommandServiceCallback>>().ToMethod(c => () => OperationContext.Current.GetCallbackChannel<ICommandServiceCallback>());
            ////Bind<>
            //Bind<>
            //Tuple<Type,Type>[] commandHandlers = new[] {PrintLineCommand};

            //IEnumerable<Type> definedCommands = 
            //AppDomain.CurrentDomain.GetAssemblies()
            //           .SelectMany(t => t.GetTypes())
            //           .Where(t => t.IsClass && t.Namespace == @namespace)
            //foreach (Tuple<Type, Type> curent in commandHandlers)
            //{
            //    curent.Item1.
            //    var command = typeof(curent.T1)
            //}

        }
    }
}
