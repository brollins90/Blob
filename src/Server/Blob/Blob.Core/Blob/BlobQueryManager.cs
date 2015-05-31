﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Blob.Contracts.Models.ViewModels;
using Blob.Contracts.ServiceContracts;
using Blob.Core;
using Blob.Core.Models;
using Blob.Core.Command;
using Blob.Core.Identity;
using Blob.Core.Services;
using EntityFramework.Extensions;
using log4net;

namespace Blob.Core.Blob
{
    public class BlobQueryManager : IBlobQueryManager
    {
        private readonly ILog _log;
        private BlobCustomerManager _customerManager;
        private BlobCustomerGroupManager _customerGroupManager;
        private ICommandConnectionManager _connectionManager;

        public BlobQueryManager(BlobDbContext context, ILog log, BlobCustomerManager customerManager, BlobCustomerGroupManager customerGroupManager)
        {
            _log = log;
            _log.Debug("Constructing BlobQueryManager");
            Context = context;
            _customerManager = customerManager;
            _customerGroupManager = customerGroupManager;
            _connectionManager = CommandConnectionManager.Instance;
        }
        public BlobDbContext Context { get; private set; }


        // Dash
        public async Task<DashCurrentConnectionsLargeVm> GetDashCurrentConnectionsLargeVmAsync(Guid searchId, int pageNum = 1, int pageSize = 10)
        {
            var activeDeviceConnections = _connectionManager.GetActiveDeviceIds().ToList();
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
                    Status = ChangeStatusRecordsToStatusInt(GetDeviceRecentStatus(x.Id))
                }),
            }).ConfigureAwait(false);
        }

        public async Task<DashDevicesLargeVm> GetDashDevicesLargeVmAsync(Guid searchId, int pageNum = 1, int pageSize = 10)
        {
            var activeDeviceConnections = _connectionManager.GetActiveDeviceIds().ToList();
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
                var deviceRecent = GetDeviceRecentStatus(x.Id);

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

        // Customer

        public async Task<CustomerDisableVm> GetCustomerDisableVmAsync(Guid customerId)
        {
            return await (from customer in Context.Customers
                          where customer.Id == customerId
                          select new CustomerDisableVm
                          {
                              CustomerId = customer.Id,
                              CustomerName = customer.Name,
                              Enabled = customer.Enabled
                          }).SingleAsync().ConfigureAwait(false);
        }

        public async Task<CustomerEnableVm> GetCustomerEnableVmAsync(Guid customerId)
        {
            return await (from customer in Context.Customers
                          where customer.Id == customerId
                          select new CustomerEnableVm
                          {
                              CustomerId = customer.Id,
                              CustomerName = customer.Name,
                              Enabled = customer.Enabled
                          }).SingleAsync().ConfigureAwait(false);
        }

        public async Task<CustomerSingleVm> GetCustomerSingleVmAsync(Guid customerId)
        {
            return await Context.Customers
                .Include("Devices").Include("Users")
                                .Where(x => x.Id == customerId)
                                .Select(cust => new CustomerSingleVm
                                                {
                                                    CreateDate = cust.CreateDateUtc,
                                                    CustomerId = cust.Id,
                                                    Name = cust.Name,
                                                    DeviceCount = cust.Devices.Count,
                                                }).SingleAsync();
        }

        public async Task<CustomerUpdateVm> GetCustomerUpdateVmAsync(Guid customerId)
        {
            return await (from customer in Context.Customers
                          where customer.Id == customerId
                          select new CustomerUpdateVm
                          {
                              CustomerId = customer.Id,
                              CustomerName = customer.Name
                          }).SingleAsync().ConfigureAwait(false);
        }


        // Device Command
        public IEnumerable<DeviceCommandVm> GetDeviceCommandVmList()
        {
            IList<Type> commandTypes = KnownCommandsMap.GetKnownCommandTypes(null);
            return commandTypes.Select(t => new DeviceCommandVm
                            {
                                CommandType = t.FullName,
                                ShortName = t.Name,
                                CommandParamters = t.GetProperties()//BindingFlags.Public & BindingFlags.Instance)
                                .Select(p => new DeviceCommandParameterPairVm
                                {
                                    Key = p.Name,
                                    Value = ""
                                })
                            });
        }

        public DeviceCommandIssueVm GetDeviceCommandIssueVm(Guid deviceId, string commandType)
        {
            var commandTypes = GetDeviceCommandVmList();
            var command = commandTypes.Single(type => type.CommandType.Equals(commandType));

            DeviceCommandIssueVm result = new DeviceCommandIssueVm
            {
                DeviceId = deviceId,
                CommandType = command.CommandType,
                CommandParameters = command.CommandParamters.ToList(),
                ShortName = command.ShortName
            };
            return result;
        }

        // Device

        public async Task<DeviceDisableVm> GetDeviceDisableVmAsync(Guid deviceId)
        {
            return await (from device in Context.Devices
                          where device.Id == deviceId
                          select new DeviceDisableVm
                          {
                              DeviceId = device.Id,
                              DeviceName = device.DeviceName,
                              Enabled = device.Enabled
                          }).SingleAsync().ConfigureAwait(false);
        }

        public async Task<DeviceEnableVm> GetDeviceEnableVmAsync(Guid deviceId)
        {
            return await (from device in Context.Devices
                          where device.Id == deviceId
                          select new DeviceEnableVm
                          {
                              DeviceId = device.Id,
                              DeviceName = device.DeviceName,
                              Enabled = device.Enabled
                          }).SingleAsync().ConfigureAwait(false);
        }

        public async Task<DevicePageVm> GetDevicePageVmAsync(Guid customerId, int pageNum = 1, int pageSize = 10)
        {
            var activeDeviceConnections = _connectionManager.GetActiveDeviceIds();

            IEnumerable<DeviceCommandVm> availableCommands = GetDeviceCommandVmList();
            var pNum = pageNum < 1 ? 0 : pageNum - 1;

            var count = Context.Devices.Where(x => x.CustomerId.Equals(customerId)).FutureCount();
            var devices = Context.Devices
                .Include("DeviceType")
                .Where(x => x.CustomerId.Equals(customerId))
                .OrderByDescending(x => x.AlertLevel).ThenBy(x => x.DeviceName)
                .Skip(pNum * pageSize).Take(pageSize).Future();

            // define future queries before any of them execute
            var pCount = ((count / pageSize) + (count % pageSize) == 0 ? 0 : 1);
            var items = await Task.FromResult(new DevicePageVm
            {
                TotalCount = count,
                PageCount = pCount,
                PageNum = pNum + 1,
                PageSize = pageSize,
                Items = devices.Select(x => new DeviceListItemVm
                {
                    AvailableCommands = (activeDeviceConnections.Contains(x.Id)) ? availableCommands : new List<DeviceCommandVm>(),
                    DeviceId = x.Id,
                    DeviceName = x.DeviceName,
                    DeviceType = x.DeviceType.Name,
                    Enabled = x.Enabled,
                    LastActivityDate = x.LastActivityDateUtc,
                    //Status = x.AlertLevel
                }),
            }).ConfigureAwait(false);
            foreach (var item in items.Items)
            {
                item.Status = await GetCurrentAlertLevelAsync(item.DeviceId);
            }
            return items;
        }


        public async Task<DeviceSingleVm> GetDeviceSingleVmAsync(Guid deviceId)
        {
            var d = await (from device in Context.Devices.Include("DeviceStatuses").Include("DeviceTypes").Include("DevicePerfDatas")
                           where device.Id == deviceId
                           select new DeviceSingleVm
                           {
                               CreateDate = device.CreateDateUtc,
                               DeviceId = device.Id,
                               DeviceName = device.DeviceName,
                               DeviceType = device.DeviceType.Name,
                               Enabled = device.Enabled,
                               LastActivityDate = device.LastActivityDateUtc,
                               //Status = device.AlertLevel,
                           }).SingleAsync().ConfigureAwait(false);

            d.Status = await GetCurrentAlertLevelAsync(deviceId);
            return d;
        }


        public async Task<DeviceUpdateVm> GetDeviceUpdateVmAsync(Guid deviceId)
        {
            return await (from device in Context.Devices.Include("DeviceTypes")
                          where device.Id == deviceId
                          select new DeviceUpdateVm
                          {
                              AvailableTypes = (from type in Context.DeviceTypes
                                                select new DeviceTypeSingleVm
                                                {
                                                    DeviceTypeId = type.Id,
                                                    Value = type.Name
                                                }),
                              DeviceId = device.Id,
                              DeviceTypeId = device.DeviceTypeId,
                              DeviceName = device.DeviceName
                          }).SingleAsync().ConfigureAwait(false);
        }

        // Performance
        public async Task<PerformanceRecordDeleteVm> GetPerformanceRecordDeleteVmAsync(long recordId)
        {
            return await (from perf in Context.DevicePerfDatas.Include("Devices")
                          where perf.Id == recordId
                          select new PerformanceRecordDeleteVm
                          {
                              DeviceName = perf.Device.DeviceName,
                              MonitorName = perf.MonitorName,
                              RecordId = perf.Id,
                              TimeGenerated = perf.TimeGeneratedUtc
                          }).SingleAsync().ConfigureAwait(false);
        }

        public async Task<PerformanceRecordPageVm> GetPerformanceRecordPageVmAsync(Guid deviceId, int pageNum = 1, int pageSize = 10)
        {
            var pNum = pageNum < 1 ? 0 : pageNum - 1;

            var count = Context.DevicePerfDatas.Where(x => x.DeviceId.Equals(deviceId)).FutureCount();
            var devices = Context.DevicePerfDatas
                .Where(x => x.DeviceId.Equals(deviceId))
                .OrderByDescending(x => x.TimeGeneratedUtc)
                .Skip(pNum * pageSize).Take(pageSize).Future();

            // define future queries before any of them execute
            var pCount = ((count / pageSize) + (count % pageSize) == 0 ? 0 : 1);
            return await Task.FromResult(new PerformanceRecordPageVm
            {
                TotalCount = count,
                PageCount = pCount,
                PageNum = pNum + 1,
                PageSize = pageSize,
                Items = devices.Select(x => new PerformanceRecordListItemVm
                {
                    Critical = x.Critical.Value.ToString(),
                    Label = x.Label,
                    Max = x.Max.Value.ToString(),
                    Min = x.Min.Value.ToString(),
                    MonitorDescription = x.MonitorDescription,
                    MonitorName = x.MonitorName,
                    RecordId = x.Id,
                    TimeGenerated = x.TimeGeneratedUtc,
                    Unit = x.UnitOfMeasure,
                    Value = x.Value.ToString(),
                    Warning = x.Warning.Value.ToString()
                }),
            }).ConfigureAwait(false);
        }

        public async Task<PerformanceRecordPageVm> GetPerformanceRecordPageVmForStatusAsync(long recordId, int pageNum = 1, int pageSize = 10)
        {
            var pNum = pageNum < 1 ? 0 : pageNum - 1;

            var count = Context.DevicePerfDatas.Where(x => x.StatusId == recordId).FutureCount();
            var data = Context.DevicePerfDatas
                .Where(x => x.StatusId == recordId)
                .OrderByDescending(x => x.TimeGeneratedUtc)
                .Skip(pNum * pageSize).Take(pageSize).Future();

            // define future queries before any of them execute
            var pCount = ((count / pageSize) + (count % pageSize) == 0 ? 0 : 1);
            return await Task.FromResult(new PerformanceRecordPageVm
            {
                TotalCount = count,
                PageCount = pCount,
                PageNum = pNum + 1,
                PageSize = pageSize,
                Items = data.Select(x => new PerformanceRecordListItemVm
                {
                    Critical = x.Critical.Value.ToString(),
                    Label = x.Label,
                    Max = x.Max.Value.ToString(),
                    Min = x.Min.Value.ToString(),
                    MonitorDescription = x.MonitorDescription,
                    MonitorName = x.MonitorName,
                    RecordId = x.Id,
                    TimeGenerated = x.TimeGeneratedUtc,
                    Unit = x.UnitOfMeasure,
                    Value = x.Value.ToString(),
                    Warning = x.Warning.Value.ToString()
                }),
            }).ConfigureAwait(false);
        }

        public async Task<PerformanceRecordSingleVm> GetPerformanceRecordSingleVmAsync(long recordId)
        {
            return await (from perf in Context.DevicePerfDatas
                          where perf.Id == recordId
                          select new PerformanceRecordSingleVm
                          {
                              Critical = perf.Max.ToString(),
                              Label = perf.Label,
                              Max = perf.Max.ToString(),
                              Min = perf.Min.ToString(),
                              MonitorDescription = perf.MonitorDescription,
                              MonitorName = perf.MonitorName,
                              RecordId = perf.Id,
                              TimeGenerated = perf.TimeGeneratedUtc,
                              Unit = perf.UnitOfMeasure,
                              Value = perf.Value.ToString(),
                              Warning = perf.Warning.ToString()
                          }).SingleAsync().ConfigureAwait(false);
        }

        // Status
        public async Task<StatusRecordDeleteVm> GetStatusRecordDeleteVmAsync(long recordId)
        {
            return await (from status in Context.DeviceStatuses.Include("Devices")
                          where status.Id == recordId
                          select new StatusRecordDeleteVm
                          {
                              DeviceName = status.Device.DeviceName,
                              MonitorName = status.MonitorName,
                              RecordId = status.Id,
                              TimeGenerated = status.TimeGeneratedUtc

                          }).SingleAsync().ConfigureAwait(false);
        }

        public async Task<StatusRecordPageVm> GetStatusRecordPageVmAsync(Guid deviceId, int pageNum = 1, int pageSize = 10)
        {
            var pNum = pageNum < 1 ? 0 : pageNum - 1;

            var count = Context.DeviceStatuses.Where(x => x.DeviceId.Equals(deviceId)).FutureCount();
            var devices = Context.DeviceStatuses
                .Where(x => x.DeviceId.Equals(deviceId))
                .OrderByDescending(x => x.TimeGeneratedUtc)
                .Skip(pNum * pageSize).Take(pageSize).Future();

            // define future queries before any of them execute
            var pCount = ((count / pageSize) + (count % pageSize) == 0 ? 0 : 1);
            return await Task.FromResult(new StatusRecordPageVm
            {
                TotalCount = count,
                PageCount = pCount,
                PageNum = pNum + 1,
                PageSize = pageSize,
                Items = devices.Select(x => new StatusRecordListItemVm
                {
                    MonitorDescription = x.MonitorDescription,
                    MonitorName = x.MonitorName,
                    RecordId = x.Id,
                    Status = x.AlertLevel,
                    TimeGenerated = x.TimeGeneratedUtc
                }),
            }).ConfigureAwait(false);
        }

        public async Task<StatusRecordSingleVm> GetStatusRecordSingleVmAsync(long recordId)
        {
            return await (from status in Context.DeviceStatuses
                          where status.Id == recordId
                          select new StatusRecordSingleVm
                          {
                              MonitorDescription = status.MonitorDescription,
                              MonitorName = status.MonitorName,
                              RecordId = status.Id,
                              Status = status.AlertLevel,
                              TimeGenerated = status.TimeGeneratedUtc

                          }).SingleAsync().ConfigureAwait(false);
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

        public async Task<CustomerGroupCreateVm> GetCustomerGroupCreateVmAsync(Guid customerId)
        {
            return await _customerGroupManager.GetCustomerGroupCreateVmAsync(customerId);
        }

        public async Task<CustomerGroupDeleteVm> GetCustomerGroupDeleteVmAsync(Guid groupId)
        {
            return await _customerGroupManager.GetCustomerGroupDeleteVmAsync(groupId); 
        }

        public async Task<CustomerGroupSingleVm> GetCustomerGroupSingleVmAsync(Guid groupId)
        {
            return await _customerGroupManager.GetCustomerGroupSingleVmAsync(groupId);
        }

        public async Task<CustomerGroupUpdateVm> GetCustomerGroupUpdateVmAsync(Guid groupId)
        {
            return await _customerGroupManager.GetCustomerGroupUpdateVmAsync(groupId);
        }

        public async Task<IEnumerable<CustomerGroupRoleListItem>> GetCustomerRolesAsync(Guid customerId)
        {
            return await _customerGroupManager.GetCustomerRolesAsync(customerId);
        }


        public async Task<CustomerGroupPageVm> GetCustomerGroupPageVmAsync(Guid customerId, int pageNum = 1, int pageSize = 10)
        {
            return await _customerGroupManager.GetCustomerGroupPageVmAsync(customerId, pageNum, pageSize);
        }

        public async Task<IEnumerable<CustomerGroupRoleListItem>> GetCustomerGroupRolesAsync(Guid groupId)
        {
            return await _customerGroupManager.GetCustomerGroupRolesAsync(groupId);
        }

        public async Task<IEnumerable<CustomerGroupUserListItem>> GetCustomerGroupUsersAsync(Guid groupId)
        {
            return await _customerGroupManager.GetCustomerGroupUsersAsync(groupId);
        }



        private async Task<IEnumerable<StatusRecord>> GetDeviceRecentStatusAsync(Guid deviceId)
        {
            return await (from s1 in Context.DeviceStatuses
                          join s2 in
                              (
                                  from s in Context.DeviceStatuses
                                  where s.DeviceId == deviceId
                                  group s by s.MonitorId
                                      into r
                                      select new { MonitorId = r.Key, TimeGeneratedUtc = r.Max(x => x.TimeGeneratedUtc) }
                                  )
                              on new { s1.MonitorId, s1.TimeGeneratedUtc } equals new { s2.MonitorId, s2.TimeGeneratedUtc }
                          select s1).ToListAsync();
        }
        private IEnumerable<StatusRecord> GetDeviceRecentStatus(Guid deviceId)
        {
            using (BlobDbContext context = new BlobDbContext())
            {
                return (from s1 in context.DeviceStatuses
                        join s2 in
                            (
                                from s in context.DeviceStatuses
                                where s.DeviceId == deviceId
                                group s by s.MonitorId
                                    into r
                                    select new { MonitorId = r.Key, TimeGeneratedUtc = r.Max(x => x.TimeGeneratedUtc) }
                            )
                            on new { s1.MonitorId, s1.TimeGeneratedUtc } equals new { s2.MonitorId, s2.TimeGeneratedUtc }
                        select s1).ToList();
            }
        }

        private async Task<int> GetCurrentAlertLevelAsync(Guid deviceId)
        {
            IEnumerable<StatusRecord> records = await GetDeviceRecentStatusAsync(deviceId);
            return ChangeStatusRecordsToStatusInt(records);
        }

        private int GetCurrentAlertLevel(Guid deviceId)
        {
            IEnumerable<StatusRecord> records = GetDeviceRecentStatus(deviceId);
            return ChangeStatusRecordsToStatusInt(records);
        }

        private string ChangeStatusRecordsToReasonString(IEnumerable<StatusRecord> records)
        {
            return records.Where(s => s.AlertLevel != 0)
                          .Aggregate<StatusRecord, string>(null, (accum, r) => accum + (accum == null ? accum : ", ") + r.CurrentValue);
        }

        private int ChangeStatusRecordsToStatusInt(IEnumerable<StatusRecord> records)
        {
            return records.Select(statusRecord => statusRecord.AlertLevel).Concat(new[] { 0 }).Max();
        }
    }
}