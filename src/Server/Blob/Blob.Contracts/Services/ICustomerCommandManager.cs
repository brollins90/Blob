using System.ServiceModel;
using System.Threading.Tasks;
using Blob.Contracts.Models;

namespace Blob.Contracts.Services
{
    [ServiceContract]
    public interface ICustomerCommandManager
    {
        [OperationContract]
        Task<BlobResultDto> DisableCustomerAsync(DisableCustomerDto dto);
        
        [OperationContract]
        Task<BlobResultDto> EnableCustomerAsync(EnableCustomerDto dto);
        
        [OperationContract]
        Task<BlobResultDto> RegisterCustomerAsync(RegisterCustomerDto dto);

        [OperationContract]
        Task<BlobResultDto> UpdateCustomerAsync(UpdateCustomerDto dto);
    }
}
