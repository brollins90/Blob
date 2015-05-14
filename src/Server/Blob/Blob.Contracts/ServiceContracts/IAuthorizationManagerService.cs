using System.ServiceModel;
using System.Threading.Tasks;
using Blob.Contracts.Models;

namespace Blob.Contracts.ServiceContracts
{
    [ServiceContract]
    public interface IAuthorizationManagerService
    {
        //todo: authenticate before user
        [OperationContract]
        Task<bool> CheckAccessAsync(AuthorizationContextDto context);
    }
}
