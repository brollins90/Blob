using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Security;
using System.Threading.Tasks;
using Blob.Contracts.Device;
using Blob.Contracts.Dto;

namespace Blob.Proxies
{
    public class DeviceStatusClient : ClientBase<IDeviceStatusService>, IDeviceStatusService
    {
        public Action<Exception> ClientErrorHandler = null;

        public DeviceStatusClient(string endpointName, string username, string password)
            : base(endpointName)
        {
            ClientCredentials.UserName.UserName = username;
            ClientCredentials.UserName.Password = password;
            ClientCredentials.ServiceCertificate.Authentication.CertificateValidationMode = X509CertificateValidationMode.None;
        }
        //public DeviceStatusClient(Binding binding, EndpointAddress address) : base(binding, address) { }

        private void HandleError(Exception ex)
        {
            if (ClientErrorHandler != null)
                ClientErrorHandler(ex);
            else
                throw new Exception("Server exception.", ex);
        }

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
