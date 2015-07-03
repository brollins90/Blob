namespace Blob.Proxies
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Contracts.ViewModel;
    using Contracts.ServiceContracts;

    public class BeforeQueryClient : BaseClient<IBlobQueryManager>, IBlobQueryManager
    {
        public BeforeQueryClient(string endpointName, string username, string password) : base(endpointName, username, password) { }

        public async Task<DashDevicesLargeViewModel> GetDashDevicesLargeVmAsync(Guid customerId, int pageNum, int pageSize)
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

        public async Task<DashCurrentConnectionsLargeViewModel> GetDashCurrentConnectionsLargeVmAsync(Guid searchId, int pageNum, int pageSize)
        {
            try
            {
                return await Channel.GetDashCurrentConnectionsLargeVmAsync(searchId, pageNum, pageSize).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
            return null;
        }

        public async Task<CustomerDisableViewModel> GetCustomerDisableVmAsync(Guid customerId)
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

        public async Task<CustomerEnableViewModel> GetCustomerEnableVmAsync(Guid customerId)
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

        public async Task<CustomerSingleViewModel> GetCustomerSingleVmAsync(Guid customerId)
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

        public async Task<CustomerUpdateViewModel> GetCustomerUpdateVmAsync(Guid customerId)
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

        public IEnumerable<DeviceCommandViewModel> GetDeviceCommandVmList()
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

        public DeviceCommandIssueViewModel GetDeviceCommandIssueVm(Guid deviceId, string commandType)
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

        public async Task<DeviceDisableViewModel> GetDeviceDisableVmAsync(Guid deviceId)
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

        public async Task<DeviceEnableViewModel> GetDeviceEnableVmAsync(Guid deviceId)
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

        public async Task<DevicePageViewModel> GetDevicePageVmAsync(Guid customerId, int pageNum, int pageSize)
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

        public async Task<DeviceSingleViewModel> GetDeviceSingleVmAsync(Guid deviceId)
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

        public async Task<DeviceUpdateViewModel> GetDeviceUpdateVmAsync(Guid deviceId)
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

        public async Task<PerformanceRecordDeleteViewModel> GetPerformanceRecordDeleteVmAsync(long recordId)
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

        public async Task<PerformanceRecordPageViewModel> GetPerformanceRecordPageVmAsync(Guid deviceId, int pageNum, int pageSize)
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

        public async Task<PerformanceRecordPageViewModel> GetPerformanceRecordPageVmForStatusAsync(long recordId, int pageNum, int pageSize)
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

        public async Task<PerformanceRecordSingleViewModel> GetPerformanceRecordSingleVmAsync(long recordId)
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

        public async Task<StatusRecordDeleteViewModel> GetStatusRecordDeleteVmAsync(long recordId)
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

        public async Task<StatusRecordPageViewModel> GetStatusRecordPageVmAsync(Guid deviceId, int pageNum, int pageSize)
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

        public async Task<StatusRecordSingleViewModel> GetStatusRecordSingleVmAsync(long recordId)
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

        public async Task<UserDisableViewModel> GetUserDisableVmAsync(Guid userId)
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

        public async Task<UserEnableViewModel> GetUserEnableVmAsync(Guid userId)
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

        public async Task<UserPageViewModel> GetUserPageVmAsync(Guid customerId, int pageNum, int pageSize)
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

        public async Task<UserSingleViewModel> GetUserSingleVmAsync(Guid userId)
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

        public async Task<UserUpdatePasswordViewModel> GetUserUpdatePasswordVmAsync(Guid userId)
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

        public async Task<CustomerGroupDeleteViewModel> GetCustomerGroupDeleteVmAsync(Guid groupId)
        {
            try
            {
                return await Channel.GetCustomerGroupDeleteVmAsync(groupId).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
            return null;
        }

        public async Task<CustomerGroupSingleViewModel> GetCustomerGroupSingleVmAsync(Guid groupId)
        {
            try
            {
                return await Channel.GetCustomerGroupSingleVmAsync(groupId).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
            return null;
        }

        public async Task<CustomerGroupUpdateViewModel> GetCustomerGroupUpdateVmAsync(Guid groupId)
        {
            try
            {
                return await Channel.GetCustomerGroupUpdateVmAsync(groupId).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
            return null;
        }


        public async Task<IEnumerable<CustomerGroupRoleListItem>> GetCustomerRolesAsync(Guid customerId)
        {
            try
            {
                return await Channel.GetCustomerRolesAsync(customerId).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
            return null;
        }


        public async Task<CustomerGroupPageViewModel> GetCustomerGroupPageVmAsync(Guid groupId, int pageNum = 1, int pageSize = 10)
        {
            try
            {
                return await Channel.GetCustomerGroupPageVmAsync(groupId, pageNum, pageSize).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
            return null;
        }

        public async Task<CustomerGroupCreateViewModel> GetCustomerGroupCreateVmAsync(Guid customerId)
        {
            try
            {
                return await Channel.GetCustomerGroupCreateVmAsync(customerId).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
            return null;
        }

        public async Task<IEnumerable<CustomerGroupRoleListItem>> GetCustomerGroupRolesAsync(Guid groupId)
        {
            try
            {
                return await Channel.GetCustomerGroupRolesAsync(groupId).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
            return null;
        }

        public async Task<IEnumerable<CustomerGroupUserListItem>> GetCustomerGroupUsersAsync(Guid groupId)
        {
            try
            {
                return await Channel.GetCustomerGroupUsersAsync(groupId).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
            return null;
        }


        public async Task<MonitorListViewModel> GetMonitorListVmAsync(Guid deviceId)
        {
            try
            {
                return await Channel.GetMonitorListVmAsync(deviceId).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
            return null;
        }


        public async Task<CustomerPageViewModel> GetCustomerPageVmAsync(Guid searchId, int pageNum = 1, int pageSize = 10)
        {
            try
            {
                return await Channel.GetCustomerPageVmAsync(searchId, pageNum, pageSize).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
            return null;
        }
    }
}