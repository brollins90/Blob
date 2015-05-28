using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Blob.Contracts.Models.ViewModels;
using Blob.Contracts.ServiceContracts;
using Blob.Core;
using Blob.Core.Identity.Store;
using Blob.Core.Models;
using Blob.Security.Identity;
using EntityFramework.Extensions;
using log4net;

namespace Blob.Managers.Blob
{
    // http://www.agile-code.com/blog/entity-framework-code-first-filtering-and-sorting-with-paging-1/


    public class BlobQueryManager : IBlobQueryManager
    {
        private readonly ILog _log;
        private BlobCustomerManager _customerManager;
        private BlobCustomerGroupManager _customerGroupManager;

        public BlobQueryManager(BlobDbContext context, ILog log, BlobCustomerManager customerManager, BlobCustomerGroupManager customerGroupManager)
        {
            _log = log;
            _log.Debug("Constructing BlobQueryManager");
            Context = context;
            _customerManager = customerManager;
            _customerGroupManager = customerGroupManager;
        }
        public BlobDbContext Context { get; private set; }


        // Dash


        public async Task<DashDevicesLargeVm> GetDashDevicesLargeVmAsync(Guid customerId, int pageNum = 1, int pageSize = 10)
        {
            IEnumerable<DeviceCommandVm> availableCommands = GetDeviceCommandVmList();
            var pNum = pageNum < 1 ? 0 : pageNum - 1;

            //var cust = Context.Customers.Where(x => x.Id.Equals(customerId));
            var count = Context.Devices.Where(x => x.CustomerId.Equals(customerId)).FutureCount();
            var devices = Context.Devices
                .Where(x => x.CustomerId.Equals(customerId))
                .OrderByDescending(x => x.AlertLevel).ThenBy(x => x.DeviceName)
                .Skip(pNum * pageSize).Take(pageSize).Future();

            // define future queries before any of them execute
            var pCount = ((count / pageSize) + (count % pageSize) == 0 ? 0 : 1);
            return await Task.FromResult(new DashDevicesLargeVm
            {
                TotalCount = count,
                PageCount = pCount,
                PageNum = pNum + 1,
                PageSize = pageSize,
                Items = devices.Select(x => new DashDevicesLargeListItemVm
                                           {
                                               AvailableCommands = availableCommands,
                                               DeviceId = x.Id,
                                               DeviceName = x.DeviceName,
                                               Reason = string.Empty,
                                               Recomendations = (x.AlertLevel == 0)
                                               ? new string[] { "Everything is Ok" }
                                               : new string[] { string.Empty },
                                               Status = x.AlertLevel
                                           }),
            }).ConfigureAwait(false);
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
                                                    //UserCount = cust.CustomerUsers.Count
                                                    //Users = cust.CustomerUsers.Select(u => new CustomerUserListItemVm{
                                                    //    Email = u.
                                                    //})
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
            return await Task.FromResult(new DevicePageVm
            {
                TotalCount = count,
                PageCount = pCount,
                PageNum = pNum + 1,
                PageSize = pageSize,
                Items = devices.Select(x => new DeviceListItemVm
                {
                    AvailableCommands = availableCommands,
                    DeviceId = x.Id,
                    DeviceName = x.DeviceName,
                    DeviceType = x.DeviceType.Name,
                    Enabled = x.Enabled,
                    LastActivityDate = x.LastActivityDateUtc,
                    Status = x.AlertLevel
                }),
            }).ConfigureAwait(false);
        }

        public async Task<DeviceSingleVm> GetDeviceSingleVmAsync(Guid deviceId)
        {
            return await (from device in Context.Devices.Include("DeviceStatuses").Include("DeviceTypes").Include("DevicePerfDatas")
                          where device.Id == deviceId
                          select new DeviceSingleVm
                          {
                              CreateDate = device.CreateDateUtc,
                              DeviceId = device.Id,
                              DeviceName = device.DeviceName,
                              DeviceType = device.DeviceType.Name,
                              Enabled = device.Enabled,
                              LastActivityDate = device.LastActivityDateUtc,
                              //PerformanceRecords = (from perf in device.StatusPerfs
                              //                      select new PerformanceRecordListItemVm
                              //                      {
                              //                          Critical = perf.Max.ToString(),
                              //                          Label = perf.Label,
                              //                          Max = perf.Max.ToString(),
                              //                          Min = perf.Min.ToString(),
                              //                          MonitorDescription = perf.MonitorDescription,
                              //                          MonitorName = perf.MonitorName,
                              //                          RecordId = perf.Id,
                              //                          TimeGenerated = perf.TimeGenerated,
                              //                          Unit = perf.UnitOfMeasure,
                              //                          Value = perf.Value.ToString(),
                              //                          Warning = perf.Warning.ToString(),
                              //                      }),
                              Status = device.AlertLevel,
                              //StatusRecords = (from status in device.Statuses
                              //                 select new StatusRecordListItemVm
                              //                 {
                              //                     MonitorDescription = status.MonitorDescription,
                              //                     MonitorName = status.MonitorName,
                              //                     RecordId = status.Id,
                              //                     Status = status.AlertLevel,
                              //                     TimeGenerated = status.TimeGenerated
                              //                 }),
                          }).SingleAsync().ConfigureAwait(false);
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
            return await (from user in Context.Users.Include("Customers")
                          where user.Id == userId
                          select new UserSingleVm
                          {
                              CreateDate = user.CreateDateUtc,
                              CustomerName = user.Customer.Name,
                              Email = user.Email,
                              EmailConfirmed = user.EmailConfirmed,
                              //Enabled = user.Enabled,
                              HasPassword = (user.PasswordHash != null),
                              HasSecurityStamp = (user.SecurityStamp != null),
                              LastActivityDate = user.LastActivityDate,
                              UserId = user.Id,
                              UserName = user.UserName,
                          }).SingleAsync().ConfigureAwait(false);
        }

        public IEnumerable<NotificationScheduleListItemVm> GetAllNotificationSchedules()
        {
            return new List<NotificationScheduleListItemVm> { new NotificationScheduleListItemVm { ScheduleId = Guid.Parse("76AA040A-253C-4AD3-838F-ADE186F40F47"), Name = "FirstSchedule" } };
        }

        public async Task<UserUpdateVm> GetUserUpdateVmAsync(Guid userId)
        {
            var availableSchedules = GetAllNotificationSchedules();
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
            var roles = await GetCustomerRolesAsync(customerId);
            return new CustomerGroupCreateVm
            {
                CustomerId = customerId,
                GroupId = Guid.NewGuid(),
                AvailableRoles = roles
            };
        }

        public async Task<CustomerGroupDeleteVm> GetCustomerGroupDeleteVmAsync(Guid groupId)
        {
            CustomerGroup group = await _customerGroupManager.FindGroupByIdAsync(groupId);
            return new CustomerGroupDeleteVm
            {
                GroupId = group.Id,
                Name = group.Name
            };
        }

        public async Task<CustomerGroupSingleVm> GetCustomerGroupSingleVmAsync(Guid groupId)
        {
            CustomerGroup group = await _customerGroupManager.FindGroupByIdAsync(groupId);
            var roles = await GetCustomerGroupRolesAsync(groupId);
            var users = await GetCustomerGroupUsersAsync(groupId);
            return new CustomerGroupSingleVm
            {
                CustomerId = group.CustomerId,
                Description = group.Description,
                GroupId = group.Id,
                Name = group.Name,
                RoleCount = roles.Count(),
                Roles = roles,
                UserCount = users.Count(),
                Users = users
            };
        }

        public async Task<CustomerGroupUpdateVm> GetCustomerGroupUpdateVmAsync(Guid groupId)
        {
            CustomerGroup group = await _customerGroupManager.FindGroupByIdAsync(groupId);
            return new CustomerGroupUpdateVm
                   {
                       CustomerId = group.CustomerId,
                       Description = group.Description,
                       GroupId = group.Id,
                       Name = group.Name
                   };
        }

        public async Task<IEnumerable<CustomerGroupRoleListItem>> GetCustomerRolesAsync(Guid customerId)
        {
            return await Context.Roles.Select(x => new CustomerGroupRoleListItem { Name = x.Name, RoleId = x.Id }).ToListAsync();
        }


        public async Task<CustomerGroupPageVm> GetCustomerGroupPageVmAsync(Guid customerId, int pageNum = 1, int pageSize = 10)
        {
            var pNum = pageNum < 1 ? 0 : pageNum - 1;

            var count = Context.Set<CustomerGroup>().Where(x => x.CustomerId.Equals(customerId)).FutureCount();
            var groups = Context.Set<CustomerGroup>()
                .Where(x => x.CustomerId.Equals(customerId))
                .OrderByDescending(x => x.Name)
                .Skip(pNum * pageSize).Take(pageSize).Future();

            // define future queries before any of them execute
            var pCount = ((count / pageSize) + (count % pageSize) == 0 ? 0 : 1);
            return await Task.FromResult(new CustomerGroupPageVm
            {
                TotalCount = count,
                PageCount = pCount,
                PageNum = pNum + 1,
                PageSize = pageSize,
                Items = groups.Select(x => new CustomerGroupListItemVm
                {
                    GroupId = x.Id,
                    Name = x.Name
                }),
            }).ConfigureAwait(false);
        }

        public async Task<IEnumerable<CustomerGroupRoleListItem>> GetCustomerGroupRolesAsync(Guid groupId)
        {
            return (await _customerGroupManager.GetGroupRolesAsync(groupId)).Select(x => new CustomerGroupRoleListItem { Name = x.Name, RoleId = x.Id }).ToList();
        }

        public async Task<IEnumerable<CustomerGroupUserListItem>> GetCustomerGroupUsersAsync(Guid groupId)
        {
            return (await _customerGroupManager.GetGroupUsersAsync(groupId)).Select(x => new CustomerGroupUserListItem { UserName = x.UserName, UserId = x.Id }).ToList();
        }
    }
}
