namespace Blob.Proxies
{
    using System;
    using System.Threading.Tasks;
    using Contracts.Request;
    using Contracts.Response;
    using Contracts.ServiceContracts;

    public class BeforeCommandClient : BaseClient<IBlobCommandManager>, IBlobCommandManager
    {
        public BeforeCommandClient(string endpointName, string username, string password) : base(endpointName, username, password) { }

        public async Task<BlobResult> DisableCustomerAsync(DisableCustomerRequest dto)
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

        public async Task<BlobResult> EnableCustomerAsync(EnableCustomerRequest dto)
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

        public async Task<BlobResult> RegisterCustomerAsync(RegisterCustomerRequest dto)
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

        public async Task<BlobResult> UpdateCustomerAsync(UpdateCustomerRequest dto)
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

        public async Task<BlobResult> IssueCommandAsync(IssueDeviceCommandRequest dto)
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

        public async Task<BlobResult> DisableDeviceAsync(DisableDeviceRequest dto)
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

        public async Task<BlobResult> EnableDeviceAsync(EnableDeviceRequest dto)
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

        public async Task<BlobResult> UpdateDeviceAsync(UpdateDeviceRequest dto)
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

        public async Task<BlobResult> DeletePerformanceRecordAsync(DeletePerformanceRecordRequest dto)
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

        public async Task<BlobResult> DeleteStatusRecordAsync(DeleteStatusRecordRequest dto)
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

        public async Task<BlobResult> CreateUserAsync(CreateUserRequest dto)
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

        public async Task<BlobResult> DisableUserAsync(DisableUserRequest dto)
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

        public async Task<BlobResult> EnableUserAsync(EnableUserRequest dto)
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

        public async Task<BlobResult> UpdateUserAsync(UpdateUserRequest dto)
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

        public async Task<BlobResult> CreateCustomerGroupAsync(CreateCustomerGroupRequest dto)
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

        public async Task<BlobResult> DeleteCustomerGroupAsync(DeleteCustomerGroupRequest dto)
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

        public async Task<BlobResult> UpdateCustomerGroupAsync(UpdateCustomerGroupRequest dto)
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

        public async Task<BlobResult> AddRoleToCustomerGroupAsync(AddRoleToCustomerGroupRequest dto)
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

        public async Task<BlobResult> AddUserToCustomerGroupAsync(AddUserToCustomerGroupRequest dto)
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

        public async Task<BlobResult> RemoveRoleFromCustomerGroupAsync(RemoveRoleFromCustomerGroupRequest dto)
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

        public async Task<BlobResult> RemoveUserFromCustomerGroupAsync(RemoveUserFromCustomerGroupRequest dto)
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