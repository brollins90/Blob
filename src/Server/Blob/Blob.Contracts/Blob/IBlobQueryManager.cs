using System;
using System.Threading.Tasks;
using Blob.Contracts.Dto.ViewModels;

namespace Blob.Contracts.Blob
{
    public interface IBlobQueryManager
    {
        Task<CustomerSingleVm> GetCustomerSingleVmAsync(Guid customerId);

        Task<DeviceSingleVm> GetDeviceSingleVmAsync(Guid deviceId);
        Task<DeviceUpdateVm> GetDeviceUpdateVmAsync(Guid deviceId);

        Task<StatusRecordSingleVm> GetStatusRecordSingleVmAsync(long recordId);

        Task<PerformanceRecordSingleVm> GetPerformanceRecordSingleVmAsync(long recordId);
    }
}
