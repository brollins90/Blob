using System;
using System.Threading.Tasks;
using Blob.Contracts.Models;
using Blob.Contracts.ServiceContracts;

namespace Blob.Proxies
{
    public class DeviceStatusClient : BaseClient<IDeviceStatusService>, IDeviceStatusService
    {
        public DeviceStatusClient(string endpointName, string username, string password)
            : base(endpointName, username, password) { }

        public async Task<RegisterDeviceResponseDto> RegisterDeviceAsync(RegisterDeviceDto dto)
        {
            try
            {
                return await Channel.RegisterDeviceAsync(dto).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
            return null;
        }

        public async Task AddStatusRecordAsync(AddStatusRecordDto dto)
        {
            try
            {
                await Channel.AddStatusRecordAsync(dto).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
        }

        public async Task AddPerformanceRecordAsync(AddPerformanceRecordDto dto)
        {
            try
            {
                await Channel.AddPerformanceRecordAsync(dto).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
        }
    }
}
