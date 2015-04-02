using System.ServiceModel;

namespace Blob.Contracts.Status
{
    [ServiceContract]
    public interface IStatusService
    {
        [OperationContract(IsOneWay = true)]
        void SendStatusToServer(string message);
    }
}
