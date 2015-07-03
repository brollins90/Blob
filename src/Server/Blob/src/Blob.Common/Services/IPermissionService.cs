namespace Blob.Common.Services
{
    using System.Threading.Tasks;
    using Contracts.Models;

    public interface IPermissionService
    {
        //Task<bool> CheckAccessAsync(Guid UserId, string context);
        Task<bool> CheckAccessAsync(AuthorizationContextDto context);
    }
}