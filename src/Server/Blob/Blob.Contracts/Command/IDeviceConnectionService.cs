using System;
using System.ServiceModel;

namespace Blob.Contracts.Command
{
    [ServiceContract(CallbackContract = typeof(IDeviceConnectionServiceCallback))]
    public interface IDeviceConnectionService
    {
        [OperationContract(IsOneWay = true)]
        void Connect(Guid deviceId);

        [OperationContract(IsOneWay = true)]
        void Disconnect(Guid deviceId);

        [OperationContract(IsOneWay = true)]
        void Ping(Guid deviceId);
    }
}
