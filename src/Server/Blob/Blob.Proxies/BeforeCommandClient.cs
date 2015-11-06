using System;
using System.Threading.Tasks;
using Blob.Contracts.Models;
using Blob.Contracts.ServiceContracts;

namespace Blob.Proxies
{
    public class BeforeCommandClient : BaseClient<IBlobCommandManager>, IBlobCommandManager
    {
        public BeforeCommandClient(string endpointName, string username, string password) : base(endpointName, username, password) { }

        public async Task<BlobResultDto> DisableCustomerAsync(DisableCustomerDto dto)
        {
            try
            {
                return await Channel.DisableCustomerAsync(dto).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
            return new BlobResultDto("Client proxy error.");
        }

        public async Task<BlobResultDto> EnableCustomerAsync(EnableCustomerDto dto)
        {
            try
            {
                return await Channel.EnableCustomerAsync(dto).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
            return new BlobResultDto("Client proxy error.");
        }

        public async Task<BlobResultDto> RegisterCustomerAsync(RegisterCustomerDto dto)
        {
            try
            {
                return await Channel.RegisterCustomerAsync(dto).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
            return new BlobResultDto("Client proxy error.");
        }

        public async Task<BlobResultDto> UpdateCustomerAsync(UpdateCustomerDto dto)
        {
            try
            {
                return await Channel.UpdateCustomerAsync(dto).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
            return new BlobResultDto("Client proxy error.");
        }

        public async Task<BlobResultDto> IssueCommandAsync(IssueDeviceCommandDto dto)
        {
            try
            {
                return await Channel.IssueCommandAsync(dto).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
            return new BlobResultDto("Client proxy error.");
        }

        public async Task<BlobResultDto> DisableDeviceAsync(DisableDeviceDto dto)
        {
            try
            {
                return await Channel.DisableDeviceAsync(dto).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
            return new BlobResultDto("Client proxy error.");
        }

        public async Task<BlobResultDto> EnableDeviceAsync(EnableDeviceDto dto)
        {
            try
            {
                return await Channel.EnableDeviceAsync(dto).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
            return new BlobResultDto("Client proxy error.");
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
            return new RegisterDeviceResponseDto();
        }

        public async Task<BlobResultDto> UpdateDeviceAsync(UpdateDeviceDto dto)
        {
            try
            {
                return await Channel.UpdateDeviceAsync(dto).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
            return new BlobResultDto("Client proxy error.");
        }

        public async Task<BlobResultDto> AddPerformanceRecordAsync(AddPerformanceRecordDto dto)
        {
            try
            {
                return await Channel.AddPerformanceRecordAsync(dto).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
            return new BlobResultDto("Client proxy error.");
        }

        public async Task<BlobResultDto> DeletePerformanceRecordAsync(DeletePerformanceRecordDto dto)
        {
            try
            {
                return await Channel.DeletePerformanceRecordAsync(dto).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
            return new BlobResultDto("Client proxy error.");
        }

        public async Task<BlobResultDto> AddStatusRecordAsync(AddStatusRecordDto dto)
        {
            try
            {
                return await Channel.AddStatusRecordAsync(dto).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
            return new BlobResultDto("Client proxy error.");
        }

        public async Task<BlobResultDto> DeleteStatusRecordAsync(DeleteStatusRecordDto dto)
        {
            try
            {
                return await Channel.DeleteStatusRecordAsync(dto).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
            return new BlobResultDto("Client proxy error.");
        }

        public async Task<BlobResultDto> CreateUserAsync(CreateUserDto dto)
        {
            try
            {
                return await Channel.CreateUserAsync(dto).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
            return new BlobResultDto("Client proxy error.");
        }

        public async Task<BlobResultDto> DisableUserAsync(DisableUserDto dto)
        {
            try
            {
                return await Channel.DisableUserAsync(dto).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
            return new BlobResultDto("Client proxy error.");
        }

        public async Task<BlobResultDto> EnableUserAsync(EnableUserDto dto)
        {
            try
            {
                return await Channel.EnableUserAsync(dto).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
            return new BlobResultDto("Client proxy error.");
        }

        public async Task<BlobResultDto> UpdateUserAsync(UpdateUserDto dto)
        {
            try
            {
                return await Channel.UpdateUserAsync(dto).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
            return new BlobResultDto("Client proxy error.");
        }

        public async Task<BlobResultDto> CreateCustomerGroupAsync(CreateCustomerGroupDto dto)
        {
            try
            {
                return await Channel.CreateCustomerGroupAsync(dto).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
            return new BlobResultDto("Client proxy error.");
        }

        public async Task<BlobResultDto> DeleteCustomerGroupAsync(DeleteCustomerGroupDto dto)
        {
            try
            {
                return await Channel.DeleteCustomerGroupAsync(dto).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
            return new BlobResultDto("Client proxy error.");
        }

        public async Task<BlobResultDto> UpdateCustomerGroupAsync(UpdateCustomerGroupDto dto)
        {
            try
            {
                return await Channel.UpdateCustomerGroupAsync(dto).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
            return new BlobResultDto("Client proxy error.");
        }

        public async Task<BlobResultDto> AddRoleToCustomerGroupAsync(AddRoleToCustomerGroupDto dto)
        {
            try
            {
                return await Channel.AddRoleToCustomerGroupAsync(dto).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
            return new BlobResultDto("Client proxy error.");
        }

        public async Task<BlobResultDto> AddUserToCustomerGroupAsync(AddUserToCustomerGroupDto dto)
        {
            try
            {
                return await Channel.AddUserToCustomerGroupAsync(dto).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
            return new BlobResultDto("Client proxy error.");
        }

        public async Task<BlobResultDto> RemoveRoleFromCustomerGroupAsync(RemoveRoleFromCustomerGroupDto dto)
        {
            try
            {
                return await Channel.RemoveRoleFromCustomerGroupAsync(dto).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
            return new BlobResultDto("Client proxy error.");
        }

        public async Task<BlobResultDto> RemoveUserFromCustomerGroupAsync(RemoveUserFromCustomerGroupDto dto)
        {
            try
            {
                return await Channel.RemoveUserFromCustomerGroupAsync(dto).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
            return new BlobResultDto("Client proxy error.");
        }
    }
}
