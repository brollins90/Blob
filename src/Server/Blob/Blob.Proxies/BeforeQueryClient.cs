using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Blob.Contracts.Models.ViewModels;
using Blob.Contracts.ServiceContracts;

namespace Blob.Proxies
{
    public class BeforeQueryClient : BaseClient<IBlobQueryManager>, IBlobQueryManager
    {
        public BeforeQueryClient(string endpointName, string username, string password) : base(endpointName, username, password) { }

        public async Task<DashDevicesLargeVm> GetDashDevicesLargeVmAsync(Guid customerId, int pageNum, int pageSize)
        {
            try
            {
                return await Channel.GetDashDevicesLargeVmAsync(customerId, pageNum, pageSize).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
            return null;
        }

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

        public IEnumerable<DeviceCommandVm> GetDeviceCommandVmList()
        {
            try
            {
                return Channel.GetDeviceCommandVmList();
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
            return null;
        }

        public DeviceCommandIssueVm GetDeviceCommandIssueVm(Guid deviceId, string commandType)
        {
            try
            {
                return Channel.GetDeviceCommandIssueVm(deviceId, commandType);
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

        public async Task<DevicePageVm> GetDevicePageVmAsync(Guid customerId, int pageNum, int pageSize)
        {
            try
            {
                return await Channel.GetDevicePageVmAsync(customerId, pageNum, pageSize).ConfigureAwait(false);
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

        public async Task<PerformanceRecordPageVm> GetPerformanceRecordPageVmAsync(Guid deviceId, int pageNum, int pageSize)
        {
            try
            {
                return await Channel.GetPerformanceRecordPageVmAsync(deviceId, pageNum, pageSize).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
            return null;
        }

        public async Task<PerformanceRecordPageVm> GetPerformanceRecordPageVmForStatusAsync(long recordId, int pageNum, int pageSize)
        {
            try
            {
                return await Channel.GetPerformanceRecordPageVmForStatusAsync(recordId, pageNum, pageSize).ConfigureAwait(false);
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

        public async Task<StatusRecordPageVm> GetStatusRecordPageVmAsync(Guid deviceId, int pageNum, int pageSize)
        {
            try
            {
                return await Channel.GetStatusRecordPageVmAsync(deviceId, pageNum, pageSize).ConfigureAwait(false);
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

        public async Task<UserPageVm> GetUserPageVmAsync(Guid customerId, int pageNum, int pageSize)
        {
            try
            {
                return await Channel.GetUserPageVmAsync(customerId, pageNum, pageSize).ConfigureAwait(false);
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
