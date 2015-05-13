﻿using System;
using System.Threading.Tasks;
using Blob.Contracts.Blob;
using Blob.Contracts.Dto.ViewModels;

namespace Blob.Proxies
{
    public class BeforeQueryClient : BaseClient<IBlobQueryManager>, IBlobQueryManager
    {
        public BeforeQueryClient(string endpointName, string username, string password) : base(endpointName, username, password) { }

        public async Task<CustomerDisableVm> GetCustomerDisableVmAsync(Guid customerId)
        {
            try
            {
                return await Channel.GetCustomerDisableVmAsync(customerId).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
            return null;
        }

        public async Task<CustomerEnableVm> GetCustomerEnableVmAsync(Guid customerId)
        {
            try
            {
                return await Channel.GetCustomerEnableVmAsync(customerId).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
            return null;
        }

        public async Task<CustomerSingleVm> GetCustomerSingleVmAsync(Guid customerId)
        {
            try
            {
                return await Channel.GetCustomerSingleVmAsync(customerId).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
            return null;
        }

        public async Task<CustomerUpdateVm> GetCustomerUpdateVmAsync(Guid customerId)
        {
            try
            {
                return await Channel.GetCustomerUpdateVmAsync(customerId).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
            return null;
        }

        public DeviceCommandIssueVm GetDeviceCommandIssueVm(Guid deviceId)
        {
            try
            {
                return Channel.GetDeviceCommandIssueVm(deviceId);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
            return null;
        }

        public async Task<DeviceDisableVm> GetDeviceDisableVmAsync(Guid deviceId)
        {
            try
            {
                return await Channel.GetDeviceDisableVmAsync(deviceId).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
            return null;
        }

        public async Task<DeviceEnableVm> GetDeviceEnableVmAsync(Guid deviceId)
        {
            try
            {
                return await Channel.GetDeviceEnableVmAsync(deviceId).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
            return null;
        }

        public async Task<DeviceSingleVm> GetDeviceSingleVmAsync(Guid deviceId)
        {
            try
            {
                return await Channel.GetDeviceSingleVmAsync(deviceId).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
            return null;
        }

        public async Task<DeviceUpdateVm> GetDeviceUpdateVmAsync(Guid deviceId)
        {
            try
            {
                return await Channel.GetDeviceUpdateVmAsync(deviceId).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
            return null;
        }

        public async Task<PerformanceRecordDeleteVm> GetPerformanceRecordDeleteVmAsync(long recordId)
        {
            try
            {
                return await Channel.GetPerformanceRecordDeleteVmAsync(recordId).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
            return null;
        }

        public async Task<PerformanceRecordSingleVm> GetPerformanceRecordSingleVmAsync(long recordId)
        {
            try
            {
                return await Channel.GetPerformanceRecordSingleVmAsync(recordId).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
            return null;
        }

        public async Task<StatusRecordDeleteVm> GetStatusRecordDeleteVmAsync(long recordId)
        {
            try
            {
                return await Channel.GetStatusRecordDeleteVmAsync(recordId).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
            return null;
        }

        public async Task<StatusRecordSingleVm> GetStatusRecordSingleVmAsync(long recordId)
        {
            try
            {
                return await Channel.GetStatusRecordSingleVmAsync(recordId).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
            return null;
        }

        public async Task<UserDisableVm> GetUserDisableVmAsync(Guid userId)
        {
            try
            {
                return await Channel.GetUserDisableVmAsync(userId).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
            return null;
        }

        public async Task<UserEnableVm> GetUserEnableVmAsync(Guid userId)
        {
            try
            {
                return await Channel.GetUserEnableVmAsync(userId).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
            return null;
        }

        public async Task<UserSingleVm> GetUserSingleVmAsync(Guid userId)
        {
            try
            {
                return await Channel.GetUserSingleVmAsync(userId).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
            return null;
        }

        public async Task<UserUpdateVm> GetUserUpdateVmAsync(Guid userId)
        {
            try
            {
                return await Channel.GetUserUpdateVmAsync(userId).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
            return null;
        }

        public async Task<UserUpdatePasswordVm> GetUserUpdatePasswordVmAsync(Guid userId)
        {
            try
            {
                return await Channel.GetUserUpdatePasswordVmAsync(userId).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
            return null;
        }
    }
}