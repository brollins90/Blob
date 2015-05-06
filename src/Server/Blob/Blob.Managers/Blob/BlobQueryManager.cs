using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Blob.Contracts.Blob;
using Blob.Contracts.Command;
using Blob.Contracts.Dto.ViewModels;
using Blob.Data;
using log4net;

namespace Blob.Managers.Blob
{
    public class BlobQueryManager : IBlobQueryManager
    {
        private readonly ILog _log;

        public BlobQueryManager(BlobDbContext context, ILog log)
        {
            _log = log;
            _log.Debug("Constructing BlobQueryManager");
            Context = context;
        }
        public BlobDbContext Context { get; private set; }


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
            return await (from cust in Context.Customers.Include("Devices").Include("DeviceTypes").Include("Users")
                          where cust.Id == customerId
                          select new CustomerSingleVm
                          {
                              CreateDate = cust.CreateDate,
                              CustomerId = cust.Id,
                              Name = cust.Name,
                              Devices = (from d in cust.Devices
                                         select new DeviceListItemVm
                                         {
                                             DeviceName = d.DeviceName,
                                             DeviceType = d.DeviceType.Value,
                                             DeviceId = d.Id,
                                             Enabled = d.Enabled,
                                             LastActivityDate = d.LastActivityDate,
                                             Status = d.AlertLevel
                                         }),
                              Users = (from u in cust.Users
                                       select new UserListItemVm
                                       {
                                           UserId = u.Id,
                                           UserName = u.UserName
                                       })
                          }).SingleAsync().ConfigureAwait(false);
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
        public DeviceCommandIssueVm GetDeviceCommandIssueVm(Guid deviceId)
        {
            IEnumerable<Type> commandTypes = KnownCommandsMap.GetKnownCommandTypes(null);

            DeviceCommandIssueVm result = new DeviceCommandIssueVm
            {
                DeviceId = deviceId,
                SelectedCommand = null,
                AvailableCommands = commandTypes.Select(
                t => new DeviceCommandVm
                     {
                         CommandType = t.FullName,
                         CommandParamters =
                            t.GetProperties(BindingFlags.Public & BindingFlags.Instance)
                            .Select(p => new { p.Name, string.Empty }).ToDictionary(p => p.Name, p => p.Empty)
                     })
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

        public async Task<DeviceSingleVm> GetDeviceSingleVmAsync(Guid deviceId)
        {
            return await (from device in Context.Devices.Include("DeviceStatuses").Include("DeviceTypes").Include("DevicePerfDatas")
                          where device.Id == deviceId
                          select new DeviceSingleVm
                          {
                              CreateDate = device.CreateDate,
                              DeviceId = device.Id,
                              DeviceName = device.DeviceName,
                              DeviceType = device.DeviceType.Value,
                              Enabled = device.Enabled,
                              LastActivityDate = device.LastActivityDate,
                              PerformanceRecords = (from perf in device.StatusPerfs
                                                    select new PerformanceRecordListItemVm
                                                    {
                                                        Critical = perf.Max.ToString(),
                                                        Label = perf.Label,
                                                        Max = perf.Max.ToString(),
                                                        Min = perf.Min.ToString(),
                                                        MonitorDescription = perf.MonitorDescription,
                                                        MonitorName = perf.MonitorName,
                                                        RecordId = perf.Id,
                                                        TimeGenerated = perf.TimeGenerated,
                                                        Unit = perf.UnitOfMeasure,
                                                        Value = perf.Value.ToString(),
                                                        Warning = perf.Warning.ToString(),
                                                    }),
                              Status = device.AlertLevel,
                              StatusRecords = (from status in device.Statuses
                                               select new StatusRecordListItemVm
                                               {
                                                   MonitorDescription = status.MonitorDescription,
                                                   MonitorName = status.MonitorName,
                                                   RecordId = status.Id,
                                                   Status = status.AlertLevel,
                                                   TimeGenerated = status.TimeGenerated
                                               }),
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
                                                    Value = type.Value
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
                              TimeGenerated = perf.TimeGenerated
                          }).SingleAsync().ConfigureAwait(false);
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
                              TimeGenerated = perf.TimeGenerated,
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
                              TimeGenerated = status.TimeGenerated

                          }).SingleAsync().ConfigureAwait(false);
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
                              TimeGenerated = status.TimeGenerated

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

        public async Task<UserSingleVm> GetUserSingleVmAsync(Guid userId)
        {
            return await (from user in Context.Users.Include("Customers")
                          where user.Id == userId
                          select new UserSingleVm
                          {
                              CreateDate = user.CreateDate,
                              CustomerName = user.Customer.Name,
                              Email = user.Email,
                              EmailConfirmed = user.EmailConfirmed,
                              Enabled = user.Enabled,
                              HasPassword = (user.PasswordHash != null),
                              HasSecurityStamp = (user.SecurityStamp != null),
                              LastActivityDate = user.LastActivityDate,
                              UserId = user.Id,
                              UserName = user.UserName,
                          }).SingleAsync().ConfigureAwait(false);
        }

        public async Task<UserUpdateVm> GetUserUpdateVmAsync(Guid userId)
        {
            return await (from user in Context.Users
                          where user.Id == userId
                          select new UserUpdateVm
                          {
                              UserId = user.Id,
                              Email = user.Email,
                              UserName = user.UserName
                          }).SingleAsync().ConfigureAwait(false);
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
    }
}
