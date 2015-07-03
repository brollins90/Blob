namespace Blob.Proxies
{
    using System;
    using System.Threading.Tasks;
    using Contracts.Request;
    using Contracts.Response;
    using Contracts.ServiceContracts;

    public class DeviceStatusClient : BaseClient<IDeviceStatusService>, IDeviceStatusService
    {
        public DeviceStatusClient(string endpointName, string username, string password)
            : base(endpointName, username, password)
        { }

        public async Task<RegisterDeviceResponse> RegisterDeviceAsync(RegisterDeviceRequest request)
        {
            try
            {
                return await Channel.RegisterDeviceAsync(request).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
            return null;
        }

        public async Task AddStatusRecordAsync(AddStatusRecordRequest request)
        {
            try
            {
                await Channel.AddStatusRecordAsync(request).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
        }

        public async Task AddPerformanceRecordAsync(AddPerformanceRecordRequest request)
        {
            try
            {
                await Channel.AddPerformanceRecordAsync(request).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
        }

        public async Task<BlobResult> AuthenticateDeviceAsync(AuthenticateDeviceRequest request)
        {
            try
            {
                return await Channel.AuthenticateDeviceAsync(request).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
            return new BlobResult("Failed");
        }
    }
}