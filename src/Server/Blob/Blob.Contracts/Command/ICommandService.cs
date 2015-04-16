using System;
using System.ServiceModel;

namespace Blob.Contracts.Command
{
    [ServiceContract(
    Name = "ICommandService",
    Namespace = "Blob.Contracts.Command",
    CallbackContract = typeof(ICommandServiceCallback))]
    public interface ICommandService
    {
        [OperationContract(IsOneWay = true)]
        void Connect(Guid deviceId);

        [OperationContract(IsOneWay = true)]
        void Disconnect(Guid deviceId);

        [OperationContract(IsOneWay = true)]
        void Ping(Guid deviceId);
    }
}
