using System.ServiceModel;
using System.Threading.Tasks;
using Blob.Contracts.Models;

namespace Blob.Contracts.ServiceContracts
{
    [ServiceContract]
    public interface IAuthorizationManagerService
    {
        //[OperationContract]
        //bool CheckAccess(AuthorizationContextDto context);
        [OperationContract]
        Task<bool> CheckAccessAsync(AuthorizationContextDto context);
    }
}
