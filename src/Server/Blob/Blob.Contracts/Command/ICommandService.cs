using System;
using System.Security.Permissions;
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
        [PrincipalPermission(SecurityAction.Demand, Role = "Device")]
        void Connect(Guid deviceId);

        [OperationContract(IsOneWay = true)]
        [PrincipalPermission(SecurityAction.Demand, Role = "Device")]
        void Disconnect(Guid deviceId);

        [OperationContract(IsOneWay = true)]
        [PrincipalPermission(SecurityAction.Demand, Role = "Device")]
        void Ping(Guid deviceId);
    }
}
