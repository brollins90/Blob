using System.ServiceModel;

namespace Blob.Contracts.Command
{
    public interface ICommandServiceCallback
    {
        [OperationContract(IsOneWay = true)]
        void OnConnect(string message);

        [OperationContract(IsOneWay = true)]
        void OnDisconnect(string message);

        [OperationContract(IsOneWay = true)]
        void OnReceivedPing(string message);

        //[OperationContract(IsOneWay = true)]
        //Task ExecuteComamnd(RemediationCommand command);
    }
}
