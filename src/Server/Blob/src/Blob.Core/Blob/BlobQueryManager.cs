namespace Blob.Core.Blob
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Contracts.ViewModel;
    using Contracts.ServiceContracts;
    using Services;
    using log4net;

    public class BlobQueryManager : IBlobQueryManager
    {
        private readonly ILog _log;
        private readonly BlobCustomerManager _customerManager;
        private readonly BlobCustomerGroupManager _customerGroupManager;
        private readonly BlobDashboardManager _blobDashboardManager;
        private readonly BlobDeviceManager _deviceManager;
        private readonly BlobDeviceCommandManager _deviceCommandManager;
        private readonly BlobPerformanceRecordManager _performanceRecordManager;
        private readonly BlobStatusRecordManager _statusRecordManager;
        private readonly BlobUserManager2 _userManager2;

        public BlobQueryManager(
            ILog log,
            BlobCustomerManager customerManager,
            BlobCustomerGroupManager customerGroupManager,
            BlobDashboardManager blobDashboardManager,
            BlobDeviceManager deviceManager,
            BlobDeviceCommandManager deviceCommandManager,
            BlobPerformanceRecordManager performanceRecordManager,
            BlobStatusRecordManager statusRecordManager,
            BlobUserManager2 userManager2)
        {
            _log = log;
            _log.Debug("Constructing BlobQueryManager");
            _customerManager = customerManager;
            _customerGroupManager = customerGroupManager;
            _blobDashboardManager = blobDashboardManager;
            _deviceManager = deviceManager;
            _deviceCommandManager = deviceCommandManager;
            _statusRecordManager = statusRecordManager;
            _performanceRecordManager = performanceRecordManager;
            _userManager2 = userManager2;
        }

        // Dash
        public async Task<DashCurrentConnectionsLargeViewModel> GetDashCurrentConnectionsLargeVmAsync(Guid searchId, int pageNum = 1, int pageSize = 10)
        {
            return await _blobDashboardManager.GetDashCurrentConnectionsLargeViewModelAsync(searchId, pageNum, pageSize);
        }

        public async Task<DashDevicesLargeViewModel> GetDashDevicesLargeVmAsync(Guid searchId, int pageNum = 1, int pageSize = 10)
        {
            return await _blobDashboardManager.GetDashDevicesLargeViewModelAsync(searchId, pageNum, pageSize);
        }

        public async Task<UserUpdatePasswordViewModel> GetUserUpdatePasswordVmAsync(Guid userId)
        {
            return await _userManager2.GetUserUpdatePasswordViewModelAsync(userId);
        }




        #region DeviceCommand
        public IEnumerable<DeviceCommandViewModel> GetDeviceCommandVmList()
        {
            return _deviceCommandManager.GetDeviceCommandViewModelList();
        }

        public DeviceCommandIssueViewModel GetDeviceCommandIssueVm(Guid deviceId, string commandType)
        {
            return _deviceCommandManager.GetDeviceCommandIssueViewModel(deviceId, commandType);
        }
        #endregion


        // Performance
        public async Task<PerformanceRecordDeleteViewModel> GetPerformanceRecordDeleteVmAsync(long recordId)
        {
            return await _performanceRecordManager.GetPerformanceRecordDeleteViewModelAsync(recordId).ConfigureAwait(false);
        }

        public async Task<PerformanceRecordPageViewModel> GetPerformanceRecordPageVmAsync(Guid deviceId, int pageNum = 1, int pageSize = 10)
        {
            return await _performanceRecordManager.GetPerformanceRecordPageViewModelAsync(deviceId, pageNum, pageSize).ConfigureAwait(false);
        }

        public async Task<PerformanceRecordPageViewModel> GetPerformanceRecordPageVmForStatusAsync(long recordId, int pageNum = 1, int pageSize = 10)
        {
            return await _performanceRecordManager.GetPerformanceRecordPageViewModelForStatusAsync(recordId, pageNum, pageSize).ConfigureAwait(false);
        }

        public async Task<PerformanceRecordSingleViewModel> GetPerformanceRecordSingleVmAsync(long recordId)
        {
            return await _performanceRecordManager.GetPerformanceRecordSingleViewModelAsync(recordId).ConfigureAwait(false);
        }

        // Status
        public async Task<StatusRecordDeleteViewModel> GetStatusRecordDeleteVmAsync(long recordId)
        {
            return await _statusRecordManager.GetStatusRecordDeleteViewModelAsync(recordId).ConfigureAwait(false);
        }

        public async Task<StatusRecordPageViewModel> GetStatusRecordPageVmAsync(Guid deviceId, int pageNum = 1, int pageSize = 10)
        {
            return await _statusRecordManager.GetStatusRecordPageViewModelAsync(deviceId, pageNum, pageSize).ConfigureAwait(false);
        }

        public async Task<StatusRecordSingleViewModel> GetStatusRecordSingleVmAsync(long recordId)
        {
            return await _statusRecordManager.GetStatusRecordSingleViewModelAsync(recordId).ConfigureAwait(false);
        }
        public async Task<MonitorListViewModel> GetMonitorListVmAsync(Guid deviceId)
        {
            return await _statusRecordManager.GetMonitorListViewModelAsync(deviceId).ConfigureAwait(false);
        }


        // User
        public async Task<UserDisableViewModel> GetUserDisableVmAsync(Guid userId)
        {
            return await _userManager2.GetUserDisableViewModelAsync(userId);
        }

        public async Task<UserEnableViewModel> GetUserEnableVmAsync(Guid userId)
        {
            return await _userManager2.GetUserEnableViewModelAsync(userId);
        }

        public async Task<UserPageViewModel> GetUserPageVmAsync(Guid customerId, int pageNum = 1, int pageSize = 10)
        {
            return await _userManager2.GetUserPageViewModelAsync(customerId, pageNum, pageSize);
        }

        public async Task<UserSingleViewModel> GetUserSingleVmAsync(Guid userId)
        {
            return await _userManager2.GetUserSingleViewModelAsync(userId);
        }

        public async Task<UserUpdateVm> GetUserUpdateVmAsync(Guid userId)
        {
            return await _userManager2.GetUserUpdateViewModelAsync(userId);
        }

        #region Device

        public async Task<DeviceDisableViewModel> GetDeviceDisableVmAsync(Guid deviceId)
        {
            return await _deviceManager.GetDeviceDisableViewModelAsync(deviceId).ConfigureAwait(false);
        }

        public async Task<DeviceEnableViewModel> GetDeviceEnableVmAsync(Guid deviceId)
        {
            return await _deviceManager.GetDeviceEnableViewModelAsync(deviceId).ConfigureAwait(false);
        }

        public async Task<DevicePageViewModel> GetDevicePageVmAsync(Guid customerId, int pageNum = 1, int pageSize = 10)
        {
            return await _deviceManager.GetDevicePageViewModelAsync(customerId, pageNum, pageSize).ConfigureAwait(false);
        }

        public async Task<DeviceSingleViewModel> GetDeviceSingleVmAsync(Guid deviceId)
        {
            return await _deviceManager.GetDeviceSingleViewModelAsync(deviceId).ConfigureAwait(false);
        }

        public async Task<DeviceUpdateViewModel> GetDeviceUpdateVmAsync(Guid deviceId)
        {
            return await _deviceManager.GetDeviceUpdateViewModelAsync(deviceId).ConfigureAwait(false);
        }

        #endregion

        #region Customer
        public async Task<CustomerDisableViewModel> GetCustomerDisableVmAsync(Guid customerId)
        {
            return await _customerManager.GetCustomerDisableViewModelAsync(customerId).ConfigureAwait(false);
        }

        public async Task<CustomerEnableViewModel> GetCustomerEnableVmAsync(Guid customerId)
        {
            return await _customerManager.GetCustomerEnableViewModelAsync(customerId).ConfigureAwait(false);
        }

        public async Task<CustomerSingleViewModel> GetCustomerSingleVmAsync(Guid customerId)
        {
            return await _customerManager.GetCustomerSingleViewModelAsync(customerId).ConfigureAwait(false);
        }

        public async Task<CustomerUpdateViewModel> GetCustomerUpdateVmAsync(Guid customerId)
        {
            return await _customerManager.GetCustomerUpdateViewModelAsync(customerId).ConfigureAwait(false);
        }
        #endregion

        #region Customer Group

        public async Task<CustomerGroupCreateViewModel> GetCustomerGroupCreateVmAsync(Guid customerId)
        {
            return await _customerGroupManager.GetCustomerGroupCreateViewModelAsync(customerId).ConfigureAwait(false);
        }

        public async Task<CustomerGroupDeleteViewModel> GetCustomerGroupDeleteVmAsync(Guid groupId)
        {
            return await _customerGroupManager.GetCustomerGroupDeleteViewModelAsync(groupId).ConfigureAwait(false);
        }

        public async Task<CustomerGroupSingleViewModel> GetCustomerGroupSingleVmAsync(Guid groupId)
        {
            return await _customerGroupManager.GetCustomerGroupSingleViewModelAsync(groupId).ConfigureAwait(false);
        }

        public async Task<CustomerGroupUpdateViewModel> GetCustomerGroupUpdateVmAsync(Guid groupId)
        {
            return await _customerGroupManager.GetCustomerGroupUpdateViewModelAsync(groupId).ConfigureAwait(false);
        }

        public async Task<IEnumerable<CustomerGroupRoleListItem>> GetCustomerRolesAsync(Guid customerId)
        {
            return await _customerGroupManager.GetCustomerRolesAsync(customerId).ConfigureAwait(false);
        }

        public async Task<CustomerGroupPageViewModel> GetCustomerGroupPageVmAsync(Guid customerId, int pageNum = 1, int pageSize = 10)
        {
            return await _customerGroupManager.GetCustomerGroupPageViewModelAsync(customerId, pageNum, pageSize).ConfigureAwait(false);
        }

        public async Task<IEnumerable<CustomerGroupRoleListItem>> GetCustomerGroupRolesAsync(Guid groupId)
        {
            return await _customerGroupManager.GetCustomerGroupRolesAsync(groupId).ConfigureAwait(false);
        }

        public async Task<IEnumerable<CustomerGroupUserListItem>> GetCustomerGroupUsersAsync(Guid groupId)
        {
            return await _customerGroupManager.GetCustomerGroupUsersAsync(groupId).ConfigureAwait(false);
        }

        public async Task<CustomerPageViewModel> GetCustomerPageVmAsync(Guid searchId, int pageNum = 1, int pageSize = 10)
        {
            return await _customerManager.GetCustomerPageViewModelAsync(searchId, pageNum, pageSize).ConfigureAwait(false);
        }
        #endregion
    }
}