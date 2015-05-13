using System.ServiceModel;
using System.Threading.Tasks;

namespace Blob.Contracts.Security
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
