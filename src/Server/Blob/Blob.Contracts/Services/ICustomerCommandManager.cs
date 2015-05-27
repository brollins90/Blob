using System.ServiceModel;
using System.Threading.Tasks;
using Blob.Contracts.Models;

namespace Blob.Contracts.Services
{
    [ServiceContract]
    public interface ICustomerCommandManager
    {
        [OperationContract]
        Task DisableCustomerAsync(DisableCustomerDto dto);
        
        [OperationContract]
        Task EnableCustomerAsync(EnableCustomerDto dto);
        
        [OperationContract]
        Task<IdentityResultDto> RegisterCustomerAsync(RegisterCustomerDto dto);

        [OperationContract]
        Task UpdateCustomerAsync(UpdateCustomerDto dto);
    }
}
