﻿using System;
using System.Threading.Tasks;
using Blob.Contracts.Models;
using Blob.Contracts.ServiceContracts;

namespace Blob.Proxies
{
    public class BeforeCommandClient : BaseClient<IBlobCommandManager>, IBlobCommandManager
    {
        public BeforeCommandClient(string endpointName, string username, string password) : base(endpointName, username, password) { }

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

        public async Task<IdentityResultDto> RegisterCustomerAsync(RegisterCustomerDto dto)
        {
            try
            {
                return await Channel.RegisterCustomerAsync(dto).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
            return null;
        }

        //public async Task RemoveUserFromCustomersAsync(Guid userId)
        //{
        //    try
        //    {
        //        await Channel.EnableCustomerAsync(dto).ConfigureAwait(false);
        //    }
        //    catch (Exception ex)
        //    {
        //        HandleError(ex);
        //    }
        //}

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

        public async Task CreateUserAsync(CreateUserDto dto)
        {
            try
            {
                await Channel.CreateUserAsync(dto).ConfigureAwait(false);
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

        public async Task CreateCustomerGroupAsync(CreateCustomerGroupDto dto)
        {
            try
            {
                await Channel.CreateCustomerGroupAsync(dto).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
        }

        public async Task DeleteCustomerGroupAsync(DeleteCustomerGroupDto dto)
        {
            try
            {
                await Channel.DeleteCustomerGroupAsync(dto).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
        }

        public async Task UpdateCustomerGroupAsync(UpdateCustomerGroupDto dto)
        {
            try
            {
                await Channel.UpdateCustomerGroupAsync(dto).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
        }

        public async Task AddRoleToCustomerGroupAsync(AddRoleToCustomerGroupDto dto)
        {
            try
            {
                await Channel.AddRoleToCustomerGroupAsync(dto).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
        }

        public async Task AddUserToCustomerGroupAsync(AddUserToCustomerGroupDto dto)
        {
            try
            {
                await Channel.AddUserToCustomerGroupAsync(dto).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
        }

        public async Task RemoveRoleFromCustomerGroupAsync(RemoveRoleFromCustomerGroupDto dto)
        {
            try
            {
                await Channel.RemoveRoleFromCustomerGroupAsync(dto).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
        }

        public async Task RemoveUserFromCustomerGroupAsync(RemoveUserFromCustomerGroupDto dto)
        {
            try
            {
                await Channel.RemoveUserFromCustomerGroupAsync(dto).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
        }
    }
}
