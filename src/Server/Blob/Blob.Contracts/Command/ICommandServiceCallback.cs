using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.ServiceModel;

namespace Blob.Contracts.Command
{
    [ServiceContract]
    [ServiceKnownType("GetKnownTypes", typeof(KnownTypeHelpers))]
    public interface ICommandServiceCallback
    {
        [OperationContract(IsOneWay = true)]
        void OnConnect(string message);

        [OperationContract(IsOneWay = true)]
        void OnDisconnect(string message);

        [OperationContract(IsOneWay = true)]
        void OnReceivedPing(string message);

        [OperationContract(IsOneWay = true)]
        void ExecuteCommand(dynamic command);
    }

    public static class KnownTypeHelpers
    {
        public static IEnumerable<Type> GetKnownTypes(ICustomAttributeProvider provider)
        {
            // Get the assembly that contains the ICommandHandler
            var coreAssembly = typeof(ICommandHandler<>).Assembly;

            // Find the classes in the assembly that end with "Command"
            var commandTypes =
                coreAssembly.GetExportedTypes().Where(type => type.Name.EndsWith("Command"));

            // Return the array of Types
            return commandTypes.ToArray();
        }
    }
}
