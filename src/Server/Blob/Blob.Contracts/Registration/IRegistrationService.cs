using System.Security.Permissions;
using System.ServiceModel;
using System.Threading.Tasks;
using Blob.Contracts.Dto;

namespace Blob.Contracts.Registration
{
    [ServiceContract]
    public interface IRegistrationService
    {
        [OperationContract]
        [PrincipalPermission(SecurityAction.Demand, Role = "Customer")]
        Task<RegisterDeviceResponseDto> Register(RegisterDeviceDto registerDeviceDto);
    }
}
