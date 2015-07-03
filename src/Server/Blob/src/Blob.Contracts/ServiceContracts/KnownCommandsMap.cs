namespace Blob.Contracts.ServiceContracts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Commands;
#if DNXCORE50
using ICustomAttributeProvider = System.Runtime.Serialization.DataContractSerializer;//Newtonsoft.Json.Utilities.CustomAttributeProvider;
#endif

    public static class KnownCommandsMap
    {
        private static readonly Type CommandHandlerInterfaceType = typeof(IDeviceCommandHandler<>);

        public static IList<Type> GetKnownCommandTypes(ICustomAttributeProvider knownTypeAttributeTarget)
        {
            return GetKnownCommandTypes();
        }

        public static IList<Type> GetKnownCommandTypes()
        {
            // Get the assembly that contains the ICommandHandler
            Assembly coreAssembly = GetCommandHandlerInterfaceAssembly();

            // Find the classes in the assembly that end with "Command"
            var commandTypes = coreAssembly.GetExportedTypes().Where(type => type.Name.EndsWith("Command"));
            //var commandTypes = coreAssembly.GetExportedTypes().Where(type => type.Name.EndsWith("CmdExecuteCommand"));

            // Return the array of Types
            return commandTypes.ToList();
        }

        public static Assembly GetCommandHandlerInterfaceAssembly()
        {
            return CommandHandlerInterfaceType.GetTypeInfo().Assembly;
        }
    }
}