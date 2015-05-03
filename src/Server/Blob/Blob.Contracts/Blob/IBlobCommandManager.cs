using System.Threading.Tasks;
using Blob.Contracts.Models;

namespace Blob.Contracts.Blob
{
    public interface IBlobCommandManager
    {
        // Customer
        Task UpdateCustomerAsync(UpdateCustomerDto dto);

        // Device
        Task DisableDeviceAsync(DisableDeviceDto dto);
        Task RegisterDeviceAsync(RegisterDeviceDto dto);
        Task UpdateDeviceAsync(UpdateDeviceDto dto);

    }
}
