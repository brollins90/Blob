using System.Threading.Tasks;
using Blob.Contracts.Dto;

namespace Blob.Contracts.Blob
{
    public interface IBlobCommandManager
    {
        // Customer
        Task DisableCustomerAsync(DisableCustomerDto dto);
        Task EnableCustomerAsync(EnableCustomerDto dto);
        Task UpdateCustomerAsync(UpdateCustomerDto dto);

        // Device
        Task DisableDeviceAsync(DisableDeviceDto dto);
        Task EnableDeviceAsync(EnableDeviceDto dto);
        Task<RegisterDeviceResponseDto> RegisterDeviceAsync(RegisterDeviceDto dto);
        Task UpdateDeviceAsync(UpdateDeviceDto dto);

        // User
        Task DisableUserAsync(DisableUserDto dto);
        Task EnableUserAsync(EnableUserDto dto);
        Task UpdateUserAsync(UpdateUserDto dto);

    }
}
