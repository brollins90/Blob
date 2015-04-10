using Blob.Contracts.Models;
using System.ServiceModel;
using System.Threading.Tasks;

namespace Blob.Contracts.Registration
{
    [ServiceContract]
    public interface IRegistrationService
    {
        [OperationContract]
        Task<RegistrationInformation> Register(RegistrationMessage message);
    }
}
