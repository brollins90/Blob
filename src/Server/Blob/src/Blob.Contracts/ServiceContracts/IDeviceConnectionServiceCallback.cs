namespace Blob.Contracts.ServiceContracts
{
    using System;
    using System.ServiceModel;

    [ServiceContract]
    [ServiceKnownType("GetKnownCommandTypes", typeof(KnownCommandsMap))]
    public interface IDeviceConnectionServiceCallback
    {
        [OperationContract(IsOneWay = true)]
        void OnConnect(string message);

        [OperationContract(IsOneWay = true)]
        void OnDisconnect(string message);

        [OperationContract(IsOneWay = true)]
        void OnReceivedPing(string message);

        [OperationContract(IsOneWay = true)]
        void ExecuteCommand(Guid commandId, dynamic command);
    }
}