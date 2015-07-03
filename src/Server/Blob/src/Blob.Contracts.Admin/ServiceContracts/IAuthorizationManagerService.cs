namespace Blob.Contracts.ServiceContracts
{
    using System.ServiceModel;
    using System.Threading.Tasks;
    using Models;

    [ServiceContract]
    public interface IAuthorizationManagerService
    {
        //todo: authenticate before user
        [OperationContract]
        Task<bool> CheckAccessAsync(AuthorizationContextDto context);
    }
}