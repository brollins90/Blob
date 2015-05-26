using System.ServiceModel;
using System.Threading.Tasks;
using Blob.Contracts.Models;

namespace Blob.Contracts.ServiceContracts
{
    public interface IBlobCustomerManager
    {
        [OperationContract]
        Task DisableCustomerAsync(DisableCustomerDto dto);
        [OperationContract]
        Task EnableCustomerAsync(EnableCustomerDto dto);
        [OperationContract]
        Task RegisterCustomerAsync(RegisterCustomerDto dto);
        [OperationContract]
        Task UpdateCustomerAsync(UpdateCustomerDto dto);
        //Task DeleteCustomerAsync(Guid customerId);
        //Task<Blob.Core.Models.Customer> FindByIdAsync(Guid customerId);
        //Task<IEnumerable<Blob.Core.Models.Role>> GetCustomerRolesAsync(Guid customerId);
        //Task<System.Collections.Generic.IEnumerable<Blob.Core.Models.User>> GetCustomerUsersAsync(Guid customerId);
        //Task<Microsoft.AspNet.Identity.IdentityResult> RemoveUserFromCustomersAsync(Guid userId);
    }
}
