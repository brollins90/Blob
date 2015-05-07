using System;
using System.IdentityModel.Selectors;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Security;
using System.Threading.Tasks;
using Blob.Contracts.Blob;
using Blob.Contracts.Dto;

namespace Blob.Proxies
{
    public class BeforeCommandClient : ClientBase<IBlobCommandManager>, IBlobCommandManager
    {
        public Action<Exception> ClientErrorHandler = null;

        public BeforeCommandClient(string endpointName, string username, string password) : base(endpointName)
        {
            ClientCredentials.UserName.UserName = username;
            ClientCredentials.UserName.Password = password;
            ClientCredentials.ServiceCertificate.Authentication.CertificateValidationMode = X509CertificateValidationMode.None;
        }

        //public BeforeCommandClient(Binding binding, EndpointAddress address) : base(binding, address) { }

        private void HandleError(Exception ex)
        {
            if (ClientErrorHandler != null)
                ClientErrorHandler(ex);
            else
                throw new Exception("Server exception.", ex);
        }

        public async Task DisableCustomerAsync(DisableCustomerDto dto)
        {
            try
            {
                await Channel.DisableCustomerAsync(dto).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
        }

        public async Task EnableCustomerAsync(EnableCustomerDto dto)
        {
            try
            {
                await Channel.EnableCustomerAsync(dto).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
        }

        public async Task UpdateCustomerAsync(UpdateCustomerDto dto)
        {
            try
            {
                await Channel.UpdateCustomerAsync(dto).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
        }

        public async Task IssueCommandAsync(IssueDeviceCommandDto dto)
        {
            try
            {
                await Channel.IssueCommandAsync(dto).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
        }

        public async Task DisableDeviceAsync(DisableDeviceDto dto)
        {
            try
            {
                await Channel.DisableDeviceAsync(dto).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
        }

        public async Task EnableDeviceAsync(EnableDeviceDto dto)
        {
            try
            {
                await Channel.EnableDeviceAsync(dto).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
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

        public async Task UpdateDeviceAsync(UpdateDeviceDto dto)
        {
            try
            {
                await Channel.UpdateDeviceAsync(dto).ConfigureAwait(false);
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

        public async Task DeletePerformanceRecordAsync(DeletePerformanceRecordDto dto)
        {
            try
            {
                await Channel.DeletePerformanceRecordAsync(dto).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
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

        public async Task DeleteStatusRecordAsync(DeleteStatusRecordDto dto)
        {
            try
            {
                await Channel.DeleteStatusRecordAsync(dto).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
        }

        public async Task DisableUserAsync(DisableUserDto dto)
        {
            try
            {
                await Channel.DisableUserAsync(dto).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
        }

        public async Task EnableUserAsync(EnableUserDto dto)
        {
            try
            {
                await Channel.EnableUserAsync(dto).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
        }

        public async Task UpdateUserAsync(UpdateUserDto dto)
        {
            try
            {
                await Channel.UpdateUserAsync(dto).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
        }
    }
}
