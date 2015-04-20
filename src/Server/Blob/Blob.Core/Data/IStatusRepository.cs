using Blob.Core.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blob.Core.Data
{
    public interface IStatusRepository
    {
        // Device

        Task CreateDeviceAsync(Device device);
        Task<Device> FindDeviceByIdAsync(Guid deviceId);
        Task UpdateDeviceAsync(Device device);
        Task DeleteDeviceAsync(Device device);


        // Status
        Task<IList<Status>> GetStatusAsync(Device device);
        Task AddStatusAsync(Device device, Status status);
        Task RemoveStatusAsync(Device device, Status status);


        // Performance
        Task<IList<Status>> GetPerformanceAsync(Device device);
        Task AddPerformanceAsync(Device device, StatusPerf statusPerf);
        Task RemovePerformanceAsync(Device device, StatusPerf statusPerf);
    }
}
