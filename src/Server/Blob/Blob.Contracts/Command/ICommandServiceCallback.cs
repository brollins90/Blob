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
            var coreAssembly = typeof(ICommandHandler<>).Assembly;

            var commandTypes =
                coreAssembly.GetExportedTypes().Where(type => type.Name.EndsWith("Command"));

            return commandTypes.ToArray();
        }
    }
}
