using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Blob.Core.Domain;

namespace Blob.Core.Data
{
    public interface IStatusRepository
    {
        // Device
        Task CreateDeviceAsync(Device device);
        Task<Device> FindDeviceByIdAsync(Guid id);
        Task<IList<Device>> FindDevicesByForCustomerAsync(Guid customerId);
        Task UpdateDeviceAsync(Device device);
        Task DeleteDeviceAsync(Device device);

        // DeviceType
        Task CreateDeviceTypeAsync(DeviceType deviceType);
        Task<DeviceType>FindDeviceTypeByIdAsync(Guid id);
        Task<DeviceType> FindDeviceTypeByValueAsync(string value);
        Task UpdateDeviceTypeAsync(DeviceType deviceType);
        Task DeleteDeviceTypeAsync(DeviceType deviceType);

        // Status
        Task<IList<Status>> FindStatusForDeviceAsync(Guid deviceId);
        Task<IList<Status>> GetStatusAsync(Device device);
        Task AddStatusAsync(Device device, Status status);
        Task RemoveStatusAsync(Device device, Status status);

        // Performance
        Task<IList<StatusPerf>> FindPerformanceForDeviceAsync(Guid deviceId);
        Task<IList<StatusPerf>> GetPerformanceAsync(Device device);
        Task AddPerformanceAsync(Device device, StatusPerf statusPerf);
        Task RemovePerformanceAsync(Device device, StatusPerf statusPerf);
    }
}
