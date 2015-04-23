using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Blob.Core.Domain;

namespace Blob.Core.Data
{
    public interface IAccountRepository
    {
        // Customer
        Task CreateCustomerAsync(Customer customer);
        Task DeleteCustomerAsync(Guid customerId);
        Task<Customer> FindCustomerByIdAsync(Guid customerId);
        Task<IList<Customer>> GetAllCustomersAsync();
        Task UpdateCustomerAsync(Customer customer);

        // Device
        Task CreateDeviceAsync(Device device);
        Task DeleteDeviceAsync(Guid deviceId);
        Task<Device> FindDeviceByIdAsync(Guid deviceId);
        Task<IList<Device>> FindDevicesByCustomerIdAsync(Guid customerId);
        Task<IList<Device>> GetAllDevicesAsync();
        Task UpdateDeviceAsync(Device device);

        // DeviceType
        Task CreateDeviceTypeAsync(DeviceType deviceType);
        Task DeleteDeviceTypeAsync(Guid deviceTypeId);
        Task<DeviceType> FindDeviceTypeByIdAsync(Guid deviceTypeId);
        Task<DeviceType> FindDeviceTypeByValueAsync(string value);
        Task UpdateDeviceTypeAsync(DeviceType deviceType);
    }
}
