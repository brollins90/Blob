using System.Security.Permissions;
using System.ServiceModel;
using System.Threading.Tasks;
using Blob.Contracts.Models;

namespace Blob.Contracts.Registration
{
    [ServiceContract]
    public interface IRegistrationService
    {
        [OperationContract]
        [PrincipalPermission(SecurityAction.Demand, Role = "Customer")]
        Task Register(RegisterDeviceDto registerDeviceDto);
    }
}
