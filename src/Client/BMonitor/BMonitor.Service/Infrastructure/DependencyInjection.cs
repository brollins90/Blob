using System;
using System.ServiceModel;
using Blob.Contracts.Command;
using Blob.Proxies;
using BMonitor.Handlers.Custom;
using BMonitor.Handlers.Default;
using BMonitor.Service.Extensions;
using log4net;
using Ninject;
using Ninject.Modules;

namespace BMonitor.Service.Infrastructure
{
    public class DependencyInjection : NinjectModule
    {
        private readonly ILog _log;

        public DependencyInjection()
        {
            _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        }

        public override void Load()
        {
            _log.Info("Registering Ninject dependencies");

            // Logging
            Bind<ILog>().ToMethod(context => LogManager.GetLogger(context.Request.Target.Member.ReflectedType));

            // Service
            Bind<ServiceHostBase>().ToSelf();
            Bind<MonitorManager>().ToSelf();

            // Callback
            Bind<ICommandServiceCallback>().To<CommandServiceCallbackHandler>();
            Bind<CommandClient>().ToSelf()
                .WithConstructorArgument("callbackInstance", x => new InstanceContext((x.Kernel.Get<ICommandServiceCallback>())))
                .WithConstructorArgument("endpointName", "CommandService");

            // Command handlers
            Type commandHandlerType = typeof (ICommandHandler<>);
            Type openUnknownCommandHandler = typeof (UnknownCommandHandler<>);
            Type openExceptionCommandHandler = typeof(ExceptionCommandHandlerDecorator<>);
            Type openLoggingCommandHandler = typeof(LoggingCommandHandlerDecorator<>);

            // Bind defaults
            Bind(commandHandlerType).To(openUnknownCommandHandler).WhenInjectedInto(openExceptionCommandHandler);
            Bind(commandHandlerType).To(openExceptionCommandHandler).WhenInjectedInto(openLoggingCommandHandler);
            Bind(commandHandlerType).To(openLoggingCommandHandler);

            // Load more handlers
            string commandHandlerLocation = "BMonitor.CommandHandler";
            // todo: load assembly from file
            var typesInHandlerAssembly = typeof(PrintLineCommandHandler).Assembly.GetTypes();
            var foundCommandHandlers = typesInHandlerAssembly.GetBindingDefinitionOf(commandHandlerType);

            foreach (var definedCommandHandler in foundCommandHandlers.GetDefinitionsWhereClosedGeneric())
            {
                Bind(commandHandlerType.MakeGenericType(definedCommandHandler.GenericType))                                 // ICommandHandler<PrintLineCommand>
                    .To(definedCommandHandler.Implementation)                                                               // PrintLineCommandHandler
                    .WhenInjectedInto(openExceptionCommandHandler.MakeGenericType(definedCommandHandler.GenericType));    // ExceptionCommandHandlerDecorator<PrintLineCommand>

                Bind(commandHandlerType.MakeGenericType(definedCommandHandler.GenericType))                                 // ICommandHandler<PrintLineCommand>
                    .To(openExceptionCommandHandler.MakeGenericType(definedCommandHandler.GenericType))                   // ExceptionCommandHandlerDecorator<PrintLineCommand>
                    .WhenInjectedInto(openLoggingCommandHandler.MakeGenericType(definedCommandHandler.GenericType));      // LoggingCommandHandlerDecorator<PrintLineCommand>

                Bind(commandHandlerType.MakeGenericType(definedCommandHandler.GenericType))                                 // ICommandHandler<PrintLineCommand>
                    .To(openLoggingCommandHandler.MakeGenericType(definedCommandHandler.GenericType));                    // LoggingCommandHandlerDecorator<PrintLineCommand>
            }
        }
    }
}
