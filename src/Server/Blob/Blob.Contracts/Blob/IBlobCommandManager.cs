using System.Threading.Tasks;
using Blob.Contracts.Dto;

namespace Blob.Contracts.Blob
{
    public interface IBlobCommandManager
    {
        // Customer
        Task UpdateCustomerAsync(UpdateCustomerDto dto);

        // Device
        Task DisableDeviceAsync(DisableDeviceDto dto);
        Task EnableDeviceAsync(EnableDeviceDto dto);
        Task<RegisterDeviceResponseDto> RegisterDeviceAsync(RegisterDeviceDto dto);
        Task UpdateDeviceAsync(UpdateDeviceDto dto);

    }
}
