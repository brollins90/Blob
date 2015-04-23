using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Blob.Contracts.Models;
using Blob.Core.Domain;

namespace Blob.Managers.Blob
{
    public interface IBlobManager
    {
        //Customer
        Task<IList<Customer>> GetAllCustomersAsync();

        // Device
        Task<RegistrationInformation> RegisterDevice(RegistrationMessage message);

        Task DeleteDeviceAsync(Guid deviceId);
        Task<IList<Device>> GetAllDevicesAsync();
        Task<Device> GetDeviceByIdAsync(Guid deviceId);
        Task<IList<Device>> FindDevicesForCustomer(Guid customerId);
        Task UpdateDeviceAsync(Device device);

        // Status
        Task<IList<Core.Domain.Status>> GetStatusForDeviceAsync(Guid deviceId);

        // Performance
        Task<IList<StatusPerf>> GetPerformanceForDeviceAsync(Guid deviceId);
    }
}
