using System.Security.Permissions;
using Blob.Contracts.Models;
using System.ServiceModel;
using System.Threading.Tasks;

namespace Blob.Contracts.Registration
{
    [ServiceContract]
    public interface IRegistrationService
    {
        [OperationContract]
        [PrincipalPermission(SecurityAction.Demand, Role = "Customer")]
        Task<RegistrationInformation> Register(RegistrationMessage message);
    }
}
