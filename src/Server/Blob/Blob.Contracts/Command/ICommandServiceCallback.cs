using System.ServiceModel;

namespace Blob.Contracts.Command
{
    [ServiceContract]
    [ServiceKnownType("GetKnownCommandTypes", typeof(KnownCommandsMap))]
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
}
