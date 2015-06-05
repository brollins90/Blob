using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Blob.Contracts.Models.ViewModels;
using Blob.Contracts.ServiceContracts;
using Blob.Core.Services;
using log4net;

namespace Blob.Core.Blob
{
    public class BlobQueryManager : IBlobQueryManager
    {
        private readonly ILog _log;
        private readonly BlobDbContext _context;
        private readonly BlobCustomerManager _customerManager;
        private readonly BlobCustomerGroupManager _customerGroupManager;
        private readonly BlobDashboardManager _blobDashboardManager;
        private readonly BlobDeviceManager _deviceManager;
        private readonly BlobDeviceCommandManager _deviceCommandManager;
        private readonly BlobPerformanceRecordManager _performanceRecordManager;
        private readonly BlobStatusRecordManager _statusRecordManager;
        private readonly BlobUserManager2 _userManager;

        public BlobQueryManager(
            ILog log,
            BlobDbContext context,
            BlobCustomerManager customerManager,
            BlobCustomerGroupManager customerGroupManager,
            BlobDashboardManager blobDashboardManager,
            BlobDeviceManager deviceManager,
            BlobDeviceCommandManager deviceCommandManager,
            BlobPerformanceRecordManager performanceRecordManager,
            BlobStatusRecordManager statusRecordManager,
            BlobUserManager2 userManager)
        {
            _log = log;
            _log.Debug("Constructing BlobQueryManager");
            _context = context;
            _customerManager = customerManager;
            _customerGroupManager = customerGroupManager;
            _blobDashboardManager = blobDashboardManager;
            _deviceManager = deviceManager;
            _deviceCommandManager = deviceCommandManager;
            _statusRecordManager = statusRecordManager;
            _performanceRecordManager = performanceRecordManager;
            _userManager = userManager;
        }

        // Dash
        public async Task<DashCurrentConnectionsLargeVm> GetDashCurrentConnectionsLargeVmAsync(Guid searchId, int pageNum = 1, int pageSize = 10)
        {
            return await _blobDashboardManager.GetDashCurrentConnectionsLargeVmAsync(searchId, pageNum, pageSize);
        }

        public async Task<DashDevicesLargeVm> GetDashDevicesLargeVmAsync(Guid searchId, int pageNum = 1, int pageSize = 10)
        {
            return await _blobDashboardManager.GetDashDevicesLargeVmAsync(searchId, pageNum, pageSize);
        }

        public async Task<UserUpdatePasswordVm> GetUserUpdatePasswordVmAsync(Guid userId)
        {
            return await (from user in _context.Users
                          where user.Id == userId
                          select new UserUpdatePasswordVm
                          {
                              UserId = user.Id
                          }).SingleAsync().ConfigureAwait(false);
        }




        #region DeviceCommand
        public IEnumerable<DeviceCommandVm> GetDeviceCommandVmList()
        {
            return _deviceCommandManager.GetDeviceCommandVmList();
        }

        public DeviceCommandIssueVm GetDeviceCommandIssueVm(Guid deviceId, string commandType)
        {
            return _deviceCommandManager.GetDeviceCommandIssueVm(deviceId, commandType);
        }
        #endregion


        // Performance
        public async Task<PerformanceRecordDeleteVm> GetPerformanceRecordDeleteVmAsync(long recordId)
        {
            return await _performanceRecordManager.GetPerformanceRecordDeleteVmAsync(recordId).ConfigureAwait(false);
        }

        public async Task<PerformanceRecordPageVm> GetPerformanceRecordPageVmAsync(Guid deviceId, int pageNum = 1, int pageSize = 10)
        {
            return await _performanceRecordManager.GetPerformanceRecordPageVmAsync(deviceId, pageNum, pageSize).ConfigureAwait(false);
        }

        public async Task<PerformanceRecordPageVm> GetPerformanceRecordPageVmForStatusAsync(long recordId, int pageNum = 1, int pageSize = 10)
        {
            return await _performanceRecordManager.GetPerformanceRecordPageVmForStatusAsync(recordId, pageNum, pageSize).ConfigureAwait(false);
        }

        public async Task<PerformanceRecordSingleVm> GetPerformanceRecordSingleVmAsync(long recordId)
        {
            return await _performanceRecordManager.GetPerformanceRecordSingleVmAsync(recordId).ConfigureAwait(false);
        }

        // Status
        public async Task<StatusRecordDeleteVm> GetStatusRecordDeleteVmAsync(long recordId)
        {
            return await _statusRecordManager.GetStatusRecordDeleteVmAsync(recordId).ConfigureAwait(false);
        }

        public async Task<StatusRecordPageVm> GetStatusRecordPageVmAsync(Guid deviceId, int pageNum = 1, int pageSize = 10)
        {
            return await _statusRecordManager.GetStatusRecordPageVmAsync(deviceId, pageNum, pageSize).ConfigureAwait(false);
        }

        public async Task<StatusRecordSingleVm> GetStatusRecordSingleVmAsync(long recordId)
        {
            return await _statusRecordManager.GetStatusRecordSingleVmAsync(recordId).ConfigureAwait(false);
        }
        public async Task<MonitorListVm> GetMonitorListVmAsync(Guid deviceId)
        {
            return await _statusRecordManager.GetMonitorListVmAsync(deviceId).ConfigureAwait(false);
        }


        // User
        public async Task<UserDisableVm> GetUserDisableVmAsync(Guid userId)
        {
            return await _userManager.GetUserDisableVmAsync(userId);
        }

        public async Task<UserEnableVm> GetUserEnableVmAsync(Guid userId)
        {
            return await _userManager.GetUserEnableVmAsync(userId);
        }

        public async Task<UserPageVm> GetUserPageVmAsync(Guid customerId, int pageNum = 1, int pageSize = 10)
        {
            return await _userManager.GetUserPageVmAsync(customerId, pageNum, pageSize);
        }

        public async Task<UserSingleVm> GetUserSingleVmAsync(Guid userId)
        {
            return await _userManager.GetUserSingleVmAsync(userId);
        }

        public async Task<UserUpdateVm> GetUserUpdateVmAsync(Guid userId)
        {
            return await _userManager.GetUserUpdateVmAsync(userId);
        }
        
        #region Device

        public async Task<DeviceDisableVm> GetDeviceDisableVmAsync(Guid deviceId)
        {
            return await _deviceManager.GetDeviceDisableVmAsync(deviceId).ConfigureAwait(false);
        }

        public async Task<DeviceEnableVm> GetDeviceEnableVmAsync(Guid deviceId)
        {
            return await _deviceManager.GetDeviceEnableVmAsync(deviceId).ConfigureAwait(false);
        }

        public async Task<DevicePageVm> GetDevicePageVmAsync(Guid customerId, int pageNum = 1, int pageSize = 10)
        {
            return await _deviceManager.GetDevicePageVmAsync(customerId, pageNum, pageSize).ConfigureAwait(false);
        }
        
        public async Task<DeviceSingleVm> GetDeviceSingleVmAsync(Guid deviceId)
        {
            return await _deviceManager.GetDeviceSingleVmAsync(deviceId).ConfigureAwait(false);
        }
        
        public async Task<DeviceUpdateVm> GetDeviceUpdateVmAsync(Guid deviceId)
        {
            return await _deviceManager.GetDeviceUpdateVmAsync(deviceId).ConfigureAwait(false);
        }

        #endregion

        #region Customer
        public async Task<CustomerDisableVm> GetCustomerDisableVmAsync(Guid customerId)
        {
            return await _customerManager.GetCustomerDisableVmAsync(customerId).ConfigureAwait(false);
        }

        public async Task<CustomerEnableVm> GetCustomerEnableVmAsync(Guid customerId)
        {
            return await _customerManager.GetCustomerEnableVmAsync(customerId).ConfigureAwait(false);
        }

        public async Task<CustomerSingleVm> GetCustomerSingleVmAsync(Guid customerId)
        {
            return await _customerManager.GetCustomerSingleVmAsync(customerId).ConfigureAwait(false);
        }

        public async Task<CustomerUpdateVm> GetCustomerUpdateVmAsync(Guid customerId)
        {
            return await _customerManager.GetCustomerUpdateVmAsync(customerId).ConfigureAwait(false);
        }
        #endregion

        #region Customer Group

        public async Task<CustomerGroupCreateVm> GetCustomerGroupCreateVmAsync(Guid customerId)
        {
            return await _customerGroupManager.GetCustomerGroupCreateVmAsync(customerId).ConfigureAwait(false);
        }

        public async Task<CustomerGroupDeleteVm> GetCustomerGroupDeleteVmAsync(Guid groupId)
        {
            return await _customerGroupManager.GetCustomerGroupDeleteVmAsync(groupId).ConfigureAwait(false); 
        }

        public async Task<CustomerGroupSingleVm> GetCustomerGroupSingleVmAsync(Guid groupId)
        {
            return await _customerGroupManager.GetCustomerGroupSingleVmAsync(groupId).ConfigureAwait(false);
        }

        public async Task<CustomerGroupUpdateVm> GetCustomerGroupUpdateVmAsync(Guid groupId)
        {
            return await _customerGroupManager.GetCustomerGroupUpdateVmAsync(groupId).ConfigureAwait(false);
        }

        public async Task<IEnumerable<CustomerGroupRoleListItem>> GetCustomerRolesAsync(Guid customerId)
        {
            return await _customerGroupManager.GetCustomerRolesAsync(customerId).ConfigureAwait(false);
        }

        public async Task<CustomerGroupPageVm> GetCustomerGroupPageVmAsync(Guid customerId, int pageNum = 1, int pageSize = 10)
        {
            return await _customerGroupManager.GetCustomerGroupPageVmAsync(customerId, pageNum, pageSize).ConfigureAwait(false);
        }

        public async Task<IEnumerable<CustomerGroupRoleListItem>> GetCustomerGroupRolesAsync(Guid groupId)
        {
            return await _customerGroupManager.GetCustomerGroupRolesAsync(groupId).ConfigureAwait(false);
        }

        public async Task<IEnumerable<CustomerGroupUserListItem>> GetCustomerGroupUsersAsync(Guid groupId)
        {
            return await _customerGroupManager.GetCustomerGroupUsersAsync(groupId).ConfigureAwait(false);
        }

        public async Task<CustomerPageVm> GetCustomerPageVmAsync(Guid searchId, int pageNum = 1, int pageSize = 10)
        {
            return await _customerManager.GetCustomerPageVmAsync(searchId, pageNum, pageSize).ConfigureAwait(false);
        }
        #endregion
    }
}
