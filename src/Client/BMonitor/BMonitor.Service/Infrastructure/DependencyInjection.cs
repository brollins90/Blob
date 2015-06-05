using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using Blob.Contracts.Commands;
using Blob.Contracts.ServiceContracts;
using Blob.Proxies;
using BMonitor.Common.Interfaces;
using BMonitor.Handlers;
using BMonitor.Monitors;
using BMonitor.Service.Connection;
using BMonitor.Service.Extensions;
using BMonitor.Service.Helpers;
using BMonitor.Service.Monitor;
using BMonitor.Service.Monitor.Quartz;
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

            Bind<IMonitorScheduler>().To<QuartzMonitorScheduler>().InSingletonScope();

            Bind<BlobClientFactory>().ToSelf();

            // Callback
            Bind<IDeviceConnectionServiceCallback>().To<ConnectionServiceCallbackHandler>();
            Bind<DeviceConnectionClient>().ToSelf()
                .WithConstructorArgument("callbackInstance", x => new InstanceContext((x.Kernel.Get<IDeviceConnectionServiceCallback>())))
                .WithConstructorArgument("endpointName", "DeviceConnectionService");
            Bind<DeviceStatusClient>().ToSelf()
                .WithConstructorArgument("endpointName", "DeviceStatusService");



            // load monitors
            var foundMonitors = typeof(BaseMonitor).Assembly.GetExportedTypes().Where(type => type.IsAssignableFrom(typeof(IMonitor)));
            foreach (var monitor in foundMonitors)
            {
                Bind(monitor).ToSelf();
            }

            // Command handlers
            Type commandHandlerType = typeof (IDeviceCommandHandler<>);
            Type openUnknownCommandHandler = typeof (UnknownCommandHandler<>);
            Type openExceptionCommandHandler = typeof(ExceptionCommandHandlerDecorator<>);
            Type openLoggingCommandHandler = typeof(LoggingCommandHandlerDecorator<>);

            // Bind defaults
            Bind(commandHandlerType).To(openUnknownCommandHandler).WhenInjectedInto(openExceptionCommandHandler);
            Bind(commandHandlerType).To(openExceptionCommandHandler).WhenInjectedInto(openLoggingCommandHandler);
            Bind(commandHandlerType).To(openLoggingCommandHandler);

            // Load more handlers
            //string commandHandlerLocation = "BMonitor.Handlers.Custom";
            // todo: load assembly from file
            Type[] typesInHandlerAssembly = typeof(PrintLineCommandHandler).Assembly.GetTypes();
            IEnumerable<BindingDefinition> foundCommandHandlers = typesInHandlerAssembly.GetBindingDefinitionOf(commandHandlerType);

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
