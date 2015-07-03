using System;
using System.Threading.Tasks;
using Blob.Contracts.Models;
using Blob.Contracts.ServiceContracts;

namespace Blob.Proxies
{
    public class BeforeCommandClient : BaseClient<IBlobCommandManager>, IBlobCommandManager
    {
        public BeforeCommandClient(string endpointName, string username, string password) : base(endpointName, username, password) { }

        public async Task<BlobResult> DisableCustomerAsync(DisableCustomerDto dto)
        {
            try
            {
                return await Channel.DisableCustomerAsync(dto).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
            return new BlobResult("Client proxy error.");
        }

        public async Task<BlobResult> EnableCustomerAsync(EnableCustomerDto dto)
        {
            try
            {
                return await Channel.EnableCustomerAsync(dto).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
            return new BlobResult("Client proxy error.");
        }

        public async Task<BlobResult> RegisterCustomerAsync(RegisterCustomerDto dto)
        {
            try
            {
                return await Channel.RegisterCustomerAsync(dto).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
            return new BlobResult("Client proxy error.");
        }

        public async Task<BlobResult> UpdateCustomerAsync(UpdateCustomerDto dto)
        {
            try
            {
                return await Channel.UpdateCustomerAsync(dto).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
            return new BlobResult("Client proxy error.");
        }

        public async Task<BlobResult> IssueCommandAsync(IssueDeviceCommandDto dto)
        {
            try
            {
                return await Channel.IssueCommandAsync(dto).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
            return new BlobResult("Client proxy error.");
        }

        public async Task<BlobResult> DisableDeviceAsync(DisableDeviceDto dto)
        {
            try
            {
                return await Channel.DisableDeviceAsync(dto).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
            return new BlobResult("Client proxy error.");
        }

        public async Task<BlobResult> EnableDeviceAsync(EnableDeviceDto dto)
        {
            try
            {
                return await Channel.EnableDeviceAsync(dto).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
            return new BlobResult("Client proxy error.");
        }

        public async Task<RegisterDeviceResponse> RegisterDeviceAsync(RegisterDeviceRequest dto)
        {
            try
            {
                return await Channel.RegisterDeviceAsync(dto).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
            return new RegisterDeviceResponse();
        }

        public async Task<BlobResult> UpdateDeviceAsync(UpdateDeviceDto dto)
        {
            try
            {
                return await Channel.UpdateDeviceAsync(dto).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
            return new BlobResult("Client proxy error.");
        }

        public async Task<BlobResult> AddPerformanceRecordAsync(AddPerformanceRecordRequest dto)
        {
            try
            {
                return await Channel.AddPerformanceRecordAsync(dto).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
            return new BlobResult("Client proxy error.");
        }

        public async Task<BlobResult> DeletePerformanceRecordAsync(DeletePerformanceRecordDto dto)
        {
            try
            {
                return await Channel.DeletePerformanceRecordAsync(dto).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
            return new BlobResult("Client proxy error.");
        }

        public async Task<BlobResult> AddStatusRecordAsync(AddStatusRecordRequest dto)
        {
            try
            {
                return await Channel.AddStatusRecordAsync(dto).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
            return new BlobResult("Client proxy error.");
        }

        public async Task<BlobResult> DeleteStatusRecordAsync(DeleteStatusRecordDto dto)
        {
            try
            {
                return await Channel.DeleteStatusRecordAsync(dto).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
            return new BlobResult("Client proxy error.");
        }

        public async Task<BlobResult> CreateUserAsync(CreateUserDto dto)
        {
            try
            {
                return await Channel.CreateUserAsync(dto).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
            return new BlobResult("Client proxy error.");
        }

        public async Task<BlobResult> DisableUserAsync(DisableUserDto dto)
        {
            try
            {
                return await Channel.DisableUserAsync(dto).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
            return new BlobResult("Client proxy error.");
        }

        public async Task<BlobResult> EnableUserAsync(EnableUserDto dto)
        {
            try
            {
                return await Channel.EnableUserAsync(dto).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
            return new BlobResult("Client proxy error.");
        }

        public async Task<BlobResult> UpdateUserAsync(UpdateUserDto dto)
        {
            try
            {
                return await Channel.UpdateUserAsync(dto).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
            return new BlobResult("Client proxy error.");
        }

        public async Task<BlobResult> CreateCustomerGroupAsync(CreateCustomerGroupDto dto)
        {
            try
            {
                return await Channel.CreateCustomerGroupAsync(dto).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
            return new BlobResult("Client proxy error.");
        }

        public async Task<BlobResult> DeleteCustomerGroupAsync(DeleteCustomerGroupDto dto)
        {
            try
            {
                return await Channel.DeleteCustomerGroupAsync(dto).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
            return new BlobResult("Client proxy error.");
        }

        public async Task<BlobResult> UpdateCustomerGroupAsync(UpdateCustomerGroupDto dto)
        {
            try
            {
                return await Channel.UpdateCustomerGroupAsync(dto).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
            return new BlobResult("Client proxy error.");
        }

        public async Task<BlobResult> AddRoleToCustomerGroupAsync(AddRoleToCustomerGroupDto dto)
        {
            try
            {
                return await Channel.AddRoleToCustomerGroupAsync(dto).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
            return new BlobResult("Client proxy error.");
        }

        public async Task<BlobResult> AddUserToCustomerGroupAsync(AddUserToCustomerGroupDto dto)
        {
            try
            {
                return await Channel.AddUserToCustomerGroupAsync(dto).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
            return new BlobResult("Client proxy error.");
        }

        public async Task<BlobResult> RemoveRoleFromCustomerGroupAsync(RemoveRoleFromCustomerGroupDto dto)
        {
            try
            {
                return await Channel.RemoveRoleFromCustomerGroupAsync(dto).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
            return new BlobResult("Client proxy error.");
        }

        public async Task<BlobResult> RemoveUserFromCustomerGroupAsync(RemoveUserFromCustomerGroupDto dto)
        {
            try
            {
                return await Channel.RemoveUserFromCustomerGroupAsync(dto).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
            return new BlobResult("Client proxy error.");
        }
    }
}
