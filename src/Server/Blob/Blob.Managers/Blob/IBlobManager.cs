using System;
using System.Collections.Generic;
using Blob.Contracts.Models;
using System.Threading.Tasks;
using Blob.Core.Domain;

namespace Blob.Managers.Blob
{
    public interface IBlobManager
    {
        Task<RegistrationInformation> RegisterDevice(RegistrationMessage message);

        Task DeleteDeviceAsync(Guid deviceId);
        Task<Device> GetDeviceByIdAsync(Guid deviceId);
        Task<IList<Device>> FindDevicesForCustomer(Guid customerId);
        Task UpdateDeviceAsync(Device device);

        // Status
        Task<IList<Core.Domain.Status>> GetStatusForDeviceAsync(Guid deviceId);

        // Performance
        Task<IList<StatusPerf>> GetPerformanceForDeviceAsync(Guid deviceId);
    }
}
