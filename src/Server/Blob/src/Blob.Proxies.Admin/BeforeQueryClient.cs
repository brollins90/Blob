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

        public async Task<DashDevicesLargeViewModel> GetDashDevicesLargeViewModelAsync(Guid customerId, int pageNum, int pageSize)
        {
            try
            {
                return await Channel.GetDashDevicesLargeViewModelAsync(customerId, pageNum, pageSize).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
            return null;
        }

        public async Task<DashCurrentConnectionsLargeViewModel> GetDashCurrentConnectionsLargeViewModelAsync(Guid searchId, int pageNum, int pageSize)
        {
            try
            {
                return await Channel.GetDashCurrentConnectionsLargeViewModelAsync(searchId, pageNum, pageSize).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
            return null;
        }

        public async Task<CustomerDisableViewModel> GetCustomerDisableViewModelAsync(Guid customerId)
        {
            try
            {
                return await Channel.GetCustomerDisableViewModelAsync(customerId).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
            return null;
        }

        public async Task<CustomerEnableViewModel> GetCustomerEnableViewModelAsync(Guid customerId)
        {
            try
            {
                return await Channel.GetCustomerEnableViewModelAsync(customerId).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
            return null;
        }

        public async Task<CustomerSingleViewModel> GetCustomerSingleViewModelAsync(Guid customerId)
        {
            try
            {
                return await Channel.GetCustomerSingleViewModelAsync(customerId).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
            return null;
        }

        public async Task<CustomerUpdateViewModel> GetCustomerUpdateViewModelAsync(Guid customerId)
        {
            try
            {
                return await Channel.GetCustomerUpdateViewModelAsync(customerId).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
            return null;
        }

        public IEnumerable<DeviceCommandViewModel> GetDeviceCommandViewModelList()
        {
            try
            {
                return Channel.GetDeviceCommandViewModelList();
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
            return null;
        }

        public DeviceCommandIssueViewModel GetDeviceCommandIssueViewModel(Guid deviceId, string commandType)
        {
            try
            {
                return Channel.GetDeviceCommandIssueViewModel(deviceId, commandType);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
            return null;
        }

        public async Task<DeviceDisableViewModel> GetDeviceDisableViewModelAsync(Guid deviceId)
        {
            try
            {
                return await Channel.GetDeviceDisableViewModelAsync(deviceId).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
            return null;
        }

        public async Task<DeviceEnableViewModel> GetDeviceEnableViewModelAsync(Guid deviceId)
        {
            try
            {
                return await Channel.GetDeviceEnableViewModelAsync(deviceId).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
            return null;
        }

        public async Task<DevicePageViewModel> GetDevicePageViewModelAsync(Guid customerId, int pageNum, int pageSize)
        {
            try
            {
                return await Channel.GetDevicePageViewModelAsync(customerId, pageNum, pageSize).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
            return null;
        }

        public async Task<DeviceSingleViewModel> GetDeviceSingleViewModelAsync(Guid deviceId)
        {
            try
            {
                return await Channel.GetDeviceSingleViewModelAsync(deviceId).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
            return null;
        }

        public async Task<DeviceUpdateViewModel> GetDeviceUpdateViewModelAsync(Guid deviceId)
        {
            try
            {
                return await Channel.GetDeviceUpdateViewModelAsync(deviceId).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
            return null;
        }

        public async Task<PerformanceRecordDeleteViewModel> GetPerformanceRecordDeleteViewModelAsync(long recordId)
        {
            try
            {
                return await Channel.GetPerformanceRecordDeleteViewModelAsync(recordId).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
            return null;
        }

        public async Task<PerformanceRecordPageViewModel> GetPerformanceRecordPageViewModelAsync(Guid deviceId, int pageNum, int pageSize)
        {
            try
            {
                return await Channel.GetPerformanceRecordPageViewModelAsync(deviceId, pageNum, pageSize).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
            return null;
        }

        public async Task<PerformanceRecordPageViewModel> GetPerformanceRecordPageViewModelForStatusAsync(long recordId, int pageNum, int pageSize)
        {
            try
            {
                return await Channel.GetPerformanceRecordPageViewModelForStatusAsync(recordId, pageNum, pageSize).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
            return null;
        }

        public async Task<PerformanceRecordSingleViewModel> GetPerformanceRecordSingleViewModelAsync(long recordId)
        {
            try
            {
                return await Channel.GetPerformanceRecordSingleViewModelAsync(recordId).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
            return null;
        }

        public async Task<StatusRecordDeleteViewModel> GetStatusRecordDeleteViewModelAsync(long recordId)
        {
            try
            {
                return await Channel.GetStatusRecordDeleteViewModelAsync(recordId).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
            return null;
        }

        public async Task<StatusRecordPageViewModel> GetStatusRecordPageViewModelAsync(Guid deviceId, int pageNum, int pageSize)
        {
            try
            {
                return await Channel.GetStatusRecordPageViewModelAsync(deviceId, pageNum, pageSize).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
            return null;
        }

        public async Task<StatusRecordSingleViewModel> GetStatusRecordSingleViewModelAsync(long recordId)
        {
            try
            {
                return await Channel.GetStatusRecordSingleViewModelAsync(recordId).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
            return null;
        }

        public async Task<UserDisableViewModel> GetUserDisableViewModelAsync(Guid userId)
        {
            try
            {
                return await Channel.GetUserDisableViewModelAsync(userId).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
            return null;
        }

        public async Task<UserEnableViewModel> GetUserEnableViewModelAsync(Guid userId)
        {
            try
            {
                return await Channel.GetUserEnableViewModelAsync(userId).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
            return null;
        }

        public async Task<UserPageViewModel> GetUserPageViewModelAsync(Guid customerId, int pageNum, int pageSize)
        {
            try
            {
                return await Channel.GetUserPageViewModelAsync(customerId, pageNum, pageSize).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
            return null;
        }

        public async Task<UserSingleViewModel> GetUserSingleViewModelAsync(Guid userId)
        {
            try
            {
                return await Channel.GetUserSingleViewModelAsync(userId).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
            return null;
        }

        public async Task<UserUpdateViewModel> GetUserUpdateViewModelAsync(Guid userId)
        {
            try
            {
                return await Channel.GetUserUpdateViewModelAsync(userId).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
            return null;
        }

        public async Task<UserUpdatePasswordViewModel> GetUserUpdatePasswordViewModelAsync(Guid userId)
        {
            try
            {
                return await Channel.GetUserUpdatePasswordViewModelAsync(userId).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
            return null;
        }

        public async Task<CustomerGroupDeleteViewModel> GetCustomerGroupDeleteViewModelAsync(Guid groupId)
        {
            try
            {
                return await Channel.GetCustomerGroupDeleteViewModelAsync(groupId).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
            return null;
        }

        public async Task<CustomerGroupSingleViewModel> GetCustomerGroupSingleViewModelAsync(Guid groupId)
        {
            try
            {
                return await Channel.GetCustomerGroupSingleViewModelAsync(groupId).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
            return null;
        }

        public async Task<CustomerGroupUpdateViewModel> GetCustomerGroupUpdateViewModelAsync(Guid groupId)
        {
            try
            {
                return await Channel.GetCustomerGroupUpdateViewModelAsync(groupId).ConfigureAwait(false);
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


        public async Task<CustomerGroupPageViewModel> GetCustomerGroupPageViewModelAsync(Guid groupId, int pageNum = 1, int pageSize = 10)
        {
            try
            {
                return await Channel.GetCustomerGroupPageViewModelAsync(groupId, pageNum, pageSize).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
            return null;
        }

        public async Task<CustomerGroupCreateViewModel> GetCustomerGroupCreateViewModelAsync(Guid customerId)
        {
            try
            {
                return await Channel.GetCustomerGroupCreateViewModelAsync(customerId).ConfigureAwait(false);
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


        public async Task<MonitorListViewModel> GetMonitorListViewModelAsync(Guid deviceId)
        {
            try
            {
                return await Channel.GetMonitorListViewModelAsync(deviceId).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
            return null;
        }


        public async Task<CustomerPageViewModel> GetCustomerPageViewModelAsync(Guid searchId, int pageNum = 1, int pageSize = 10)
        {
            try
            {
                return await Channel.GetCustomerPageViewModelAsync(searchId, pageNum, pageSize).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
            return null;
        }
    }
}