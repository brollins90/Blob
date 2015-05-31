using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Blob.Contracts.Models.ViewModels;
using Blob.Contracts.ServiceContracts;
using Blob.Core.Services;
using EntityFramework.Extensions;
using log4net;

namespace Blob.Core.Blob
{
    public class BlobQueryManager : IBlobQueryManager
    {
        private readonly ILog _log;
        private readonly BlobCustomerManager _customerManager;
        private readonly BlobCustomerGroupManager _customerGroupManager;
        private readonly BlobDeviceManager _deviceManager;
        private readonly BlobDeviceCommandManager _deviceCommandManager;
        private readonly BlobStatusRecordManager _statusRecordManager;
        private readonly BlobPerformanceRecordManager _performanceRecordManager;

        public BlobQueryManager(ILog log, BlobDbContext context, BlobCustomerManager customerManager, BlobCustomerGroupManager customerGroupManager,
            BlobDeviceManager deviceManager, BlobDeviceCommandManager deviceCommandManager, BlobStatusRecordManager statusRecordManager,
            BlobPerformanceRecordManager performanceRecordManager)
        {
            _log = log;
            _log.Debug("Constructing BlobQueryManager");
            Context = context;
            _customerManager = customerManager;
            _customerGroupManager = customerGroupManager;
            _deviceManager = deviceManager;
            _deviceCommandManager = deviceCommandManager;
            _statusRecordManager = statusRecordManager;
            _performanceRecordManager = performanceRecordManager;
        }
        public BlobDbContext Context { get; private set; }


        // Dash
        public async Task<DashCurrentConnectionsLargeVm> GetDashCurrentConnectionsLargeVmAsync(Guid searchId, int pageNum = 1, int pageSize = 10)
        {
            var activeDeviceConnections = _deviceCommandManager.GetActiveDeviceIds().ToList();
            var pNum = pageNum < 1 ? 0 : pageNum - 1;

            //var cust = Context.Customers.Where(x => x.Id.Equals(customerId));
            var count = Context.Devices.Where(x => activeDeviceConnections.Contains(x.Id)).FutureCount();
            var devices = Context.Devices.Where(x => activeDeviceConnections.Contains(x.Id))
                .OrderByDescending(x => x.AlertLevel).ThenBy(x => x.DeviceName)
                .Skip(pNum * pageSize).Take(pageSize).Future();

            // define future queries before any of them execute
            var pCount = ((count / pageSize) + (count % pageSize) == 0 ? 0 : 1);
            return await Task.FromResult(new DashCurrentConnectionsLargeVm
            {
                TotalCount = count,
                PageCount = pCount,
                PageNum = pNum + 1,
                PageSize = pageSize,
                Items = devices.Select(x => new DashCurrentConnectionsListItemVm
                {
                    CustomerId = x.CustomerId,
                    DeviceId = x.Id,
                    DeviceName = x.DeviceName,
                    Status = _deviceManager.CalculateDeviceAlertLevel(x.Id)
                }),
            }).ConfigureAwait(false);
        }

        public async Task<DashDevicesLargeVm> GetDashDevicesLargeVmAsync(Guid searchId, int pageNum = 1, int pageSize = 10)
        {
            var activeDeviceConnections = _deviceCommandManager.GetActiveDeviceIds().ToList();
            IEnumerable<DeviceCommandVm> availableCommands = GetDeviceCommandVmList();
            var pNum = pageNum < 1 ? 0 : pageNum - 1;

            //var cust = Context.Customers.Where(x => x.Id.Equals(customerId));
            var count = Context.Devices.Where(x => x.CustomerId.Equals(searchId)).FutureCount();
            var devices = Context.Devices
                .Where(x => x.CustomerId.Equals(searchId))
                .OrderByDescending(x => x.AlertLevel).ThenBy(x => x.DeviceName)
                .Skip(pNum * pageSize).Take(pageSize).Future();

            // define future queries before any of them execute
            var pCount = ((count / pageSize) + (count % pageSize) == 0 ? 0 : 1);

            List<DashDevicesLargeListItemVm> dItems = new List<DashDevicesLargeListItemVm>();
            foreach (var x in devices)
            {
                var deviceRecent = await _statusRecordManager.GetDeviceRecentStatusAsync(x.Id);

                var li = new DashDevicesLargeListItemVm
                         {
                             AvailableCommands = (activeDeviceConnections.Contains(x.Id)) ? availableCommands : new List<DeviceCommandVm>(),
                             DeviceId = x.Id,
                             DeviceName = x.DeviceName,
                             Reason = ChangeStatusRecordsToReasonString(deviceRecent),
                             Recomendations = new string[] { "not yet" },
                             Status = ChangeStatusRecordsToStatusInt(deviceRecent)
                         };
                dItems.Add(li);
            }
            return await Task.FromResult(new DashDevicesLargeVm
            {
                TotalCount = count,
                PageCount = pCount,
                PageNum = pNum + 1,
                PageSize = pageSize,
                Items = dItems
            });

        }

        private string ChangeStatusRecordsToReasonString(IList<StatusRecordListItemVm> records)
        {
            return records.Where(s => s.Status != 0).Aggregate<StatusRecordListItemVm, string>(null, (accum, r) => accum + (accum == null ? accum : ", ") + r.CurrentValue);
        }

        private int ChangeStatusRecordsToStatusInt(IList<StatusRecordListItemVm> records)
        {
            return records.Select(statusRecord => statusRecord.Status).Concat(new[] { 0 }).Max();
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


        // User

        public async Task<UserDisableVm> GetUserDisableVmAsync(Guid userId)
        {
            return await (from user in Context.Users
                          where user.Id == userId
                          select new UserDisableVm
                          {
                              Email = user.Email,
                              Enabled = user.Enabled,
                              UserId = user.Id,
                              UserName = user.UserName
                          }).SingleAsync().ConfigureAwait(false);
        }

        public async Task<UserEnableVm> GetUserEnableVmAsync(Guid userId)
        {
            return await (from user in Context.Users
                          where user.Id == userId
                          select new UserEnableVm
                          {
                              Email = user.Email,
                              Enabled = user.Enabled,
                              UserId = user.Id,
                              UserName = user.UserName
                          }).SingleAsync().ConfigureAwait(false);
        }

        public async Task<UserPageVm> GetUserPageVmAsync(Guid customerId, int pageNum = 1, int pageSize = 10)
        {
            var pNum = pageNum < 1 ? 0 : pageNum - 1;

            var count = Context.Users.Where(x => x.CustomerId.Equals(customerId)).FutureCount();
            var devices = Context.Users
                .Where(x => x.CustomerId.Equals(customerId))
                .OrderBy(x => x.UserName).ThenBy(x => x.Email)
                .Skip(pNum * pageSize).Take(pageSize).Future();

            // define future queries before any of them execute
            var pCount = ((count / pageSize) + (count % pageSize) == 0 ? 0 : 1);
            return await Task.FromResult(new UserPageVm
            {
                TotalCount = count,
                PageCount = pCount,
                PageNum = pNum + 1,
                PageSize = pageSize,
                Items = devices.Select(x => new UserListItemVm
                {
                    Email = x.Email,
                    Enabled = x.Enabled,
                    UserId = x.Id,
                    UserName = x.UserName
                }),
            }).ConfigureAwait(false);
        }

        public async Task<UserSingleVm> GetUserSingleVmAsync(Guid userId)
        {
            // NotificationSchedule
            return await (from user in Context.Users.Include("Customers")
                          join p in Context.UserProfiles on user.Id equals p.UserId
                          where user.Id == userId
                          select new UserSingleVm
                          {
                              CreateDate = user.CreateDateUtc,
                              CustomerName = user.Customer.Name,
                              Email = user.Email,
                              EmailConfirmed = user.EmailConfirmed,
                              EmailNotificationsEnabled = p.SendEmailNotifications,
                              Enabled = user.Enabled,
                              HasPassword = (user.PasswordHash != null),
                              HasSecurityStamp = (user.SecurityStamp != null),
                              LastActivityDate = user.LastActivityDate,
                              UserId = user.Id,
                              UserName = user.UserName,
                              NotificationSchedule = new NotificationScheduleListItemVm { Name = p.EmailNotificationSchedule.Name, ScheduleId = p.EmailNotificationSchedule.Id },
                          }).SingleAsync().ConfigureAwait(false);
        }

        public async Task<IEnumerable<NotificationScheduleListItemVm>> GetAllNotificationSchedules()
        {
            return await (from x in Context.NotificationSchedules
                          select new NotificationScheduleListItemVm
                          {
                              ScheduleId = x.Id,
                              Name = x.Name
                          }).ToListAsync().ConfigureAwait(false);
            //return new List<NotificationScheduleListItemVm> { new NotificationScheduleListItemVm { ScheduleId = Guid.Parse("76AA040A-253C-4AD3-838F-ADE186F40F47"), Name = "FirstSchedule" } };
        }

        public async Task<UserUpdateVm> GetUserUpdateVmAsync(Guid userId)
        {
            var availableSchedules = await GetAllNotificationSchedules();
            var u = await (from p in Context.UserProfiles.Include("User")
                           where p.UserId == userId
                           select new UserUpdateVm
                           {
                               UserId = p.User.Id,
                               Email = p.User.Email,
                               UserName = p.User.UserName,
                               ScheduleId = p.EmailNotificationScheduleId
                           }).SingleAsync().ConfigureAwait(false);
            u.AvailableSchedules = availableSchedules;
            return u;
        }

        public async Task<UserUpdatePasswordVm> GetUserUpdatePasswordVmAsync(Guid userId)
        {
            return await (from user in Context.Users
                          where user.Id == userId
                          select new UserUpdatePasswordVm
                          {
                              UserId = user.Id
                          }).SingleAsync().ConfigureAwait(false);
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
        #endregion



    }
}
